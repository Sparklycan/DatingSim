using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    
    public float minSpeed, maxSpeed;
    [HideInInspector]public bool direction;
    [HideInInspector]public Vector3 goalDirection = new Vector3(1, 0,0);
    public GameObject birdPoop;
    public float poopTimer;
    public GameObject value1, value2;
    public float dropChance;
    
    private float speed;
    private float timer;

        private void Start()
        {
            timer = poopTimer;
            GetRandomValueSpeed();
            if (direction)
            {
                goalDirection *= -1;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,  transform.position + goalDirection, speed * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = poopTimer;
                SpawnPoop();
            }
        }

        private void GetRandomValueSpeed()
        {
            speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        private float GetRandomValueDrop()
        {
            return UnityEngine.Random.Range(0, dropChance);
        }


        private void SpawnPoop()
        {
            Instantiate(birdPoop, transform.position, Quaternion.identity);
        }
        
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
}
