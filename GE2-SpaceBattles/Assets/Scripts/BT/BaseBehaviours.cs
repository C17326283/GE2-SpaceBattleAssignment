using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseBehaviours : MonoBehaviour
{
    public ShipBoid shipBoid;
    public Transform target;
    public GameObject shipsHolder;//Object holding all ships for easy searching
    
    public String[] tagsToShoot;

    public float slowingDistance = 40.0f;
    public float stoppingDistance = 20.0f;
    
    public float engagementDistance = 20.0f;

    public GameObject defaultPosObj;

    public PathManage path;
    
    public Shooting gun;
    
    public GameObject wanderPoint;
    public float divertRadius = 20;

    
    // Start is called before the first frame update
    void Start()
    {
        shipBoid = GetComponent<ShipBoid>();
        shipsHolder = GameObject.Find("-ActiveShips-");
        gun = GetComponentInChildren<Shooting>();
        wanderPoint = new GameObject("wanderpoint-"+transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Task]
    public void HasArrived()
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
    public void Divert()
    {
        foreach (var tag in tagsToShoot)
        {
            if (target.transform.CompareTag(tag))
            {
                wanderPoint.transform.position = target.transform.position + (Random.onUnitSphere * divertRadius);
                target = wanderPoint.transform;
                Task.current.Succeed();
                return;
            }
        }
        Task.current.Fail();
    }
    
    /*
     * [Task]
    public void Divert()
    {
        if (target && target == wanderPoint)
        {
            wanderPoint.transform.position = target.transform.position + (Random.onUnitSphere * divertRadius);
            target = wanderPoint.transform;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
     */
    
    /*
    [Task]
    public void NextPath()
    {
        if (path!=null)
        {
            path.next
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
        
    }
    */
    [Task]
    public void GetPatrolTarget()
    {
        if (defaultPosObj)
        {
            target = defaultPosObj.transform;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    
    
    
    [Task]
    public void StopFollowing()
    {
        target = null;//remove target so no target to chase
        Task.current.Succeed();
        
    }

    [Task]
    public void HasActiveTarget()
    {
        if (target && target.gameObject.activeInHierarchy && Vector3.Distance(target.position,transform.position)<engagementDistance)
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
        //Transform[] allShips = shipsHolder.GetComponentsInChildren<Transform>();//all ships in the holder
        foreach (Transform ship in shipsHolder.transform)//search all immediate children
        {
            foreach (var tag in tagsToShoot)
            {
                if (ship.transform.CompareTag(tag) && ship!=transform && Vector3.Distance(ship.transform.position,transform.position)<engagementDistance)
                {
//                    print("set target bt");
                    target = ship.transform;
                    gun.target = target.gameObject;
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
