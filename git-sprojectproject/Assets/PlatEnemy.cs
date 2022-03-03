using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatEnemy : MonoBehaviour
{
    #region VARS
    public Rigidbody2D myBody;
    private Vector3 point1;
    [Tooltip("Enemy will patrol between this point and spawn coords. X only")]
    private Vector3 point2;
    private float firstPoint;
    public float secondPoint;
    private float moveSpeed = 2f;
    SpriteRenderer sevenUP;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        firstPoint = transform.position.x;
        point1 = transform.position;
        point2 = new Vector3(secondPoint, transform.position.y, transform.position.z);
        sevenUP = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

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
        moveSpeed *= -1f;
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
