using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectRandomMove : MoveSelector
{

    public IEnumerable<Ability> abilities;
    private CharacterClass character;

    public override void OnBeginSelect(CharacterClass character)
    {
        this.character = character;
        abilities = character.Abilities.Except(character.DisabledAbilities);
    }

    public override void OnEndSelect()
    {
    }

    public override IEnumerator<Move> Select()
    {
        Ability ability = abilities.ElementAt(Random.Range(0, abilities.Count() - 1));
        CharacterClass[] targets = null;

        switch (ability.Target)
        {
            case Ability.Targets.Single:
                {
                    var characters = GetAvalableTargets(ability.Target, character);
                    if (characters.Count() > 0)
                    {
                        int random = Random.Range(0, characters.Count());
                        targets = characters.Where((e, i) => i == random)
                            .ToArray();
                    }
                    break;
                }
            default:
                targets = GetAvalableTargets(ability.Target, character)
                    .ToArray();
                break;
        }

        yield return new Move(character, ability, targets);
    }

    public override bool CanSelect(IEnumerable<Ability> abilities, CharacterClass character)
    {
        return abilities
            .Select(a => a.Target)
            .Distinct()
            .Select(t => GetAvalableTargets(t, character))
            .Select(t => t.Any())
            .Any();
    }
}
