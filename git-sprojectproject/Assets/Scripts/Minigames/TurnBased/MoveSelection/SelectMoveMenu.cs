using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Fungus.Flowchart))]
public class SelectMoveMenu : MoveSelector
{

    [SerializeField]
    private string characterVariable = "SelectedCharacter";
    private string moveVariable = "SelectedMove";

    private Fungus.Flowchart flowchart = null;
    private Fungus.Flowchart Flowchart
    {
        get
        {
            if (flowchart == null)
                flowchart = GetComponent<Fungus.Flowchart>();
            return flowchart;
        }
    }

    private Move selectedMove;

    public override void OnBeginSelect(CharacterClass character)
    {
        selectedMove = null;
        Fungus.CharacterClassVariable variable = Flowchart.GetVariable<Fungus.CharacterClassVariable>(characterVariable);
        variable.Value = character;

        gameObject.SetActive(true);
    }

    public override void OnEndSelect()
    {
        gameObject.SetActive(false);
    }

    public void SetMove(string moveKey)
    {
        selectedMove = flowchart.GetVariable<Fungus.MoveVariable>(moveKey).Value;
    }

    public override IEnumerator<Move> Select()
    {
        while (selectedMove == null)
            yield return null;
        yield return selectedMove;
    }

    public override bool CanSelect(IEnumerable<Ability> abilities, CharacterClass character)
    {
        return abilities
            .Select(a => a.Target)
            .Distinct()
            .Select(t => GetAvalableTargets(t, character))
            .Select(t => t.Any())
            .Any();
    }
}
