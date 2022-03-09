using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fungus
{

    [CommandInfo("Flex", "Heal", helpText: "Heals a CharacterClass")]
    public class HealCommand : Command
    {

        [SerializeField]
        private CharacterClassData character;
        [SerializeField]
        private IntegerData ammount;

        public override void OnEnter()
        {
            character.Value.Heal(ammount);
            Continue();
        }

    }

}