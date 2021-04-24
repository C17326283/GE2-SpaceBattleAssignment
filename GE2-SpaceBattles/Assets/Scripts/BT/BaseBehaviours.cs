using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class BaseBehaviours : MonoBehaviour
{
    public ShipBoid shipBoid;
    public Transform target;
    public GameObject shipsHolder;//Object holding all ships for easy searching
    
    public String[] tagsToShoot;
    
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
        if (target && target.gameObject.activeInHierarchy)
        {
            Vector3 desired = target.position - transform.position;
            desired.Normalize();
            //desired *= maxSpeed;
        
            shipBoid.AddToForce(desired);
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
        
    }
}
