using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* If the ship can get a target it will fire a shot in the direction of the enemy. */
[RequireComponent(typeof(GunTargeting))]
public class Shooting : MonoBehaviour
{
    public GameObject prefab;
    public float shootAttemptRate = 0.01f;
    public float shotCooldown = 0.2f;
    public float shootAngle = 30;

    public bool readyToShoot;

    public Transform target;

    public Vector3 toTarget;
    

    public float maxAccuracyOffset = .1f;

    public CombatBehaviour combatBehaviours;//use combat behaviour target if it has one

    public GunTargeting targeting;
    
    public String[] enemyTags;

    public AudioSource shootingSfx;

    
    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        StartCoroutine(RepeatShooting());
        if (combatBehaviours == null && GetComponentInParent<CombatBehaviour>() != null)
        {
            combatBehaviours = GetComponentInParent<CombatBehaviour>();
            
            enemyTags = GetComponentInParent<CombatBehaviour>().enemyTags;
        }

        if (shootingSfx == null)
            shootingSfx = GetComponent<AudioSource>();

        targeting = GetComponent<GunTargeting>();
    }
    
    /* repeat firing and try to get target*/
    IEnumerator RepeatShooting()
    {
        while (true)
        {
//            print("repeast shooting");
            if (GetAngleToTarget() < shootAngle && readyToShoot)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toTarget, out hit, Vector3.Distance(target.transform.position,transform.position)))
                {
//                    print("direct line of sight, shoot");
                    Shoot();
                    StartCoroutine(Reload());
                }
            }
            yield return new WaitForSeconds(shootAttemptRate);
        }
    }
    
    
    /* fire at the targets direction*/
    public void Shoot()
    {
        shootingSfx.pitch = Random.Range(shootingSfx.pitch-.2f, shootingSfx.pitch+.1f);//add variance to shooting noise
        shootingSfx.Play();
        Vector3 toTarget = (target.transform.position-transform.position).normalized;
        
        float randX = Random.Range(-maxAccuracyOffset, maxAccuracyOffset);
        float randY = Random.Range(-maxAccuracyOffset, maxAccuracyOffset);
        toTarget.x += randX;
        toTarget.y += randY;
        GameObject spawned = Instantiate(prefab);

        
        spawned.transform.forward = toTarget ;
        spawned.transform.position = this.transform.position;
        spawned.GetComponent<Projectile>().enemyTags = enemyTags;
        //spawned.GetComponent<Projectile>().parentShipTag = transform.parent.tag;//tag the projectile to avoid hitting own ships

        if (spawned.GetComponent<SeekingRocket>())
            spawned.GetComponent<SeekingRocket>().target = target;
    }

    /* find if the target is within the firing range angle*/
    public float GetAngleToTarget()
    {
        if (GetTarget() != null && target.gameObject.activeInHierarchy)
        {
            toTarget = (target.transform.position-transform.position).normalized;
            
            float angleToTarget = Vector3.Angle(transform.forward, toTarget);
            return angleToTarget;//returns value between 0 and 180 based on angle to sun
        }
        else
        {
            return Mathf.Infinity;//no target
        }
    }
    
    /* get the target from the targeting system*/
    public Transform GetTarget()
    {
        if (targeting)
        {
            target = targeting.GetTarget();
        }
        else
        {
            target = null;
        }

        return target;
    }


    /* disable weapon and renable after reload time*/
    IEnumerator Reload()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        readyToShoot = true;
    }
}
