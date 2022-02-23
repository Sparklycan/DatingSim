using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthHandler : MonoBehaviour
{
    
    [Tooltip("X: Sus, Y: Lust, Z: Love")]
    public Vector3 Points;

    [Space(10)]
    public int allNuggets;
    private int takenNuggets;

    public GameObject EndGameCanvas;
    public Text SusPointsText;
    public Text NuggetsCollectedText;
    public Text TimeTakenText;

    private float timer;
    private int concludedSus;

    private Minigame _minigame;
    /*
    public void GetSusPoints()
    {
       GameObject[] AIArray = GameObject.FindGameObjectsWithTag("AI");
       foreach (GameObject G in AIArray)
       {
          concludedSus += G.GetComponent<PoliceScript>().Sus;
       }

       Points.x = concludedSus;

    }
    */
    public void SusPlus(int point)
    {
        concludedSus += point;
    }

    public void NuggetPlus()
    {
        takenNuggets++;
    }

    public void EndGame()
    {
        SusPointsText.text = "SusPoints: " + concludedSus;
        NuggetsCollectedText.text = "Nuggets found: " + takenNuggets + "/ " + allNuggets;
        TimeTakenText.text = "Time taken: " + timer;
        
        EndGameCanvas.SetActive(true);
    }
    
    
    

}
