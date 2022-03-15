#region ENEMY SCRIPT
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatEnemy : MonoBehaviour
{
    #region VARS
    //set in editor
    public Rigidbody2D myBody;

    //vec3s for patrol points
    private Vector3 point1;
    private Vector3 point2;

    //first patrol point's x will be x coord on spawn
    private float firstPoint;

    //set second patrol point in editor
    [Tooltip("Enemy will patrol between this point and spawn coords. X only")]
    public float secondPoint;

    //set speed in editor
    public float moveSpeed;

    //name of child's sprite
    SpriteRenderer sevenUP;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //sets first point X to spawn X for float and Vec3
        firstPoint = transform.position.x;
        point1 = transform.position;

        //sets up second point vec3 based with x float from editor
        point2 = new Vector3(secondPoint, transform.position.y, transform.position.z);

        //gets the sprite from child
        sevenUP = GetComponentInChildren<SpriteRenderer>();

        //get rigidbody2d
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //enemy patrols between spawn X and X coord set in editor
        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //when a patrol point has been reached; change direction
        if (transform.position.x >= secondPoint)
		{
            ChangeDirection();
		}
        if (transform.position.x <= firstPoint)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
	{
        //changes movement direction to opposite of what it was
        moveSpeed *= -1f;

        //flips the sprite
        if (sevenUP.flipX == false)
        {
            sevenUP.flipX = true;
        }
        else
        {
            sevenUP.flipX = false;
        }
	}
}
#endregion