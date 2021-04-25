using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoid : MonoBehaviour
{
    
    public Vector3 forceToApply;//The force thats added to multiple times before its calculated each frame
    public Rigidbody rb;

    public float moveSpeed;
    public float maxMag;

    private Vector3 force;

    public float bankingAmount = 0.1f;
    public float turnSpeed = 0.1f;
    
    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, rb.transform.InverseTransformDirection(forceToApply.normalized));
    }

    private void Start()
    {
        if (!rb)
            rb = GetComponentInChildren<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyForce();
        AimAtMag();
    }

    public void AddToForce(Vector3 forceToAdd,float multiplier)
    {
        forceToApply = forceToApply + (forceToAdd*multiplier);
    }

    public void ApplyForce()
    {
        
        force = forceToApply.normalized * moveSpeed;
        
        
        rb.AddForce(force * (Time.deltaTime * 100));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMag);//clamp

        forceToApply = Vector3.zero;//reset
    }

    public void AimAtMag()
    {
        Vector3 velocity = rb.velocity;
        Vector3 acceleration = force / 1;//1 is temp mass
        
        
        if (velocity.magnitude > 0)
        {
            //transform.forward = velocity.normalized;
            Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up);
            //do it slowly over time
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, (turnSpeed)*Time.deltaTime);

            //Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * bankingAmount), Time.deltaTime * 3.0f);
            //transform.LookAt(transform.position + velocity, tempUp);
            //transform.LookAt(transform.position + velocity, transform.up);

            //transform.position += velocity * Time.deltaTime;
            //velocity *= (1.0f - (damping * Time.deltaTime));
        }
    }
}
