using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Fungus
{

    [CommandInfo("Flex", "Sort moves", helpText: "Sort moves")]
    public class SortMovesCommand : Command
    {

        [SerializeField]
        private CollectionData moves;

        public override void OnEnter()
        {
            List<Move> sortedMoves = new List<Move>();

            foreach (Move move in moves.Value)
            {
                if (move.waitTurns > 0)
                    move.waitTurns--;
                sortedMoves.Add(move);
            }

            moves.Value.Clear();
            foreach (Move move in sortedMoves
                .OrderByDescending(m => m.waitTurns)
                .ThenBy(m => m.ability.Priority)
                .ThenBy(m => m.character.Allegience.Priority))
                    moves.Value.Insert(0, move);
            Continue();
        }
    }

}