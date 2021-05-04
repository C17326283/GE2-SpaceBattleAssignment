using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatBehaviours : BaseShipBehaviour
{
    public Transform targetEnemy;
    
    [Task]
    public void TargetEnemy()
    {
        
        Vector3 desired = targetEnemy.position - transform.position;
        desired.Normalize();
        //desired *= maxSpeed;
        
        shipBoid.AddToForce(desired,2);
        Task.current.Succeed();
    }

    [Task]
    public void Pursuit()
    {
        
        Vector3 desired = targetEnemy.position - transform.position;
        desired.Normalize();
        //desired *= maxSpeed;
        
        shipBoid.AddToForce(desired,2);
        Task.current.Succeed();
    }
}
