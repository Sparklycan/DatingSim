using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BirdSpawner : MonoBehaviour
{
    public float spawnTimer = 2f;
    public bool direction;
    
    public GameObject birdPrefab;
    public float minY, maxY;
    
    private float yAxis;
    private float timer;

    private void Start()
    {
        timer = spawnTimer;
        SpawnBird();
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = spawnTimer;
            SpawnBird();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
           SpawnBird();
        }
    }


    public void SpawnBird()
    {
        GetRandomValue();
        BirdScript bird = Instantiate(birdPrefab, transform.position + new Vector3(0, -yAxis, 0), Quaternion.identity).GetComponent<BirdScript>();
        bird.direction = direction;
    }
    private void GetRandomValue()
    {
        yAxis = UnityEngine.Random.Range(minY, maxY);
    }
    
    
}
