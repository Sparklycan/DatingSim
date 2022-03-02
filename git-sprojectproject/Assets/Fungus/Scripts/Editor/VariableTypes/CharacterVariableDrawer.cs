using UnityEditor;

namespace Fungus.EditorUtils
{
    [CustomPropertyDrawer(typeof(CharacterData))]
    public class CharacterDataDrawer : VariableDataDrawer<CharacterVariable>
    { }
}