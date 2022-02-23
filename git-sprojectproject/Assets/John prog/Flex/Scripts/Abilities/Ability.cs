using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Flex/Ability")]
public class Ability : ScriptableObject
{

    public enum Targets
    {
        Single,
        Self,
        None,
        All
    }

    [SerializeField]
    private new string name = "Ability";
    [SerializeField]
    [Multiline]
    private string description = "Ability description";
    [SerializeField]
    private Targets target = Targets.Single;
    [SerializeField]
    private int priority = 0;
    [SerializeField]
    private Attack attackPrefab;

    public string Name => name;
    public string Description => description;
    public Targets Target => target;
    public Attack AttackPrefab => attackPrefab;
    public int Priority => priority;

}
