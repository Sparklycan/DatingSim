using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D myBody;
    private BoxCollider2D boxcollider2d;
    private float moveSpeed = 3000f;
    private float jumpForce = 20000f;
    private float wallJumpXmod = 1.0f;
    private float wallJumpYmod = 1.0f;
    private float extraHeight = 0.02f;
    private float extraWidth = 0.05f;

    [SerializeField] private LayerMask groundLayerMask;



    private void Awake()
    {
        boxcollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update   CREATE
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        
       

    }


    // Update is called once per frame  STEP
    void Update()
    {
        //STRAFING
        if (Input.GetKey(KeyCode.D))
        {
            if (gripRight())
            {
                myBody.velocity = new Vector2(moveSpeed * Time.deltaTime, 0f);
            }
            else {
                myBody.velocity = new Vector2(moveSpeed * Time.deltaTime, myBody.velocity.y);
            }
         
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (gripLeft()) {
                myBody.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0f);
            }
            else {
                myBody.velocity = new Vector2(-moveSpeed * Time.deltaTime, myBody.velocity.y);
            }
          
        }

        //JUMPING AND WALLJUMPING
        if (grounded() && Input.GetKey(KeyCode.W))
                {
             myBody.velocity = new Vector2(myBody.velocity.x, jumpForce * Time.deltaTime);
        }
        else if (gripRight() && Input.GetKey(KeyCode.W)) 
        {
            myBody.velocity = new Vector2(-jumpForce * Time.deltaTime * wallJumpXmod, jumpForce * Time.deltaTime * wallJumpYmod);
        }
        else if (gripLeft() && Input.GetKey(KeyCode.W))
        {
            myBody.velocity = new Vector2(jumpForce * Time.deltaTime * wallJumpXmod, jumpForce * Time.deltaTime * wallJumpYmod );
        }

        //CLAMP VELOCITY
      //  myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -moveSpeed, moveSpeed), myBody.velocity.y);
      //  myBody.velocity = new Vector2(myBody.velocity.x, Mathf.Clamp(myBody.velocity.y, -jumpForce, jumpForce));
    }

    //GROUND AND GRIP CHECKS
    private bool grounded()
    {
       
        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider2d.bounds.center, Vector2.down, boxcollider2d.bounds.extents.y + extraHeight, groundLayerMask);
        Color  rayColor;

        if (raycastHit.collider != null){
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxcollider2d.bounds.center, Vector2.down * (boxcollider2d.bounds.extents.y + extraHeight), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private bool gripRight()
    {
        
        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider2d.bounds.center, Vector2.right, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxcollider2d.bounds.center, Vector2.right * (boxcollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;

    }
    private bool gripLeft()
    {
        
        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider2d.bounds.center, Vector2.left, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxcollider2d.bounds.center, Vector2.left * (boxcollider2d.bounds.extents.x + extraWidth), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

   
}
