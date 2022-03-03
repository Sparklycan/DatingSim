using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject projectile;

    public float speed;
    public float jumpForce;

    public string groundTag;
    
    private Rigidbody2D rb;

    private bool isGrounded;
   //float firerate
   //private float timer
   //private floats for love, lust, sus

   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       isGrounded = true;
   }

   //update: movement och fire (vilken knapp) 
   private void Update()
   {
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
   }
   //on trigger enter
   //projectiles: damage
   //points: value1 or value2
   
   
    //void fire
}
