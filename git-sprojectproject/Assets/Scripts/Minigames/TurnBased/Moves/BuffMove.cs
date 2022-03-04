using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMove : Move
{

    public readonly float attack = 0.0f;
    public readonly float defense = 0.0f;

    private Attack attackObject;

    public BuffMove(CharacterClass character, CharacterClass[] targets, Ability ability, float attack, float defense, int waitTurns = 0)
        :base(character, ability, targets, waitTurns, true)
    {
        this.attack = attack;
        this.defense = defense;
    }

    public override Attack GetAttack(MoveCollection moveCollection, Action onAttackFinished)
    {
        GameObject gameObject = new GameObject();

        attackObject = gameObject.AddComponent<Attack>();
        attackObject.onAttackFinished += onAttackFinished;
        attackObject.Initialize(character, targets, moveCollection, allowMoreMoves);
        attackObject.onAttack.AddListener(delegate { OnAttack(); });
        
        return attackObject;
    }

    private void OnAttack()
    {
        foreach (CharacterClass target in targets)
            target.AddBuff(attack, defense);

        attackObject.FinishAttack();
    }
}
