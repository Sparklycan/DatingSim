#region PLAYER SCRIPT (Controller)
using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	#region VARS
	#region PHYSICS VARS
	public Rigidbody2D myBody; //is set in Start(), but is public so can be accessed by camera
    private BoxCollider2D boxcollider2d;
	#endregion

	#region PARTICLE VARS
	public ParticleSystem blood;
    private float bloodTime = 0.5f;
    private float currentBloodTime;
    private bool bleeding = false;
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
    private bool moveLock = false;
    private float maxMoveLockTime = 1f;
    private float moveLockTime;
    SpriteRenderer mySprite;
	#endregion

	#region RAYCAST VARS
	private float extraHeight = 0.02f;
    private float extraWidth = 0.3f;
    [SerializeField] private LayerMask groundLayerMask;
    #endregion

    #region GENERAL STATE VARS
    private int hp;
    private int maxHp = 3;
    private Vector3 respawn;
    public bool vampire = false;
    public float killDepth;
    public int score = 0;
    private float timer = 0f;
    public int ShortenedTimer;
    private bool won = false;
	#endregion

    #region UI VARS
	public GameObject[] hearts;
    public GameObject EndCanvas;
    public Text scoreText;
    public Text timerText;
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
        hp = maxHp;
        moveLockTime = maxMoveLockTime;
        mySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame  STEP
    void Update()
    {
        #region INPUTS //checks get axis raw and sets var hore for horizontal input or jumping
        if (moveLock == false)
        {
            hore = Input.GetAxisRaw("Horizontal");
            if (Input.GetButton("Jump"))
            {
                jumping = true;
            }
        }
        #endregion

        #region DEATH
        if (hp < 1) {
            Die();
        }
		#endregion

		#region Kill depth //if too far down on map, kill player
		//check if too far down
        /*
		if (transform.position.y < killDepth)
        {
            Die();
        }
        */
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

        #region moveLock timer  //Upon death, player loses control of movment for set time. This is the timer
        if (moveLock == true)
        {
            moveLockTime -= Time.deltaTime;
            if (moveLockTime <= 0f)
            {
                moveLock = false;
                moveLockTime = maxMoveLockTime;
            }
        }

        #endregion

        #region TIMER
        //adds part of a second based on last update frame to timer if not won yet
        if (won == false)
        {
            timer += Time.deltaTime;
        }
        #endregion

    }

    #region RAYCASTS to check if grounded or gripping
    //GROUND AND GRIP CHECKS

    #region GROUND CHECK
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
	#endregion

	#region RIGHT GRIP CHECK
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
        
        //debug
      //  Debug.DrawRay(boxcollider2d.bounds.center, Vector2.right * (boxcollider2d.bounds.extents.x + extraWidth), rayColor3);

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

	#region LEFT GRIP CHECK
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

    #endregion

    //MOVEMENT HERE
    private void FixedUpdate()
    {
        #region STRAFING
        //STRAFING
        if (moveLock == false)
        {
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
        }
		#endregion

		#region JUMPING AND WALL JUMPING
		//JUMPING AND WALLJUMPING
		// input.GetButtonDown("Jump")
		#region NORMAL JUMP
		if (grounded() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            jump();
            
        }
		#endregion

		#region WALLJUMP FROM RIGHT TO LEFT
		else if (gripRight() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(-jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            walljumpFromRight(); //for sprite flip
            walljump(); //for sound
            
            
        }
		#endregion

		#region WALLJUMP FROM LEFT TO RIGHT
		else if (gripLeft() && jumping && jumpReady)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(jumpForce * wallJumpXmod, jumpForce * wallJumpYmod), ForceMode2D.Impulse);
            jumping = false;
            jumpReady = false;
            walljumpFromLeft();  //for sprite flip
            walljump(); //for sound
        }
        #endregion

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
		#region ENEMY COLLISION
		//Enemy collison. Kills either enemy or player depending on vampire var
		if (collision.transform.tag == "Enemy")
        {
            if (vampire == true)
            {
                //destroys enemy
                Destroy(collision.gameObject);
                Bite();
            }

            if (vampire == false)
            {
                //kills player
                Die();
            }
        }
		#endregion

		#region THORN/SPIKE COLLISION
		//Thorn collision. Hurts player
		if (collision.transform.tag == "Thorn")
        {
            Hurt();
        }
		#endregion

		#region CHECKPOINT COLLISION
		//checkpoint collision. also heals player
		if (collision.transform.tag == "Checkpoint")
        {
            Heal();
            respawn = collision.transform.position;
            Destroy(collision.gameObject);
        }
		#endregion

		#region GOAL COLLISION
		//collides with end zone to end game
		if (collision.transform.tag == "PlatformerGoal")
		{
            Win();
		}
        #endregion

        #region KILL ZONE COLLISION
        if (collision.transform.tag == "KillZone")
        {
            Die();
        }
        #endregion
        


    }

    #region CUSTOM FUNCTIONS  //sound goes here
    //sets player to last checkpoint and heals them
    void Die() {

        transform.position = respawn;
        myBody.velocity = new Vector3(0f, 0f, 0f);
        Heal();
        Bleed();
        moveLock = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Platformer/Health", transform.position);
    }

    //deals damage to player and starts Bleed
    void Hurt()
    {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        hp--;
        Bleed();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Platformer/Damage", transform.position);
    }

    //stops particles
    void stopBlood()
    {
        
      //  blood.startLifetime = 0f;
        bleeding = false;
        blood.gameObject.SetActive(false);
        currentBloodTime = bloodTime;

    }

    //starts particles
    void Bleed()
    {
        //blood.startLifetime = bleedDuration;
        blood.gameObject.SetActive(true);
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Platformer/Jump", transform.position);
	}

    //only used for sound
    void walljump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Platformer/Jump", transform.position);
    }

    //used for sprite flip, not sound
    void walljumpFromLeft()
    {
        //jag vet inte om man ljuderlägger hopp, men here you go bro

        //flips the sprite
         mySprite.flipX = true;
        
    }
    
    //used for sprite flip, not sound
    void walljumpFromRight()
    {
        //jag vet inte om man ljuderlägger hopp, men here you go bro

        //flips the sprite
         mySprite.flipX = false;

    }

    //not used in mechanics, only for sound?
    void strafe()
	{
        #region Sprite flip
        //flips the sprite
        if (hore == 1)
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
        #endregion
    }

    //called to end game
    void Win()
	{
        won = true;
        ShortenedTimer = (int)timer;
        EndCanvas.SetActive(true);
        
        scoreText.text = "Score: " + score;

        timerText.text = "Time: " + ShortenedTimer + " seconds!";
        
    }

    //called when killing enemy. Sound goes here
    void Bite()
    {
        score++;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Platformer/Bite", transform.position);
    }



    #endregion

}
#endregion