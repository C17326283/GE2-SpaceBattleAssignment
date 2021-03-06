using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

/* combat behaviour for ships fighting */
public class CombatBehaviour : BaseShipBehaviour
{
    public Transform enemyTarget;
    public Vector3 divertTarget = Vector3.zero;
    [HideInInspector]
    public GameObject shipsHolder;//Object holding all ships for easy searching
    
    public String[] enemyTags;
    public float engagementDistance = 200.0f;

    public float divertDistance = 50;
    public float arrivedDist = 20;


    private void Start()
    {
        shipsHolder = GameObject.Find("-ActiveShips-");
    }
    
    /* call add force towards enemy*/
    [Task]
    public void PursueEnemy()
    {
        base.shipBoid.ArriveForce(enemyTarget.position,divertDistance,arrivedDist);
        //Vector3 desired = enemyTarget.position - transform.position;
        //desired.Normalize();
        //desired *= maxSpeed;
        
//        print("shipBoid "+base.shipBoid+",desired"+desired);
        //base.shipBoid.AddToForce(desired,2);
        Task.current.Succeed();
    }
    
    /* go to a divert point to get ready for attack run*/
    [Task]
    public void Diverting()
    {
        //does need to divert but no divert point so get one
        if (divertTarget == Vector3.zero)
        {
            //get a point around enemy to fly back to
            divertTarget = enemyTarget.position+(Random.onUnitSphere * (divertDistance*2));
        }
        
        if (Vector3.Distance(divertTarget, transform.position)>arrivedDist)
        {
            Vector3 desired = divertTarget - transform.position;
            desired.Normalize();
            //desired *= maxSpeed;
        
//            print("shipBoid "+base.shipBoid+",desired"+desired);
            base.shipBoid.AddToForce(desired,2);
            Task.current.Succeed();
        }
        else//reached
        {
            divertTarget = Vector3.zero;
            Task.current.Fail();
        }
        
    }
    
    /* needs to get a point to divert to*/
    [Task]
    public void NeedsToDivertCondition()
    {
        //if too close to enemy or in the middle of a divert
        if (enemyTarget && Vector3.Distance(enemyTarget.position, transform.position) < divertDistance || divertTarget!=Vector3.zero)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    
    /*get target from an enemy in world*/
    [Task]
    public void GetEnemyTarget()
    {
        //avoid contantly getting enamy so hold onto previous one if they are still valid
        if (enemyTarget!=null && Vector3.Distance(enemyTarget.transform.position, transform.position) < engagementDistance &&
            enemyTarget.GetComponent<Life>() &&enemyTarget.GetComponent<Life>().currentHealth > 0)
        {
            Task.current.Succeed();
        }
        else
        {
            bool foundTarget = false;
            float closestDist = Mathf.Infinity;
        
            foreach (Transform ship in shipsHolder.transform) //search all immediate children of shipholder
            {
                foreach (var tag in enemyTags)//matches enemy tags
                {
                    if (ship.transform.CompareTag(tag) && ship.gameObject.activeInHierarchy && ship != transform &&
                        Vector3.Distance(ship.transform.position, transform.position) < engagementDistance)
                    {
                        float curDist = Vector3.Distance(ship.transform.position, transform.position);
                        if (curDist < closestDist)
                        {
                            enemyTarget = ship.transform;
                            closestDist = curDist;
                            foundTarget = true;
                        }
                    }
                }
            }
            
            if(foundTarget == true)
                Task.current.Succeed();
            else
            {
                enemyTarget = null;
                Task.current.Fail();
            }
                
        }
    }
}
