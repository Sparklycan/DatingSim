using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_FishPong : MonoBehaviour
{

    public float timer;

    public Canvas GameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            GameOver.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        timer -= Time.deltaTime;
    }
}
