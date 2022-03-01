using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Arcade/MinigamesUnlocked")]
public class GamesUnlocked : ScriptableObject
{
    public bool useDefaultPath = true;
    public bool useCurrentApplicationPath = false;
    public string customPath = "C:/Users/YourUser/AppData/LocalLow";
    public bool allMinigamesUnlocked = false;
     public UnityAction onSave = delegate{};
     
    

    public void SendSaveMessage()
    {
        onSave();
    }



}
