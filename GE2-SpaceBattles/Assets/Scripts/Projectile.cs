using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public LayerMask projectileLayerToAvoid;

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
        //projectileLayerToAvoid = 1 << 6;//set to only avoid layer 5
        
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
                    HitObj(hit.transform.gameObject, hit.transform.position);
            }
        }

        lastPos = transform.position;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //HitObj(other.transform.gameObject);
    }

    public void HitObj(GameObject hitGameObject, Vector3 hitPos)
    {
//        print("hitobj: "+hitGameObject);
        //stop obj but let trail stay 
        rb.isKinematic = true;
        
//        print(transform.name+" hit "+hitGameObject.transform.name);
        GameObject explosion = GameObject.Instantiate(hitExplosion);
        explosion.transform.position = this.transform.position;
        explosion.transform.localScale = new Vector3(hitExplosionSize,hitExplosionSize,hitExplosionSize);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        
        //try get a life script reference
        Life otherLife = CheckHitParentLifeRecursive(hitGameObject.transform.gameObject);
            
        if (otherLife!=null)
        {
            otherLife.currentHealth -= hitDamage;
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
        Destroy(this.gameObject);
    }
    
    
}
