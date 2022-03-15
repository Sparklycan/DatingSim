using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Stun character", helpText: "Stops a character from using any moves")]
    public class StunCommand : Command
    {

        [SerializeField]
        private CharacterClassData character;
        [SerializeField]
        private CollectionData moves;
        [SerializeField]
        private Fungus.BooleanData stun;

        public override void OnEnter()
        {
            if (stun)
            {
                Stunned stunned = character.Value.gameObject.AddComponent<Stunned>();
                if (stunned != null)
                    stunned.moves = moves.Value as MoveCollection;
            }
            else
            {
                Stunned stunned = character.Value.GetComponent<Stunned>();
                if (stunned != null)
                    Destroy(stunned);
            }

            Continue();
        }
    }

}