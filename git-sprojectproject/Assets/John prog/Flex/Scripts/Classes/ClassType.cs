using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class Type", menuName = "Flex/Class Type")]
public class ClassType : ScriptableObject, ClassBase
{

    [SerializeField]
    private new string name = "Class";
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private List<Ability> abilities = new List<Ability>();

    public string ClassName => name;

    public int MaxHealth => maxHealth;

    public IEnumerable<Ability> Abilities => abilities;

}
