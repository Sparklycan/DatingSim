using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthPlayer : MonoBehaviour
{

    public float speed, sprintMultiplier;

    public float sprintDuration, coolDownDuration, acceleration;

    bool sprinting = false, coolDown;

    Vector3 movement;

    Rigidbody Rb;

    float vertical, horizontal, sprintSpeed;
        
    [HideInInspector]
    public float timer1, timer2;

    stealthCamera StealthCam;


    public Slider slider;


    void Start()
    {
        StealthCam = Camera.main.GetComponent<stealthCamera>();
        Rb = GetComponent<Rigidbody>();
        sprintSpeed = speed * sprintMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift) && !sprinting && !coolDown)
        {
            sprinting = true;
        }
        if(sprinting)
        {
            slider.gameObject.SetActive(true);
            //Debug.Log("sprint");
            timer1 += Time.deltaTime;
            slider.maxValue = sprintDuration;
            setStamina(timer1);
            if (timer1 >= sprintDuration)
            {
             //   Debug.Log("done");
                timer2 = coolDownDuration;
                coolDown = true;
                sprinting = false;
                timer1 = 0;
            }
        }
        if (coolDown)
        {
           // Debug.Log("cooldown");
            
            slider.maxValue = coolDownDuration;
            setStamina(timer2);
            timer2 -= Time.deltaTime;
            if(timer2 <= 0)
            {
                coolDown = false;
                slider.gameObject.SetActive(false);
            }
        }





        
    }
    // FIX SOME "KEY OFF FINGER" DELAY ON MOVEMENT.

    private void FixedUpdate()
    {


        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector3(horizontal, 0, vertical).normalized;

        if (sprinting)
        {
            Rb.velocity = (movement * sprintSpeed);
            
        }
        else
            Rb.velocity = (movement * speed);
        

       // Debug.Log(Rb.velocity);
    }




    void setStamina(float stamina)
    {
        slider.value = stamina;
    }
    
    
    
}
