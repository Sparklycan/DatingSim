using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{

    public int startSteps = 8;
    public int startStepsIfDiagonal= 5;
    public Text stepsLeftText;

    public GameObject player;
    public GameObject obstacleCollection;
    public bool extraSusCollection;

    [Tooltip("Which tiles can be stepped on at the start")]
    public TileBehaviour[] startAvailable;

    public LayerMask obstacleMask, tileMask;
    public MinigameSettings settings;
    public GameObject startpositionObject;

    public Minigame mg;
   [HideInInspector] public int lust =0, love =0, sus =0;
    
    private Vector3 playerPosition;
    private Vector3 playerStartPosition;
    private TileBehaviour[] tileArray;
    private bool firstTurn = true;

    private int stepsTaken;

    public Vector2 detectionDistanceBasic = new Vector2(3,3);

    // Start is called before the first frame update
    void Start()
    {
        playerStartPosition = startpositionObject.transform.position;
        tileArray = FindObjectsOfType<TileBehaviour>();
        if (settings.diagonalMovement)
        {
            stepsTaken = startStepsIfDiagonal;
        }
        else
        {
            stepsTaken = startSteps;
        }
        
        stepsLeftText.text = "Steps left: " + stepsTaken.ToString();
        player.transform.position = playerStartPosition;
        playerPosition = player.transform.position;

        if (settings.doubleDistance)
        {
            obstacleCollection.SetActive(true);
        }
        else
        {
            obstacleCollection.SetActive(false);
        }

        foreach (TileBehaviour tile in startAvailable)
        {
            tile.ColourchangeAvailable();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStartSteps(Vector3 position)
    {
        
        
        if (stepsTaken > 0 && firstTurn)
        {
            stepsTaken--;
            stepsLeftText.text = "Steps left: " + stepsTaken.ToString();
            if (stepsTaken <= 0)
            {
                foreach (TileBehaviour tile in tileArray)
                {
                    tile.firstPass = false; 
                    tile.secondPass = true;
                    tile.canbeStepped = true;
                    stepsLeftText.text = "Keep walking now! ";
                }
                
                firstTurn= false;
                playerPosition = playerStartPosition;
                player.transform.position = playerPosition; 
                BegginerStepAvailability();
            }
            else
            {
                playerPosition.x = position.x;
                playerPosition.y = position.y;
                player.transform.position = playerPosition; 
                CheckStepAvailability();  
            }
            
        }
        
    }

    public void UpdateSecondturnSteps(Vector3 position)
    {
        if (!firstTurn)
        {

            playerPosition.x = position.x;
            playerPosition.y = position.y;
            player.transform.position = playerPosition;
            CheckStepAvailability();
        }
    }

    public void BegginerStepAvailability()
    {
        foreach (TileBehaviour tile in tileArray)
        {
            tile.canbeStepped = false;
            tile.ChangeBackColour();
        }

        foreach (TileBehaviour startTile in startAvailable)
        {
            startTile.canbeStepped = true;
            startTile.ColourchangeAvailable();
        }
    }

    private void CheckStepAvailability()
    {
        
        foreach (TileBehaviour tile in tileArray)
        {
            tile.canbeStepped = false;
            tile.ChangeBackColour();
        }
        //Check overlap between player box and tiles

        Collider2D[] overlaps;
        
        if (settings.doubleDistance)
        {
            overlaps = Physics2D.OverlapBoxAll(playerPosition, detectionDistanceBasic *2 , 0, tileMask);
        }
        else
        {
            overlaps = Physics2D.OverlapBoxAll(playerPosition, detectionDistanceBasic, 0, tileMask);
        }
        
        foreach (Collider2D col in overlaps)
        {
            if (settings.diagonalMovement)
            {
                RaycastHit2D hit = Physics2D.Linecast(playerPosition, col.transform.position, obstacleMask);
                                
                if (hit.collider != null)
                {
                    TileBehaviour tile = col.gameObject.GetComponent<TileBehaviour>();
                    tile.canbeStepped = false;

                    tile.ChangeBackColour();
                }
                else
                {
                    TileBehaviour tile = col.gameObject.GetComponent<TileBehaviour>();
                    tile.canbeStepped = true;
                    
                    tile.ColourchangeAvailable();
                }                
            }

            else
            {
                

                 if (playerPosition.x != col.transform.position.x && playerPosition.y != col.transform.position.y)
                 {
                     TileBehaviour tile = col.gameObject.GetComponent<TileBehaviour>();
                     tile.canbeStepped = false;
                     
                     tile.ChangeBackColour();
                 }

                 else
                 {
                     RaycastHit2D hit = Physics2D.Linecast(playerPosition, col.transform.position, obstacleMask);
                     if (hit.collider != null)
                     {
                         TileBehaviour tile = col.gameObject.GetComponent<TileBehaviour>();
                         tile.canbeStepped = false;
                     
                         tile.ChangeBackColour();
                     }
                     else
                     {
                         TileBehaviour tile = col.gameObject.GetComponent<TileBehaviour>();
                         tile.canbeStepped = true;
                     
                         tile.ColourchangeAvailable();
                     }
                 }
                 
                 
            }
            
            
        }

    }

    public void FinishGame()
    {
        foreach (TileBehaviour tile in tileArray)
        {
            tile.canbeStepped = false;
        }
        
        mg.EndGame(love, lust, sus);
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        if (settings.doubleDistance)
        {
            Gizmos.DrawCube(player.transform.position, new Vector3(detectionDistanceBasic.x, detectionDistanceBasic.y, 1) *2);
        }
        else
        {
            Gizmos.DrawCube(player.transform.position, new Vector3(detectionDistanceBasic.x, detectionDistanceBasic.y, 1));
        }
        
    }
    
}
