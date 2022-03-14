using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public UnityAction<int, int, int> healthOut = delegate{  };
    
    public GameObject projectile;

    public int healthPoints;
    public int romanceGain = 1, lustGain = 1, susGain = 1;
    [HideInInspector]public int romance, lust, sus;

    public Text healthText;
    
    public float speed;
    public float jumpForce;

    public string groundTag;
    private Rigidbody2D rb;

    private bool isGrounded;
    private int health;
    

   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       isGrounded = true;
       health = healthPoints;
       healthText.text = "Health: " + health;
   }

   //update: movement och fire (vilken knapp) 
   private void Update()
   {

       if (health <= 0)
       {
           healthOut(romance, lust, sus);
       }
       if (Input.GetButtonDown("Fire1"))
       {
           Instantiate(projectile, transform.position, Quaternion.identity);

       }

       if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
       {
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           isGrounded = false;
       }

       Move();
   }

   private void Move()
   {
       float x = Input.GetAxisRaw("Horizontal"); 
       float step = x * speed; 
       rb.velocity = new Vector2(step, rb.velocity.y); 
   }
   

   private void OnCollisionEnter2D(Collision2D other)
   {
       
       if (other.gameObject.CompareTag(groundTag))
       {
           
           if (!isGrounded)
           {
              
               isGrounded = true;
           }
       }
       
       if (other.gameObject.CompareTag("Romance"))
       {
           romance += romanceGain;
           Destroy(other.gameObject);
       }

       if (other.gameObject.CompareTag("Lust"))
       {
           lust += lustGain;
           Destroy(other.gameObject);
       }

       if (other.gameObject.CompareTag("EnemyProjectile"))
       {
           sus += susGain;
           health--;
           healthText.text = "Health: " + health;
           Destroy(other.gameObject);
       }

   }
   
   
}
