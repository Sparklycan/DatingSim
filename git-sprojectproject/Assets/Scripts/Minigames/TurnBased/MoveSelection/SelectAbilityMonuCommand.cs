using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using UnityEngine.UI;
using System.Linq;

[CommandInfo("Flex", "Select ability menu", helpText: "Select an ability.")]
public class SelectAbilityMonuCommand : WaitForCondition
{

    [SerializeField]
    private AbilityButton buttonPrefab;
    [SerializeField]
    private LayoutGroup buttonLayoutGroup;
    [SerializeField]
    private GameObject abilityDescriptionObject;
    [SerializeField]
    private Text abilityDescriptionText;
    [SerializeField]
    private Selectable defaultSelectable;

    [SerializeField]
    private CharacterClassData character;
    [SerializeField]
    private AbilityData selectedAbility;

    protected override void PreEvaluate()
    {
        selectedAbility.Value = null;

        foreach (AbilityButton button in buttonLayoutGroup.GetComponentsInChildren<AbilityButton>())
            Destroy(button.gameObject);

        foreach (Ability ability in character.Value.Abilities)
        {
            AbilityButton button = Instantiate(buttonPrefab, buttonLayoutGroup.transform);
            button.onClick.AddListener(delegate { selectedAbility.Value = ability; });
            button.interactable = !character.Value.DisabledAbilities.Any(a => a == ability);

            button.Initialize(ability, abilityDescriptionObject, abilityDescriptionText, defaultSelectable);

            Text text = button.GetComponentInChildren<Text>();
            text.text = ability.Name;
        }
    }

    protected override bool? EvaluateCondition()
    {
        if (selectedAbility.Value == null)
            return null;
        else
            return true;
    }

}
