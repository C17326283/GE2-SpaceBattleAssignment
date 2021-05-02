using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{
    public float startForce = 1000;
    void Start()
    {
        //lasers have initial force
        rb.AddForce(transform.forward*startForce,ForceMode.Impulse);
        //rockets explode before death
        Invoke("DestroyObj",destroyTime);//explode before normal 
    }
}
