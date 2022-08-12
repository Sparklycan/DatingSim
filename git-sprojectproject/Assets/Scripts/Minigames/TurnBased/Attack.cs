using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Attack : MonoBehaviour
{

    [SerializeField]
    protected CharacterClass attacker;
    [SerializeField]
    protected CharacterClass[] targets;

    [SerializeField]
    protected string attackerKey = "Attacker";
    [SerializeField]
    protected string movesKey = "Moves";
    [SerializeField]
    protected string allowMoreMovesKey = "AllowMoreMoves";
    [SerializeField]
    protected Fungus.CharacterClassCollection targetCollection;
    [SerializeField]
    protected Fungus.MoveCollection moveCollection;

    [SerializeField]
    protected Damages damages = null;

    public CharacterClass Attacker => attacker;
    public CharacterClass[] Targets => targets;

    public bool IsAttacking { get; private set; }
    public bool AllowMoreMoves { get; private set; }
    public Fungus.MoveCollection Moves => moveCollection;

    public UnityEngine.Events.UnityEvent onAttack = new UnityEngine.Events.UnityEvent();

    public event Action onAttackFinished;

    public Attack()
    {
        IsAttacking = false;
    }

    public virtual void Initialize(CharacterClass attacker, CharacterClass[] targets, Fungus.MoveCollection moves, bool allowMoreMoves)
    {
        this.attacker = attacker;
        this.targets = targets;
        moveCollection = moves;
        AllowMoreMoves = allowMoreMoves;
    }

    public virtual void InitializeFlowchart(Fungus.Flowchart flowchart)
    {
        var attackerVar = flowchart.GetVariable<Fungus.CharacterClassVariable>(attackerKey);
        if (attackerVar != null)    attackerVar.Value = attacker;

        var movesVar = flowchart.GetVariable<Fungus.CollectionVariable>(movesKey);
        if (movesVar != null) movesVar.Value = Moves;

        var allowMoreMovesVar = flowchart.GetVariable<Fungus.BooleanVariable>(allowMoreMovesKey);
        if (allowMoreMovesVar != null) allowMoreMovesVar.Value = AllowMoreMoves;

        targetCollection.Clear();
        foreach (CharacterClass target in targets)
            targetCollection.Add(target);
    }

    public void RemoveCharacterMoves(CharacterClass character)
    {
        for(int i = Moves.Count-1; i >= 0; i--)
        {
            Move move = Moves.Get(i) as Move;
            if (move.character != character)
                continue;

            Moves.RemoveAt(i);
        }
    }

    public void StartAttack(bool destroyWhenFinished = true)
    {
        IsAttacking = true;

        if (destroyWhenFinished)
            onAttackFinished += () => Destroy(gameObject);

        onAttack?.Invoke();
    }

    public void FinishAttack()
    {
        IsAttacking = false;
        onAttackFinished?.Invoke();
    }

}
