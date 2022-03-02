using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterClass))]
public class Assassin : MonoBehaviour
{

    private CharacterClass character;

    [SerializeField]
    private List<Ability> disableOnAttacked = new List<Ability>();

    private void Awake()
    {
        character = GetComponent<CharacterClass>();
        character.onTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(CharacterClass attacker, int damage)
    {
        foreach (Ability ability in disableOnAttacked)
            character.EnableTarget(ability, attacker, false);
    }
}
