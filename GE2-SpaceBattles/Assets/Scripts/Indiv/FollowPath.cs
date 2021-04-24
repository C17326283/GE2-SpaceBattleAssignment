using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehaviour {

    public PathManage path;

    Vector3 nextWaypoint;

    public float waypointDistance = 5;

    public string spawnedPathName;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, nextWaypoint);
        }
    }

    public void Start()
    {
        if (path == null) //No path on start so spawned ones need to dynamically assign it
        {
            path = GameObject.Find(spawnedPathName).GetComponent<PathManage>();
        }
    }

    public override Vector3 Calculate()
    {
        nextWaypoint = path.NextWaypoint();
        if (Vector3.Distance(transform.position, nextWaypoint) < waypointDistance)
        {
            path.AdvanceToNext();
        }

        if (!path.looped && path.IsLast())
        {
            return boid.ArriveForce(nextWaypoint, 3);
        }
        else
        {
            return boid.SeekForce(nextWaypoint);
        }
    }
}
