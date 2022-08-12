using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damages : MonoBehaviour
{

    //private Dictionary<CharacterClass, int> damages = new Dictionary<CharacterClass, int>();
    private List<CharacterClass> characters = new List<CharacterClass>();
    private List<int> damages = new List<int>();
    private List<int> heals = new List<int>();

    public UnityEngine.Events.UnityEvent onCalculateDamages;

    public void CalculateDamages()
    {
        characters.Clear();
        damages.Clear();
        heals.Clear();
        onCalculateDamages?.Invoke();
    }

    public virtual int GetDamage(CharacterClass target)
    {
        int damage = 0;

        int index = characters.IndexOf(target);
        if (index >= 0)
            damage = damages[index];

        return damage;
    }

    public virtual int GetHeal(CharacterClass target)
    {
        int heal = 0;

        int index = characters.IndexOf(target);
        if (index >= 0)
            heal = heals[index];

        return heal;
    }

    public void SetDamage(CharacterClass target, int damage)
    {
        int index = characters.IndexOf(target);
        if (index >= 0)
            damages[index] = damage;
        else
        {
            characters.Add(target);
            damages.Add(damage);
            heals.Add(0);
        }
    }

    public void SetHeal(CharacterClass target, int heal)
    {
        int index = characters.IndexOf(target);
        if (index >= 0)
            heals[index] = heal;
        else
        {
            characters.Add(target);
            heals.Add(heal);
            damages.Add(0);
        }
    }

}
