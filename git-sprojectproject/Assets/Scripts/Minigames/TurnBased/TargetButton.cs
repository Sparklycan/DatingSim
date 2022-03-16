using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetButton : MonoBehaviour
{

    [SerializeField]
    private Highlight highlight;

    public event Action onClick;

    private void OnEnable()
    {
        highlight.state = Highlight.HighlightState.Default;
    }

    private void OnDisable()
    {
        highlight.state = Highlight.HighlightState.Default;
        onClick = null;
    }

    private void OnMouseEnter()
    {
        if (enabled)
            highlight.state = Highlight.HighlightState.Highlight;
    }

    private void OnMouseExit()
    {
        if (enabled)
            highlight.state = Highlight.HighlightState.Default;
    }

    private void OnMouseDown()
    {
        if (enabled)
            onClick?.Invoke();
    }

}
