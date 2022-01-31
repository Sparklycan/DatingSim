using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[CommandInfo("Minigame", "Play minigame", helpText: "Plays a minigame and waits until the minigame has ended.")]
public class PlayMinigameCommand : Command
{

    [Header("Instantiation of the minigame")]
    [Tooltip("The object to copy when starting the minigame.\nCan be a scene object or a prefab.")]
    public Minigame minigameSourceObect;
    [Tooltip("Transform to use as a parent during instantiate.")]
    public Transform parentTransform;

    [Space]
    [Header("Variables to store the resaults of the minigame in")]
    public FloatData romance;
    public FloatData lust;
    public FloatData suspicion;

    private Minigame minigame = null;

    public override void OnEnter()
    {
        if (minigameSourceObect != null && minigame == null)
            PlayMinigame();
        else
            Continue();
    }

    private void PlayMinigame()
    {
        minigame = Instantiate(minigameSourceObect, parentTransform);
        minigame.StartGame(OnMinigameEnd);
    }

    private void OnMinigameEnd(float interest, float lust, float suspicion)
    {
        this.romance.Value = interest;
        this.lust.Value = lust;
        this.suspicion.Value = suspicion;

        Destroy(minigame.gameObject);
        minigame = null;

        Continue();
    }
}
