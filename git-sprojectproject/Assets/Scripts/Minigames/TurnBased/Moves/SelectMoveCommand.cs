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
        private CollectionData moves;
        [SerializeField]
        private IntegerData index;
        [SerializeField]
        private BooleanData allowMoreMoves;
        [SerializeField]
        private BooleanData isHidden;

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
                ability.Current.isHidden = isHidden.Value;
                moves.Value.Insert(index.Value, ability.Current);
            }
            Continue();
            yield return null;
        }
    }

}