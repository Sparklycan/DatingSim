using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// CharacterClass variable type.
    /// </summary>
    [VariableInfo("Other", "CharacterClass")]
    [AddComponentMenu("")]
    [System.Serializable]
    public class CharacterClassVariable : VariableBase<CharacterClass>
    {
    }

    /// <summary>
    /// Container for a CharacterClass variable reference or constant value.
    /// </summary>
    [System.Serializable]
    public struct CharacterClassData
    {
        [SerializeField]
        [VariableProperty("<Value>", typeof(CharacterClassVariable))]
        public CharacterClassVariable characterClassRef;

        [SerializeField]
        public CharacterClass characterClassVal;

        public static implicit operator CharacterClass(CharacterClassData characterClassData)
        {
            return characterClassData.Value;
        }

        public CharacterClassData(CharacterClass v)
        {
            characterClassVal = v;
            characterClassRef = null;
        }

        public CharacterClass Value
        {
            get { return (characterClassRef == null) ? characterClassVal : characterClassRef.Value; }
            set { if (characterClassRef == null) { characterClassVal = value; } else { characterClassRef.Value = value; } }
        }

        public string GetDescription()
        {
            if (characterClassRef == null)
            {
                return characterClassVal != null ? characterClassVal.ToString() : "Null";
            }
            else
            {
                return characterClassRef.Key;
            }
        }
    }
}