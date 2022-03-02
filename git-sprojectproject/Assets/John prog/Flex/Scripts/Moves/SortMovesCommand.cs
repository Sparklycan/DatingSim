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
            List<Move> moveList = new List<Move>();

            foreach (Move move in moves.Value)
                moveList.Add(move);

            moves.Value.Clear();
            foreach (Move move in moveList
                .OrderByDescending(m => m.waitTurns)
                .ThenBy(m => m.ability.Priority)
                .ThenBy(m => m.character.Allegience.Priority))
                    moves.Value.Insert(0, move);

            Continue();
        }
    }

}