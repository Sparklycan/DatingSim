using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHighscoreManager : MonoBehaviour
{
    public HighScoreSaver scoreSaver;
    public HighscoreContainer scoreContainer;
    public Text[] scoreTexts;
    
    

    public void FishpongScoreSetter(int a, int b, int c)
    {
        Vector3 temporary = new Vector3(a, b ,c);
        float currentscore = temporary.x + temporary.y - temporary.z;

        float score1 = scoreContainer.fishpongScore1.x + scoreContainer.fishpongScore1.y -
                       scoreContainer.fishpongScore1.z;
        
        float score2 = scoreContainer.fishpongScore2.x + scoreContainer.fishpongScore2.y -
                       scoreContainer.fishpongScore2.z;
        
        float score3 = scoreContainer.fishpongScore3.x + scoreContainer.fishpongScore3.y -
                       scoreContainer.fishpongScore3.z;


        if (currentscore >= score1)
        {
            scoreContainer.fishpongScore3 = scoreContainer.fishpongScore2;
            scoreContainer.fishpongScore2 = scoreContainer.fishpongScore1;
            scoreContainer.fishpongScore1 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score2)
        {
            scoreContainer.fishpongScore3 = scoreContainer.fishpongScore2;
            scoreContainer.fishpongScore2 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score3)
        {
            scoreContainer.fishpongScore3 = temporary;
            scoreSaver.SaveGame();
        }
        
        PrintFishpongScores();
        
    }

    public void PlatformScoreSetter(float time)
    {
        if (time >= scoreContainer.platformerTimeScores.x)
        {
            scoreContainer.platformerTimeScores.z = scoreContainer.platformerTimeScores.y;
            scoreContainer.platformerTimeScores.y = scoreContainer.platformerTimeScores.x;
            scoreContainer.platformerTimeScores.x = time;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScores.y)
        {
            scoreContainer.platformerTimeScores.z = scoreContainer.platformerTimeScores.y;
            scoreContainer.platformerTimeScores.y = time;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScores.z)
        {
            scoreContainer.platformerTimeScores.z = time;
            scoreSaver.SaveGame();
        }
        PrintPlatformScores();
    }

    public void SweeperScoreSetter(int a, int b, int c)
    {
        Vector3 temporary = new Vector3(a, b ,c);
        float currentscore = temporary.x + temporary.y - temporary.z;

        float score1 = scoreContainer.sweeperScore1.x + scoreContainer.sweeperScore1.y -
                       scoreContainer.sweeperScore1.z;
        
        float score2 = scoreContainer.sweeperScore2.x + scoreContainer.sweeperScore2.y -
                       scoreContainer.sweeperScore2.z;
        
        float score3 = scoreContainer.sweeperscore3.x + scoreContainer.sweeperscore3.y -
                       scoreContainer.sweeperscore3.z;


        if (currentscore >= score1)
        {
            scoreContainer.sweeperscore3 = scoreContainer.sweeperScore2;
            scoreContainer.sweeperScore2 = scoreContainer.sweeperScore1;
            scoreContainer.sweeperScore1 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score2)
        {
            scoreContainer.sweeperscore3 = scoreContainer.sweeperScore2;
            scoreContainer.sweeperScore2 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score3)
        {
            scoreContainer.sweeperscore3 = temporary;
            scoreSaver.SaveGame();
        }
        
        PrintSweeperScores();
    }

    public void PrintFishpongScores()
    {
        scoreTexts[0].text = scoreContainer.fishpongScore1.x.ToString() +" "+ scoreContainer.fishpongScore1.y.ToString() +" "+
                             scoreContainer.fishpongScore1.z.ToString();
        scoreTexts[1].text = scoreContainer.fishpongScore2.x.ToString() +" "+ scoreContainer.fishpongScore2.y.ToString() +" "+
                             scoreContainer.fishpongScore2.z.ToString();
        scoreTexts[2].text = scoreContainer.fishpongScore3.x.ToString() +" "+ scoreContainer.fishpongScore3.y.ToString() +" "+
                             scoreContainer.fishpongScore3.z.ToString();
        Debug.Log(scoreContainer.fishpongScore1);
        Debug.Log(scoreContainer.fishpongScore2);
        Debug.Log(scoreContainer.fishpongScore3);
    }

    public void PrintPlatformScores()
    {
       Debug.Log(scoreContainer.platformerTimeScores.x); 
       Debug.Log(scoreContainer.platformerTimeScores.y);
       Debug.Log(scoreContainer.platformerTimeScores.z); 
    }

    public void PrintSweeperScores()
    {
        Debug.Log(scoreContainer.sweeperScore1);
        Debug.Log(scoreContainer.sweeperScore2);
        Debug.Log(scoreContainer.sweeperscore3);
    }
    
}
