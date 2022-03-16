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
    private AbilityData selectedAbility;

    [SerializeField]
    private CharacterClassData attacker;

    [SerializeField]
    private MoveData selectedMove;

    [SerializeField]
    private Button buttonPrefab;

    [SerializeField]
    private LayoutGroup buttonGroup;

    private bool? selected;
    private CharacterClass[] selection = null;

    private Action onReset = null;

    protected override void PreEvaluate()
    {
        selected = null;

        CreateCharacterSelection();

        CreateReturnButton();
    }

    protected override bool? EvaluateCondition()
    {
        return selected;
    }

    protected override void OnTrue()
    {
        ClearButtons();

        selectedMove.Value = new Move(attacker.Value, selectedAbility.Value, selection);

        base.OnTrue();
    }

    protected override void OnFalse()
    {
        ClearButtons();

        base.OnFalse();
    }

    private void ClearButtons()
    {
        foreach(Button button in buttonGroup.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
    }

    private void CreateCharacterSelection()
    {
        switch (selectedAbility.Value.Target)
        {
            case Ability.Targets.None:
                CreateConfirmButton();
                break;
            case Ability.Targets.Self:
                CreateSelfButton();
                break;
            case Ability.Targets.Single:
                CreateSingleTargetButtons();
                break;
            case Ability.Targets.All:
                CreateTargetAllButton();
                break;
        }
    }

    private void CreateConfirmButton()
    {
        Button button = Instantiate(buttonPrefab, buttonGroup.transform);
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Confirm";
        button.onClick.AddListener(delegate
        {
            selection = new CharacterClass[0];
            selected = true;
        });
    }

    private void CreateSelfButton()
    {
        Highlight highlight = attacker.Value.GetComponent<Highlight>();
        highlight.state = Highlight.HighlightState.Highlight;

        onReset = () => highlight.state = Highlight.HighlightState.Default;

        Button button = Instantiate(buttonPrefab, buttonGroup.transform);
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Confirm";
        button.onClick.AddListener(delegate
        {
            selection = new CharacterClass[1]
            {
                attacker.Value
            };
            selected = true;

            onReset?.Invoke();
        });
    }

    private void CreateSingleTargetButtons()
    {
        Button button = Instantiate(buttonPrefab, buttonGroup.transform);
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Select a Target";
        button.interactable = false;

        ShadeDisabledTargets();

        IEnumerable<CharacterClass> targets = FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Value.Allegience)
            .Where(c => attacker.Value.CanAttack(selectedAbility.Value, c));

        onReset = () =>
        {
            foreach (CharacterClass target in FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Value.Allegience))
            {
                TargetButton targetButton = target.GetComponentInChildren<TargetButton>();
                targetButton.enabled = false;

                Highlight highlight = target.GetComponent<Highlight>();
                if (highlight != null)
                    highlight.state = Highlight.HighlightState.Default;
            }
        };

        foreach (CharacterClass target in targets)
        {
            TargetButton targetButton = target.GetComponentInChildren<TargetButton>();
            targetButton.enabled = true;
            targetButton.onClick += () =>
            {
                selection = new CharacterClass[1]
                {
                    target
                };

                selected = true;

                onReset?.Invoke();
            };
        }
    }

    private void ShadeDisabledTargets()
    {
        IEnumerable<CharacterClass> targets = FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Value.Allegience)
            .Where(c => !attacker.Value.CanAttack(selectedAbility.Value, c));

        foreach (CharacterClass target in targets)
        {
            Highlight highlight = target.GetComponent<Highlight>();
            if (highlight != null)
                highlight.state = Highlight.HighlightState.Shade;
        }
    }

    private void CreateTargetAllButton()
    {
        IEnumerable<CharacterClass> targets = FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Value.Allegience)
            .Where(c => attacker.Value.CanAttack(selectedAbility.Value, c));

        ShadeDisabledTargets();

        foreach (Highlight target in FindObjectsOfType<CharacterClass>()
            .Where(c => c.Allegience != attacker.Value.Allegience)
            .Select(c => c.GetComponent<Highlight>())
            .Where(h => h != null))
        {
            target.state = Highlight.HighlightState.Highlight;
        }

        onReset = () =>
        {
            foreach (Highlight target in targets
            .Select(c => c.GetComponent<Highlight>())
            .Where(h => h != null))
            {
                target.state = Highlight.HighlightState.Default;
            }
        };

        Button button = Instantiate(buttonPrefab, buttonGroup.transform);
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Confirm";
        button.onClick.AddListener(delegate
        {
            selection = targets.ToArray();
            selected = true;
            onReset?.Invoke();
        });
    }

    private void CreateReturnButton()
    {
        Button button = Instantiate(buttonPrefab, buttonGroup.transform);
        Text text = button.GetComponentInChildren<Text>();
        text.text = "Return";
        button.onClick.AddListener(delegate
        {
            selected = false;
            onReset?.Invoke();
        });
    }
}
