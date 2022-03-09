using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeScriptBirdGame : MonoBehaviour
{
   public UnityAction timeOut = delegate {  };
    public float timeForTimer;
    public Text timerText;
    private bool checkUpdate = true;
    private float timer;
    
    private void Start()
    {
        timer = timeForTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && checkUpdate)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time left: " + timer;
        }

        else if (checkUpdate)
        {
            checkUpdate = false;
            Debug.Log("game over here");
            TimeOut();
        }
        
    }


    public void TimeOut()
    {
        timeOut();
    }
}
