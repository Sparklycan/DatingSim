using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverText;
    public GamesUnlocked gamesUnlocked;

    private void Start()
    {
        hoverText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        if (gamesUnlocked.allMinigamesUnlocked == false)
        {
            hoverText.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        if (hoverText.activeSelf)
        {
            hoverText.SetActive(false);
        }
        
    }
}
