using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSequence : MonoBehaviour
{
    public String nameOfTriggerObj;
    
    public UnityEvent triggerEvent;

    public SequenceManager sequenceManager;
    public int elementOfTriggerSequence;
    
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == nameOfTriggerObj)
        {
            sequenceManager.events[elementOfTriggerSequence].Invoke();
        }
    }
}
