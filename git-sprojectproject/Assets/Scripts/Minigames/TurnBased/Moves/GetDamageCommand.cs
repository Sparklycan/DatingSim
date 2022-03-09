using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flex", "Get damage", helpText: "Get the amount of damage that should be dealt to a target.")]
public class GetDamageCommand : Command
{

    [SerializeField]
    private Damages damages;

    [SerializeField]
    private CharacterClassData target;

    [SerializeField]
    private IntegerData damage;

    public override void OnEnter()
    {
        if(damages != null)
            damage.Value = damages.GetDamage(target.Value);

        Continue();
    }

}
