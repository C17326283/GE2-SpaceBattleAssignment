using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    public float fireRate = 0.2f;

    public String tagToShoot = "Enemy";

    public bool readyToShoot;

    public bool aimingAtTarget;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatShooting());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator RepeatShooting()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
            {
                if (hit.transform.CompareTag(tagToShoot))
                {
                    GameObject spawned = Instantiate(prefab);
                    spawned.transform.rotation = this.transform.rotation;
                    spawned.transform.position = this.transform.position;
                }
            
            }
            
        }
        
    }
}
