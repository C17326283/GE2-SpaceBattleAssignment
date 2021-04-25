using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    public float shootAttemptRate = 0.01f;
    public float shotCooldown = 0.2f;
    public float shootAngle = 30;

    public String[] tagsToShoot;

    public bool readyToShoot;

    public bool aimingAtTarget;

    public GameObject target;


    public float rayDist = 10000f;
    // Start is called before the first frame update
    void Start()
    {
        rayDist = 10000f;
        readyToShoot = true;
        StartCoroutine(RepeatShooting());
    }
    

    // Update is called once per frame
    /*
    void Update()
    {
        //temp find target
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDist))
        {
//            print(hit.transform.name+", tag:"+hit.transform.tag);
            foreach (var tag in tagsToShoot)
            {
                if (hit.transform.CompareTag(tag))
                {
                    print("set target");
                    target = hit.transform.gameObject;
                }
            }
        }
    }
    */

    public float GetAngleToTarget()
    {
        if (target != null)
        {
//            print("get angle");
            Vector3 toTarget = (target.transform.position-transform.position).normalized;
            
            float angleToTarget = Vector3.Angle(transform.forward, toTarget);
//            print("angleToTarget "+angleToTarget);
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
//                print("shoot");
                Shoot();
                StartCoroutine(Reload());
            }
            yield return new WaitForSeconds(shootAttemptRate);
        }
    }

    public void Shoot()
    {
        Vector3 toTarget = (target.transform.position-transform.position).normalized;
        GameObject spawned = Instantiate(prefab);
        spawned.transform.forward = toTarget;
        spawned.transform.position = this.transform.position;
    }

    IEnumerator Reload()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        readyToShoot = true;
    }
}
