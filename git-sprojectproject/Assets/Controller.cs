using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D myBody;
    public float moveSpeed = 10.0f;
    public float jumpForce = 20.0f;
    private Vector2 zero;
    public float lerpSpeed = 0.5f;
    public Transform groundCheck;
    public LayerMask Ground;
    private bool grounded = false;
    public float gravity = 10.0f;
    private Vector2 velocity;
    Vector3 movement;
    float vertical, horizontal;



    // Start is called before the first frame update   CREATE
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        myBody = GetComponent<Rigidbody2D>();

    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            grounded = false;
        }
    }

    // Update is called once per frame  STEP
    void Update()
    {
        //lösa grounded med vertical input
      
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        movement = new Vector3(horizontal, vertical, 0).normalized;

        //"raka" keybinds.
        /*
        if (Input.GetKey(KeyCode.A)){
            velocity.x = -moveSpeed;
            myBody.velocity += velocity;
        }
       else  if (Input.GetKey(KeyCode.D))
        {
            myBody.velocity = transform.right * moveSpeed;
        }
      
        else
        {
            myBody.velocity = Vector2.Lerp(myBody.velocity, Vector2.zero, lerpSpeed * Time.deltaTime);
        }
      

        if (Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            myBody.velocity = transform.up * jumpForce;
            
        }
        */

        //hitta ett sätt att applicera en impulse på bara en axel?
        myBody.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
        myBody.velocity = Vector2.Lerp(myBody.velocity, Vector2.zero, lerpSpeed * Time.deltaTime); //gör just nu ingenting
        myBody.velocity = Vector3.ClampMagnitude(myBody.velocity, moveSpeed);

        //gravity
        /*
        if (grounded == false)
        {
            myBody.velocity = -transform.up * gravity;

        }
        */



    }
}
