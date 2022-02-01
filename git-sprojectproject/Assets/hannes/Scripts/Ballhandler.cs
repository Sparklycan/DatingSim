using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ballhandler : MonoBehaviour
{
    

    public List<GameObject> Balls = new List<GameObject>();


    public int ballAmount;


    [Tooltip("prefab of the ball.")]
    public GameObject ball;

    private void Awake()
    {
        Balls.Clear();
        GameObject[] BallArray = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject node in BallArray)
        {
            Balls.Add(node);

        }
        BallCheck();
    }


    void Start()
    {

        


    }

    public void RemoveBall()
    {
        Debug.Log("removing");
        DestroyImmediate(Balls[Balls.Count - 1]);
    }

    public void AddBall()
    {
        Debug.Log("adding");
        GameObject.Instantiate(ball, Vector3.zero, Quaternion.identity);
    }

    public void BallCheck()
    {
        if (ballAmount > Balls.Count) AddBall();
        if (ballAmount < Balls.Count) RemoveBall();
    }


    void Update()
    {
        Balls.Clear();
        GameObject[] BallArray = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject node in BallArray)
        {
            Balls.Add(node);

        }

        for (var i = Balls.Count - 1; i > -1; i--)
        {
            if (Balls[i] == null)
                Balls.RemoveAt(i);
        }



        Debug.Log("woop");


    }
}
