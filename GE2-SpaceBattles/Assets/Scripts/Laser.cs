using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{
    void Start()
    {
        //rockets explode before death
        Invoke("DestroyObj",Random.Range(minDestroyTime,maxDestroyTime));//explode before normal 
    }
}
