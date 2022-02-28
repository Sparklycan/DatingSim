using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D myBody;

    private float moveForce = 2000f;
    private float moveSpeed = 1f;
    private float jumpForce = 20f;
    private float maxMoveSpeed = 8f;
    private float tSpeed = 0.1f;
  

  



    // Start is called before the first frame update   CREATE
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();


    }


    // Update is called once per frame  STEP
    void Update()
    {
        // rb.AddForce(forwardForce * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.D))
        {
            myBody.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
            /*
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            
            moveSpeed = Mathf.Lerp(0.0f, maxMoveSpeed, tSpeed);
            tSpeed += 1.0f * Time.deltaTime;
            */

        }
        else if (Input.GetKey(KeyCode.A))
        {
            myBody.AddForce(-transform.right * moveSpeed, ForceMode2D.Impulse);
            /*
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            moveSpeed = Mathf.Lerp(0.0f, maxMoveSpeed, tSpeed);
            tSpeed += 1.0f * Time.deltaTime;
            */

        }
        /*
		else
		{
            moveSpeed = Mathf.Lerp(0.0f, maxMoveSpeed, tSpeed);
           tSpeed -= 0.5f * Time.deltaTime;

        }
        //  rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);

        }
        */

        Mathf.Clamp(tSpeed, 0.0f, 1.0f);

        //myBody.velocity.x = Mathf.Clamp(myBody.velocity.x, 0f, 1f);
        
        //grounded check for jump constraint (and gravity?)
    }
}
