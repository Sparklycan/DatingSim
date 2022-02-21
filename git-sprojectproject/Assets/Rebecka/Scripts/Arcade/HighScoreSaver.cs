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
        //FileStream file = File.Create("C:/users/c17rebma/MyHighscoreSaveData.dat"); 
        FileStream file;
        if (scoreContainer.useDefaultPath)
        {
            file = File.Create(Application.persistentDataPath + "/MyHighscoreSaveData.dat"); 
        }
        else
        {
            file = File.Create(scoreContainer.saveDataPath + "/MyHighscoreSaveData.dat"); 
        }
        
        HighscoreSaveData data = new HighscoreSaveData();

        data.defaultPathUsed = scoreContainer.useDefaultPath;
        data.customPath = scoreContainer.saveDataPath;
        data.values1ax = scoreContainer.fishpongScore1.x;
        data.values1ay = scoreContainer.fishpongScore1.y;
        data.values1az = scoreContainer.fishpongScore1.z;
        
        data.values2ax = scoreContainer.fishpongScore2.x;
        data.values2ay = scoreContainer.fishpongScore2.y;
        data.values2az = scoreContainer.fishpongScore2.z;
        
        data.values3ax = scoreContainer.fishpongScore3.x;
        data.values3ay = scoreContainer.fishpongScore3.y;
        data.values3az = scoreContainer.fishpongScore3.z;
        
        data.values1bx = scoreContainer.platformerTimeScores.x;
        data.values1by = scoreContainer.platformerTimeScores.y;
        data.values1bz = scoreContainer.platformerTimeScores.z;
        
        data.values1ex = scoreContainer.sweeperScore1.x;
        data.values1ey = scoreContainer.sweeperScore1.y;
        data.values1ez = scoreContainer.sweeperScore1.z;
        
        data.values2ex = scoreContainer.sweeperScore2.x;
        data.values2ey = scoreContainer.sweeperScore2.y;
        data.values2ez = scoreContainer.sweeperScore2.z;
        
        data.values3ex = scoreContainer.sweeperScore3.x;
        data.values3ey = scoreContainer.sweeperScore3.y;
        data.values3ez = scoreContainer.sweeperScore3.z;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (scoreContainer.useDefaultPath)
        {
            
        
            if (File.Exists(Application.persistentDataPath + "/MyHighscoreSaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = 
                    File.Open(Application.persistentDataPath 
                              + "MyHighscoreSaveData.dat", FileMode.Open);
                HighscoreSaveData data = (HighscoreSaveData)bf.Deserialize(file);
                file.Close();
                scoreContainer.useDefaultPath = data.defaultPathUsed;
                scoreContainer.saveDataPath = data.customPath;
                scoreContainer.fishpongScore1.x = data.values1ax;
                scoreContainer.fishpongScore1.y = data.values1ay;
                scoreContainer.fishpongScore1.z = data.values1az;
                
                scoreContainer.fishpongScore2.x = data.values2ax;
                scoreContainer.fishpongScore2.y = data.values2ay;
                scoreContainer.fishpongScore2.z = data.values2az;
                
                scoreContainer.fishpongScore3.x = data.values3ax;
                scoreContainer.fishpongScore3.y = data.values3ay;
                scoreContainer.fishpongScore3.z = data.values3az;
                
                scoreContainer.platformerTimeScores.x = data.values1bx;
                scoreContainer.platformerTimeScores.y = data.values1by;
                scoreContainer.platformerTimeScores.z = data.values1bz;
                
                scoreContainer.sweeperScore1.x = data.values1ex;
                scoreContainer.sweeperScore1.y = data.values1ey;
                scoreContainer.sweeperScore1.z = data.values1ez;
                
                scoreContainer.sweeperScore2.x = data.values2ex;
                scoreContainer.sweeperScore2.y = data.values2ey;
                scoreContainer.sweeperScore2.z = data.values2ez;
                
                scoreContainer.sweeperScore3.x = data.values3ex;
                scoreContainer.sweeperScore3.y = data.values3ey;
                scoreContainer.sweeperScore3.z = data.values3ez;
                Debug.Log("Game data loaded!");
            }
            else
            {
                Debug.Log("ur a big dummy, no save here!");
            }
        }
        else
        {
            if (File.Exists( scoreContainer.saveDataPath + "/MyHighscoreSaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = 
                    File.Open(scoreContainer.saveDataPath + "/MyHighscoreSaveData.dat", FileMode.Open);
                HighscoreSaveData data = (HighscoreSaveData)bf.Deserialize(file);
                file.Close();
                scoreContainer.useDefaultPath = data.defaultPathUsed;
                scoreContainer.saveDataPath = data.customPath;
                scoreContainer.useDefaultPath = data.defaultPathUsed;
                scoreContainer.saveDataPath = data.customPath;
                scoreContainer.fishpongScore1.x = data.values1ax;
                scoreContainer.fishpongScore1.y = data.values1ay;
                scoreContainer.fishpongScore1.z = data.values1az;
                
                scoreContainer.fishpongScore2.x = data.values2ax;
                scoreContainer.fishpongScore2.y = data.values2ay;
                scoreContainer.fishpongScore2.z = data.values2az;
                
                scoreContainer.fishpongScore3.x = data.values3ax;
                scoreContainer.fishpongScore3.y = data.values3ay;
                scoreContainer.fishpongScore3.z = data.values3az;
                
                scoreContainer.platformerTimeScores.x = data.values1bx;
                scoreContainer.platformerTimeScores.y = data.values1by;
                scoreContainer.platformerTimeScores.z = data.values1bz;
                
                scoreContainer.sweeperScore1.x = data.values1ex;
                scoreContainer.sweeperScore1.y = data.values1ey;
                scoreContainer.sweeperScore1.z = data.values1ez;
                
                scoreContainer.sweeperScore2.x = data.values2ex;
                scoreContainer.sweeperScore2.y = data.values2ey;
                scoreContainer.sweeperScore2.z = data.values2ez;
                
                scoreContainer.sweeperScore3.x = data.values3ex;
                scoreContainer.sweeperScore3.y = data.values3ey;
                scoreContainer.sweeperScore3.z = data.values3ez;
                Debug.Log("Game data loaded!");
            }
            else
            {
                Debug.Log("ur a big dummy, no save here!");
            }
        }
    }

    public void ResetData()
    {
        if (scoreContainer.useDefaultPath)
        {
            if (File.Exists( Application.persistentDataPath 
                             +"/MyHighscoreSaveData.dat"))
            {
                File.Delete(Application.persistentDataPath 
                            +"/MyHighscoreSaveData.dat");
                scoreContainer.useDefaultPath = true;
                scoreContainer.saveDataPath = "C:/Users/YourUser/AppData/LocalLow";
                scoreContainer.fishpongScore1 = Vector3.zero;
                scoreContainer.fishpongScore2 = Vector3.zero;
                scoreContainer.fishpongScore3 = Vector3.zero;
                scoreContainer.platformerTimeScores = Vector3.zero;
                scoreContainer.sweeperScore1 = Vector3.zero;
                scoreContainer.sweeperScore2 = Vector3.zero;
                scoreContainer.sweeperScore3 = Vector3.zero;
                Debug.Log("Data reset done, yeet");
            }
            else
            {
                Debug.Log("bro there's nothing HERE");
            }
        }
        else
        {
            if (File.Exists(scoreContainer.saveDataPath + "/MyHighscoreSaveData.dat"))
            {
                File.Delete(scoreContainer.saveDataPath + "/MyHighscoreSaveData.dat");
                scoreContainer.useDefaultPath = true;
                scoreContainer.saveDataPath = "C:/Users/YourUser/AppData/LocalLow";
                scoreContainer.fishpongScore1 = Vector3.zero;
                scoreContainer.fishpongScore2 = Vector3.zero;
                scoreContainer.fishpongScore3 = Vector3.zero;
                scoreContainer.platformerTimeScores = Vector3.zero;
                scoreContainer.sweeperScore1 = Vector3.zero;
                scoreContainer.sweeperScore2 = Vector3.zero;
                scoreContainer.sweeperScore3 = Vector3.zero;
                Debug.Log("Data reset done, yeet");
            }
            else
            {
                Debug.Log("bro there's nothing HERE");
            }
        }
       
    }
}


[Serializable]
class HighscoreSaveData
{

    public bool defaultPathUsed;

    public string customPath;
    //fishpong
    public float values1ax;
    public float values2ax;
    public float values3ax;
    
    public float values1ay;
    public float values2ay;
    public float values3ay;
    
    public float values1az;
    public float values2az;
    public float values3az;
    

    //platformer
    public float values1bx;
    public float values1by;
    public float values1bz;

    //stealth
    
    //Flex game

    //sweeper
    public float values1ex;
    public float values2ex;
    public float values3ex;
    
    public float values1ey;
    public float values2ey;
    public float values3ey;
    
    public float values1ez;
    public float values2ez;
    public float values3ez;
    
}