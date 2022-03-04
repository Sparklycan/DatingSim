using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CreateAssetMenu(menuName = "HighscoreSaving")]
public class HighscoreContainer : ScriptableObject
{
    [HideInInspector]public Vector3 fishpongScore1 = new Vector3(0,0,0 ), fishpongScore2 = new Vector3(0,0,0 ), fishpongScore3 =new Vector3(0,0,0 );
    [HideInInspector]public Vector3 platformerTimeScores = new Vector3(0,0,0 );
    [HideInInspector]public Vector3 sweeperScore1 = new Vector3(0,0,0), sweeperScore2 = new Vector3(0,0,0), sweeperScore3 = new Vector3(0,0,0);
    [HideInInspector]public Vector3 stealthSuspicion = new Vector3(0, 0, 0), stealthNuggetScores = new Vector3(0,0,0);
    [HideInInspector]public Vector3 flexTurnScores = new Vector3(0,0,0);

 
}
