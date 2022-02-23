using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Fungus;
using FMODUnity;

public class StealthFungusTrigger : MonoBehaviour
{
    private FlowchartCommunicator _flowchartCommunicator;

    public string Message;

    private StealthPointSaver _stealthPointSaver;
    
    private void Start()
    {
        _stealthPointSaver = GameObject.FindWithTag("StealthHandler").GetComponent<StealthPointSaver>();
        _flowchartCommunicator = GetComponent<FlowchartCommunicator>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StealthPlayer"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound/SFX/Minigames/Stealth/Vampire Ping");
            _stealthPointSaver.TriggerPlus();
            _flowchartCommunicator.SendMessage(Message);
            Destroy(this.gameObject);
        }
    }
}

