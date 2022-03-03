using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flex", "Enable move", helpText: "Enable/disale a characters ability.")]
public class EnableMoveCommand : Command
{

    [SerializeField]
    private CharacterClassData character;

    [SerializeField]
    private Ability ability;

    [SerializeField]
    private bool enable = true;

    public override void OnEnter()
    {
        character.Value.EnableAbility(ability, enable);
        Continue();
    }

}
