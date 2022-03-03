using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flex", "Next turn", helpText: "Reduce the turn count for a move collection.")]
public class NextTurnCommand : Command
{

    [SerializeField]
    private CollectionData moves;

    public override void OnEnter()
    {
        MoveCollection moveCollection = moves.Value as MoveCollection;
        foreach (Move move in moveCollection)
            move.waitTurns--;

        Continue();
    }

}
