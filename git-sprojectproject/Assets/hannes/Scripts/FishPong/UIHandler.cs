using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public HookPoints H;
    public GameManager_FishPong G;

    public Text X, Y, Z, TIMER;

    void Start()
    {
        
    }

    void Update()
    {
        X.text = "Sus: " + H.Points.x;
        Y.text = "Lust: " + H.Points.y;
        Z.text = "Love: " + H.Points.z;
        if (TIMER != null)
        {
            TIMER.text = "" + (int)G.timer;
        }
        
    }
}
