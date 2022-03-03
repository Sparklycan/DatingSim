using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshBake : MonoBehaviour
{

    public NavMeshSurface Surface;
    // Start is called before the first frame update
    void Awake()
    {
        Surface.BuildNavMesh();
    }

}
