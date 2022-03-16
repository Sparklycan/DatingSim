using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerScoreFetcher : MonoBehaviour
{
    public bool includeKills = false;
    public Controller playerScript;
   public MinigameHighscoreManager scoreManager;


   private void OnEnable()
   {
       if (includeKills)
       {
           scoreManager.PlatformScoreSetterFun(playerScript.ShortenedTimer, playerScript.score);
       }

       else
       {
           scoreManager.PlatformScoreSetterGood(playerScript.ShortenedTimer);
       }
   }
}
