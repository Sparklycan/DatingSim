using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Select move", helpText: "Select a move")]
    public class SelectMoveCommand : Command
    {

        [SerializeField]
        private CharacterClassData character;
        [SerializeField]
        [Tooltip("The collection to add the move to.")]
        private CollectionData moves;
        [SerializeField]
        [Tooltip("Where in the collection the move should be inserted")]
        private IntegerData index;
        [SerializeField]
        [Tooltip("Should this move allow for more moves to be added this turn")]
        private BooleanData allowMoreMoves;

        public override void OnEnter()
        {
            StartCoroutine("SelectAbility");
        }

        private IEnumerator SelectAbility()
        {
            if (character.Value.CanSelectAbility())
            {
                IEnumerator<Move> ability = character.Value.SelectAbility();
                while (ability.MoveNext())
                    yield return null;

                ability.Current.allowMoreMoves = allowMoreMoves.Value;
                moves.Value.Insert(index.Value, ability.Current);
            }
            Continue();
            yield return null;
        }
    }

}