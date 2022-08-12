using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDamages : Damages
{

    [SerializeField]
    private int targetDamage = 5;
    [SerializeField]
    private int attackerDamage = 5;

    public override int GetDamage(CharacterClass target)
    {
        Attack attack = GetComponent<Attack>();
        if (attack == null)
            return 0;

        if (target == attack.Attacker)
            return attackerDamage;

        return targetDamage;
    }

}
