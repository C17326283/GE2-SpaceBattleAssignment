using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{
    
    public String[] enemyTags;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude > 0.1f) //is moving and not stationary
        {
            foreach (var tag in enemyTags)
            {
                if (other.transform.CompareTag(tag) && other.gameObject.GetComponent<Life>())
                {
                    other.gameObject.GetComponent<Life>().currentHealth -= 1000;
                }
            }
        }
        
        
    }
}
