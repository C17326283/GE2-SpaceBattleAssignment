using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class FollowBehaviours : BaseBehaviours
{
    public ShipBoid shipBoid;
    public Transform target;
    public GameObject shipsHolder;//Object holding all ships for easy searching
    
    public String[] tagsToShoot;

    public float slowingDistance = 40.0f;
    public float stoppingDistance = 20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        shipBoid = GetComponent<ShipBoid>();
        shipsHolder = GameObject.Find("-ActiveShips-");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Task]
    public void Arrive()
    {
        if (Vector3.Distance(target.position,transform.position)<stoppingDistance)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
        
    }

    [Task]
    public void HasActiveTarget()
    {
        if (target && target.gameObject.activeInHierarchy)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
        
    }
    
    [Task]
    public void GetTarget()
    {
        Transform[] allShips = shipsHolder.GetComponentsInChildren<Transform>();//all ships in the holder
        foreach (Transform ship in allShips)
        {
            foreach (var tag in tagsToShoot)
            {
                if (ship.transform.CompareTag(tag) && ship!=transform)
                {
                    print("set target bt");
                    target = ship.transform;
                    Task.current.Succeed();
                    return;//exit once found
                }
            }
        }
        print("no t bt");
        Task.current.Fail();//only gets here if no target
    }
    
    [Task]
    public void SeekForce()
    {
        Vector3 desired = target.position - transform.position;
        desired.Normalize();
        //desired *= maxSpeed;
        
        shipBoid.AddToForce(desired,2);
        Task.current.Succeed();
    }
    
    

}
