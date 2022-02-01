using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{

    public Transform pos1, pos2, hook;

    public float speed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
