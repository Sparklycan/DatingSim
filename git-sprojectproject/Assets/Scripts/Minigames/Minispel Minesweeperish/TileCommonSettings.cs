using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Minigame/CommonTileSettings")]
public class TileCommonSettings : ScriptableObject
{
    public Color firstPassColour, secondPassColour, highlightedColour;
    public Color downlightedColour;
    public MinigameSettings1 settings;
    public AudioClips tileSounds;
    public AudioClips crunchSound;
    public Sprite loveIcon, lustIcon, susIcon;
    public Sprite spriteDownMovable, spriteDownDenied, spriteUp, spriteUpWithIcon;
}
