using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Scrollabe_Material : MonoBehaviour
{
    public Material scrollableMaterial;
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 1.0f;

    private Vector2 currentOffset;

    private GameObject Player;
    
    void Start()
    {
        Player = GameObject.FindWithTag("StealthPlayer");
        currentOffset = scrollableMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        currentOffset += direction * speed * Time.deltaTime;
        scrollableMaterial.SetTextureOffset("_MainTex", currentOffset);
    }
}
