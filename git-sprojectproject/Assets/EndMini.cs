using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMini : MonoBehaviour
{
    private Minigame _minigame;
    
    // Start is called before the first frame update
    void Start()
    {
        _minigame = GetComponent<Minigame>();
    }


    public void END(HookPoints hookPoints)
    {
        _minigame.EndGame(hookPoints.Points.z, hookPoints.Points.y, hookPoints.Points.x);
    }
}
