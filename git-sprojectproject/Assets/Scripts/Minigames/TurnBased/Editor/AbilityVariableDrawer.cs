using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fungus.EditorUtils
{
    [CustomPropertyDrawer(typeof(AbilityData))]
    public class AbilityVariableDrawer : VariableDataDrawer<AbilityVariable>
    { }
}