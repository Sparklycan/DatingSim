using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceScript : MonoBehaviour
{

    NavMeshAgent agent;

    private GameObject player;
    // Start is called before the first frame update
    
    // Attention area
    public float distance = 10;
    public float angle = 30;
    public float height = 1.1f;
    public color meshcolor = Color.red;

    private mesh mesh;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("StealthPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            agent.SetDestination(player.transform.position);
        }

    }

     Mesh createWedgeMesh()
     {
         Mesh mesh = new Mesh();

         int numtriangles = 8;
         int numVertices = numtriangles * 3;
         
         
         Vector3[] vertices = new Vector3[numVertices];
         int[] triangles = new int[numVertices];

         Vector3 bottomCenter = Vector3.zero;
         Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance);
         Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance);

         Vector3 topCenter = bottomCenter + Vector3.up * height;
         Vector3 topRight = bottomRight + Vector3.up * height;
         Vector3 topLeft = bottomLeft + Vector3.up * height;

         int vert = 0;
         
         // left side
         vertices[vert++] = bottomCenter;
         vertices[vert++] = bottomLeft;
         vertices[vert++] = bottomRight;
         
         vertices[vert++] = topLeft;
         vertices[vert++] = topCenter;
         vertices[vert++] = bottomCenter;
         // right side 
         vertices[vert++] = bottomCenter;
         vertices[vert++] = topCenter;
         vertices[vert++] = topRight;
         
         vertices[vert++] = topRight;
         vertices[vert++] = bottomRight;
         vertices[vert++] = bottomCenter;
         // far side
         vertices[vert++] = bottomLeft;
         vertices[vert++] = bottomRight;
         vertices[vert++] = topRight;
         
         vertices[vert++] = topRight;
         vertices[vert++] = topLeft;
         vertices[vert++] = bottomLeft;
         // top
         vertices[vert++] = topCenter;
         vertices[vert++] = topLeft;
         vertices[vert++] = topRight;
         
         // bottom
         vertices[vert++] = bottomCenter;
         vertices[vert++] = bottomRight;
         vertices[vert++] = bottomLeft;


         for (int i = 0; i < numVertices; i++)
         {
             triangles[i] = i
         }

         mesh.vertices = vertices;
         mesh.triangles = triangles;
         mesh.RecalculateNormals();
         
         
         return mesh;
     }


     private void OnValidate()
     {
         mesh = createWedgeMesh();

     }
     

     private void OnDrawGizmos()
     {
         if(mesh)
         {
             Gizmos.color = meshcolor;
             Gizmos.DrawMesh((mesh, transform.position, transform.rotation));
         }
         
     }
}
