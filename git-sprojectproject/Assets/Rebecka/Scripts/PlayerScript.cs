using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject projectile;
   //float firerate
   //private float timer
   //private floats for love, lust, sus
   
    
    
   //update: movement och fire (vilken knapp) 
   private void Update()
   {
       if (Input.GetButtonDown("Fire1"))
       {
           Debug.Log("shoot");
           //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           Instantiate(projectile, transform.position, Quaternion.identity);

       }
   }

   //on trigger enter
   //projectiles: damage
   //points: value1 or value2
   
   
    //void fire
}
