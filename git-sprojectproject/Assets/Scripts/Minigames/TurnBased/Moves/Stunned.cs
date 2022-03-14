using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterClass))]
[DisallowMultipleComponent]
public class Stunned : MonoBehaviour
{

    public Fungus.MoveCollection moves;

    private void Start()
    {
        CharacterClass character = GetComponent<CharacterClass>();

        for (int i = moves.Count - 1; i >= 0; i--)
        {
            Move move = moves.Get(i) as Move;
            if (move.character != character)
                continue;

            moves.RemoveAt(i);
            Destroy(move.Attack.gameObject);
        }
    }

}
