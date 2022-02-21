using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
   public GamesUnlocked gamesUnlocked;
   public Button[] buttons;
   public Image[] gamePreviews;
   public ArcadeSaving saving;
   public GameObject playallgamesText;
   
   private void Start()
   {
      
      if (gamesUnlocked.minigame1)
      {
         buttons[0].interactable = true;
         gamePreviews[0].color = Color.white;
      }
      else
      {
         buttons[0].interactable = false;
         gamePreviews[0].color = Color.gray;
      }

      if (gamesUnlocked.minigame2)
      {
         buttons[1].interactable = true;
      }
      else
      {
         buttons[1].interactable = false;
      }
      
      if (gamesUnlocked.minigame3)
      {
         buttons[2].interactable = true;
      }
      else
      {
         buttons[2].interactable = false;
      }
      
      if (gamesUnlocked.minigame4)
      {
         buttons[3].interactable = true;
      }
      else
      {
         buttons[3].interactable = false;
      }
      
      if (gamesUnlocked.minigame5)
      {
         buttons[4].interactable = true;
         playallgamesText.SetActive(false);
      }
      else
      {
         buttons[4].interactable = false;
         playallgamesText.SetActive(true);
      }
      
   }


   public void ToggleBoolTest()
   {
      gamesUnlocked.minigame1 = !gamesUnlocked.minigame1;
      
      Debug.Log(gamesUnlocked.minigame1);
   }
   
   
}
