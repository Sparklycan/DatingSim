using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthPlayer : MonoBehaviour
{
    [Tooltip("Speed of player")]
    public float speed;
    
    [Tooltip("Multiplied speed of original speed when sprinting")]
    public float sprintMultiplier;
    
    [Tooltip("Duration calculated in time (seconds)")]
    public float sprintDuration, coolDownDuration, decelerationTime;

    private float timeElapsedX, timeElapsedZ;
    
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

    void Update()
    {

        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        movement = new Vector3(horizontal, 0, vertical).normalized;
        
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
    // IT WORKS AHAHAHAHAHAHAHAHAHHAHAHHAAH HELL YEAH DUMBASS HORIZONTAL AND VERTICAL AAAAAAA
    private void FixedUpdate()
    {




        if (Input.GetAxis("Vertical") == 0f && Rb.velocity.z != 0)
        {
//            Debug.Log("SLOW DOWN Z" );
            if (timeElapsedZ < decelerationTime)
            {
                Rb.velocity = Vector3.Lerp(Rb.velocity, new Vector3(Rb.velocity.x, Rb.velocity.y, 0f), timeElapsedZ / decelerationTime);
                timeElapsedZ += Time.deltaTime;
            }
        } 
        else
        {
            timeElapsedZ = 0;
        }
        
        if ((Input.GetAxis("Horizontal") == 0f && Rb.velocity.x != 0f))
        {
//            Debug.Log("SLOW DOWN X");
            if (timeElapsedX < decelerationTime)
            {
                Rb.velocity = Vector3.Lerp(Rb.velocity, new Vector3(0f, Rb.velocity.y, Rb.velocity.z), timeElapsedX / decelerationTime);
                timeElapsedX += Time.deltaTime;
            }
        }
        else
        {
            timeElapsedX = 0;
        }
        


        
        
        
        
        if (sprinting)
        {
            Rb.AddForce(movement * sprintSpeed, ForceMode.Impulse);
           Rb.velocity = Vector3.ClampMagnitude(Rb.velocity, sprintSpeed);
        }
        else
        {
            Rb.AddForce(movement * speed, ForceMode.Impulse);
            Rb.velocity = Vector3.ClampMagnitude(Rb.velocity, speed);
        }

    }
    



    void setStamina(float stamina)
    {
        slider.value = stamina;
    }
    
    
    
}
