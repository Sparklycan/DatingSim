using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnlockMinigameObject")]
public class UnlockThisMinigame : MonoBehaviour
{

    public bool allGames;
    public GamesUnlocked gamesUnlocked;
    
   

    public void UnlockAll()
    {
        if (allGames)
        {
            gamesUnlocked.allMinigamesUnlocked = true;
            gamesUnlocked.onSave();
        }
        else
        {
            Debug.Log("Cannot unlock without allGames being set to true.");
        }
    }
    
}
