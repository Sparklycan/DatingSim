using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("Minigame", "End Stealth-game", "Activates the endCanvas")]
public class EndGame : Command
{
    private StealthPointSaver _stealthPointSaver;


    public override void OnEnter()
    {
        _stealthPointSaver = GameObject.FindWithTag("StealthHandler").GetComponent<StealthPointSaver>();
        
        _stealthPointSaver.EndGame();

        Continue();
    }
}
