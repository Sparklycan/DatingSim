using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Relationships", "Modify relationships", helpText: "")]
public class ModifyRelationshipCommand : Command
{

    [Header("The stats used to modify the relationships")]
    public FloatData romance;
    public FloatData lust;
    public FloatData suspicion;

    [Space]
    [Header("A list of relationships to modify")]
    public List<Relationship> relationships = new List<Relationship>();

    public override void OnEnter()
    {
        foreach (Relationship relationship in relationships)
            relationship.Modify(new Vector3(romance, lust, suspicion));

        Continue();
    }

}
