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
    public List<Ability> prioritizedAbilities = new List<Ability>();

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

    public void SortMoves()
    {
        List<Move> sortedMoves = new List<Move>();

        foreach (Ability priority in prioritizedAbilities)
        {
            foreach(Move move in pastMoves)
            {
                if (move.ability == priority)
                    sortedMoves.Add(move);
            }
        }
        foreach (Move move in pastMoves)
        {
            if (!sortedMoves.Contains(move))
                sortedMoves.Add(move);
        }

        pastMoves.Clear();
        foreach (Move move in sortedMoves)
            pastMoves.Add(move);
    }

    private void OnMakeMove(Move move)
    {
        if (move.isHidden)
            return;

        if (ignoredAbilities.Contains(move.ability))
            return;

        pastMoves.Add(move);
    }
}
