using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Relationships", "Load relationship stats", helpText: "Loads the stats from a relationship into the flowchart.")]
public class LoadRelationshipCommand : Command
{

    [Header("Variables to load the stats into")]
    public FloatData romance;
    public FloatData lust;
    public FloatData suspicion;

    [Space]
    [Header("The relationship to load the stats from")]
    public Relationship relationship;

    public override void OnEnter()
    {
        if (romance.floatRef != null) romance.Value = relationship.stats.x;
        if (lust.floatRef != null) lust.Value = relationship.stats.y;
        if (suspicion.floatRef != null) suspicion.Value = relationship.stats.z;

        Continue();
    }

}