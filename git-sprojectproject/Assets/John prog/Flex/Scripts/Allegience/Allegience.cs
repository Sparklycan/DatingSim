using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Allegience", menuName = "Flex/Allegience")]
public class Allegience : ScriptableObject
{
    [SerializeField]
    private int priority = 0;

    public int Priority => priority;
}
