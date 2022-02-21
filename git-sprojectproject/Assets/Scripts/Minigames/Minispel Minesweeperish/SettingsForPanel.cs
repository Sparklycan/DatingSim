using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsForPanel : MonoBehaviour
{
    public MinigameSettings1 settings;
    public Image diagonal, doublemov, lovebite;
    public bool diagonalMove
    {
        set {ToggleDiagonal() ; }
    }
    
    public bool doubleDis
    {
        set { ToggleDouble(); }
    }
    
    public bool loveBite
    {
        set { ToggleLovebite(); }
    }

    private void Awake()
    {
        SetAllDark();   
    }


    public void ToggleDiagonal()
    {
        if (settings.diagonalMovement)
        {
            settings.diagonalMovement = false;
            diagonal.color = Color.gray;
        }
        else
        {
            settings.diagonalMovement = true;
            diagonal.color = Color.white;
        }
    }

    public void ToggleDouble()
    {
        if (settings.doubleDistance)
        {
            settings.doubleDistance = false;
            doublemov.color = Color.grey;
        }
        else
        {
            settings.doubleDistance = true;
            doublemov.color = Color.white;
        }
    }

    public void ToggleLovebite()
    {
        if (settings.loveBite)
        {
            settings.loveBite = false;
            lovebite.color = Color.grey;
        }
        else
        {
            settings.loveBite = true;
            lovebite.color = Color.white;
        }
    }

    public void SetAllDark()
    {
        diagonal.color = Color.gray;
        doublemov.color = Color.grey;
        lovebite.color = Color.grey;
    }
}
