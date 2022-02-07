using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayer : MonoBehaviour
{

    public float speed;

    Vector3 movement;

    Rigidbody Rb;

    float vertical, horizontal;


    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Rb.velocity = movement * speed * Time.deltaTime;
        //Debug.Log(Rb.velocity);
        
    }

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        movement = new Vector3(horizontal, 0, vertical).normalized;
        Rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }
}
