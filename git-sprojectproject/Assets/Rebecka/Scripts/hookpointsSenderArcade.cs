using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class hookpointsSenderArcade : MonoBehaviour
{
    public HookPoints hp;
    public MinigameHighscoreManager scoreManager;
    public Flowchart flowchart; 

    private void OnEnable()
    {
        scoreManager.FishpongScoreSetter(hp.Points.z,hp.Points.y,hp.Points.x);
        flowchart.SendFungusMessage("End Music");
    }
}
