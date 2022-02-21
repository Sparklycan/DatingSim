using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFishGame : Minigame
{

    public void EndGame(HookPoints hookPoints)
    {
        EndGame(hookPoints.Points.z, hookPoints.Points.y, hookPoints.Points.x);
    }

}
