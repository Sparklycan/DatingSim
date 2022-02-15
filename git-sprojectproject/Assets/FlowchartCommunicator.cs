using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartCommunicator : MonoBehaviour
{
    
    
    public void SendMessage(string text)
    {
        Debug.Log("Sent message");
        string t = text;
        Flowchart.BroadcastFungusMessage(t);
    }




}
