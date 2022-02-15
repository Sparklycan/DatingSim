using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class ArcadeSaving : MonoBehaviour
{
    public GamesUnlocked gamesUnlocked;

    
    public void TestBoolStatus()
    {
        Debug.Log(gamesUnlocked.minigame1);
    }

    public void SaveGame()
    {
      
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create("C:/users/c17rebma/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.savedBool = gamesUnlocked.minigame1;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (File.Exists("C:/users/c17rebma/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open("C:/users/c17rebma/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            gamesUnlocked.minigame1 = data.savedBool;
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.Log("ur a big dummy, no save here!");
        }
    }

    public void ResetData()
    {
        if (File.Exists("C:/users/c17rebma/MySaveData.dat"))
        {
            File.Delete("C:/users/c17rebma/MySaveData.dat");
            gamesUnlocked.minigame1 = false;
            Debug.Log("Data reset done, yeet");
        }
        else
        {
            Debug.LogError("bro there's nothing HERE");
        }
    }
    
    
}

[Serializable]
class SaveData
{
    public bool savedBool;
}

//TODO: change filepath of saving to more generic path, does not work on school computers so it is user specific atm