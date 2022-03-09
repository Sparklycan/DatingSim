using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommingMoves : MonoBehaviour
{

    public Fungus.MoveCollection moveCollection;

    private void OnEnable()
    {
        LogCommingMoves();
        enabled = false;
    }

    private void LogCommingMoves()
    {
        Debug.ClearDeveloperConsole();

        for(int i = 0; i < moveCollection.Count; i++)
        {
            Move move = moveCollection[i] as Move;
            if (move.isHidden)
                continue;
            if (move.waitTurns != 0)
                continue;

            Debug.Log(move.Description);
        }
    }
}
