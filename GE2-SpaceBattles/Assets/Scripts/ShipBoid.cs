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
    private Vector3 acceleration;

    public float bankingAmount = 0.1f;
    public float turnSpeed = 0.1f;
    public float mass = 5;
    
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
        CalculateForce();
        ApplyForce();
        AimAtMag();
        
    }

    //add to the force that needs to be added on the update but compound them together
    public void AddToForce(Vector3 forceToAdd,float multiplier)
    {
        //forceToApply = forceToApply + (transform.InverseTransformDirection(forceToAdd)*multiplier);
        forceToApply = forceToApply + (forceToAdd*multiplier);
    }

    //add the force based on the built up calculations
    public void CalculateForce()
    {
        force = forceToApply.normalized * moveSpeed;
        force = force * (Time.deltaTime * 100);
        
        forceToApply = Vector3.zero;//reset
    }
    
    //add the force based on the built up calculations
    public void ApplyForce()
    {
        Vector3 newAcceleration = force / mass;//1 is temp mass
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);
        
        rb.AddForce(acceleration,ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMag);//clamp

    }

    public void AimAtMag()
    {
        if (rb.velocity.magnitude > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * bankingAmount), Time.deltaTime * 3.0f);

            transform.LookAt(transform.position+rb.velocity, tempUp);

            //Quaternion rotation = Quaternion.LookRotation(rb.velocity, tempUp);
            //do it slowly over time
            //rb.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, (turnSpeed)*Time.deltaTime);

        }
    }
}
