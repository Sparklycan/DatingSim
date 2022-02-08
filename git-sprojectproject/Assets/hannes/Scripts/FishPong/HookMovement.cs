using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HookMovement : MonoBehaviour
{

    [Tooltip("Hhe two different positions the hook will move between, this makes it possible to move the positions around... if that is wanted.")]
    public Transform pos1, pos2;

    [Tooltip("The hooks position will be accessed through this")]
    public Transform hook;

    [Range(0,10)]
    [Tooltip("How quick the hook moves between the points")]
    public float speed;

    private Transform target;

    private FMOD.Studio.EventInstance hookSound;

    // Start is called before the first frame update
    void Start()
    {
        target = pos2;
        hookSound = FMODUnity.RuntimeManager.CreateInstance("event:/Sound/SFX/Minigames/MinigameGoFish/Hook");
        hookSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        
       if(hook.position.x <= pos1.position.x)
        {
            target = pos2;
        }
       if(hook.position.x >= pos2.position.x)
        {
            target = pos1;
        }


        hook.position = Vector3.MoveTowards(hook.position, target.position, speed * Time.deltaTime);

    }

    private void OnDisable()
    {
        hookSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
