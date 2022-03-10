using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnlockThisMinigame : MonoBehaviour
{
    
    public GamesUnlocked gamesUnlocked;
    
   

    public void UnlockAll()
    {
        
        
            gamesUnlocked.allMinigamesUnlocked = true;
            gamesUnlocked.onSave();
            
    }
    
}
