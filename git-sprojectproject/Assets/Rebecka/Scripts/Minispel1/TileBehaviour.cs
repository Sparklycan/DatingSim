using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{


    public bool finishline;
    public TileCommonSettings tileSettings;
    
    public GameObject powerIcon;
    public GamemanagerGame1 gm;
   
    public int pickupValue;
    
    //Sounds: Step, love, lust, sus and win, in that order
    private AudioClips tileSounds;
    //For when you remove with Lovebite
    private AudioClips crunchSound;
    public AudioSource audioSource;
    [HideInInspector] public bool firstPass = true;
    [HideInInspector]public bool secondPass = false;
    [HideInInspector]public bool canbeStepped = false;
    
    [HideInInspector]public bool loveBite;
    
    private MinigameSettings1 settings;
    private Color temporaryColor;
    private AudioClip tileStep, loveStep, lustStep, susStep, winStep, pointlossStep;

    [Tooltip("This bool sets whether you want to do minus points instead of plus")]
    [SerializeField] private bool detractPoints = false;
    [Tooltip("You cannot use extraSus and detractPoints at the same time")]
    [SerializeField]private bool love, lust, sus, extraSus;
    private bool canBePicked = true;
    private bool turnedUp = false;
    
    private SpriteRenderer sr;
    private BoxCollider2D boxC;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        temporaryColor = sr.color;
        tileSounds = tileSettings.tileSounds;
        crunchSound = tileSettings.crunchSound;
        settings = tileSettings.settings;
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
        pointlossStep = tileSounds.clips[5];

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && loveBite && !turnedUp)
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
                    //sr.color = tileSettings.firstPassColour;
                   // temporaryColor = sr.color;
                    if (powerIcon != null)
                    {
                        powerIcon.SetActive(true);
                        sr.sprite = tileSettings.spriteUpWithIcon;
                    }
                    else
                    {
                        sr.sprite = tileSettings.spriteUp;
                    }

                    turnedUp = true;
                    audioSource.clip = tileStep;
                    audioSource.Play();
                }

                else if(secondPass)
                {
                    gm.UpdateSecondturnSteps(transform.position); 
                    if (finishline)
                    {   
                        audioSource.clip = winStep;
                        gm.FinishGame();
                        

                    }
                    
                    if (powerIcon != null && canBePicked)
                    {
                        if (powerIcon.activeInHierarchy == false)
                        {
                            powerIcon.SetActive(true);
                            
                        }
                        if (detractPoints)
                        {
                            pickupValue *= -1;
                            audioSource.clip = pointlossStep;
                        }
                        
                        
                        //value of pickup to be sent here, then setting the pickup to not be pickupable
                        if (love)
                        {
                            gm.love += pickupValue;
                            if (!detractPoints)
                            {
                               audioSource.clip = loveStep; 
                            }
                           
                        }
                        
                        if (lust)
                        {
                            gm.lust += pickupValue;
                            if (!detractPoints)
                            {
                                audioSource.clip = lustStep;
                            }
                            
                        }
                        
                        if (sus || extraSus)
                        {
                            gm.sus += pickupValue;
                            if (!detractPoints)
                            {
                                audioSource.clip = susStep;
                            }
                            
                        }
                        
                        canBePicked = false;
                        if (!turnedUp)
                        {
                            sr.sprite = tileSettings.spriteUpWithIcon;
                        }

                    }
                    else
                    {
                        if (!turnedUp)
                        {
                             sr.sprite = tileSettings.spriteUp;
                        }
                       
                        audioSource.clip = tileStep;
                        
                    }
                    turnedUp = true;
                    
                    audioSource.Play();
                }
            }
        }
    }


    public void ColourchangeAvailable()
    {
        if (!finishline)
        {
            sr.color = tileSettings.highlightedColour;
        }
        
    }

    public void ChangeBackColour()
    {
        if (!finishline)
        {
            sr.color = tileSettings.downlightedColour;
        }
        
    }
}
