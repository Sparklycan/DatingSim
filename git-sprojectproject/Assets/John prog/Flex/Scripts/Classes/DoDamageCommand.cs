using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fungus
{

    [CommandInfo("Flex", "Do damage", helpText: "Deal some damage to a CharacterClass")]
    public class DoDamageCommand : Command
    {

        [SerializeField]
        private CharacterClassData character;
        [SerializeField]
        private IntegerData damage;
        [SerializeField]
        private StringData hurtAnimation;
        [SerializeField]
        private StringData deathAnimation;

        public override void OnEnter()
        {
            character.Value.DoDamage(damage.Value, hurtAnimation.stringVal, deathAnimation.stringVal);
            Continue();
        }

    }

}