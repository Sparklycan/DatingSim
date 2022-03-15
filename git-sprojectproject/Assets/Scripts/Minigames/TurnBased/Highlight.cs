using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{

    public enum HighlightState
    {
        Shade,
        Default,
        Highlight
    }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string variable = "Highlight";

    [SerializeField]
    public HighlightState state = HighlightState.Default;
    private HighlightState previousState;

    private float speed = 0.0f;
    private IEnumerator highlight = null;

    private void Awake()
    {
        previousState = state;
        animator.SetFloat(variable, GetHighlight(state));
    }

    private float GetHighlight(HighlightState state)
    {
        switch (state)
        {
            case HighlightState.Shade:      return -1.0f;
            case HighlightState.Highlight:  return 1.0f;
            default:                        return 0.0f;
        }
    }

    private void Update()
    {
        if (state == previousState)
            return;

        previousState = state;

        float value = animator.GetFloat(variable);
        float distance = GetHighlight(state) - value;
        distance = Mathf.Sign(distance) * Mathf.Ceil(Mathf.Abs(distance));
        speed = distance;

        if (highlight == null)
        {
            highlight = SetHighlight();
            StartCoroutine(highlight);
        }
    }

    private IEnumerator SetHighlight()
    {
        while (highlight != null)
        {
            float s = 2.0f * speed * Time.deltaTime;
            float value = animator.GetFloat(variable);
            float distance = GetHighlight(state) - value;

            if (Mathf.Abs(distance) <= Mathf.Abs(s))
            {
                value = GetHighlight(state);
                speed = 0.0f;
                highlight = null;
            }
            else
            {
                value += s;
            }

            animator.SetFloat(variable, value);
            yield return null;
        }
    }
}
