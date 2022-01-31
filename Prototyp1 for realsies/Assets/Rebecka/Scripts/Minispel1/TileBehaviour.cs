using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{


    public bool finishline;
    
    public Color firstPassColour, secondPassColour, highlightedColour;
    public GameObject powerIcon;
    public GamemanagerGame1 gm;
    public MinigameSettings1 settings;
    public int pickupValue;
    
    [Tooltip("Sounds: Step, love, lust, sus and win, in that order")]
    public AudioClips tileSounds;
    
    [Tooltip("For when you remove with Lovebite")]
    public AudioClips crunchSound;
    public AudioSource audioSource;
    [HideInInspector] public bool firstPass = true;
    [HideInInspector]public bool secondPass = false;
    [HideInInspector]public bool canbeStepped = false;
    
    [HideInInspector]public bool loveBite;

    private Color temporaryColor;
    private AudioClip tileStep, loveStep, lustStep, susStep, winStep;
     
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

        tileStep = tileSounds.clips[0];
        loveStep = tileSounds.clips[1];
        lustStep = tileSounds.clips[2];
        susStep = tileSounds.clips[3];
        winStep = tileSounds.clips[4];

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && loveBite)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxC.OverlapPoint(mousePosition))
            {
                audioSource.clip = crunchSound.clips[0];
                audioSource.Play();
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
                    
                    audioSource.clip = tileStep;
                    audioSource.Play();
                }

                else if(secondPass)
                {
                    gm.UpdateSecondturnSteps(transform.position); 
                    if (finishline)
                    {
                        gm.FinishGame();
                        audioSource.clip = winStep;

                    }
                    
                    if (powerIcon != null && canBePicked)
                    {
                        if (powerIcon.activeInHierarchy == false)
                        {
                            powerIcon.SetActive(true);
                            
                        }

                        //value of pickup to be sent here, then setting the pickup to not be pickupable
                        if (love)
                        {
                           gm.love += pickupValue;
                           audioSource.clip = loveStep;
                        }
                        
                        if (lust)
                        {
                            gm.lust += pickupValue;
                            audioSource.clip = lustStep;
                        }
                        
                        if (sus || extraSus)
                        {
                            gm.sus += pickupValue;
                            audioSource.clip = susStep;
                        }
                        
                        canBePicked = false;
                    }
                    else
                    {
                        audioSource.clip = tileStep;
                    }
                    
                    sr.color = secondPassColour;
                    temporaryColor = sr.color;
                    audioSource.Play();
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
