using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Move
{

    private Attack attackObject;
    public Attack Attack => attackObject;

    public CharacterClass character;
    public Ability ability;
    public CharacterClass[] targets;
    public string move;
    public bool allowMoreMoves = true;
    public bool isHidden = false;
    public int waitTurns;
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

    public string Description
    {
        get
        {
            string description = "";

            description += character.ClassName + '\n';
            description += ability.Name + '\n';
            description += "Targets:";

            //foreach (CharacterClass target in targets)
            //    description += "\n\t: " + target.ClassName + GetDamage(target).ToString();

            return description;
        }
    }

    protected virtual Attack CreateAttackObject()
    {
        return GameObject.Instantiate(ability.AttackPrefab, GameObject.FindObjectOfType<TurnManager>().transform);
    }

    public void InitializeAttack(MoveCollection moveCollection, Action onAttackFinished)
    {
        Attack.onAttackFinished += onAttackFinished;
        Attack.Initialize(character, targets, moveCollection, allowMoreMoves);
    }

    public Move(CharacterClass character, Ability ability, CharacterClass[] targets, int waitTurns = 0, bool isHidden = false)
    {
        this.character = character;
        this.ability = ability;
        this.targets = targets;
        this.waitTurns = waitTurns;
        this.isHidden = isHidden;

        move = ability.Name;

        attackObject = CreateAttackObject();
        Damages damages = attackObject.GetComponent<Damages>();
        if (damages)
        {
            Attack.Initialize(character, targets, null, allowMoreMoves);
            damages.CalculateDamages();
        }
    }

}
