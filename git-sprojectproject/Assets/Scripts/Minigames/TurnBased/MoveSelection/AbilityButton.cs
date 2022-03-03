using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : Button
{

    private Ability ability;
    private GameObject abilityDescriptionObject;
    private Text abilityDescription;
    private Selectable defaultSelectable;

    public void Initialize(Ability ability, GameObject descriptionObject, Text desctiption, Selectable defaultSelectable)
    {
        this.ability = ability;
        abilityDescriptionObject = descriptionObject;
        abilityDescription = desctiption;
        this.defaultSelectable = defaultSelectable;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Highliht(true);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Highliht(false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Select();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        defaultSelectable.Select();
    }

    private void Highliht(bool show)
    {
        if (abilityDescription == null)
            return;

        if (show)
        {
            abilityDescription.text = ability.Description;
            abilityDescriptionObject.SetActive(true);
        }
        else
            abilityDescriptionObject.SetActive(false);
    }
}
