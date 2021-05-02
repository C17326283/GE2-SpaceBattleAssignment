using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    public float continuousForce = 100f;
    
    void Start()
    {
        //rockets explode before death
        Invoke("Explode",destroyTime);//explode before normal 
    }
    public void FixedUpdate()
    {
        base.rb.AddForce(transform.forward*continuousForce,ForceMode.Force);
    }

    public void Explode()
    {
        GameObject.Instantiate(hitExplosion);
        base.DestroyObj();
    }
}
