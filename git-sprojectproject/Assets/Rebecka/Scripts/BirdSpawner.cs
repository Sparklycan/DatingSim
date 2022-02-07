using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;

    public GameObject birdGoal;
    public float minY, maxY;
    
    private float yAxis;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           SpawnBird();
        }
    }


    public void SpawnBird()
    {
        GetRandomValue();
        BirdScript bird = Instantiate(birdPrefab, transform.position + new Vector3(0, -yAxis, 0), Quaternion.identity).GetComponent<BirdScript>();
    }
    private void GetRandomValue()
    {
        yAxis = UnityEngine.Random.Range(minY, maxY);
    }
    
    
}
