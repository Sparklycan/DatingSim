using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MoveSelector : MonoBehaviour
{

    public abstract bool CanSelect(IEnumerable<Ability> abilities, CharacterClass character);
    public abstract void OnBeginSelect(CharacterClass character);
    public abstract IEnumerator<Move> Select();
    public abstract void OnEndSelect();


    public IEnumerable<CharacterClass> GetAvalableTargets(Ability.Targets targets, CharacterClass attacker)
    {
        IEnumerable<CharacterClass> enemies = FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Allegience)
            .Where(c => c != attacker);

        if (!enemies.Any())
            return Enumerable.Empty<CharacterClass>();

        switch (targets)
        {
            case Ability.Targets.Single:
                return enemies;
            case Ability.Targets.Self:
                return Enumerable.Repeat(attacker, 1);
            case Ability.Targets.None:
                return Enumerable.Empty<CharacterClass>();
            case Ability.Targets.All:
                return enemies;
            default:
                return Enumerable.Empty<CharacterClass>();
        }
    }

}
