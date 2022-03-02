using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject projectile;

    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
   //float firerate
   //private float timer
   //private floats for love, lust, sus

   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
   }

   //update: movement och fire (vilken knapp) 
   private void Update()
   {
       if (Input.GetButtonDown("Fire1"))
       {
           Debug.Log("shoot");
           //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           Instantiate(projectile, transform.position, Quaternion.identity);

       }

       if (Input.GetKeyDown(KeyCode.Space))
       {
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       }

       Move();
   }

   private void Move()
   {
       float x = Input.GetAxisRaw("Horizontal"); 
       float step = x * speed; 
       rb.velocity = new Vector2(step, rb.velocity.y); 
   }

   //on trigger enter
   //projectiles: damage
   //points: value1 or value2
   
   
    //void fire
}
