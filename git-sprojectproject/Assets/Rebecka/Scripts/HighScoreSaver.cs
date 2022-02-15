using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class HighScoreSaver : MonoBehaviour
{
    public Vector3 fishpongScore1 = new Vector3(0,0,0 ), fishpongScore2 = new Vector3(0,0,0 ), fishpongScore3 =new Vector3(0,0,0 );
    public Vector3 platformerTimeScores = new Vector3(0,0,0 );
    public Vector3 sweeperScore1 = new Vector3(0,0,0), sweeperScore2 = new Vector3(0,0,0), sweeperscore3 = new Vector3(0,0,0);
    
    
    public void SaveGame()
    {
      
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create("C:/users/c17rebma/MyHighscoreSaveData.dat"); 
        HighscoreSaveData data = new HighscoreSaveData();
        
        data.values1a = fishpongScore1;
        data.values2a = fishpongScore2;
        data.values3a = fishpongScore3;
        data.values1b = platformerTimeScores;
        data.values1e = sweeperScore1;
        data.values2e = sweeperScore2;
        data.values3e = sweeperscore3;
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
            fishpongScore1 = data.values1a;
            fishpongScore2 = data.values2a;
            fishpongScore3 = data.values3a;
            platformerTimeScores = data.values1b;
            sweeperScore1 = data.values1e;
            sweeperScore2 = data.values2e;
            sweeperscore3 = data.values3e;
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
            fishpongScore1 = Vector3.zero;
            fishpongScore2 = Vector3.zero;
            fishpongScore3 = Vector3.zero;
            platformerTimeScores = Vector3.zero;
            sweeperScore1 = Vector3.zero;
            sweeperScore2 = Vector3.zero;
            sweeperscore3 = Vector3.zero;
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