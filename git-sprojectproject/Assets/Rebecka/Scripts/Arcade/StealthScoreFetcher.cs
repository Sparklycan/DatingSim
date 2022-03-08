using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthScoreFetcher : MonoBehaviour
{
    public StealthPointSaver stealthPointSaver;
    public MinigameHighscoreManager scoremanager;

    private void OnEnable()
    {
        stealthPointSaver.sendPoints += SendAlongScore;
    }
    private void OnDisable()
    {
        stealthPointSaver.sendPoints -= SendAlongScore;
    }

    public void SendAlongScore(int sus, int nugget)
    {
        scoremanager.StealthScoreSetter(sus, nugget);
    }
}
