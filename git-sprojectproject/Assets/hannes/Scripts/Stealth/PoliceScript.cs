using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Fungus;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

[ExecuteInEditMode]
public class PoliceScript : MonoBehaviour
{

    NavMeshAgent agent;

    private GameObject player;
    // Start is called before the first frame update
    
    // Attention area
    
    public float distance = 10;
    public float angle = 30;
    public float height = 1.1f;
    public Color meshcolor = Color.red;
    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask OcclusionLayers;
    public List<GameObject> Objects = new List<GameObject>();
    
    
    private Collider[] colliders = new Collider[50];
    private Mesh mesh;
    private int count;
    private float scanInterval;
    private float scanTimer;

    public Light SpotLight;
    [Space(15)] [Header("RoamPoints")] public Vector3[] Points;
    
    
    
    
    // movement
    private bool seen;
    [Tooltip("The time the Police waits on the different points.")]
    public float PauseTime;
    
    private float pauseTimer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("StealthPlayer");
        scanInterval = 1.0f / scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        SpotLight.spotAngle = angle * 2;
        SpotLight.range = distance + 2;
        if(Input.GetKeyDown(KeyCode.C))
        {
            agent.SetDestination(player.transform.position);
        }


        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }



    }

    void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers,
            QueryTriggerInteraction.Collide);
        Objects.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInsIght((obj)))
            {
                Objects.Add((obj));
            }

        }
        if (Objects.Count > 0)
        {
            agent.SetDestination(Objects[0].transform.position);
        }
        else
        {
            
        }
    }

    void Roam()
    {
        bool arrived;
        int current = 0;
        //Arrived.

        agent.SetDestination(Points[current]);
        
        if (arrived)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= PauseTime)
            {
                
            }    
        }
            // PSEUDO
            // IF REACHED GOAL
            // BOOL ARRIVED TRUE
            // IF ARRIVED TRUE: PAUSETIMER + TIME
            // IF PAUSETIMER > PAUSETIME: CURRENT ++
            // AS LONG AS CURRENT <= POINTS.LENGTH
            // SHOULD WORK



            float dist=agent.remainingDistance;
            
            if (dist != Mathf.infinite && agent.pathStatus == NavMeshPathStatus.completed && agent.remainingDistance == 0)
            {
                arrived = true;
            }

        


    }
    
    
    public bool IsInsIght(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if ((direction.y < 0) || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, OcclusionLayers))
        {
            return false;
        }

        return true;
    }
    
     Mesh createWedgeMesh()
     {
         Mesh mesh = new Mesh();

         int segments = 10;
         int numTriangles = (segments*4) + 2 + 2;
         int numVertices = numTriangles * 3;
         
         
         Vector3[] vertices = new Vector3[numVertices];
         int[] triangles = new int[numVertices];

         Vector3 bottomCenter = Vector3.zero;
         Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
         Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

         Vector3 topCenter = bottomCenter + Vector3.up * height;
         Vector3 topRight = bottomRight + Vector3.up * height;
         Vector3 topLeft = bottomLeft + Vector3.up * height;

         int vert = 0;
         
         // left side
         vertices[vert++] = bottomCenter;
         vertices[vert++] = bottomLeft;
         vertices[vert++] = topLeft;
         
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

         float currentAngle = -angle;
         float deltaAngle = (angle * 2) / segments;
         for (int i = 0; i < segments; i++)
         {
              bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
              bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

              topRight = bottomRight + Vector3.up * height;
              topLeft = bottomLeft + Vector3.up * height;
             
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
             
             currentAngle += deltaAngle;
         }
         
         for (int i = 0; i < numVertices; i++)
         {
             triangles[i] = i;
         }

         mesh.vertices = vertices;
         mesh.triangles = triangles;
         mesh.RecalculateNormals();
         
         
         return mesh;
     }


     private void OnValidate()
     {
         mesh = createWedgeMesh();
         scanInterval = 1.0f / scanFrequency;

     }
     

     private void OnDrawGizmos()
     {
         if(mesh)
         {
             Gizmos.color = meshcolor;
             Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
         }
          
         
         Gizmos.DrawWireSphere(transform.position, distance);
         for (int i = 0; i < count; i++)
         {
             Gizmos.DrawSphere((colliders[i].transform.position), 0.2f);
         }


         Gizmos.color = Color.green;
         foreach (var obj in Objects)
         {
             Gizmos.DrawSphere((obj.transform.position), 0.2f);
         }
     }

     private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.white;

         for (int i = 0; i < Points.Length - 1; i++)
         {
            Gizmos.DrawLine(Points[i], Points[i+1]);
                    
         }
         Gizmos.DrawWireSphere(Points[0], 1);
         Gizmos.DrawLine(Points[Points.Length - 1], Points[0]);

     }
}
