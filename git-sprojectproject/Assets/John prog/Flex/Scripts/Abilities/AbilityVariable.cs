using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Ability variable type.
    /// </summary>
    [VariableInfo("Other", "Ability")]
    [AddComponentMenu("")]
    [System.Serializable]
    public class AbilityVariable : VariableBase<Ability>
    {
    }

    /// <summary>
    /// Container for a Ability variable reference or constant value.
    /// </summary>
    [System.Serializable]
    public struct AbilityData
    {
        [SerializeField]
        [VariableProperty("<Value>", typeof(AbilityVariable))]
        public AbilityVariable abilityRef;

        [SerializeField]
        public Ability abilityVal;

        public static implicit operator Ability(AbilityData abilityData)
        {
            return abilityData.Value;
        }

        public AbilityData(Ability v)
        {
            abilityVal = v;
            abilityRef = null;
        }

        public Ability Value
        {
            get { return (abilityRef == null) ? abilityVal : abilityRef.Value; }
            set { if (abilityRef == null) { abilityVal = value; } else { abilityRef.Value = value; } }
        }

        public string GetDescription()
        {
            if (abilityRef == null)
            {
                return abilityVal != null ? abilityVal.ToString() : "Null";
            }
            else
            {
                return abilityRef.Key;
            }
        }
    }
}