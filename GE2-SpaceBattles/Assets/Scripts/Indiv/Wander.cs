using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : SteeringBehaviour
{
    public Vector3 target = Vector3.zero;
    public float timeToWait = 1;
    public float radius = 50;

    private void Start()
    {
        StartCoroutine(ChangeTarget());
    }

    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);    
    }

    
    IEnumerator ChangeTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToWait);
            target = Random.insideUnitSphere * radius;
        }
        
    }
}
