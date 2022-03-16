using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using  Fungus;


//[CommandInfo("End Credits", "Play End Credits", "Plays the End credits")]
public class CreditsScroll : MonoBehaviour
{
    public float speed, length;
    float start;

    private Vector2 endPoint;

    public ContinueScript ContinueScript;

    private bool cintonue;
    
    float timer, startTimer, startTime = 2;
    
    private void Start()
    {
        start = transform.position.y;
        endPoint = new Vector2(transform.position.x, transform.position.y + length);
    }


    



    private void Update()
    {
        startTimer += Time.deltaTime;
        if (startTimer > startTime)
        {
            if (transform.position.y < endPoint.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
           //     cintonue = true;
            }
        }

/*
        if (cintonue)
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
           //     ContinueScript.Continue();
            }
        }
        */
        
        
    }

        
        
}
