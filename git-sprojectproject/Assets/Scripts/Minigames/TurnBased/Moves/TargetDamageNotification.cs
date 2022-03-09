using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDamageNotification : MonoBehaviour
{

    [SerializeField]
    private CharacterClass attacker;

    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject objectToHide;

    private Dictionary<CharacterClass, int> targets = new Dictionary<CharacterClass, int>();

    private void FixedUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        targets.Clear();

        foreach(Attack attack in FindObjectsOfType<Attack>())
        {
            if (attack.Attacker != attacker)
                continue;

            Damages damages = attack.GetComponent<Damages>();
            if (damages == null)
                continue;

            foreach(CharacterClass target in attack.Targets)
            {
                int damage = damages.GetDamage(target);
                int totalDamage = (int)((float)damage * attack.Attacker.AttackBuff / target.DefenseBuff);
                if (targets.ContainsKey(target))
                    targets[target] += totalDamage;
                else
                    targets[target] = totalDamage;
            }
        }

        if(targets.Count == 0)
        {
            objectToHide.SetActive(false);
        }
        else
        {
            objectToHide.SetActive(true);

            text.text = "";
            foreach (var target in targets)
            {
                text.text += $"{target.Key.Name}\n\tDamage: {target.Value}\n";
            }
            text.text = text.text.Remove(text.text.Length - 1);
        }
    }

}
