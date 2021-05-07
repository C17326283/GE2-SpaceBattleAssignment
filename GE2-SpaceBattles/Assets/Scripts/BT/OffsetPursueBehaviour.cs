using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

public class OffsetPursueBehaviour : BaseShipBehaviour
{
    public String followObjName;
    public GameObject followObj;

    public Vector3 followOffset; 
    public float followOffsetDistance;

    public float maxDistAway = 1000;

    [Task]
    public void GetTargetOffset()
    {
        if (GameObject.Find(followObjName))
        {
            followObj = GameObject.Find(followObjName);
            followOffset = Random.onUnitSphere * followOffsetDistance;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    [Task]
    public void ShouldFightCondition()
    {
        if (followObj)
        {
            //WithinOriginDistanceCondition
            if (Vector3.Distance(followObj.transform.position, transform.position) < followOffsetDistance * 1.5f)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    public void OffsetPursuit()
    {
        if (followObj)
        {
            Vector3 targetOffset = followObj.transform.position + followObj.transform.InverseTransformDirection(followOffset);
            Vector3 desired = targetOffset - transform.position;
            desired.Normalize();
            //desired *= maxSpeed;

            shipBoid.AddToForce(desired, 2);
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
