using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{

    public ArcadeSaving saveBehaviour;

    public Button arcadeButton;
    // Start is called before the first frame update
    void Start()
    {
        saveBehaviour.LoadGame();
        if (saveBehaviour.gamesUnlocked.allMinigamesUnlocked)
        {
            arcadeButton.interactable = true;
        }
        else
        {
            arcadeButton.interactable = false;
        }
    }

    
}
