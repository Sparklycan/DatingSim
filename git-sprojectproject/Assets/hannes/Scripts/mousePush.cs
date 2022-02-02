using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePush : MonoBehaviour
{
    Vector3 worldPosition;

    private float pushSize, pushTimer;

    public float pushMultiplier, maxPush, pushDelay;

    [Space(10)]

    public float power;

    CircleCollider2D circle;

    

    void Start()
    {
        circle = gameObject.GetComponent<CircleCollider2D>();
    }



    // gradual SIZE DIFFERENCE OF POWER REMEMBER UNTIL YOU GET HOME M-KAAAY?
    // gradual SIZE DIFFERENCE OF POWER REMEMBER UNTIL YOU GET HOME M-KAAAY?
    // gradual SIZE DIFFERENCE OF POWER REMEMBER UNTIL YOU GET HOME M-KAAAY?
    // gradual SIZE DIFFERENCE OF POWER REMEMBER UNTIL YOU GET HOME M-KAAAY?
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPosition;

        if(pushSize >= maxPush)
        {
            pushSize = maxPush;
        }


        if(Input.GetMouseButton(0))
        {
            // circle.radius += pushSize * power * Time.deltaTime;

            pushSize += Time.deltaTime * pushMultiplier;
        }
        if(Input.GetMouseButtonUp(0))
        {
            pushTimer = 0;
            circle.radius = pushSize;
            circle.enabled = true;
            pushSize = 0;
        }


        pushTimer += Time.deltaTime;
        if(pushTimer >= pushDelay)
        {
            circle.enabled = false;
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(worldPosition, pushSize);
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            Debug.Log("BALL");
            Vector3 dir = (collision.transform.position - worldPosition).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(dir * power);
        }
    }
}
