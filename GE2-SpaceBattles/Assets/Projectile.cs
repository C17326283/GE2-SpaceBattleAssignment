using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force = 1000;

    private Rigidbody rb;
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
        
    }
    
    
}
