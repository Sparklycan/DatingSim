using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]public Vector3 direction;
    public float speed = 5;
    private Rigidbody2D rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = cursorPos - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
