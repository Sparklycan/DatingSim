using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    [SerializeField]
    private CharacterClass character;

    [SerializeField]
    private UnityEngine.UI.Image bar;

    [SerializeField]
    private Gradient healthGradient;

    private void OnEnable()
    {
        character.onHealthChange += OnHealthChange;

        float target;
        if (character.MaxHealth != 0)
            target = (float)character.CurrentHealth / (float)character.MaxHealth;
        else target = 1.0f;
        bar.transform.localScale = new Vector3(target, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.color = healthGradient.Evaluate(target);
    }

    private void OnDisable()
    {
        character.onHealthChange -= OnHealthChange;
    }

    private void OnHealthChange(int change)
    {
        float target;
        if (character.MaxHealth != 0)
            target = (float)character.CurrentHealth / (float)character.MaxHealth;
        else target = 1.0f;
        bar.transform.localScale = new Vector3(target, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.color = healthGradient.Evaluate(target);
    }
}
