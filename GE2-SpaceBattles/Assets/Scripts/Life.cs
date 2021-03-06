using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*  handles the health, turning off of ai and the explosions on death. */
public class Life : MonoBehaviour
{

    public float maxHealth = 200;

    public float currentHealth = 200;

    public GameObject DeathExplosion;

    private float explosionForce = 200f;

    public bool alive = true;//for calling death once

    public float explosionSize = 5f;

    public float timeToDespawnAfterDeath = 20f;

    public bool breakUpChildObjsOnDeath = false;
    public DebrisSpawner debrisSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //currentHealth -= 0.2f;//auto kill for testing
        if (currentHealth < .001f)
        {
            if(alive)
                Die();
        }
    }

    public void Die()
    {
        alive = false;
        GameObject explosion = GameObject.Instantiate(DeathExplosion, transform.position, this.transform.rotation);
        explosion.transform.localScale = new Vector3(explosionSize,explosionSize,explosionSize);
        gameObject.GetComponent<ShipBoid>().enabled = false;
        ExplodeParts();
        
        this.enabled = false;
        Destroy(this.gameObject, timeToDespawnAfterDeath);
    }
    
    /* split objects or spawn debris*/
    private void ExplodeParts()
    {
        if (breakUpChildObjsOnDeath)
        {
            foreach (Transform tran in this.GetComponentsInChildren<Transform>())
            {
                Rigidbody tranRb = tran.gameObject.GetComponent<Rigidbody>();
                if (tranRb == null)
                {
                    tranRb = tran.gameObject.AddComponent<Rigidbody>();
                }
                tranRb.transform.SetParent(transform);
                tranRb.velocity = new Vector3(Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce));
                
            }
        }
        
        if(debrisSpawner)
            debrisSpawner.SpawnDebris();
    }
}
