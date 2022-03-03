using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	#region VARS
	#region PHYSICS VARS
	public Rigidbody2D myBody;
    private BoxCollider2D boxcollider2d;
	#endregion

	#region PARTICLE VARS
	public ParticleSystem blood;
    private float bloodTime = 0.5f;
    private float currentBloodTime;
    private bool bleeding;
    private float bleedDuration = 0.2f;
    #endregion

    #region MOVEMENT VARS
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
	#endregion

	#region RAYCAST VARS
	private float extraHeight = 0.02f;
    private float extraWidth = 0.6f;
    [SerializeField] private LayerMask groundLayerMask;
    #endregion

    #region GENERAL STATE VARS
    private int hp;
    private int maxHp = 3;
    private Vector3 respawn;
    private bool vampire = false;
	#endregion

	#region UI VARS
	public GameObject[] hearts;
    #endregion
    #endregion

    //Before start
    private void Awake()
    {
        //def for hitbox
        boxcollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update   CREATE
    void Start()
    {
        //var defs
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
        #region INPUTS //checks get axis raw and sets var hore for horizontal input or jumping
        hore = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Jump"))
        {
            jumping = true;
        }

        if (hp < 1) {
            Die();
        }
		#endregion

		#region Kill depth //if too far down on map, kill player
		//check if too far down
		if (transform.position.y < -5f)
        {
            Die();
        }
        #endregion

        #region BLEEDING  //timer for particles
        if (bleeding)
        {
            currentBloodTime -= Time.deltaTime;
            if (currentBloodTime < 0f)
            {
                stopBlood();
            }
        }
        #endregion

        #region HEARTS //sets UI number of hearts to hp
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

	#region RAYCASTS to check if grounded or gripping
	//GROUND AND GRIP CHECKS
	private bool grounded()
    {
        int rayHits = 0;
        //ray 1 LEFT
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
        
        //ray 2 RIGHT
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

        //ray 3 CENTER
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
        //ray 1 TOP
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

        //ray 2 BOTTOM
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

        //ray 3 CENTER
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

        //ray 1 TOP
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

        //ray 2 BOTTOM
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

        //ray 3 CENTER
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

    //MOVEMENT HERE
    private void FixedUpdate()
    {
        #region STRAFING
        //STRAFING
        if (hore > 0)
        {
            myBody.AddForce(new Vector2(moveSpeed, 0f), ForceMode2D.Impulse);
            strafe();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            myBody.AddForce(new Vector2(-moveSpeed, 0f), ForceMode2D.Impulse);
            strafe();
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
            jump();
            
        }
        else if (gripRight() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(-jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            jump();
            
        }
        else if (gripLeft() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            jump();
        }
        #endregion
        #region CLAMP
        //Limits velocity to be within given range
        myBody.velocity = new Vector2(Mathf.Clamp(myBody.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(myBody.velocity.y, -jumpForce, jumpForce * jumpClamper));
        #endregion
        #region JUMP CD
        //to prevent mega jumps
        if (jumpReady == false)
        {
            jumpCDtimer -= Time.deltaTime;
            if (jumpCDtimer < 0f)
            {
                jumpReady = true;
                jumpCDtimer = origJumpCD;
            }
        }

        //to prevent buffered jump whenever next grounded or gripped
        jumping = false;
        #endregion

    }

    //all collisions; Enemy, Thorn, Checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy collison. Kills either enemy or player depending on vampire var
        if (collision.transform.tag == "Enemy")
        {
            if (vampire == true)
            {
                //destroys enemy
                Destroy(collision.gameObject);
            }

            if (vampire == false)
            {
                //kills player
                Die();
            }
        }
        //Thorn collision. Hurts player
        if (collision.transform.tag == "Thorn")
        {
            Hurt();
        }

        //checkpoint collision. also heals player
        if (collision.transform.tag == "Checkpoint")
        {
            Heal();
            respawn = collision.transform.position;
            Destroy(collision.gameObject);
        }

        //collides with end zone to end game
        if (collision.transform.tag == "PlatformerGoal")
		{
            Win();
		}
    }

    //sets player to last checkpoint and heals them
    void Die() {

        transform.position = respawn;
        myBody.velocity = new Vector3(0f, 0f, 0f);
        Heal();
        Bleed();
    }

    //deals damage to player and starts Bleed
    void Hurt()
    {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        hp--;
        Bleed();
    }

    //stops particles
    void stopBlood()
    {
        currentBloodTime = bloodTime;
        bleeding = false;
        blood.startLifetime = 0f;
    }

    //starts particles
    void Bleed()
    {
        blood.startLifetime = bleedDuration;
        bleeding = true;
    }

    //heals player to max hp
    void Heal()
	{
        hp = maxHp;
	}

    //not used in mechanics, only for sound?
    void jump()
	{
        //jag vet inte om man ljuderlägger hopp, men here you go bro
	}

    //not used in mechanics, only for sound?
	void strafe()
	{

	}

    //called to end game
    void Win()
	{

	}

}
