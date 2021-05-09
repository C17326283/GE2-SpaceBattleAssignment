using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

/* follow each point in the path behaviour and methods to control what point*/
public class PathFollowBehaviour : BaseShipBehaviour
{
    //public PathManage path;
    public PointManager path;

    public float waypointDistance = 50;
    public float arriveSlowDist = 200f;

    public string spawnedPathName;
    
    public void Start()
    {
        if (path == null) //No path on start so spawned ones need to dynamically assign it
        {
            path = GameObject.Find(spawnedPathName).GetComponent<PointManager>();
        }
    }
    
    /* the current target is the last point*/
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
    
    /* has finished path so stop*/
    [Task]
    public void Stop()
    {
        Task.current.Succeed();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PandaBehaviour>().enabled = false;
    }
    
    /* hit waypoint*/
    [Task]
    public void IsAtWaypointCondition()
    {
        if (Vector3.Distance(transform.position, path.GetCurrentPoint().position) < waypointDistance)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    /* target the next waypoint*/
    [Task]
    public void GetNextWaypoint()
    {
//        print("get next checkpoint");
        path.AdvanceToNext();

        if (path.GetCurrentPoint().position != null)
        {
            if (path.IsLast())
            {
                GetComponent<Rigidbody>().drag *= 2;//increase drag for better stopping control to not overshoot
                GetComponent<Rigidbody>().mass *= 2;//increase mass for better stopping control to not overshoot
            }
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }
    
    /* add force toward target*/
    [Task]
    public void SeekNextPoint()
    {
        if (path.IsLast())
        {
            base.shipBoid.ArriveForce(path.GetCurrentPoint().position,arriveSlowDist,waypointDistance);
        }
        else
        {
            base.shipBoid.SeekForce(path.GetCurrentPoint().position);
        }
        
        
        //Vector3 desired = nextWaypoint - transform.position;
        //desired.Normalize();

        //shipBoid.AddToForce(desired, 2);
        Task.current.Succeed();
        
    }
}
