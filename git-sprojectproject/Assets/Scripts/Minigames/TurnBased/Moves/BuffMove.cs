using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMove : Move
{

    public readonly float attack = 0.0f;
    public readonly float defense = 0.0f;

    public BuffMove(CharacterClass character, CharacterClass[] targets, Ability ability, float attack, float defense, int waitTurns = 0)
        :base(character, ability, targets, waitTurns, true)
    {
        this.attack = attack;
        this.defense = defense;
    }

    protected override Attack CreateAttackObject()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(GameObject.FindObjectOfType<TurnManager>().transform);

        Attack attackObject = gameObject.AddComponent<Attack>();
        attackObject.onAttack.AddListener(delegate
        {
            OnAttack();
        });

        return attackObject;
    }

    private void OnAttack()
    {
        character.AddBuff(attack, defense);

        Attack.FinishAttack();
    }
}
