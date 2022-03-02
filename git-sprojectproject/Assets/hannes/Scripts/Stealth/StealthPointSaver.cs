using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StealthPointSaver : MonoBehaviour
{
    public UnityAction<int, int> sendPoints = delegate { };
    
    
    /*[Tooltip("X: Sus, Y: Lust, Z: Love")]
    public Vector3 Points;*/


    private List<GameObject> AI = new List<GameObject>();

     

    public int maxTriggers;
    int triggers;
    
    int concludedSus;
    
    private float time;

    public GameObject EndGameCanvas;

    public Text susText, triggersText, timeText;

    private Minigame _minigame;
    
    
    
    
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

        sendPoints(concludedSus,triggers);
        
        susText.text = "SusPoints: " + concludedSus;
        triggersText.text = "Storynuggets: " + triggers + " / " + maxTriggers;
        timeText.text = "Time: " + time;
        

    }

    public void ReallyEndGame()
    {
        _minigame = GetComponent<Minigame>();
        _minigame.EndGame(0,0,concludedSus);
    }
    
    
    
    
    
    
    
    
    
}
