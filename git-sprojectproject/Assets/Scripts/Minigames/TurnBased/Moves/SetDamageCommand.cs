using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flex", "Set damage", helpText: "Set the amount of damage that should be dealt to a target.")]
public class SetDamageCommand : Command
{

    [SerializeField]
    private Damages damages;

    [SerializeField]
    private CharacterClassData target;

    [SerializeField]
    private IntegerData damage;

    public override void OnEnter()
    {
        if (damages != null)
            damages.SetDamage(target.Value, damage.Value);

        Continue();
    }

}
