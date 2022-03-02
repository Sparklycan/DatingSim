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
    private Button buttonPrefab;
    [SerializeField]
    private LayoutGroup buttonLayoutGroup;

    [SerializeField]
    private CharacterClassData character;
    [SerializeField]
    private AbilityData selectedAbility;

    protected override void PreEvaluate()
    {
        selectedAbility.Value = null;

        foreach (Button button in buttonLayoutGroup.GetComponentsInChildren<Button>())
            Destroy(button.gameObject);

        foreach (Ability ability in character.Value.Abilities)
        {
            Button button = Instantiate(buttonPrefab, buttonLayoutGroup.transform);
            button.onClick.AddListener(delegate { selectedAbility.Value = ability; });
            button.interactable = !character.Value.DisabledAbilities.Any(a => a == ability);

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
