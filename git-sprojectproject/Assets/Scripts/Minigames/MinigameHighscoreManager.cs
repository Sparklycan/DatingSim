using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHighscoreManager : MonoBehaviour
{
    public HighScoreSaver scoreSaver;
    public HighscoreContainer scoreContainer;
    public Text[] scoreTexts;
    
    

    public void FishpongScoreSetter(float a, float b, float c)
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

    public void SweeperScoreSetter(float a, float b, float c)
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
        scoreTexts[0].text ="Romance: " +  scoreContainer.fishpongScore1.x.ToString() +", Lust: "+ scoreContainer.fishpongScore1.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore1.z.ToString();
        scoreTexts[1].text ="Romance: " +  scoreContainer.fishpongScore2.x.ToString() +", Lust: "+ scoreContainer.fishpongScore2.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore2.z.ToString();
        scoreTexts[2].text ="Romance: " +  scoreContainer.fishpongScore3.x.ToString() +", Lust: "+ scoreContainer.fishpongScore3.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore3.z.ToString();
       
    }

    public void PrintPlatformScores()
    {
        scoreTexts[0].text = scoreContainer.platformerTimeScores.x.ToString();
        scoreTexts[1].text = scoreContainer.platformerTimeScores.y.ToString();
        scoreTexts[2].text = scoreContainer.platformerTimeScores.z.ToString();
    }

    public void PrintSweeperScores()
    {
        scoreTexts[0].text ="Romance: " + scoreContainer.sweeperScore1.x.ToString() +", Lust: "+ scoreContainer.sweeperScore1.y.ToString() +", Suspicion: "+
                             scoreContainer.sweeperScore1.z.ToString();
        scoreTexts[1].text ="Romance: " +  scoreContainer.sweeperScore2.x.ToString() +", Lust: "+ scoreContainer.sweeperScore2.y.ToString() +", Suspicion: "+
                            scoreContainer.sweeperScore2.z.ToString();
        scoreTexts[2].text ="Romance: " +  scoreContainer.sweeperscore3.x.ToString() +", Lust: "+ scoreContainer.sweeperscore3.y.ToString() +", Suspicion: "+
                            scoreContainer.sweeperscore3.z.ToString();
    }
    
}
