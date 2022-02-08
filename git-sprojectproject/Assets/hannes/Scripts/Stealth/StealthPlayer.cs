using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayer : MonoBehaviour
{

    public float speed, sprintMultiplier;

    public float sprintDuration, coolDownDuration, acceleration;

    bool sprinting = false, coolDown;

    Vector3 movement;

    Rigidbody Rb;

    float vertical, horizontal, sprintSpeed, timer1, timer2;

    stealthCamera StealthCam;

    

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
            Debug.Log("sprint");
            timer1 += Time.deltaTime;
            if(timer1 >= sprintDuration)
            {
                Debug.Log("done");
                coolDown = true;
                sprinting = false;
                timer1 = 0;
            }
            
        }
        if(coolDown)
        {
            Debug.Log("cooldown");
            timer2 += Time.deltaTime;
            if(timer2 >= coolDownDuration)
            {
                coolDown = false;
            }
        }
         
        


        

        
    }

    private void FixedUpdate()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector3(horizontal, 0, vertical).normalized;
        if (sprinting)
        {
            Rb.velocity = Vector3.MoveTowards(Rb.velocity, movement * sprintSpeed, Time.deltaTime * acceleration);

        }
        else
            Rb.velocity = Vector3.MoveTowards(Rb.velocity, movement * speed, Time.deltaTime * acceleration);
    }
}
