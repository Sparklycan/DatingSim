using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockThisMinigame : MonoBehaviour
{

    public bool game1, game2, game3, game4, allGames;
    public GamesUnlocked gamesUnlocked;

    public ArcadeSaving save;
    // Start is called before the first frame update
    void Start()
    {
        if (game1)
        {
            gamesUnlocked.minigame1 = true;
        }

        if (game2)
        {
            gamesUnlocked.minigame2 = true;
        }

        if (game3)
        {
            gamesUnlocked.minigame3 = true;
        }

        if (game4)
        {
            gamesUnlocked.minigame4 = true;
        }
        
        
        save.SaveGame();
    }

    public void UnlockAll()
    {
        if (allGames)
        {
            gamesUnlocked.allMinigamesUnlocked = true;
            save.SaveGame();
        }
    }
    
}
