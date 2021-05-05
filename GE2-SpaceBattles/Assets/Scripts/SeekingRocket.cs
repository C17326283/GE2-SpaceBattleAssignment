using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingRocket : Rocket
{
    public Transform target;
    public float maxSeekingAngle = 90;
    public float turnSpeed = 10;
    public void FixedUpdate()
    {
        if (GetAngleToTarget() < maxSeekingAngle)
        {
            Quaternion toTarget = Quaternion.LookRotation(target.transform.position - transform.position);
            
            
            transform.rotation = Quaternion.Slerp( transform.rotation, toTarget, Time.deltaTime * turnSpeed );

        }
        base.rb.AddForce(transform.forward*continuousForce,ForceMode.VelocityChange);

    }

    public float GetAngleToTarget()
    {
        Vector3 toTarget = (target.transform.position - transform.position).normalized;

        float angleToTarget = Vector3.Angle(transform.forward, toTarget);
        
        return angleToTarget; //returns value between 0 and 180 based on angle to sun
    }
    

}
