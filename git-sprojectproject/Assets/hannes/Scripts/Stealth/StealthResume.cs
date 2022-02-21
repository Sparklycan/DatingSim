using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.AI;

[CommandInfo("Minigame", "Resume Stealth-game", "Resumes all of AIs and the players movement")]
public class StealthResume : Command
{
    private GameObject Player;
    private GameObject[] AIs;
    
    public override void OnEnter()
    {
        AIs = FindGameObjectsWithLayer(7);
        Player = FindGameObjectsWithLayer(3)[0];
        
        // think i had a stroke here jesus fuck. 
        foreach (GameObject gaemOubjeact in AIs)
        {
            gaemOubjeact.GetComponent<NavMeshAgent>().isStopped = false;
            gaemOubjeact.GetComponent<PoliceScript>().enabled = true;
        }
        
        Player.GetComponent<StealthPlayer>().enabled = enabled;
        
        Continue();
    }
    

    
    
    // need to get all objects on one layer, so function to find them.
    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[]; 
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer) { goList.Add(goArray[i]); }
        } 
        if (goList.Count == 0) { return null; } return goList.ToArray();
    }
}
