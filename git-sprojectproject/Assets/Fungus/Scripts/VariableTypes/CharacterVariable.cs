using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Character variable type.
    /// </summary>
    [VariableInfo("Other", "Character")]
    [AddComponentMenu("")]
    [System.Serializable]
    public class CharacterVariable : VariableBase<Character>
    {
    }

    /// <summary>
    /// Container for an Character variable reference or constant value.
    /// </summary>
    [System.Serializable]
    public struct CharacterData
    {
        [SerializeField]
        [VariableProperty("<Value>", typeof(CharacterVariable))]
        public CharacterVariable characterRef;

        [SerializeField]
        public Character characterVal;

        public static implicit operator Character(CharacterData characterVal)
        {
            return characterVal.Value;
        }

        public CharacterData(Character v)
        {
            characterVal = v;
            characterRef = null;
        }

        public Character Value
        {
            get { return (characterRef == null) ? characterVal : characterRef.Value; }
            set { if (characterRef == null) { characterVal = value; } else { characterRef.Value = value; } }
        }

        public string GetDescription()
        {
            if (characterRef == null)
            {
                return characterVal != null ? characterVal.ToString() : "Null";
            }
            else
            {
                return characterRef.Key;
            }
        }
    }
}