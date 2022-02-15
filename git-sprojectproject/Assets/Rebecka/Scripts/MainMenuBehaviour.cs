using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{

    public ArcadeSaving saveBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        saveBehaviour.gamesUnlocked.minigame1 = false;
        /*saveBehaviour.gamesUnlocked.minigame2 = false;
        saveBehaviour.gamesUnlocked.minigame3 = false;
        saveBehaviour.gamesUnlocked.minigame4 = false;*/
        saveBehaviour.LoadGame();
    }

    
}
