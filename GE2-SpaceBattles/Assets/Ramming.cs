using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{
    
    public String[] enemyTags;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
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
