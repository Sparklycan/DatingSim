using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Move variable type.
    /// </summary>
    [VariableInfo("Other", "Move")]
    [AddComponentMenu("")]
    [System.Serializable]
    public class MoveVariable : VariableBase<Move>
    {
    }

    /// <summary>
    /// Container for a Move variable reference or constant value.
    /// </summary>
    [System.Serializable]
    public struct MoveData
    {
        [SerializeField]
        [VariableProperty("<Value>", typeof(MoveVariable))]
        public MoveVariable moveRef;

        [SerializeField]
        public Move moveVal;

        public static implicit operator Move(MoveData moveData)
        {
            return moveData.Value;
        }

        public MoveData(Move v)
        {
            moveVal = v;
            moveRef = null;
        }

        public Move Value
        {
            get { return (moveRef == null) ? moveVal : moveRef.Value; }
            set { if (moveRef == null) { moveVal = value; } else { moveRef.Value = value; } }
        }

        public string GetDescription()
        {
            if (moveRef == null)
            {
                return moveVal != null ? moveVal.ToString() : "Null";
            }
            else
            {
                return moveRef.Key;
            }
        }
    }
}