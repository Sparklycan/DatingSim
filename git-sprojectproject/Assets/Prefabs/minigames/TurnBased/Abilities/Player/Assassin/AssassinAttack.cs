using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAttack : Attack
{

    [SerializeField]
    private Assassin assassin;

    [SerializeField]
    private Fungus.MoveCollection pastMoves;

    [SerializeField]
    private string pastMovesKey = "PastMoves";

    public override void Initialize(CharacterClass attacker, CharacterClass[] targets, MoveCollection moves, bool allowMoreMoves)
    {
        base.Initialize(attacker, targets, moves, allowMoreMoves);

        assassin = attacker.GetComponent<Assassin>();
        pastMoves = assassin == null ? null : assassin.PastMoves;
    }

    public override void InitializeFlowchart(Flowchart flowchart)
    {
        base.InitializeFlowchart(flowchart);

        var movesVar = flowchart.GetVariable<Fungus.CollectionVariable>(pastMovesKey);
        if (movesVar != null) movesVar.Value = pastMoves;
    }

}
