using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

/* follow a target object with an offset*/
public class OffsetPursueBehaviour : BaseShipBehaviour
{
    public String followObjName;
    public GameObject followObj;

    public Vector3 followOffset; 
    public float followOffsetDistance;
    public float arriveDist = 100;

    public float maxDistAway = 1000;

    public bool keepFirstTeleportOffset = true;

    public Vector3 followObjLastPos;

    private Vector3 targetWorldPos;

    /* get the offset of the target, random or set on teleport in*/
    [Task]
    public void GetTargetOffset()
    {
        if (GameObject.Find(followObjName))
        {
            if (keepFirstTeleportOffset)
            {
                followObj = GameObject.Find(followObjName);
                float forwardOffset = 50;//needs to be slightly in front to avoid being at point and turning at spawn
                followOffset = followObj.transform.InverseTransformPoint(transform.position+(followObj.transform.forward*forwardOffset));//get current pos relative to folowObj
                
                //followOffset = followObj.transform.position - transform.position;
                keepFirstTeleportOffset = false;//prevent from triggering again
            }
            else
            {
                followObj = GameObject.Find(followObjName);
                followOffset = Random.onUnitSphere * followOffsetDistance;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    /* check if the ship should actually try fight*/
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
    
    /* add the actual forces towards dir offset*/
    [Task]
    public void OffsetPursuit()
    {
        if (followObj)
        {
            targetWorldPos = followObj.transform.position + followObj.transform.InverseTransformDirection(followOffset);
            Vector3 desired = targetWorldPos - transform.position;
            desired.Normalize();
            //desired *= maxSpeed;

            //base.shipBoid.ArriveForce(targetWorldPos, arriveDist * 2, arriveDist);

            shipBoid.AddToForce(desired, 2);

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    /* need to get a new target point because either something changed or they arrived*/
    [Task]
    public void NeedNewTargetCondition()
    {
        if (followObj==null)
        {
            Task.current.Succeed();
        }
        else
        {
            //has arrived, check that the target is moving, if its not then it should be getting new points instead of flipping around one spot
            if (Vector3.Distance(targetWorldPos, transform.position)< arriveDist && followObjLastPos != followObj.transform.position)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
            
        }
    }
}
