using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Arcade/MinigamesUnlocked")]
public class GamesUnlocked : ScriptableObject
{
    public bool minigame1 = false;
    public bool minigame2 = false;
    public bool minigame3 = false;
    public bool minigame4 = false;
    public bool minigame5
    {
        get
        {
            if (minigame1 && minigame2 && minigame3 && minigame4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



}
