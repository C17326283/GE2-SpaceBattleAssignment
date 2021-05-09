using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* a projectile that shoots forward with initial force*/
public class Laser : Projectile
{
    
    /* destroy but dont explode*/
    void Awake()
    {
        //rockets explode before death
        Invoke("DestroyObj",Random.Range(minDestroyTime,maxDestroyTime));//explode before normal 
    }
}
