using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSequence : MonoBehaviour
{
    public String nameOfTriggerObj;
    
    public SequenceManager sequenceManager;
    public int elementOfTriggerSequence;


    private void Start()
    {
        if (sequenceManager == null)
            sequenceManager = GameObject.Find("Sequencemanager").GetComponent<SequenceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("trigger from "+other);
        if (other.transform.name == nameOfTriggerObj)
        {
            print("trigger match ");
            sequenceManager.events[elementOfTriggerSequence].Invoke();
            sequenceManager.curEvent = elementOfTriggerSequence+1;
            this.transform.gameObject.SetActive(false);
        }
    }
}
