using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Add move", helpText: "Add a move")]
    public class AddMoveCommand : Command
    {

        [SerializeField]
        private CollectionData moves;
        [SerializeField]
        private CharacterClassData attacker;
        [SerializeField]
        private CharacterClassData target;
        [SerializeField]
        private AbilityData ability;
        [SerializeField]
        private IntegerData index;
        [SerializeField]
        private BooleanData allowMoreMoves;

        public override void OnEnter()
        {
            Move move = new Move(attacker.Value, ability.Value, new CharacterClass[] { target.Value });
            move.allowMoreMoves = allowMoreMoves;
            moves.Value.Insert(index.Value, move);
            Continue();
        }
    }

}