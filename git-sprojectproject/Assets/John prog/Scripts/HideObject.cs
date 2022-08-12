using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{

    public enum Messages
    {
        Hide,
        Show
    }

    private void OnEnable()
    {
        onHide += OnHide;
        onShow += OnShow;
    }

    private void OnDisable()
    {
        onHide -= OnHide;
        onShow -= OnShow;
    }

    private void OnHide()
    {
        foreach(Canvas canvas in GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = false;
        }
    }

    private void OnShow()
    {
        foreach(Canvas canvas in GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = true;
        }
    }

    private static event Action onHide;
    private static event Action onShow;

    public static void SendMessage(Messages message)
    {
        switch (message)
        {
            case Messages.Hide:
                onHide?.Invoke();
                break;
            case Messages.Show:
                onShow?.Invoke();
                break;
        }
    }

}
