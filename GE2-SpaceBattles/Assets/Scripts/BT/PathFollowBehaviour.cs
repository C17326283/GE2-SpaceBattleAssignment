using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathFollowBehaviour : BaseShipBehaviour
{
    public PathManage path;

    public float waypointDistance = 50;
    public float arriveSlowDist = 200f;

    public string spawnedPathName;
    
    public void Start()
    {
        if (path == null) //No path on start so spawned ones need to dynamically assign it
        {
            path = GameObject.Find(spawnedPathName).GetComponent<PathManage>();
        }
    }
    
    [Task]
    public void IsLastWaypoint()
    {
        if (path.IsLast())
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    [Task]
    public void Stop()
    {
        Task.current.Succeed();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PandaBehaviour>().enabled = false;
    }
    
    [Task]
    public void IsAtWaypointCondition()
    {
        if (Vector3.Distance(transform.position, path.NextWaypoint()) < waypointDistance)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    [Task]
    public void GetNextWaypoint()
    {
//        print("get next checkpoint");
        path.AdvanceToNext();
        
        if(path.NextWaypoint()!=null)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }
    
    [Task]
    public void SeekNextPoint()
    {
        if (path.IsLast())
        {
            base.shipBoid.ArriveForce(path.NextWaypoint(),arriveSlowDist,waypointDistance);
        }
        else
        {
            base.shipBoid.SeekForce(path.NextWaypoint());
        }
        
        
        //Vector3 desired = nextWaypoint - transform.position;
        //desired.Normalize();

        //shipBoid.AddToForce(desired, 2);
        Task.current.Succeed();
        
    }
}
