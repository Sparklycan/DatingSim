using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthPointSaver : MonoBehaviour
{
    
    [Tooltip("X: Sus, Y: Lust, Z: Love")]
    public Vector3 Points;

    private List<GameObject> AI = new List<GameObject>();

     int concludedSus;

     public int maxTriggers;
     int triggers;

    private float time;

    public GameObject EndGameCanvas;

    public Text susText, triggersText, timeText;

    public void SusPlus(int points)
    {
        concludedSus += points;
    }
    
    public void TriggerPlus()
    {
        triggers++;
    }

    public void EndGame()
    {
        EndGameCanvas.SetActive(true);
        susText.text = "SusPoints: " + concludedSus;
        triggersText.text = "Storynuggets found: " + triggers + " / " + maxTriggers;
        timeText.text = "Time: " + time;

    }
    
    
    
    
    
    
    
    
    
}
