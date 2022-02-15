using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class HighScoreSaver : MonoBehaviour
{
    public HighscoreContainer scoreContainer;
    
    
    
    public void SaveGame()
    {
      
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create("C:/users/c17rebma/MyHighscoreSaveData.dat"); 
        HighscoreSaveData data = new HighscoreSaveData();
        
        data.values1a = scoreContainer.fishpongScore1;
        data.values2a = scoreContainer.fishpongScore2;
        data.values3a = scoreContainer.fishpongScore3;
        data.values1b = scoreContainer.platformerTimeScores;
        data.values1e = scoreContainer.sweeperScore1;
        data.values2e = scoreContainer.sweeperScore2;
        data.values3e = scoreContainer.sweeperscore3;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (File.Exists("C:/users/c17rebma/MyHighscoreSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open("C:/users/c17rebma/MyHighscoreSaveData.dat", FileMode.Open);
            HighscoreSaveData data = (HighscoreSaveData)bf.Deserialize(file);
            file.Close();
            scoreContainer.fishpongScore1 = data.values1a;
            scoreContainer.fishpongScore2 = data.values2a;
            scoreContainer.fishpongScore3 = data.values3a;
            scoreContainer.platformerTimeScores = data.values1b;
            scoreContainer.sweeperScore1 = data.values1e;
            scoreContainer.sweeperScore2 = data.values2e;
            scoreContainer.sweeperscore3 = data.values3e;
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.Log("ur a big dummy, no save here!");
        }
    }

    public void ResetData()
    {
        if (File.Exists("C:/users/c17rebma/MyHighscoreSaveData.dat"))
        {
            File.Delete("C:/users/c17rebma/MyHighscoreSaveData.dat");
            scoreContainer.fishpongScore1 = Vector3.zero;
            scoreContainer.fishpongScore2 = Vector3.zero;
            scoreContainer.fishpongScore3 = Vector3.zero;
            scoreContainer.platformerTimeScores = Vector3.zero;
            scoreContainer.sweeperScore1 = Vector3.zero;
            scoreContainer.sweeperScore2 = Vector3.zero;
            scoreContainer.sweeperscore3 = Vector3.zero;
            Debug.Log("Data reset done, yeet");
        }
        else
        {
            Debug.LogError("bro there's nothing HERE");
        }
    }
}


[Serializable]
class HighscoreSaveData
{
    //fishpong
    public Vector3 values1a;
    public Vector3 values2a;
    public Vector3 values3a;

    //platformer
    public Vector3 values1b;

    //stealth
    
    //fourth minigame

    //sweeper
    public Vector3 values1e;
    public Vector3 values2e;
    public Vector3 values3e;
}