using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CharacterClass))]
public class Assassin : MonoBehaviour
{

    private CharacterClass character;

    [SerializeField]
    private List<Ability> disableOnAttacked = new List<Ability>();

    [SerializeField]
    private List<Ability> ignoredAbilities = new List<Ability>();

    [SerializeField]
    private Fungus.MoveCollection pastMoves;

    public Fungus.MoveCollection PastMoves => pastMoves;

    private void OnEnable()
    {
        character = GetComponent<CharacterClass>();
        character.onTakeDamage += OnTakeDamage;
        character.onMakeMove += OnMakeMove;
    }

    private void OnDisable()
    {
        character.onTakeDamage -= OnTakeDamage;
        character.onMakeMove -= OnMakeMove;
    }

    private void OnTakeDamage(CharacterClass attacker, int damage)
    {
        foreach (Ability ability in disableOnAttacked)
            character.EnableTarget(ability, attacker, false);
    }

    private void OnMakeMove(Move move)
    {
        if (!move.allowMoreMoves)
            return;

        if (ignoredAbilities.Contains(move.ability))
            return;

        foreach (Move m in pastMoves)
        {
            if (move.ability == m.ability)
                return;
        }

        pastMoves.Add(move);
    }
}
