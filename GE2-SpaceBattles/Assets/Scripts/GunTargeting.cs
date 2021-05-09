using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* used by shooting to get a target thats infront of ship in cyclinder*/
public class GunTargeting : MonoBehaviour
{
    public float range = 3000;

    public float radius = 1000;

    private Vector3 centerOfStartSphere;
    private Vector3 centerOfEndSphere;
    
    public String[] enemyTags;

    public bool debugTargeting;
        public void OnDrawGizmos()
    {
        if (debugTargeting && isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(centerOfStartSphere,radius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(centerOfEndSphere,radius);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponentInParent<CombatBehaviour>() && enemyTags.Length == 0)//is a fighter and no specific tags set
            enemyTags = GetComponentInParent<CombatBehaviour>().enemyTags;//get the same enemy tags as combat behav
    }

    /* get everything overlapping in area*/
    public List<Collider> GetCollidersInArea()
    {
        centerOfStartSphere = transform.position+ (transform.forward * radius);
        centerOfEndSphere = transform.position + (transform.forward * range);

        Collider[] colliderArray = Physics.OverlapCapsule(centerOfStartSphere, centerOfEndSphere, radius);
        List<Collider> validTargetList = new List<Collider>();
        
        foreach (var collider in colliderArray)
        {
            foreach (var tag in enemyTags)
            {
                if (collider.transform.CompareTag(tag) && collider.GetComponent<Life>())
                {
                    validTargetList.Add(collider);
                }
            }
        }
        
        
        
        return validTargetList;
    }

    /* get specific target by calling the get overlap method and picking a random one*/
    public Transform GetTarget()
    {
        List<Collider> validTargetList = GetCollidersInArea();
        int listLen = validTargetList.Count;
//        print("got possible targets: "+listLen);
        if (listLen > 0)
        {
            int targetNum = Random.Range(0, listLen);
            return validTargetList[targetNum].transform; 
        }
        else
        {
            return null;
        }

        
    }
}
