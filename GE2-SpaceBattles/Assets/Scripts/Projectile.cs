using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force = 1000;

    private Rigidbody rb;

    public float hitInFrontDistance = 50f;

    public float hitDamage = 5;

    public Vector3 lastPos;

    public float destroyTime = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*force,ForceMode.Acceleration);
        Destroy(this.gameObject,destroyTime);
    }
    
    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward*hitInFrontDistance));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 bulletDir = ( lastPos - transform.position ).normalized;
        float moveLen = Vector3.Distance(lastPos,transform.position);
        RaycastHit hit;
        if (Physics.Raycast(lastPos, bulletDir, out hit, moveLen))
        {
            //try get a life script reference
            Life otherLife = CheckParentLifeRecursive(hit.transform.gameObject);
            
            if (otherLife!=null)
            {
                print("hit "+hit.transform.name);
                otherLife.currentHealth -= hitDamage;
                transform.position = hit.point;
                
                rb.isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;

            }
            else
            {
//                print("no life hit "+hit.transform.name);
            }
        }
        lastPos = transform.position;
        
    }

    public Life CheckParentLifeRecursive(GameObject objToCheck)
    {
        if (objToCheck.GetComponent<Life>() != null)
        {
            //found life obj
            return objToCheck.GetComponent<Life>();
        }
        else if(objToCheck.transform.parent != null && objToCheck.transform.parent.transform.CompareTag(objToCheck.transform.tag))//check parent if its still obj
        {
            //check parent and pass result back
            return CheckParentLifeRecursive(objToCheck.transform.parent.gameObject);
        }
        else
        {
            //found nothing
            return null;
        }
    }
    
    
}
