using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

[CommandInfo("Relationships", "Set relationship stats", helpText: "")]
public class SetRelationshipCommand : Command
{

    [Header("The values to set")]
    public bool setRomance = false;
    public FloatData romance;
    [Space]
    public bool setLust = false;
    public FloatData lust;
    [Space]
    public bool setSuspicion = false;
    public FloatData suspicion;

    [Space]
    [Header("A list of relationships to set")]
    public List<Relationship> relationships = new List<Relationship>();

    public override void OnEnter()
    {
        foreach (Relationship relationship in relationships)
        {
            if (setRomance) relationship.stats.x = romance;
            if (setLust) relationship.stats.y = lust;
            if (setSuspicion) relationship.stats.z = suspicion;
        }

        Continue();
    }

}
