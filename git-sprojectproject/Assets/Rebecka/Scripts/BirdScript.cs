using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
        //speed float: private
        //speed range

        public float minSpeed, maxSpeed;
        private float speed;
       public Vector3 goalDirection = new Vector3(1, 0,0);
        
        //float shittimer

        private void Start()
        {
            GetRandomValue();
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,  transform.position - goalDirection, speed * Time.deltaTime);
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
