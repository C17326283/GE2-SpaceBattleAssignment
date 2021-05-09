using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  There are multiple projectiles that act differently that all pull from the projectile class */
public class Projectile : MonoBehaviour
{
    
    public float hitDamage = 5;
    public float hitInFrontDistance = 500f;
    public float minDestroyTime = 9;
    public float maxDestroyTime = 11;

    public Rigidbody rb;
    public Vector3 lastPos;

    public GameObject hitExplosion;
    public float hitExplosionSize = 10;
    
    public float startForce = 10;
    public String parentShipTag;
    
    public String[] enemyTags;


    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward*hitInFrontDistance));
        
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        //lasers have initial force
        rb.AddForce(transform.forward*startForce,ForceMode.Impulse);
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 bulletDir = ( lastPos - transform.position ).normalized;
        float moveLen = Vector3.Distance(lastPos,transform.position);
        
        RaycastHit hit;
        if (Physics.Raycast(lastPos, bulletDir, out hit, moveLen + hitInFrontDistance))
        {
            foreach (var tag in enemyTags)
            {
                if (hit.transform.CompareTag(tag)) //dont hit self
                    HitObj(hit.transform.gameObject, hit.point);
            }
        }

        lastPos = transform.position;
        
    }

    /* actual hit */
    public void HitObj(GameObject hitGameObject, Vector3 hitPos)
    {
        //stop obj but let trail stay 
        rb.isKinematic = true;
        
//        print(transform.name+" hit "+hitGameObject.transform.name);
        GameObject explosion = GameObject.Instantiate(hitExplosion);
        explosion.transform.position = hitPos;
        explosion.transform.localScale = new Vector3(hitExplosionSize,hitExplosionSize,hitExplosionSize);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        
        Life otherLife = hitGameObject.transform.gameObject.GetComponent<Life>();
        //Life otherLife = CheckHitParentLifeRecursive(hitGameObject.transform.gameObject);
            
        if (otherLife!=null)
        {
            otherLife.currentHealth -= hitDamage;
        }
    }

    //try get a life script reference, this is deprecated but may be useful again
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
        Destroy(this.gameObject);
    }
    
    
}
