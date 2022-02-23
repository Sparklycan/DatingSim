using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Move
{
    public readonly CharacterClass character;
    public readonly Ability ability;
    public readonly CharacterClass[] targets;
    public bool allowMoreMoves = true;
    public bool IsValid
    {
        get
        {
            if (character == null)
                return false;
            if (targets == null)
                return false;

            if (ability == null)
                return targets.Length == 0;

            switch (ability.Target)
            {
                case Ability.Targets.Self:
                    {
                        if (targets.Length != 1)
                            return false;
                        else
                            return targets[0] == character;
                    }
                case Ability.Targets.Single:
                    {
                        if (targets.Length != 1)
                            return false;
                        else
                            return targets[0].Allegience != character.Allegience;
                    }
                case Ability.Targets.None:
                    return targets.Length == 0;
                case Ability.Targets.All:
                    return targets.Length > 0;
            }

            return false;
        }
    }

    public Move(CharacterClass character, Ability ability, CharacterClass[] targets)
    {
        this.character = character;
        this.ability = ability;
        this.targets = targets;
    }

}
