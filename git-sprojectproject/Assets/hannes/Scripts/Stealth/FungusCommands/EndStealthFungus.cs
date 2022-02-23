using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("Minigame", "End Stealth-game", "Enables the endgame canvas and gets all the points accounted")]
public class EndStealthFungus : Command
{
    private StealthHandler _stealthHandler;
    public override void OnEnter()
    {
        _stealthHandler = GameObject.FindWithTag("StealthHandler").GetComponent<StealthHandler>();
        _stealthHandler.GetSusPoints();
        _stealthHandler.EndGame();
        Continue();
    }
    
    
    
    
    
}
