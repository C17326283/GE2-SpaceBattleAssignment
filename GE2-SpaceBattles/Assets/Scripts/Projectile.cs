using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force = 1000;

    private Rigidbody rb;

    public float hitInFrontDistance = 20f;

    public float hitDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*force,ForceMode.Acceleration);
        Destroy(this.gameObject,20);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, hitInFrontDistance))
        {
            
            if (hit.transform.GetComponentInParent<Life>())
            {
                print("hit "+hit.transform.name);
                Life otherLife = hit.transform.GetComponentInParent<Life>();
                otherLife.currentHealth -= hitDamage;
            }
            else
            {
                print("no life hit "+hit.transform.name);
            }
        }
        
    }
    
    
}
