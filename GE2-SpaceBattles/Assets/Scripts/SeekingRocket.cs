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
            Quaternion toTarget = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
            
            
            transform.rotation = Quaternion.Slerp( transform.rotation, toTarget, Time.deltaTime * turnSpeed );

        }

        if (rb)
        {
            base.rb.AddForce(transform.forward * (continuousForce * Time.deltaTime),ForceMode.Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVel);//prevent goinf too fast
        }
        

    }

    public float GetAngleToTarget()
    {
        if (target && target.gameObject.activeInHierarchy)
        {
            Vector3 toTarget = (target.transform.position - transform.position).normalized;

            float angleToTarget = Vector3.Angle(transform.forward, toTarget);
        
            return angleToTarget; //returns value between 0 and 180 based on angle to sun
        }
        else
        {
            return Mathf.Infinity;//no target
        }
    }
    

}
