using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BallType : MonoBehaviour
{
    public Color Sus = Color.green, Lust = Color.cyan, Love = Color.red, Neutral = Color.white;

    private SpriteRenderer SPR;

    public List<GameObject> SubBalls = new List<GameObject>();
    


    public enum Value // your custom enumeration
    {
        SUS,
        LUST,
        LOVE,
        NEUTRAL,
    };

    public Value dropDown;

    private void Awake()
    {
        SubBalls.Clear();
        foreach (Transform child in this.transform)
        {
            SubBalls.Add(child.gameObject);
            Change(child.gameObject);
        }


    }

    public void Change(GameObject obj)
    {
        SPR =  obj.GetComponent<SpriteRenderer>();
        BallValue V = obj.GetComponent<BallValue>();
        switch (dropDown)
        {
            case Value.SUS:
                {
                    SPR.color = Sus;
                    V.Value.x = 1;
                    Debug.Log("S");
                    break;
                }
            case Value.LUST:
                {
                    SPR.color = Lust;
                    V.Value.y = 1;
                    Debug.Log("LU");

                    break;
                }
            case Value.LOVE:
                {
                    SPR.color = Love;
                    V.Value.z = 1;
                    Debug.Log("LO");

                    break;
                }
            case Value.NEUTRAL:
                {
                    SPR.color = Neutral;
                    Debug.Log("N");

                    break;
                }

            default: break;
        }
    }

    // time to frankenstein this shit.
    void Start()
    {

    }

    void Update()
    {
        
    }
}
