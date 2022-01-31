using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Value : MonoBehaviour
{

    public float value = 0.0f;

    public Text text;

    public void Increase()
    {
        value += 1.0f;
        UpdateTextMesh();
    }
    public void Decrease()
    {
        value -= 1.0f;
        UpdateTextMesh();
    }

    private void OnEnable()
    {
        UpdateTextMesh();
    }

    private void UpdateTextMesh()
    {
        text.text = value.ToString();
    }
}
