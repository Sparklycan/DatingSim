using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Minigame", "set timescale", "set the timescale to the amount you want it to be, 0 is freeze time. 1 is normal speed.")]
public class ChangeTime : Command
{
    [Range(0, 1)] public float timeScale; 
    
    
    public override void OnEnter()
    {
        Time.timeScale = timeScale;
        
        Continue();
    }

    public override string GetSummary()
    {
        return timeScale.ToString();
    }
    
    
    
}
