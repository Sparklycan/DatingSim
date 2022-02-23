using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ClassBase
{

    public string ClassName { get; }
    public int MaxHealth { get; }
    public IEnumerable<Ability> Abilities { get; }

}
