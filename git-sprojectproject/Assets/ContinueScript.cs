using System.Collections;
using System.Collections.Generic;
using Fungus;
using Unity.Mathematics;
using UnityEngine;
[CommandInfo("End Credits", "End the Credits", "It ends the credits")]
public class ContinueScript : Command
{

    public GameObject Canvas;
    public bool Instantiate;
    private GameObject instantiated;
    public override void OnEnter()
    {
        if(Instantiate)
       instantiated =  Instantiate(Canvas, transform.position, quaternion.identity);
    }

    public override void OnExit()
    {
        Destroy(instantiated);
    }

    public void Continue()
    {
        //Destroy(instantiated);
        Continue();
    }

}
