using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Sort moves", helpText: "Sort moves first by ability, second by allegience.")]
    public class SortMovesCommand : Command
    {

        [SerializeField]
        private CollectionData moves;

        public override void OnEnter()
        {
            List<Move> sortedMoves = new List<Move>();

            foreach (Move move in moves.Value)
            {
                int index = -1;
                while (true)
                {
                    index++;
                    if (index >= sortedMoves.Count)
                        break;

                    Move m = sortedMoves[index];

                    if (m.ability.Priority < move.ability.Priority)
                        continue;
                    if (m.ability.Priority > move.ability.Priority)
                        break;
                    if (m.character.Allegience.Priority > move.character.Allegience.Priority)
                        break;
                }
                sortedMoves.Insert(index, move);
            }

            moves.Value.Clear();
            foreach (Move move in sortedMoves)
                moves.Value.Insert(0, move);
            Continue();
        }
    }

}