using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using UnityEngine.UI;
using System.Linq;

[CommandInfo("Flex", "Select targets menu", helpText: "Select the targets.")]
public class SelectTargetMenuCommand : WaitForCondition
{

    [SerializeField]
    private CharacterClassData character;
    [SerializeField]
    private AbilityData selectedAbility;
    [SerializeField]
    private MoveData selectedMove;
    [SerializeField]
    private Toggle togglePrefab;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button returnButton;
    [SerializeField]
    private ToggleGroup toggles;

    private bool? selected;

    private Dictionary<CharacterClass, Toggle> characterToggles = new Dictionary<CharacterClass, Toggle>();

    protected override void PreEvaluate()
    {
        selected = null;
        characterToggles.Clear();

        foreach (Toggle toggle in toggles.GetComponentsInChildren<Toggle>())
            Destroy(toggle.gameObject);

        continueButton.onClick.AddListener(OnContinue);
        returnButton.onClick.AddListener(OnReturn);

        CreateToggles();
        SetToggles();
        EvaluateToggles();
    }

    private void OnContinue()
    {
        selected = true;

        continueButton.onClick.RemoveListener(OnContinue);
        returnButton.onClick.RemoveListener(OnReturn);
    }

    private void OnReturn()
    {
        selected = false;

        continueButton.onClick.RemoveListener(OnContinue);
        returnButton.onClick.RemoveListener(OnReturn);
    }

    private void CreateToggles()
    {
        switch (selectedAbility.Value.Target)
        {
            case Ability.Targets.None:
                break;
            case Ability.Targets.Self:
                CreateToggle(character, false);
                break;
            case Ability.Targets.Single:
            case Ability.Targets.All:
                {
                    ToggleGroup toggleGroup = selectedAbility.Value.Target == Ability.Targets.Single ? toggles : null;
                    bool interactable = selectedAbility.Value.Target == Ability.Targets.Single;
                    foreach (CharacterClass character in FindObjectsOfType<CharacterClass>().Where(c => c.Allegience != this.character.Value.Allegience))
                        CreateToggle(character, interactable, toggleGroup);
                    break;
                }
        }
    }

    private void CreateToggle(CharacterClass character, bool interactable, ToggleGroup toggleGroup = null)
    {
        Vector2 position = character.transform.position;

        Toggle toggle = Instantiate(togglePrefab, position, Quaternion.identity, toggles.transform);
        toggle.interactable = interactable && this.character.Value.CanAttack(selectedAbility.Value, character);
        toggle.onValueChanged.AddListener(delegate { EvaluateToggles(); });
        toggle.group = toggleGroup;

        characterToggles[character] = toggle;
    }

    private void SetToggles()
    {
        switch (selectedAbility.Value.Target)
        {
            case Ability.Targets.None:
                toggles.enabled = false;
                break;
            case Ability.Targets.Self:
                toggles.enabled = false;
                characterToggles[character].isOn = true;
                break;
            case Ability.Targets.All:
                toggles.enabled = false;
                foreach (var toggle in characterToggles)
                    toggle.Value.isOn = true;
                break;
            case Ability.Targets.Single:
                toggles.enabled = true;
                break;
        }
    }

    private void EvaluateToggles()
    {
        bool canContinue = false;

        switch (selectedAbility.Value.Target)
        {
            case Ability.Targets.None:
                canContinue = true;
                break;
            case Ability.Targets.Self:
                canContinue = characterToggles[character.Value].isOn;
                break;
            case Ability.Targets.All:
                canContinue = true;
                foreach (Toggle toggle in characterToggles.Select(t => t.Value))
                    canContinue = toggle.isOn && canContinue;
                break;
            case Ability.Targets.Single:
                canContinue = toggles.AnyTogglesOn();
                break;
        }
        continueButton.interactable = canContinue;
    }

    protected override bool? EvaluateCondition()
    {
        return selected;
    }

    protected override void OnTrue()
    {
        CharacterClass character = this.character.Value;
        Ability ability = selectedAbility.Value;
        CharacterClass[] targets = characterToggles
            .Where(t => t.Value.isOn)
            .Select(t => t.Key)
            .ToArray();
        selectedMove.Value = new Move(character, ability, targets);

        Continue();
    }
}
