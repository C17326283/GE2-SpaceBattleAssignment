using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float startDist = 2000f;

    public void TriggerSpawn()
    {
        StartCoroutine(SpawnGroup());
    }

    IEnumerator SpawnGroup()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randPos = Random.insideUnitSphere * radiusToSpawn;//random in circle
            Vector3 randSpawnPos = randPos + this.transform.position;//around center of obj

            //Spawn ship far away
            GameObject spawnedObj = GameObject.Instantiate(shipToSpawn, randSpawnPos+(-transform.forward*startDist), this.transform.rotation);
            spawnedObj.transform.forward = this.transform.forward;
            spawnedObj.transform.SetParent(activeShipsHolder.transform);

            //add trail
            GameObject spawnedTrail = GameObject.Instantiate(spawnTrail, spawnedObj.transform.position, this.transform.rotation,spawnedObj.transform);
            spawnedTrail.GetComponent<TrailRenderer>().startWidth = spawnEffectScale*4;
            spawnedTrail.GetComponent<TrailRenderer>().startWidth = spawnEffectScale*4;
            Destroy(spawnedTrail,.1f);

            yield return new WaitForSeconds(0.01f);
            //zoom to spawn pos
            spawnedObj.transform.position = randSpawnPos;
            //add spawn particle effect
            GameObject spawnedSfx = GameObject.Instantiate(spawnEffect, spawnedObj.transform.position, this.transform.rotation);
            spawnedSfx.transform.localScale = new Vector3(spawnEffectScale,spawnEffectScale,spawnEffectScale);
            Destroy(spawnedSfx,2);
            
            yield return new WaitForSeconds(timeBetweenSpawn);

        }
    }

    
    
}
