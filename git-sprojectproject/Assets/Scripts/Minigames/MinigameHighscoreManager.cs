using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHighscoreManager : MonoBehaviour
{
    public HighScoreSaver scoreSaver;
    public Text[] scoreTexts;
    
    [HideInInspector]public Vector3 fishpongScore1 = new Vector3(0,0,0 ), fishpongScore2 = new Vector3(0,0,0 ), fishpongScore3 =new Vector3(0,0,0 );
    [HideInInspector]public Vector3 platformerTimeScores = new Vector3(0,0,0 );
    [HideInInspector]public Vector3 sweeperScore1 = new Vector3(0,0,0), sweeperScore2 = new Vector3(0,0,0), sweeperscore3 = new Vector3(0,0,0);

    public void FishpongScoreSetter(int a, int b, int c)
    {
        scoreSaver.fishpongScore1 = fishpongScore1;
        scoreSaver.fishpongScore2 = fishpongScore2;
        scoreSaver.fishpongScore3 = fishpongScore3;
        scoreSaver.SaveGame();
    }

    public void PlatformScoreSetter(float time)
    {
        
    }

    public void SweeperScoreSetter(int a, int b, int c)
    {
        
    }

    public void PrintFishpongScores()
    {
        
    }

    public void PrintPlatformScores()
    {
        
    }

    public void PrintSweeperScores()
    {
        
    }
    
}
