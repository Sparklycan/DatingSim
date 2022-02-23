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

    [HideInInspector]
    public int concludedSus;

    [HideInInspector]
    public int triggers;

    private float time;

    public GameObject EndGameCanvas;

    public Text susText, triggersText, timeText;

    void SusPlus()
    {
        concludedSus++;
    }
    
    void TriggerPlus()
    {
        triggers++;
    }


    void EndGame()
    {
        susText.text = "Suspoints: " + concludedSus;
        triggersText.text = "Nuggets found: " + triggers;
        timeText.text = "Time: " + time;
        
        //  ENABLA SCENEN OCH LÄGG IN I POLICE SCRIPT PLUS TRIGGERS TIHI HAHAHAHAHAHAHAHHAH
    }
    
    
    
    
    
    
    
    
}
