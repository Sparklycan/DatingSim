using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdgameManager : MonoBehaviour
{
   public UnityAction<int, int, int> sendPoints = delegate{  };
   
   public TimeScriptBirdGame timer;
   public PlayerScript player;
   


   private void Start()
   {
      timer.timeOut += GameOverTime;
      player.healthOut += GameOverHealth;
   }
   
   private void GameOverHealth(int romance, int lust, int sus)
   {
      sendPoints(romance, lust, sus);
      Destroy(timer.gameObject);
      Destroy(player.gameObject);
   }

   private void GameOverTime()
   {
      sendPoints(player.romance, player.lust, player.sus);
      Destroy(player.gameObject);
      Destroy(timer.gameObject);
   }
   
}
