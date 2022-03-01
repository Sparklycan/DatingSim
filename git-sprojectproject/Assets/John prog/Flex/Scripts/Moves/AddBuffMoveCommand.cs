using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Add buff", helpText: "Add a status buff")]
    public class AddBuffMoveCommand : Command
    {

        [SerializeField]
        [Tooltip("The collection to add the move to.")]
        private CollectionData moves;
        [SerializeField]
        private CharacterClassData target;
        [SerializeField]
        private AbilityData ability;
        [SerializeField]
        private FloatData attack;
        [SerializeField]
        private FloatData defense;
        [SerializeField]
        [Tooltip("Where in the collection the move should be inserted")]
        private IntegerData index;
        [SerializeField]
        private IntegerData waitTurns;

        public override void OnEnter()
        {
            BuffMove move = new BuffMove(target, null, ability, attack.Value, defense.Value, waitTurns.Value);
            move.allowMoreMoves = false;
            if (waitTurns.Value > 0)
                moves.Value.Add(move);
            else
                moves.Value.Insert(index.Value, move);
            Continue();
        }
    }

}