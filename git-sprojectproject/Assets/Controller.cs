using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    #region VARS
    //public vars need to be changed in editor, not script
    public Rigidbody2D myBody;
    private BoxCollider2D boxcollider2d;

    public ParticleSystem blood;

    private float moveSpeed = 20f;
    private float jumpForce = 200f;
    private float maxSpeed = 8f;
    private bool jumping;
    private float wallJumpXmod = 1.8f;
    private float wallJumpYmod = 1.05f;
    private float hore = 0f;
    private float jumpClamper = 1f;
    private bool jumpReady = true;
    private float jumpCDtimer;
    private float origJumpCD = 0.2f;
   
    private float extraHeight = 0.02f;
    private float extraWidth = 0.6f;

    private int hp;
    private int maxHp = 3;
    private Vector3 respawn;
    private bool vampire = false;

    private float bloodTime = 0.5f;
    private float currentBloodTime;
    private bool bleeding;
    private float bleedDuration = 0.2f;

    public GameObject[] hearts;

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
        respawn = transform.position;
        jumpCDtimer = origJumpCD;
        currentBloodTime = bloodTime;
        bleeding = false;
        hp = maxHp;
    }

    // Update is called once per frame  STEP
    void Update()
    {
        #region INPUTS
        hore = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Jump"))
        {
            jumping = true;
        }

        if (hp < 1) {
            Die();
        }
        #endregion

        //check if too far down
        if (transform.position.y < -5f)
        {
            Die();
        }

        Debug.Log(hp + " hp");

        #region BLEEDING
        if (bleeding)
        {
            currentBloodTime -= Time.deltaTime;
            if (currentBloodTime < 0f)
            {
                stopBlood();
            }
        }
        #endregion

        #region HEARTS
       if (hp < 1){
            hearts[0].SetActive(false);
        }
		else
		{
            hearts[0].SetActive(true);
        }
       if (hp < 2){
            hearts[1].SetActive(false);
        }
        else
        {
            hearts[1].SetActive(true);
        }
        if (hp < 3)
		{
            hearts[2].SetActive(false);
        }
        else
        {
            hearts[2].SetActive(true);
        }

        #endregion

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
        if (grounded() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            
        }
        else if (gripRight() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(-jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            
        }
        else if (gripLeft() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
        }
        #endregion
        #region CLAMP
        //CLAMP VELOCITY orsakar problem med walljump eftersom jumpforce > movespeed
        myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(myBody.velocity.y, -jumpForce, jumpForce * jumpClamper));
        #endregion

        //to prevent buffered jump whenever next grounded or gripped
        jumping = false;

        #region JUMP CD
        if (jumpReady == false)
        {
            jumpCDtimer -= Time.deltaTime;
            if (jumpCDtimer < 0f)
            {
                jumpReady = true;
                jumpCDtimer = origJumpCD;
            }
        }
        #endregion

    }

    #region COLLISIONS

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy collison
        if (collision.transform.tag == "Enemy")
        {
            if (vampire == true)
            {
                //destroys enemy
                Destroy(collision.gameObject);
            }

            if (vampire == false)
            {
                //self delete
                //  Destroy(gameObject);
                Die();
            }
            /*
            //push self
           myBody.AddForce(new Vector2(-jumpForce, 0f), ForceMode2D.Impulse);
            */

            
        }
        //Thorn collision
        if (collision.transform.tag == "Thorn")
        {
            Hurt();
        }

        //checkpoint collision
        if (collision.transform.tag == "Checkpoint")
        {
            Heal();
            respawn = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
    #endregion

    void Die() {

        transform.position = respawn;
        myBody.velocity = new Vector3(0f, 0f, 0f);
        Heal();
        Bleed();
        /*
        Destroy(gameObject);
        SceneManager.LoadScene("DinMammaHopparRunt");
        */
    }

    void Hurt()
    {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        hp--;
        Bleed();
    }

    void stopBlood()
    {
        currentBloodTime = bloodTime;
        bleeding = false;
        blood.startLifetime = 0f;
    }

    void Bleed()
    {

        blood.startLifetime = bleedDuration;
        bleeding = true;
    }

    void Heal()
	{
        hp = maxHp;
	}

}
