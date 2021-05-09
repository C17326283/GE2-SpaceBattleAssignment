using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseBehaviours2 : MonoBehaviour
{
    public ShipBoid shipBoid;
    public Transform targetEnemy;
    public Transform targetPos;
    public GameObject shipsHolder;//Object holding all ships for easy searching
    
    

    public String followObjName;
    public GameObject followObj;

    public Vector3 followOffset; 
    public float followOffsetDistance;

    
    // Start is called before the first frame update
    void Start()
    {
        shipBoid = GetComponent<ShipBoid>();
        shipsHolder = GameObject.Find("-ActiveShips-");
    }
    
    [Task]
    public void GetTargetOffset()
    {
        if (GameObject.Find(followObjName))
        {
            followObj = GameObject.Find(followObjName);
            followOffset = Random.onUnitSphere * followOffsetDistance;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    public void OffsetPursuit()
    {
        if (followObj)
        {
            Vector3 targetOffset = followObj.transform.position + followObj.transform.InverseTransformDirection(followOffset);
            Vector3 desired = targetOffset - transform.position;
            desired.Normalize();
            //desired *= maxSpeed;

            shipBoid.AddToForce(desired, 2);
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
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
