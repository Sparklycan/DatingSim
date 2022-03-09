using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damages : MonoBehaviour
{

    private Dictionary<CharacterClass, int> damages = new Dictionary<CharacterClass, int>();

    public UnityEngine.Events.UnityEvent onCalculateDamages;

    public void CalculateDamages()
    {
        damages.Clear();
        onCalculateDamages?.Invoke();
    }

    public int GetDamage(CharacterClass target)
    {
        int damage = 0;
        if (target == null)
            return 0;
        damages.TryGetValue(target, out damage);
        return damage;
    }

    public void SetDamage(CharacterClass target, int damage)
    {
        damages[target] = damage;
    }

}
