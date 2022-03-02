using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHighscoreManager : MonoBehaviour
{
    public HighScoreSaver scoreSaver;
    public HighscoreContainer scoreContainer;
    public Text[] scoreTextsFishpong;
    public Text[] scoreTextsPlatformer;
    public Text[] scoreTextsStealthSuspicion;
    public Text[] scoreTextsStealthNuggets;
    public Text[] scoreTextsFlex;
    public Text[] scoreTextsSweeper;

    private void Start()
    {
       scoreSaver.LoadGame();
    }

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

        if (scoreTextsFishpong.Length > 0)
        {
            PrintFishpongScores();
        }
        
        
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

        if (scoreTextsPlatformer.Length > 0)
        {
            PrintPlatformScores();
        }
        
    }


    public void StealthScoreSetter(int sus, int nuggets)
    {
        if (sus < scoreContainer.stealthSuspicion.x)
        {
            scoreContainer.stealthSuspicion.z = scoreContainer.stealthSuspicion.y;
            scoreContainer.stealthSuspicion.y = scoreContainer.stealthSuspicion.x;
            scoreContainer.stealthSuspicion.x = sus;

            scoreContainer.stealthNuggetScores.z = scoreContainer.stealthNuggetScores.y;
            scoreContainer.stealthNuggetScores.y = scoreContainer.stealthNuggetScores.x;
            scoreContainer.stealthNuggetScores.x = nuggets;
            scoreSaver.SaveGame();
        }
        else if (sus < scoreContainer.stealthSuspicion.y)
        {
            scoreContainer.stealthSuspicion.z = scoreContainer.stealthSuspicion.y;
            scoreContainer.stealthSuspicion.y = sus;
            
            scoreContainer.stealthNuggetScores.z = scoreContainer.stealthNuggetScores.y;
            scoreContainer.stealthNuggetScores.y = nuggets;
            scoreSaver.SaveGame();
        }
        else if (sus < scoreContainer.stealthSuspicion.z)
        {
            scoreContainer.stealthSuspicion.z = sus;
            scoreContainer.stealthNuggetScores.z = nuggets;
            scoreSaver.SaveGame();
        }

        if (scoreTextsStealthSuspicion.Length > 0)
        {
            PrintStealthScores();
        }
        
    }


    public void FlexScoreSetter()
    {
        
        //save score
    }
    
    
    public void SweeperScoreSetter(float a, float b, float c)
    {
        Vector3 temporary = new Vector3(a, b ,c);
        float currentscore = temporary.x + temporary.y - temporary.z;

        float score1 = scoreContainer.sweeperScore1.x + scoreContainer.sweeperScore1.y -
                       scoreContainer.sweeperScore1.z;
        
        float score2 = scoreContainer.sweeperScore2.x + scoreContainer.sweeperScore2.y -
                       scoreContainer.sweeperScore2.z;
        
        float score3 = scoreContainer.sweeperScore3.x + scoreContainer.sweeperScore3.y -
                       scoreContainer.sweeperScore3.z;


        if (currentscore >= score1)
        {
            scoreContainer.sweeperScore3 = scoreContainer.sweeperScore2;
            scoreContainer.sweeperScore2 = scoreContainer.sweeperScore1;
            scoreContainer.sweeperScore1 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score2)
        {
            scoreContainer.sweeperScore3 = scoreContainer.sweeperScore2;
            scoreContainer.sweeperScore2 = temporary;
            scoreSaver.SaveGame();
        }
        else if (currentscore >= score3)
        {
            scoreContainer.sweeperScore3 = temporary;
            scoreSaver.SaveGame();
        }

        if (scoreTextsSweeper.Length > 0)
        {
            PrintSweeperScores();
        }
        
    }

    public void PrintFishpongScores()
    {
        scoreTextsFishpong[0].text ="Romance: " +  scoreContainer.fishpongScore1.x.ToString() +", Lust: "+ scoreContainer.fishpongScore1.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore1.z.ToString();
        scoreTextsFishpong[1].text ="Romance: " +  scoreContainer.fishpongScore2.x.ToString() +", Lust: "+ scoreContainer.fishpongScore2.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore2.z.ToString();
        scoreTextsFishpong[2].text ="Romance: " +  scoreContainer.fishpongScore3.x.ToString() +", Lust: "+ scoreContainer.fishpongScore3.y.ToString() +", Suspicion: "+
                            scoreContainer.fishpongScore3.z.ToString();
       
    }

    public void PrintPlatformScores()
    {
        scoreTextsPlatformer[0].text = "Time: " + scoreContainer.platformerTimeScores.x.ToString();
        scoreTextsPlatformer[1].text = "Time: " + scoreContainer.platformerTimeScores.y.ToString();
        scoreTextsPlatformer[2].text = "Time: " + scoreContainer.platformerTimeScores.z.ToString();
    }

    public void PrintStealthScores()
    {
        scoreTextsStealthSuspicion[0].text = scoreContainer.stealthSuspicion.x.ToString();
        scoreTextsStealthSuspicion[1].text = scoreContainer.stealthSuspicion.y.ToString();
        scoreTextsStealthSuspicion[2].text = scoreContainer.stealthSuspicion.z.ToString();

        if (scoreTextsStealthNuggets.Length > 0)
        {
            scoreTextsStealthNuggets[0].text = scoreContainer.stealthNuggetScores.x.ToString();
            scoreTextsStealthNuggets[1].text = scoreContainer.stealthNuggetScores.y.ToString();
            scoreTextsStealthNuggets[2].text = scoreContainer.stealthNuggetScores.z.ToString();
        }
        

    }

    public void PrintFlexScores()
    {
        scoreTextsFlex[0].text = "Turns: " + scoreContainer.flexTurnScores.x.ToString();
        scoreTextsFlex[1].text = "Turns: " + scoreContainer.flexTurnScores.y.ToString();
        scoreTextsFlex[2].text = "Turns: " + scoreContainer.flexTurnScores.z.ToString();
    }

    public void PrintSweeperScores()
    {
        
        Debug.Log(Directory.GetCurrentDirectory());
        Debug.Log(scoreContainer.sweeperScore1);
        scoreTextsSweeper[0].text ="Romance: " + scoreContainer.sweeperScore1.x.ToString() +", Lust: "+ scoreContainer.sweeperScore1.y.ToString() +", Suspicion: "+
                             scoreContainer.sweeperScore1.z.ToString();
        scoreTextsSweeper[1].text ="Romance: " +  scoreContainer.sweeperScore2.x.ToString() +", Lust: "+ scoreContainer.sweeperScore2.y.ToString() +", Suspicion: "+
                            scoreContainer.sweeperScore2.z.ToString();
        scoreTextsSweeper[2].text ="Romance: " +  scoreContainer.sweeperScore3.x.ToString() +", Lust: "+ scoreContainer.sweeperScore3.y.ToString() +", Suspicion: "+
                            scoreContainer.sweeperScore3.z.ToString();
    }

    public void PrintAllScores()
    {
        PrintFishpongScores();
        PrintFlexScores();
        PrintPlatformScores();
        PrintStealthScores();
        PrintSweeperScores();
    }
    
}
