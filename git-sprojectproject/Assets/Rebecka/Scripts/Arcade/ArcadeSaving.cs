using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class ArcadeSaving : MonoBehaviour
{
    public GamesUnlocked gamesUnlocked;
    //public string FilePath => Directory.GetCurrentDirectory() + '\\' + Path.GetDirectoryName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().path);

    private void Start()
    {
        gamesUnlocked.onSave += SaveGame;
    }

    public void SaveGame()
    {
      
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if (gamesUnlocked.useDefaultPath)
        {
            file = File.Create(  Application.persistentDataPath + "/GamesUnlocked.dat"); 
        }
        
        else if (gamesUnlocked.useCurrentApplicationPath)
        {
            file = File.Create(  Directory.GetCurrentDirectory() + "/GamesUnlocked.dat"); 
        }
        else
        {
            file = File.Create(  gamesUnlocked.customPath + "/GamesUnlocked.dat"); 
        }
        SaveData data = new SaveData();
        
        data.allMinigamesSave = gamesUnlocked.allMinigamesUnlocked;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        
        if (File.Exists(Application.persistentDataPath + "/GamesUnlocked.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open(Application.persistentDataPath + "/GamesUnlocked.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            gamesUnlocked.allMinigamesUnlocked = data.allMinigamesSave;
            Debug.Log("Game data loaded!");
        }
        
        else if (File.Exists( Directory.GetCurrentDirectory() + "/GamesUnlocked.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open(Directory.GetCurrentDirectory() + "/GamesUnlocked.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            gamesUnlocked.allMinigamesUnlocked = data.allMinigamesSave;
            Debug.Log("Game data loaded!");
        }
        else if(File.Exists( gamesUnlocked.customPath + "/GamesUnlocked.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open(gamesUnlocked.customPath + "/GamesUnlocked.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            gamesUnlocked.allMinigamesUnlocked = data.allMinigamesSave;
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.Log("ur a big dummy, no save here!");
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/GamesUnlocked.dat"))
        {
            File.Delete(Application.persistentDataPath + "/GamesUnlocked.dat");
            gamesUnlocked.allMinigamesUnlocked = false;
            Debug.Log("Data reset done, yeet");
        }
        
        else if (File.Exists(Directory.GetCurrentDirectory() + "/GamesUnlocked.dat"))
        {
            File.Delete(Directory.GetCurrentDirectory() + "/GamesUnlocked.dat");
            gamesUnlocked.allMinigamesUnlocked = false;
            Debug.Log("Data reset done, yeet");
        }
        
        else if (File.Exists(gamesUnlocked.customPath + "/GamesUnlocked.dat"))
        {
            File.Delete(gamesUnlocked.customPath + "/GamesUnlocked.dat");
            gamesUnlocked.allMinigamesUnlocked = false;
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
    public bool allMinigamesSave;
}

//TODO: change filepath of saving to more generic path, does not work on school computers so it is user specific atm