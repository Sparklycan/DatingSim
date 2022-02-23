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
    [Tooltip("The flowchart variable that should contain the attacker")]
    protected string attackerKey = "Attacker";
    [SerializeField]
    [Tooltip("The flowchart variable that should contain the collection of moves")]
    protected string movesKey = "Moves";
    [SerializeField]
    [Tooltip("The flowchart variable that should contain wether the attack sould allow more moves to be added")]
    protected string allowMoreMovesKey = "AllowMoreMoves";
    [SerializeField]
    protected Fungus.CharacterClassCollection targetCollection;
    [SerializeField]
    protected Fungus.MoveCollection moveCollection;

    public bool IsAttacking { get; private set; }
    public bool AllowMoreMoves { get; private set; }
    public Fungus.MoveCollection Moves => moveCollection;

    [SerializeField]
    protected UnityEngine.Events.UnityEvent onAttack;

    public event Action onAttackFinished;

    public Attack()
    {
        IsAttacking = false;
    }

    public void Initialize(CharacterClass attacker, CharacterClass[] targets, Fungus.MoveCollection moves, bool allowMoreMoves)
    {
        this.attacker = attacker;
        this.targets = targets;
        moveCollection = moves;
        AllowMoreMoves = allowMoreMoves;
    }

    public void InitializeFlowchart(Fungus.Flowchart flowchart)
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

    public void StartAttack()
    {
        IsAttacking = true;
        if (attacker != null)
            onAttack?.Invoke();
        else
            FinishAttack();
    }

    public void FinishAttack()
    {
        IsAttacking = false;
        onAttackFinished?.Invoke();
        Destroy(gameObject);
    }

}
