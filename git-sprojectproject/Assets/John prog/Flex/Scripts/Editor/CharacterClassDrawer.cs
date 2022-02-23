using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fungus.EditorUtils
{
    [CustomPropertyDrawer(typeof(CharacterClassData))]
    public class CharacterClassDrawer : VariableDataDrawer<CharacterClassVariable>
    { }
}