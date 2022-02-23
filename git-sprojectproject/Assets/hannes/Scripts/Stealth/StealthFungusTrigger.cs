using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Fungus;

public class StealthFungusTrigger : MonoBehaviour
{
    private FlowchartCommunicator _flowchartCommunicator;
    [Tooltip("Message sent to the flowchart")]
    public string Message;

    private StealthHandler _stealthPointSaver;
    
    private void Start()
    {
        _stealthPointSaver = GameObject.FindWithTag("StealthHandler").GetComponent<StealthHandler>();
        _flowchartCommunicator = GetComponent<FlowchartCommunicator>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StealthPlayer"))
        {
            _flowchartCommunicator.SendMessage(Message);
            _stealthPointSaver.NuggetPlus();
            Destroy(this.gameObject);
        }
    }
}

