using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    public float shootAttemptRate = 0.01f;
    public float shotCooldown = 0.2f;
    public float shootAngle = 30;

    public bool readyToShoot;

    public GameObject target;

    public Vector3 toTarget;
    

    public float maxAccuracyOffset = .1f;
    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        StartCoroutine(RepeatShooting());
    }
    


    public float GetAngleToTarget()
    {
        if (target != null && target.activeInHierarchy)
        {
            toTarget = (target.transform.position-transform.position).normalized;
            
            float angleToTarget = Vector3.Angle(transform.forward, toTarget);
            return angleToTarget;//returns value between 0 and 180 based on angle to sun
        }
        else
        {
            return Mathf.Infinity;//no target
        }
    }

    IEnumerator RepeatShooting()
    {
        while (true)
        {
//            print("repeast shooting");
            if (GetAngleToTarget() < shootAngle && readyToShoot)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toTarget, out hit, Vector3.Distance(target.transform.position,transform.position)))
                {
//                    print("direct line of sight, shoot");
                    Shoot();
                    StartCoroutine(Reload());
                }
            }
            yield return new WaitForSeconds(shootAttemptRate);
        }
    }

    public void Shoot()
    {
        Vector3 toTarget = (target.transform.position-transform.position).normalized;
        
        float randX = Random.Range(-maxAccuracyOffset, maxAccuracyOffset);
        float randY = Random.Range(-maxAccuracyOffset, maxAccuracyOffset);
        toTarget.x += randX;
        toTarget.y += randY;
        GameObject spawned = Instantiate(prefab);

        
        spawned.transform.forward = toTarget ;
        spawned.transform.position = this.transform.position;
    }

    IEnumerator Reload()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        readyToShoot = true;
    }
}
