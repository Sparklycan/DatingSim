using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Fungus;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PoliceScript : MonoBehaviour
{
 
    // FIX SOME "KEY OFF FINGER" DELAY ON MOVEMENT.
    
    NavMeshAgent agent;

    private GameObject player;

    // Attention area
    public float distance = 10;
    public float angle = 30;
    public float height = 1.1f;
    public Color meshcolor = Color.red;
    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask OcclusionLayers;
    public List<GameObject> Objects = new List<GameObject>();
    public Light SpotLight;


    private Collider[] colliders = new Collider[50];
    private Mesh mesh;
    private int count;
    private float scanInterval;
    private float scanTimer;

    [Tooltip(
        "All the points that the Police will go through, the cyan line is from starting position to show which direction the police will move.")]
    [Space(15)]
    public Vector3[] Points;


    // movement
    private bool seen, chase, roam = true, confused;

    [Tooltip("The time the Police waits on the different points.")]
    public float PauseTime;

    [Tooltip("How long the police will remember your position even after you are outside his viewcone.")]
    public float chaseTime;

    [Tooltip("How long the police will pause after forgetting the player at a spot before going back to its roam.")]
    public float confusedTime;

    private float pauseTimer, chaseTimer, confusedTimer;
    int current = 0;
    GameObject Chased;

    [Header("Picture")]
    // Picture
    public float PictureTime;
    private float pictureTimer;
    private bool picture, pictureTaken;
    private FlowchartCommunicator _flowchartCommunicator;
    public Slider slider;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("StealthPlayer");
        scanInterval = 1.0f / scanFrequency;
        _flowchartCommunicator = GetComponent<FlowchartCommunicator>();
        slider.maxValue = PictureTime;
    }

    // Update is called once per frame
    void Update()
    {

        SpotLight.spotAngle = angle * 2;
        SpotLight.range = distance + 2;
        if (Input.GetKeyDown(KeyCode.C))
        {
            agent.SetDestination(player.transform.position);
        }


        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }

        if (chase)
        {
            Debug.Log("CHASING");
            agent.isStopped = false;
            agent.SetDestination(Chased.transform.position);
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseTime)
            {
                chaseTimer = 0;
                confused = true;
                chase = false;
            }
        }
        else
        {
            chaseTimer = 0;
        }


        if (confused)
        {
            confusedTimer += Time.deltaTime;
            agent.isStopped = true;
            pictureTimer = 0;
            if (confusedTimer >= confusedTime)
            {
                confusedTimer = 0;
                confused = false;
                roam = true;
            }
        }
        else
        {
            confusedTimer = 0;
        }


        if (roam)
        {
            if (Points.Length > 0)
                Roam();
        }


        if (picture && !pictureTaken)
        {
            Picture();
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
                Chased = Objects[0];
            }

        }

        if (Objects.Count > 0 && !pictureTaken)
        {
            chaseTimer = 0;
            chase = true;
            confused = false;
            roam = false;
            picture = true;
        }
        else
        {
            picture = false;
        }

    }

    void Roam()
    {
        bool arrived = false;
        agent.isStopped = false;

        agent.SetDestination(Points[current]);
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&
            agent.remainingDistance == 0 && !chase)
        {
            arrived = true;
        }

        if (arrived)
        {
            pauseTimer += Time.deltaTime * 2;
            if (pauseTimer >= PauseTime)
            {
                if (current < Points.Length - 1)
                {
                    current++;
                }
                else
                {
                    current = 0;
                }

                pauseTimer = 0;
                arrived = false;
            }
        }

        // PSEUDO
        // IF REACHED GOAL �
        // BOOL ARRIVED TRUE �
        // IF ARRIVED TRUE: PAUSETIMER + TIME � (might not work as the scan is on a timer...)
        // IF PAUSETIMER > PAUSETIME: CURRENT ++ � 
        // AS LONG AS CURRENT <= POINTS.LENGTH �
        // SHOULD WORK �
    }
    
    void Picture()
    {
        Light light = GetComponent<Light>();
        pictureTimer += Time.deltaTime;
        slider.value = chaseTimer;
        
        if (pictureTimer > 0)
        {
            slider.gameObject.SetActive(true);
        }
        else
            slider.gameObject.SetActive(false);
        
        if (pictureTimer > PictureTime)
        {
            confused = true;
            chase = false;
            _flowchartCommunicator.SendMessage("Click");
            pictureTaken = true;
            light.color = Color.magenta;
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
        int numTriangles = (segments * 4) + 2 + 2;
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
        if (mesh)
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

        if (Points.Length > 0)
        {
            for (int i = 1; i < Points.Length - 1; i++)
            {
                Gizmos.DrawLine(Points[i], Points[i + 1]);
                Gizmos.DrawIcon(Points[i], "blendKeySelected", true);
            }

            Gizmos.DrawLine(Points[Points.Length - 1], Points[0]);
            //Gizmos.DrawWireSphere(Points[1], 1);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(Points[0], 1);
            Gizmos.DrawLine(Points[0], Points[1]);

        }
    }

    // POLICE FIND PLAYER, COUNTER GOES DOWN FOR EVERY SECOND THEY SEE YOU
    // IF STILL IN CHASE STATE COUNTER UP, BUT ONLY WHEN YOU'RE IN VISION.
    // WHEN TIMES GONE UP, PICTURE TAKEN. 
    // TIMER ON X-SECONDS UNTIL PERSON UPLOADS THE PICTURE.




}