using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Remove character moves", helpText: "Remove moves that a character will use")]
    public class RemoveCharacterMovesCommand : Command
    {

        [SerializeField]
        private CollectionData moves;
        [SerializeField]
        private CharacterClassData character;

        public override void OnEnter()
        {
            for(int i = moves.Value.Count - 1; i >= 0; i--)
            {
                Move move = moves.Value.Get(i) as Move;
                if (move.character != character.Value)
                    continue;

                moves.Value.RemoveAt(i);
            }

            Continue();
        }
    }

}