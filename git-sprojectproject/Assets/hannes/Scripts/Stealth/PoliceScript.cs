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
using Random = System.Random;

[ExecuteInEditMode]
public class PoliceScript : MonoBehaviour
{
 
    
    NavMeshAgent agent;

    private GameObject player;

    private Light _light;
    // Attention area
    [Header("ViewArea")]
    public float distance = 10;
    public float angle = 30;
    public float height = 1.1f;
    public Color meshcolor = Color.red;
    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask OcclusionLayers;
    [HideInInspector]
    public List<GameObject> Objects = new List<GameObject>();


    private Collider[] colliders = new Collider[50];
    private Mesh mesh;
    private int count;
    private float scanInterval;
    private float scanTimer;

    [Tooltip( "All the points that the Police will go through, the cyan line is from starting position to show which direction the police will move.")]
    [Space(15)]
    public Vector3[] Points;


    // movement
    [Header("Movement")]
    private bool seen, chase, roam = true, confused, scared;

    private Transform startTransform;
    
    [Tooltip("The time the Police waits on the different points.")]
    public float PauseTime;

    [Tooltip("How long the police will remember your position even after you are outside his viewcone.")]
    public float chaseTime;

    [Tooltip("How long the police will pause after forgetting the player at a spot before going back to its roam.")]
    public float confusedTime;

    [Tooltip("")]
    public float scaredTime;

    [Tooltip("")]
    public float AlertSpeedMultiplier;
    

    private float AlertSpeed, originalSpeed;
    
    private float pauseTimer, chaseTimer, confusedTimer, scaredTimer;
    int current = 0;

    private float multiplyBy = 2 ;
    
    GameObject Chased;

    // Picture
    [Header("Picture")]
    public float PictureTime;
    private float pictureTimer;
    private bool picture, pictureTaken = false;
    private FlowchartCommunicator _flowchartCommunicator;
    
    [Space(10)]
    public Slider slider;
    public GameObject DeathParticle;
    public Light SpotLight;

    [Header("Picture")]
    // Backstab
    private BoxCollider boxCollider;
    
    
    void Start()
    {
        _light = GetComponent<Light>();
        boxCollider = GetComponent<BoxCollider>();
        slider.gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("StealthPlayer");
        scanInterval = 1.0f / scanFrequency;
        _flowchartCommunicator = GetComponent<FlowchartCommunicator>();
        originalSpeed = agent.speed;
        AlertSpeed = agent.speed * AlertSpeedMultiplier;
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
            Chase();
        }
        else
        {
            chaseTimer = 0;
        }
        
        if (confused)
        {
            Confused();
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
        else if (pictureTaken)
        {
            boxCollider.enabled = true;
        }

        if (scared)
        {
            Scared();
        }
        else
        {
            scaredTimer = 0;
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
        else if (Objects.Count > 0 && pictureTaken)
        {
            scaredTimer = 0;
            scared = true;
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

        if (pictureTaken)
        {
            _light.color = Color.magenta;
        }
        else
        {
            _light.color = Color.blue;
        }
        
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
        // IF ARRIVED TRUE: PAUSETIMER + TIME � (might not work as the scan is on a timer...) §
        // IF PAUSETIMER > PAUSETIME: CURRENT ++ � 
        // AS LONG AS CURRENT <= POINTS.LENGTH �
        // SHOULD WORK �
        
    }

    void Chase()
    {
        _light.color = Color.red;
        Debug.Log("CHASING");
        agent.isStopped = false;
        agent.SetDestination(Chased.transform.position);
        chaseTimer += Time.deltaTime;
        agent.speed = AlertSpeed;
        
        if(chaseTimer >= chaseTime)
        {
            agent.speed = originalSpeed;
            chaseTimer = 0;
            confused = true;
            chase = false;
        }
        
    }

    
    // YOU ARE HERE MOTHERBITCHFUCK
    void Scared()
    {
        Debug.Log("SCARED");
        _light.color = Color.cyan;
        
        Vector3 dirToPlayer = transform.position - player.transform.position;

        Vector3 newPos = transform.position + dirToPlayer;

        agent.SetDestination(newPos);

        scaredTimer += Time.deltaTime;
        if (scaredTimer > scaredTime)
        {
            Debug.Log("not scared anymore");
            scaredTimer = 0;
            confused = true;
            scared = false;
        }
    }
    
    void Picture()
    {
        slider.gameObject.SetActive(true);
        slider.maxValue = PictureTime;
        pictureTimer += Time.deltaTime;
        setProgress(pictureTimer);
        if (pictureTimer > 0)
        {
            slider.gameObject.SetActive(true);
        }
        else
            slider.gameObject.SetActive(false);
        
        if (pictureTimer > PictureTime)
        {
            agent.speed = originalSpeed;
            confused = true;
            chase = false;
            scared = false;
            _flowchartCommunicator.SendMessage("Click");
            pictureTaken = true;
            _light.color = Color.magenta;
            slider.gameObject.SetActive(false);
        }
        
        
    }

    void Confused()
    {
        if (!pictureTaken)
        {
            _light.color = Color.blue;
        }

        scared = false;
        slider.gameObject.SetActive(false);
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

    void setProgress(float progress)
    {
        slider.value = progress;
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
        // this... was interesting to code to say the least. Thank you emil for your amazing Datorgrafik Course!
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StealthPlayer")
        {
            GameObject.Instantiate(DeathParticle, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
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






}
