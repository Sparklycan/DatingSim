using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownScript : MonoBehaviour
{
    public bool enemyPoop;
    public float dropSpeed;
    private Vector3 direction = new Vector3(0, -1, 0);
    public string playerTag = "Player";
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,  transform.position + direction, dropSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) == false)
        {
            Destroy(gameObject);
        }
            
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag) == false)
        {
            Destroy(gameObject);
        }
        
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
