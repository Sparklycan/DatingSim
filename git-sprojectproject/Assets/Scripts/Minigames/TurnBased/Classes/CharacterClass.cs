using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CharacterClass : MonoBehaviour, ClassBase
{

    private string className;
    private int maxHealth;

    [SerializeField]
    private new string name;
    [SerializeField]
    private bool startAtMaxHealth = true;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private ClassType baseClass;
    [SerializeField]
    private Allegience allegience;
    [SerializeField]
    [Tooltip("The component that will be used to select this characters moves.")]
    private MoveSelector abilitySelector;
    [SerializeField]
    private List<Ability> abilities;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Vector2 targetOffset = Vector2.zero;
    public Vector2 TargetOffset => targetOffset * transform.localScale;

    [SerializeField]
    private float attackBuff = 1.0f;
    [SerializeField]
    private float defenseBuff = 1.0f;
    [SerializeField]
    private float defenseLimit = 0.5f;

    public string Name => name;
    public string ClassName => className;

    public int CurrentHealth
    {
        get => currentHealth;
        private set
        {
            int previousHealth = currentHealth;
            currentHealth = value;
            
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;
            else if (currentHealth < 0)
                currentHealth = 0;

            onHealthChange?.Invoke(currentHealth - previousHealth);
        }
    }
    public int MaxHealth => maxHealth;

    public IEnumerable<Ability> Abilities => abilities;
    public IEnumerable<Ability> DisabledAbilities => disabledAbilities;

    public ClassType BaseClass => baseClass;
    public Allegience Allegience => allegience;
    public Animator Animator => animator;
    public float AttackBuff => attackBuff;
    public float DefenseBuff => Mathf.Max(defenseBuff, defenseLimit);

    // Used by TurnManager to keep track of all the characters
    static public event Action<CharacterClass> onCharacterEnable;
    static public event Action<CharacterClass> onCharacterDisable;

    public event Action<CharacterClass, int> onTakeDamage;
    public event Action<Move> onMakeMove;
    public event Action<int> onHealthChange;

    private HashSet<Ability> disabledAbilities = new HashSet<Ability>();
    private HashSet<Tuple<Ability, CharacterClass>> disabledTargets = new HashSet<Tuple<Ability, CharacterClass>>();

    public void Awake()
    {
        className = BaseClass.ClassName;
        maxHealth = BaseClass.MaxHealth;
        abilities = baseClass.Abilities.ToList();

        if (startAtMaxHealth)
            CurrentHealth = maxHealth;
        else
            onHealthChange?.Invoke(0);
    }

    private void OnEnable()
    {
        onCharacterEnable?.Invoke(this);
    }

    private void OnDisable()
    {
        onCharacterDisable?.Invoke(this);
    }

    public void EnableAbility(Ability ability, bool enabled)
    {
        if (ability == null)
        {
            if (enabled)
                disabledAbilities.Clear();
            else
                foreach (Ability a in Abilities)
                    disabledAbilities.Add(a);
        }
        else
        {
            if (enabled)
                disabledAbilities.Remove(ability);
            else
                disabledAbilities.Add(ability);
        }
    }

    public void EnableTarget(Ability ability, CharacterClass target, bool enabled)
    {
        var t = Tuple.Create(ability, target);
        if (enabled)
            disabledTargets.Remove(t);
        else
            disabledTargets.Add(t);
    }

    public void EnableAllAbilities()
    {
        disabledAbilities.Clear();
    }

    public void EnableAllTargets()
    {
        disabledTargets.Clear();
    }

    public bool CanSelectAbility()
    {
        return GetComponent<Stunned>() == null &&  abilitySelector.CanSelect(Abilities, this);
    }

    public IEnumerator<Move> SelectAbility()
    {
        abilitySelector.OnBeginSelect(this);

        IEnumerator<Move> ability = abilitySelector.Select();
        while (ability.MoveNext())
            yield return null;

        abilitySelector.OnEndSelect();

        yield return ability.Current;
    }

    public void DoDamage(CharacterClass attacker, int damage, string hurtAnimation, string deathAnimation)
    {
        if (attacker != null)
        {
            int totalDamage = (int)((float)damage * attacker.AttackBuff / DefenseBuff);
            onTakeDamage?.Invoke(attacker, totalDamage);
            CurrentHealth -= totalDamage;
        }

        if (CurrentHealth > 0)
        {
            if (!string.IsNullOrWhiteSpace(hurtAnimation))
                Animator.Play(hurtAnimation);
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(deathAnimation))
                Animator.Play(deathAnimation);
        }
    }

    public void Heal(int ammount)
    {
        CurrentHealth += ammount;
    }

    public void OnMakeMove(Move move)
    {
        onMakeMove?.Invoke(move);
    }

    public bool CanAttack(Ability ability, CharacterClass target)
    {
        return !disabledTargets.Contains(Tuple.Create(ability, target));
    }

    public void AddBuff(float attack, float defense)
    {
        attackBuff *= attack;
        defenseBuff *= defense;
    }

}
