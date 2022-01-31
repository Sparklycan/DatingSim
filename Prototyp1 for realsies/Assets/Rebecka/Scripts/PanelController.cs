using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject minigameOverPanel;
    
    
    
    public int love, lust, sus;
    public Minigame mg;

    //call this from other scripts to signal that you want to turn on the gameover panel
    public void GameDone(int lo, int lu, int s)
    {
        love = lo;
        lust = lu;
        sus = s;
        minigameOverPanel.SetActive(true);
    }

    //call this through a button or some such to end the minigame completely
    public void FinishGame()
    {
        mg.EndGame(love, lust, sus);
    }
}
