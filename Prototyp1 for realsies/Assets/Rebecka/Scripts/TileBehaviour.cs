using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{


    public bool finishline;
    
    public Color firstPassColour, secondPassColour, highlightedColour;
    public GameObject powerIcon;
    public Gamemanager gm;
    public MinigameSettings settings;
    public int pickupValue;
    [HideInInspector] public bool firstPass = true;
    [HideInInspector]public bool secondPass = false;
    [HideInInspector]public bool canbeStepped = false;
    
    [HideInInspector]public bool loveBite;

    private Color temporaryColor;
    
     //isn't used just yet
    [SerializeField]private bool love, lust, sus, extraSus;
    private bool canBePicked = true;
    
    
    private SpriteRenderer sr;
    private BoxCollider2D boxC;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        temporaryColor = sr.color;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        loveBite = settings.loveBite;
        boxC = GetComponent<BoxCollider2D>();
        if (powerIcon != null)
        {
            powerIcon.SetActive(false);
        }

        if (extraSus)
        {
            if (!loveBite)
            {
                Destroy(powerIcon);
                powerIcon = null;
                extraSus = false; 
            }


            love = false;
            love = false;
            lust = false;
            sus = false;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxC.OverlapPoint(mousePosition))
            {
                if (powerIcon != null)
                {
                    Destroy(powerIcon);
                    powerIcon = null;
                    love = false;
                    lust = false;
                    sus = false;
                    extraSus = false;
                }
            }
        }
        
        if (Input.GetButtonDown("Fire1") && canbeStepped)
        {
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 
            if(boxC.OverlapPoint(mousePosition))
            {
                if (firstPass && !finishline)
                {
                    
                    gm.UpdateStartSteps(transform.position); 
                    sr.color = firstPassColour;
                    temporaryColor = sr.color;
                    if (powerIcon != null)
                    {
                        powerIcon.SetActive(true);
                    }
                    
                }

                else if(secondPass)
                {
                    gm.UpdateSecondturnSteps(transform.position); 
                    if (finishline)
                    {
                        gm.FinishGame();
                    }
                    
                    if (powerIcon != null && canBePicked)
                    {
                        if (powerIcon.activeInHierarchy == false)
                        {
                            powerIcon.SetActive(true);
                            
                        }
                        Debug.Log("Picked up value");

                        //value of pickup to be sent here, then setting the pickup to not be pickupable
                        if (lust)
                        {
                            gm.lust += pickupValue;
                        }
                        

                        if (love)
                        {
                           gm.love += pickupValue;
                        }

                        if (sus || extraSus)
                        {
                            gm.sus += pickupValue;
                        }
                        
                        canBePicked = false;
                    }
                    
                    sr.color = secondPassColour;
                    temporaryColor = sr.color;
                }
            }
        }
    }


    public void ColourchangeAvailable()
    {
        Debug.Log(temporaryColor);
        temporaryColor = sr.color;
        sr.color = highlightedColour;
    }

    public void ChangeBackColour()
    {
        sr.color = temporaryColor;
    }
}
