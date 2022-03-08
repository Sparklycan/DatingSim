using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]public Vector2 direction;
    public float speed = 5;
    public string groundTag = "Ground";
    
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            Destroy(gameObject);
        }
    }
}
