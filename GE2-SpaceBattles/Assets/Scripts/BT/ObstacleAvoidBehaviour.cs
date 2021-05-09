using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class ObstacleAvoidBehaviour : BaseShipBehaviour
{
    public float feelerLength = 300;
    public float rayAngle = 45;

    public float strengthMultiplier = 2;
    
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Quaternion shipRot = transform.rotation;
        Quaternion rotOffset = Quaternion.AngleAxis(rayAngle,transform.up);
        Quaternion rayQuat = shipRot * rotOffset;//added quaternions
        Vector3 rayDir = rayQuat*Vector3.forward;//convert quaternion to vector3
        Gizmos.DrawRay(this.transform.position,rayDir);
    }*/

    // Start is called before the first frame update
    void Start()
    {
    }
    
    [Task]
    public void AttemptAvoidance()
    {
        
        ShootRay(0, transform.up, -transform.forward, feelerLength);
        ShootRay(-rayAngle, transform.up, transform.right, feelerLength);
        ShootRay(rayAngle, transform.up, -transform.right, feelerLength);
        ShootRay(-rayAngle, transform.right, -transform.up, feelerLength);
        ShootRay(rayAngle, transform.right, transform.up, feelerLength);

        Task.current.Succeed();
    }


    public void ShootRay(float angle, Vector3 RotAround, Vector3 forceToAddDirection, float length)
    {
        Quaternion shipRot = transform.rotation;
        Quaternion rotOffset = Quaternion.AngleAxis(angle,RotAround);
        Quaternion rayQuat = shipRot * rotOffset;//added quaternions
        Vector3 rayDir = rayQuat*Vector3.forward;//convert quaternion to vector3
       // Debug.DrawRay(transform.position, rayDir, Color.yellow,length);
        
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDir, out hit, length))
        {
            //Debug.DrawRay(transform.position, rayDir, Color.red,length);
            //Debug.DrawLine(transform.position, hit.point, Color.red,5);
            
            shipBoid.AddToForce(forceToAddDirection*shipBoid.moveSpeed,strengthMultiplier);

            //print("avoiding object"+forceToAddDirection*shipBoid.moveSpeed);
        }
        
    }
}
