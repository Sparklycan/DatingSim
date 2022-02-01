using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PersonalityTrait", menuName = "Relationship/Personality trait")]
public class PersonalityTrait : ScriptableObject
{

    [Tooltip("How this trait is influenced by romance.\nX = romance\nY = lust\nZ = suspicion")]
    [SerializeField]
    private Vector3 romance = Vector3.right;
    [Tooltip("How this trait is influenced by lust.\nX = romance\nY = lust\nZ = suspicion")]
    [SerializeField]
    private Vector3 lust = Vector3.up;
    [Tooltip("How this trait is influenced by suspicion.\nX = romance\nY = lust\nZ = suspicion")]
    [SerializeField]
    private Vector3 suspicion = Vector3.forward;

    public Matrix4x4 Modifier(float strength) => new Matrix4x4(
        strength * romance,
        strength * lust,
        strength * suspicion,
        new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

}
