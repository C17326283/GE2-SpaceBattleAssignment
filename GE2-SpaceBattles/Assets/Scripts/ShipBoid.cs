using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateForce();
        ApplyForce();
        AimAtMag();
        
    }
    
    public void SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= moveSpeed;

        AddToForce(desired,2);
    }

    public void ArriveForce(Vector3 target, float slowingDistance = 100.0f)
    {
        Vector3 toTarget = target - transform.position;

        float distance = Vector3.Distance(target,transform.position);
        
        if (distance > slowingDistance/100)//dont apply if super close
        {        
            float ramped = 500 * (distance / slowingDistance);

            float clamped = Mathf.Min(ramped, 500);
            Vector3 desired = clamped * (toTarget / distance);

            AddToForce(desired,2);
        }  
    }

    //separated so can be called in special cases as well as from seek or arrive force
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
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * bankingAmount), Time.deltaTime * turnSpeed);

            transform.LookAt(transform.position+rb.velocity, tempUp);
        }
    }
}
