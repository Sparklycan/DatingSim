using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fungus.EditorUtils
{
    [CustomPropertyDrawer(typeof(MoveData))]
    public class MoveVariableDrawer : VariableDataDrawer<MoveVariable>
    { }
}