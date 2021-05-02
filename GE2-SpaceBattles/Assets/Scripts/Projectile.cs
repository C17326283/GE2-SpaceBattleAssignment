using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public float hitDamage = 5;
    public float hitInFrontDistance = 50f;
    public float destroyTime = 10;

    public Rigidbody rb;
    public Vector3 lastPos;

    public GameObject hitExplosion;


    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward*hitInFrontDistance));
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 bulletDir = ( lastPos - transform.position ).normalized;
        float moveLen = Vector3.Distance(lastPos,transform.position);
        
        RaycastHit hit;
        if (Physics.Raycast(lastPos, bulletDir, out hit, moveLen))
        {
            HitObj(hit);
        }
        lastPos = transform.position;
        
    }

    public void HitObj(RaycastHit hit)
    {
        //try get a life script reference
        Life otherLife = CheckHitParentLifeRecursive(hit.transform.gameObject);
            
        if (otherLife!=null)
        {
            print("hit "+hit.transform.name);
            otherLife.currentHealth -= hitDamage;
            transform.position = hit.point;
            GameObject.Instantiate(hitExplosion);
                
            rb.isKinematic = true;
            GetComponent<MeshRenderer>().enabled = false;

        }
        else
        {
//                print("no life hit "+hit.transform.name);
        }
    }

    public Life CheckHitParentLifeRecursive(GameObject objToCheck)
    {
        if (objToCheck.GetComponent<Life>() != null)
        {
            //found life obj
            return objToCheck.GetComponent<Life>();
        }
        else if(objToCheck.transform.parent != null && objToCheck.transform.parent.transform.CompareTag(objToCheck.transform.tag))//check parent if its still obj
        {
            //check parent and pass result back
            return CheckHitParentLifeRecursive(objToCheck.transform.parent.gameObject);
        }
        else
        {
            //found nothing
            return null;
        }
    }

    public void DestroyObj()
    {
        CancelInvoke();
        Destroy(this);
    }
    
    
}
