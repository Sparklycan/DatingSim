using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : MonoBehaviour
{

    [SerializeField]
    private Ability noAbility;

    private HashSet<CharacterClass> characters = new HashSet<CharacterClass>();
    private Dictionary<CharacterClass, Move> selectedMoves = new Dictionary<CharacterClass, Move>();

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

    public IEnumerator SelectAbilities(Allegience allegience)
    {
        List<CharacterClass> characters = this.characters
            .Where(c => c.Allegience == allegience)
            .ToList();

        foreach (CharacterClass character in this.characters
            .Where(c => c.Allegience == allegience))
        {
            IEnumerator<Move> ability = character.SelectAbility();
            while (ability.MoveNext())
                yield return null;
            selectedMoves[character] = ability.Current;
        }
    }

    public IEnumerator SelectAbility(Fungus.CharacterClassData character)
    {
        IEnumerator<Move> ability = character.Value.SelectAbility();
        while (ability.MoveNext())
            yield return null;
        selectedMoves[character.Value] = ability.Current;
    }

    public void CollectAbilities(Fungus.MoveCollection collection)
    {
        collection.Clear();
        foreach (Move move in selectedMoves.Select(a => a.Value))
        {
            int index = -1;
            while (true)
            {
                index++;
                if (index >= collection.Count)
                    break;

                Move m = collection.Get(index) as Move;

                if (m.ability.Priority < move.ability.Priority)
                    continue;
                if (m.ability.Priority > move.ability.Priority)
                    break;
                if (m.character.Allegience.Priority > move.character.Allegience.Priority)
                    break;
            }
            collection.Insert(index, move);
        }
        collection.Reverse();
    }

    public int GetAllegienceCount(Allegience allegience)
    {
        return characters
            .Select(c => c.Allegience)
            .Where(a => a == allegience)
            .Count();
    }

}
