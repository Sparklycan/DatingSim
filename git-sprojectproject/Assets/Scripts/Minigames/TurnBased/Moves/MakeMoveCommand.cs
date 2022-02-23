using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{

    [CommandInfo("Flex", "Make move", helpText: "Make a move")]
    public class MakeMoveCommand : Command
    {

        [SerializeField]
        private MoveData move;
        [SerializeField]
        [Tooltip("The collection to use if the move adds more moves.")]
        private CollectionData moves;

        public override void OnEnter()
        {
            Attack attackPrefab = move.Value.ability.AttackPrefab;

            Attack attack = Instantiate(attackPrefab);
            attack.onAttackFinished += OnAttackFinished;
            attack.Initialize(move.Value.character, move.Value.targets, moves.Value as MoveCollection, move.Value.allowMoreMoves);
            attack.StartAttack();
        }

        private void OnAttackFinished()
        {
            for(int i = moves.Value.Count - 1; i >= 0; i--)
            {
                Move move = moves.Value.Get(i) as Move;
                if (move.character == null)
                    moves.Value.RemoveAt(i);
            }

            Continue();
        }
    }

}