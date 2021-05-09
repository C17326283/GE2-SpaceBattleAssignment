using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The behaviours add forces to the ship boid script which handles applying the actual forces that are applied to the rigidbody as well as the turning & banking.*/
public class ShipBoid : MonoBehaviour
{
    
    public Vector3 forceToApply;//The force thats added to multiple times before its calculated each frame
    public Rigidbody rb;

    public float moveSpeed;
    
    //public float maxSpeed;
    public float maxMag;

    private Vector3 force;
    private Vector3 acceleration;

    public float bankingAmount = 0.1f;
    public float turnSpeed = 3f;
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
        rb.AddForce(transform.forward);//add tiny bit force forward on start so it doesnt spin
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateForce();
        ApplyForce();
        AimAtMag();
        
    }
    
    /* go directly to target */
    public void SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= moveSpeed;

        AddToForce(desired,2);
    }

    /* go to target but add backward force as it approaches to slow it down*/
    public void ArriveForce(Vector3 target, float slowingDistance, float stoppingDist)
    {
        Vector3 toTarget = target - transform.position;

        float distance = toTarget.magnitude-stoppingDist;//account for dist they actually stop at
        
        if (distance > 0)
        {        
            float ramped = maxMag * (distance / slowingDistance);

            float clamped = Mathf.Min(ramped, maxMag);
            Vector3 desired = clamped * (toTarget / distance);

            AddToForce(desired-rb.velocity,2);
        }  
    }

    //called from behaviours to build on the force thats applied in calculate force
    public void AddToForce(Vector3 forceToAdd,float mult)
    {
        forceToApply = forceToApply + (forceToAdd*mult);
    }

    //add the force based on the built up calculations
    public void CalculateForce()
    {
        force = forceToApply.normalized * moveSpeed;
        force = force * (Time.deltaTime * 100);
        
        forceToApply = Vector3.zero;//reset
    }
    
    //apply the force to the rigidbody
    public void ApplyForce()
    {
        Vector3 newAcceleration = force / mass;//1 is temp mass
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);
        
        rb.AddForce(acceleration,ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMag);//clamp

    }

    //turn to look at the direction the ship is going
    public void AimAtMag()
    {
        if (rb.velocity.magnitude > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * bankingAmount), Time.deltaTime * turnSpeed);

            transform.LookAt(transform.position+rb.velocity, tempUp);
        }
    }
    
}
