using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshBake : MonoBehaviour
{

    public NavMeshSurface Surface;
    void Awake()
    {
        Surface.BuildNavMesh();
    }
    
}
