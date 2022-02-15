using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
   public GamesUnlocked gamesUnlocked;
   public Button[] buttons;
   public ArcadeSaving saving;

   private void Start()
   {

      buttons[4].interactable = false;
      if (gamesUnlocked.minigame1)
      {
         buttons[0].interactable = true;
      }
      else
      {
         buttons[0].interactable = false;
      }

      if (gamesUnlocked.minigame5)
      {
         buttons[4].interactable = true;
      }
      else
      {
         buttons[4].interactable = false;
      }
      
   }


   public void ToggleBoolTest()
   {
      gamesUnlocked.minigame1 = !gamesUnlocked.minigame1;
      
      Debug.Log(gamesUnlocked.minigame1);
   }
   
   
}
