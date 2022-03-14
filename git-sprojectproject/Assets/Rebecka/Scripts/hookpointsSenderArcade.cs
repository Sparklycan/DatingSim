using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookpointsSenderArcade : MonoBehaviour
{
    public HookPoints hp;
    public MinigameHighscoreManager scoreManager;

    private void OnEnable()
    {
        scoreManager.FishpongScoreSetter(hp.Points.z,hp.Points.y,hp.Points.x);
    }
}
