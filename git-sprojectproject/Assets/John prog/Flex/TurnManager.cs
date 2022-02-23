using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : MonoBehaviour
{

    private HashSet<CharacterClass> characters = new HashSet<CharacterClass>();

    public TurnManager()
    {
        CharacterClass.onCharacterEnable += OnCharacterEnable;
        CharacterClass.onCharacterDisable += OnCharacterDisable;
    }

    private void OnDestroy()
    {
        CharacterClass.onCharacterEnable -= OnCharacterEnable;
        CharacterClass.onCharacterDisable -= OnCharacterDisable;
    }

    private void OnCharacterEnable(CharacterClass character)
    {
        characters.Add(character);
    }

    private void OnCharacterDisable(CharacterClass character)
    {
        characters.Remove(character);
    }

    public void CollectCharacters(Fungus.CharacterClassCollection collection)
    {
        collection.Clear();

        foreach (CharacterClass character in characters.OrderBy(c => c.Allegience.Priority))
            collection.Add(character);
    }

    public int GetAllegienceCount(Allegience allegience)
    {
        return characters
            .Select(c => c.Allegience)
            .Where(a => a == allegience)
            .Count();
    }

}
