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
        if (settings.diagonalMovement)
        {
            diagonal.color = Color.white;
        }
        else
        {
            diagonal.color = Color.gray;
        }
        
        if (settings.doubleDistance)
        {
            doublemov.color = Color.white;
        }
        else
        {
            doublemov.color = Color.gray;
        }
        
        if (settings.loveBite)
        {
            lovebite.color = Color.white;
        }
        else
        {
            lovebite.color = Color.gray;
        }
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
}
