using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This has a continuous force forward as it moves. These will also explode after a set amount of time if it doesnt hit anything.*/
public class Rocket : Projectile
{
    public float continuousForce = 100f;
    public float explosionSize =2;
    public float maxVel = 100;
    
    void Awake()
    {
        //rockets explode before death
        Invoke("Explode",Random.Range(minDestroyTime,maxDestroyTime));//explode before normal 
    }
    public void FixedUpdate()
    {
        base.rb.AddForce(transform.forward*continuousForce,ForceMode.Force);
    }

    public void Explode()
    {
        GameObject explosion = GameObject.Instantiate(hitExplosion);
        explosion.transform.localScale = new Vector3(explosionSize,explosionSize,explosionSize);
        explosion.transform.position = this.transform.position;
        base.DestroyObj();
    }
}
