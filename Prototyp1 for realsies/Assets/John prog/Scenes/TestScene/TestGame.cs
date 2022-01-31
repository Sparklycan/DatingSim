using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGame : Minigame
{

    public Value romance;
    public Value lust;
    public Value suspicion;

    public void EndGame()
    {
        EndGame(romance.value, lust.value, suspicion.value);
    }

}
