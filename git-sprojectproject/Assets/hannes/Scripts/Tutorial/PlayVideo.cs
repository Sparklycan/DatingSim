using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using UnityEngine.Video;


[CommandInfo("Minigame", "Play Tutorial Video", "Starts the Chosen video on an image")]
public class PlayVideo : Command
{
    public VideoClip VideoClip;
    public GameObject VideoCanvas;
    private GameObject instantiated;
    private Button Button;
    public override void OnEnter()
    {
        instantiated = GameObject.Instantiate(VideoCanvas, VideoCanvas.transform.position, Quaternion.identity);
        instantiated.GetComponentInChildren<VideoPlayer>().clip = VideoClip;
        Button = instantiated.GetComponentInChildren<Button>();
        Button.onClick.AddListener(Continue);
    }
    

    public override void OnExit()
    {
        GameObject.Destroy(instantiated);
    }
    
}
