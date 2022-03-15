using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEnd : MonoBehaviour
{
    private Minigame _minigame;
    public Controller playerController;
    
    
    private void Start()
    {
        _minigame = GetComponent<Minigame>();
    }
    
    public void ReallyEnd()
    {
        // the points (where should score go? sus?)
        _minigame.EndGame(0,0, playerController.score);
    }
}
