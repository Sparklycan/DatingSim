using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    
    public float minSpeed, maxSpeed;
    [HideInInspector]public bool direction;
    public Vector3 goalDirection = new Vector3(1, 0,0);
    
    private float speed;
        //float shittimer

        private void Start()
        {
            GetRandomValue();
            if (direction)
            {
                goalDirection *= -1;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,  transform.position + goalDirection, speed * Time.deltaTime);
        }
    
        
        //void onBecameInvisible destroy
        //void birdpoop
        //void setrandomSpeed

        
        private void GetRandomValue()
        {
            speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
}
