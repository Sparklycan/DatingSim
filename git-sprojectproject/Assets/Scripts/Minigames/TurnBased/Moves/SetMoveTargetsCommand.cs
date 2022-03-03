using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flex", "Set move targets", helpText: "Set the targets of a move.")]
public class SetMoveTargetsCommand : Command
{

    [SerializeField]
    private MoveData move;

    [SerializeField]
    private CollectionData targets;

    public override void OnEnter()
    {
        List<CharacterClass> targetList = new List<CharacterClass>();
        for (int i = 0; i < targets.Value.Count; i++)
            targetList.Add(targets.Value[i] as CharacterClass);

        move.Value.targets = targetList.ToArray();

        Continue();
    }

}
