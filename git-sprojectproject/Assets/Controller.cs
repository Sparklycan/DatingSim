using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    #region VARS
    public Rigidbody2D myBody;
    private BoxCollider2D boxcollider2d;
    private float moveSpeed = 20f;
    private float jumpForce = 500f;
    private float wallJumpXmod = 1.8f;
    private float wallJumpYmod = 1.5f;
    private float extraHeight = 0.02f;
    private float extraWidth = 0.05f;
    private float maxSpeed = 10f;
    public float hore = 0f;
    public bool jumping;
    private int hp = 3;

    [SerializeField] private LayerMask groundLayerMask;
    #endregion

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
        hore = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("IM JUMPING");
            jumping = true;
        }

        if (hp < 1) {
            Die();
        }
    }
    #region RAYCASTS
    //GROUND AND GRIP CHECKS
    private bool grounded()
    {
        int rayHits = 0;
        //ray 1
        RaycastHit2D raycastHit1 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x - 0.5f, boxcollider2d.bounds.center.y), Vector2.down, boxcollider2d.bounds.extents.y + extraHeight, groundLayerMask);
        Color  rayColor1;

        if (raycastHit1.collider != null){
            rayColor1 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor1 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x - 0.5f, boxcollider2d.bounds.center.y), Vector2.down * (boxcollider2d.bounds.extents.y + extraHeight), rayColor1);
      //  Debug.Log(raycastHit1.collider);
        
        //ray 2
        RaycastHit2D raycastHit2 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x + 0.5f, boxcollider2d.bounds.center.y), Vector2.down, boxcollider2d.bounds.extents.y + extraHeight, groundLayerMask);
        Color rayColor2;

        if (raycastHit2.collider != null)
        {
            rayColor2 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor2 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x + 0.5f, boxcollider2d.bounds.center.y), Vector2.down * (boxcollider2d.bounds.extents.y + extraHeight), rayColor2);
       // Debug.Log(raycastHit2.collider);

        //ray 3
        RaycastHit2D raycastHit3 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.down, boxcollider2d.bounds.extents.y + extraHeight, groundLayerMask);
        Color rayColor3;

        if (raycastHit3.collider != null)
        {
            rayColor3 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor3 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.down * (boxcollider2d.bounds.extents.y + extraHeight), rayColor3);
       // Debug.Log(raycastHit3.collider);

        //returns
        if (rayHits != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private bool gripRight()
    {

        int rayHits = 0;
        //ray 1
        RaycastHit2D raycastHit1 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y + 0.5f), Vector2.right, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor1;

        if (raycastHit1.collider != null)
        {
            rayColor1 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor1 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y + 0.5f), Vector2.right * (boxcollider2d.bounds.extents.x + extraWidth), rayColor1);
       // Debug.Log(raycastHit1.collider);

        //ray 2
        RaycastHit2D raycastHit2 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y - 0.5f), Vector2.right, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor2;

        if (raycastHit2.collider != null)
        {
            rayColor2 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor2 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y - 0.5f), Vector2.right * (boxcollider2d.bounds.extents.x + extraWidth), rayColor2);
       // Debug.Log(raycastHit2.collider);

        //ray 3
        RaycastHit2D raycastHit3 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.right, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor3;

        if (raycastHit3.collider != null)
        {
            rayColor3 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor3 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.right * (boxcollider2d.bounds.extents.x + extraWidth), rayColor3);
      //  Debug.Log(raycastHit3.collider);

        //returns
        if (rayHits != 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool gripLeft()
    {

        int rayHits = 0;

        //ray 1
        RaycastHit2D raycastHit1 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y + 0.5f), Vector2.left, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor1;

        if (raycastHit1.collider != null)
        {
            rayColor1 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor1 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y + 0.5f), Vector2.left * (boxcollider2d.bounds.extents.x + extraWidth), rayColor1);
     //   Debug.Log(raycastHit1.collider);

        //ray 2
        RaycastHit2D raycastHit2 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y - 0.5f), Vector2.left, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
       Color rayColor2;

        if (raycastHit2.collider != null)
        {
            rayColor2 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor2 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y - 0.5f), Vector2.left * (boxcollider2d.bounds.extents.x + extraWidth), rayColor2);
     //   Debug.Log(raycastHit2.collider);
        //ray 3
        RaycastHit2D raycastHit3 = Physics2D.Raycast(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.left, boxcollider2d.bounds.extents.x + extraWidth, groundLayerMask);
        Color rayColor3;

        if (raycastHit3.collider != null)
        {
            rayColor3 = Color.green;
            rayHits++;
        }
        else
        {
            rayColor3 = Color.red;
        }

        Debug.DrawRay(new Vector2(boxcollider2d.bounds.center.x, boxcollider2d.bounds.center.y), Vector2.left * (boxcollider2d.bounds.extents.x + extraWidth), rayColor3);
       // Debug.Log(raycastHit3.collider);

        //returns
        if (rayHits != 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion

    private void FixedUpdate()
    {
        #region STRAFING
        //STRAFING
        if (hore > 0)
        {
            myBody.AddForce(new Vector2(moveSpeed, 0f), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            myBody.AddForce(new Vector2(-moveSpeed, 0f), ForceMode2D.Impulse);
        }
        #endregion
        #region JUMPING AND WALL JUMPING
        //JUMPING AND WALLJUMPING
        // input.GetButtonDown("Jump")
        Debug.Log(grounded() + " grounded");
        Debug.Log(jumping + " jumping");
        if (grounded() && jumping)
        {
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumping = false;
        }
        else if (gripRight() && jumping)
        {
            myBody.AddForce(new Vector2(-jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
        }
        else if (gripLeft() && jumping)
        {
            myBody.AddForce(new Vector2(jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
        }
        #endregion
        #region CLAMP
        //CLAMP VELOCITY orsakar problem med walljump eftersom jumpforce > movespeed

        myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -maxSpeed, maxSpeed), myBody.velocity.y);

        myBody.velocity = new Vector2(myBody.velocity.x, Mathf.Clamp(myBody.velocity.y, -jumpForce, jumpForce));
        #endregion
    }
    #region COLLISIONS
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy collison
        if (collision.transform.tag == "Enemy")
        {
            //destroys enemy
            Destroy(collision.gameObject);

            //self delete
            //  Destroy(gameObject);

            //push self
           myBody.AddForce(new Vector2(-jumpForce, 0f), ForceMode2D.Impulse);

            
        }
        //Thorn collision
        if (collision.transform.tag == "Thorn")
        {
            Hurt();
        }

            //debugs if works
            Debug.Log("WTF I AM COLLIDING");
    }
    #endregion

    void Die() {

        Destroy(gameObject);
        SceneManager.LoadScene("DinMammaHopparRunt");

        Debug.Log("You died");
    }

    void Hurt()
    {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        hp--;
        Debug.Log("OW");
    }
}
