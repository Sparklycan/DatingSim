using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour
{

    [Serializable]
    private class CharacterTrait
    {
        public PersonalityTrait personalityTrait;
        [Range(0.0f, 2.0f)]
        public float strength = 1.0f;
    }

    [Tooltip("X = romance\nY = lust\nZ = suspicion")]
    public Vector3 stats;

    [Space]
    [SerializeField]
    private List<CharacterTrait> characterTraits = new List<CharacterTrait>();

    public void Modify(Vector3 values)
    {
        Matrix4x4 modifier = Matrix4x4.identity;
        foreach(CharacterTrait characterTrait in characterTraits)
        {
            modifier *= characterTrait.personalityTrait.Modifier(characterTrait.strength);
        }

        stats += modifier.MultiplyPoint(values);
    }

}
