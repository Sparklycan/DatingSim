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
    private float attackBuff = 1.0f;
    [SerializeField]
    private float defenseBuff = 1.0f;

    public string Name => name;
    public string ClassName => className;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public IEnumerable<Ability> Abilities => abilities;
    public IEnumerable<Ability> DisabledAbilities => disabledAbilities;

    public ClassType BaseClass => baseClass;
    public Allegience Allegience => allegience;
    public Animator Animator => animator;
    public float AttackBuff => attackBuff;
    public float DefenseBuff => defenseBuff;

    // Used by TurnManager to keep track of all the characters
    static public event Action<CharacterClass> onCharacterEnable;
    static public event Action<CharacterClass> onCharacterDisable;

    private HashSet<Ability> disabledAbilities = new HashSet<Ability>();

    public void Awake()
    {
        className = BaseClass.ClassName;
        maxHealth = BaseClass.MaxHealth;
        abilities = baseClass.Abilities.ToList();

        if (startAtMaxHealth)
            currentHealth = maxHealth;
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
        if (enabled)
            disabledAbilities.Remove(ability);
        else
            disabledAbilities.Add(ability);
    }

    public bool CanSelectAbility()
    {
        return abilitySelector.CanSelect(Abilities, this);
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

    public void DoDamage(int damage, string hurtAnimation, string deathAnimation)
    {
        currentHealth -= (int)((float)damage * defenseBuff);

        if (currentHealth > 0)
            Animator.Play(hurtAnimation);
        else
            Animator.Play(deathAnimation);
    }

    public void AddBuff(float attack, float defense)
    {
        attackBuff *= attack;
        defenseBuff *= defense;
    }

}
