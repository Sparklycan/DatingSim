using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Add move", helpText: "Add a move")]
    public class AddMoveCommand : Command
    {

        [SerializeField]
        [Tooltip("The collection to add the move to.")]
        private CollectionData moves;
        [SerializeField]
        private CharacterClassData attacker;
        [SerializeField]
        private CharacterClassData target;
        [SerializeField]
        private AbilityData ability;
        [SerializeField]
        [Tooltip("Where in the collection the move should be inserted")]
        private IntegerData index;
        [SerializeField]
        [Tooltip("Should this move allow for more moves to be added this turn")]
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