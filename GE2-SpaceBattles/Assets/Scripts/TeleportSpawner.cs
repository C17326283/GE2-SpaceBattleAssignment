using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

/* handles the spawning of groups without overlap and teleporting trail & spawn effect. */
public class TeleportSpawner : MonoBehaviour
{
    public GameObject shipToSpawn;

    public float radiusToSpawn = 50;

    public float amountToSpawn = 3;
    public GameObject spawnEffect;//todo
    public GameObject spawnTrail;//todo
    public float timeBetweenSpawn = .2f;
    public float spawnEffectScale = 2f;

    public GameObject activeShipsHolder;
    public Vector3 spawnOffset;

    private float startDist = 2000f;

    public float overlapSize = 30;
    public int noOverlapSpawnAttempts = 10;

    public void TriggerSpawn()
    {
        StartCoroutine(SpawnGroup());
    }

    IEnumerator SpawnGroup()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randPos = Random.insideUnitSphere * radiusToSpawn;//random in circle

            Vector3 randSpawnPos = randPos + this.transform.position +spawnOffset;//around center of obj
            
            //try get a pos where the ship can spawn without overlap
            int j = 0;
            while (CheckOverlap(randSpawnPos) && j<noOverlapSpawnAttempts)
            {
                randSpawnPos = randPos + this.transform.position +spawnOffset;//around center of obj

                j++;
            }
            //might timeout and spawn with an overlap if couldnt find a valid one
            
            
            //Spawn ship far away
            GameObject spawnedObj = GameObject.Instantiate(shipToSpawn, randSpawnPos, this.transform.rotation);
            spawnedObj.transform.forward = this.transform.forward;
            spawnedObj.transform.SetParent(activeShipsHolder.transform);

            //add trail but spawn it very far behind so it can zoom to pos for teleport effect
            GameObject spawnedTrail = GameObject.Instantiate(spawnTrail, spawnedObj.transform.position+(-transform.forward*startDist), this.transform.rotation,spawnedObj.transform);
            TrailRenderer trail = spawnedTrail.GetComponent<TrailRenderer>();//only call expensive method once
            trail.startWidth = spawnEffectScale*4;
            trail.startWidth = spawnEffectScale*4;
            Destroy(spawnedTrail,.1f);
            
            yield return new WaitForSeconds(0.001f);

            //zoom trail to spawn pos
            spawnedTrail.transform.position = spawnedObj.transform.position;
            //add spawn particle effect
            GameObject spawnedSfx = GameObject.Instantiate(spawnEffect, spawnedObj.transform.position, this.transform.rotation);
            spawnedSfx.transform.localScale = new Vector3(spawnEffectScale,spawnEffectScale,spawnEffectScale);
            Destroy(spawnedSfx,2);
            
            
            yield return new WaitForSeconds(timeBetweenSpawn);

        }
    }
    
    public bool CheckOverlap(Vector3 spawnPos)
    {
//        print("CheckOverlap");
        Vector3 overlapBox = new Vector3(overlapSize,overlapSize,overlapSize);
        Collider[] collidersOverlapping = new Collider[1];
        int numOfCollidersFound = Physics.OverlapBoxNonAlloc(spawnPos, overlapBox,collidersOverlapping,this.transform.rotation);

//        print("numOfCollidersFound"+numOfCollidersFound);
        if(numOfCollidersFound>0)
            return true;
        else
            return false;
    }

    
    
}
