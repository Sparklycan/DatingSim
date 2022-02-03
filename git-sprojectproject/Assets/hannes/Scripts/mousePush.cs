using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePush : MonoBehaviour
{
    Vector3 worldPosition;

    private float pushSize, pushTimer;
    private float power;

    [Tooltip("When ticked in, Kacpers awesome movement is active, otherwise Hannes weird ass movement is implemented.")]
    public bool GoodMovement;

    [Space(20)]

    public float pushSizeMultiplier, pushDelay;

    [Space(10)]

    [Range(1000, 10000)]
    public float minPower;
    [Range(1000, 10000)]
    public float maxPower;
    private float powerRange;

    [Space(10)]

    [Range(0,10)]
    public float minPushSize;
    [Range(0, 10)]
    public float maxPushSize;
    private float PSizeRange;
    

    CircleCollider2D circle;

    // Variables for Hannes Movement

    Vector2 StartForcePos, EndForcePos;







    // PSEUDO for gradual amount increase.
    /*
    Arange = distance of the A values (Amin … Amax)
    Avalue = value on Arange

    Brange = distance of the B values (Bmin … Bmax)
    Bvalue = value on Brange

    Arange = Amax - Amin
    Brange = Bmax - Bmin

    Bvalue = Bmin + (Avalue-Amin)/Arange*Brange
    */



    void Start()
    {
        circle = gameObject.GetComponent<CircleCollider2D>();
        powerRange = maxPower - minPower;
        PSizeRange = maxPushSize - minPushSize;
    }

    


    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPosition;


        if (GoodMovement)
        {

            if (pushSize >= maxPushSize)
            {
                pushSize = maxPushSize;
            }


            if (Input.GetMouseButton(0))
            {
                // circle.radius += pushSize * power * Time.deltaTime;

                pushSize += Time.deltaTime * pushSizeMultiplier;
                power = minPower + (pushSize - minPushSize) / PSizeRange * powerRange;
            }
            if (Input.GetMouseButtonUp(0))
            {
                pushTimer = 0;
                circle.radius = pushSize;
                circle.enabled = true;
                pushSize = minPushSize;
            }


            pushTimer += Time.deltaTime;
            if (pushTimer >= pushDelay)
            {
                circle.enabled = false;
            }
        }
        else
        {

            if(Input.GetMouseButtonDown(0))
            {
                EndForcePos = worldPosition;
                StartForcePos = worldPosition;
            }

            if(Input.GetMouseButtonUp(0))
            {
                EndForcePos = worldPosition;
            }




        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(GoodMovement)
        Gizmos.DrawWireSphere(worldPosition, pushSize);

        else {
            Gizmos.DrawLine(StartForcePos, EndForcePos);
        }

    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            Debug.Log("BALL");
            Vector3 dir = (collision.transform.position - worldPosition).normalized;
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            collision.GetComponent<Rigidbody2D>().AddForce(dir * power);
        }
    }
}
