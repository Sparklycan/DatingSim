using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Enable all targets", helpText: "Enable all targets")]
    public class EnableAllTargetsCommand : Command
    {

        [SerializeField]
        private CharacterClassData character;

        public override void OnEnter()
        {
            character.Value.EnableAllTargets();
            Continue();
        }

    }

}