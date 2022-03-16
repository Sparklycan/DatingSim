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
    public Text[] scoreTextsPlatformerFun;
    public Text[] scoreTextsPlatformerGood;
    public Text[] scoreTextsPlatformerKills;
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

    public void PlatformScoreSetterFun(int time, int kills)
    {
        if (time >= scoreContainer.platformerTimeScoresFun.x)
        {
            scoreContainer.platformerTimeScoresFun.z = scoreContainer.platformerTimeScoresFun.y;
            scoreContainer.platformerTimeScoresFun.y = scoreContainer.platformerTimeScoresFun.x;
            scoreContainer.platformerTimeScoresFun.x = time;

            scoreContainer.platformerKillsScores.z = scoreContainer.platformerKillsScores.y;
            scoreContainer.platformerKillsScores.y = scoreContainer.platformerKillsScores.x;
            scoreContainer.platformerKillsScores.x = kills;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScoresFun.y)
        {
            scoreContainer.platformerTimeScoresFun.z = scoreContainer.platformerTimeScoresFun.y;
            scoreContainer.platformerTimeScoresFun.y = time;
            
            scoreContainer.platformerKillsScores.z = scoreContainer.platformerKillsScores.y;
            scoreContainer.platformerKillsScores.y = kills;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScoresFun.z)
        {
            scoreContainer.platformerTimeScoresFun.z = time;

            scoreContainer.platformerKillsScores.z = kills;
            scoreSaver.SaveGame();
        }

        if (scoreTextsPlatformerFun.Length > 0)
        {
            PrintPlatformScoresFun();
        }

        if (scoreTextsPlatformerKills.Length > 0)
        {
            PrintPlatformScoresKills();
        }
        
    }

    public void PlatformScoreSetterGood(int time)
    {
        if (time >= scoreContainer.platformerTimeScoresGood.x)
        {
            scoreContainer.platformerTimeScoresGood.z = scoreContainer.platformerTimeScoresGood.y;
            scoreContainer.platformerTimeScoresGood.y = scoreContainer.platformerTimeScoresGood.x;
            scoreContainer.platformerTimeScoresGood.x = time;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScoresGood.y)
        {
            scoreContainer.platformerTimeScoresGood.z = scoreContainer.platformerTimeScoresGood.y;
            scoreContainer.platformerTimeScoresGood.y = time;
            scoreSaver.SaveGame();
        }
        else if (time >= scoreContainer.platformerTimeScoresGood.z)
        {
            scoreContainer.platformerTimeScoresGood.z = time;
            scoreSaver.SaveGame();
        }

        if (scoreTextsPlatformerGood.Length > 0)
        {
            PrintPlatformScoresGood();
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


    public void FlexScoreSetter(int turns)
    {
        if (turns > scoreContainer.flexTurnScores.x)
        {

            scoreContainer.flexTurnScores.z = scoreContainer.flexTurnScores.y;
            scoreContainer.flexTurnScores.y = scoreContainer.flexTurnScores.x;
            scoreContainer.flexTurnScores.x = turns;
            scoreSaver.SaveGame();
        }
        
        else if (turns > scoreContainer.flexTurnScores.y)
        {

            scoreContainer.flexTurnScores.z = scoreContainer.flexTurnScores.y;
            scoreContainer.flexTurnScores.y = turns;
            scoreSaver.SaveGame();
        }
        
        else if (turns > scoreContainer.flexTurnScores.z)
        {

            
            scoreContainer.flexTurnScores.z = turns;
            scoreSaver.SaveGame();
        }

        if (scoreTextsFlex.Length > 0)
        {
            PrintFlexScores();
        }
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

    public void PrintPlatformScoresFun()
    {
        scoreTextsPlatformerFun[0].text = scoreContainer.platformerTimeScoresFun.x.ToString();
        scoreTextsPlatformerFun[1].text = scoreContainer.platformerTimeScoresFun.y.ToString();
        scoreTextsPlatformerFun[2].text = scoreContainer.platformerTimeScoresFun.z.ToString();
    }
    
    public void PrintPlatformScoresGood()
    {
        scoreTextsPlatformerGood[0].text = scoreContainer.platformerTimeScoresGood.x.ToString();
        scoreTextsPlatformerGood[1].text = scoreContainer.platformerTimeScoresGood.y.ToString();
        scoreTextsPlatformerGood[2].text = scoreContainer.platformerTimeScoresGood.z.ToString();
    }
    
    public void PrintPlatformScoresKills()
    {
        scoreTextsPlatformerKills[0].text = scoreContainer.platformerKillsScores.x.ToString();
        scoreTextsPlatformerKills[1].text = scoreContainer.platformerKillsScores.y.ToString();
        scoreTextsPlatformerKills[2].text = scoreContainer.platformerKillsScores.z.ToString();
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
        scoreTextsFlex[0].text = scoreContainer.flexTurnScores.x.ToString();
        scoreTextsFlex[1].text = scoreContainer.flexTurnScores.y.ToString();
        scoreTextsFlex[2].text = scoreContainer.flexTurnScores.z.ToString();
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
        PrintPlatformScoresFun();
        PrintPlatformScoresKills();
        PrintPlatformScoresGood();
        PrintStealthScores();
        PrintSweeperScores();
    }
    
}
