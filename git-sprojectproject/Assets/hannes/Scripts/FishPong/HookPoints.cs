using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoints : MonoBehaviour
{
    [Tooltip("X = Sus, Y = Lust, Z = Love")]
    public Vector3 Points;

    public

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Ball")
        {
            if(collision.GetComponent<BallValue>() != null)
            {
                Points += collision.GetComponent<BallValue>().Value;
            }
            Destroy(collision.gameObject);
        }
    }
}
