using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{

    private Action<float, float, float> onEndGameCallback;

    public void StartGame(Action<float, float, float> endGameCallback)
    {
        if (onEndGameCallback != null)
            return;

        onEndGameCallback = endGameCallback;

        OnStartGame();
    }

    protected virtual void OnStartGame() { }

    public void EndGame(float romance, float lust, float suspicion)
    {
        OnEndGame(ref romance, ref lust, ref suspicion);

        if (onEndGameCallback != null)
            onEndGameCallback(romance, lust, suspicion);

    }

    protected virtual void OnEndGame(ref float romance, ref float lust, ref float suspicion) { }

}
