using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpawner : MonoBehaviour
{
    public GameObject shipToSpawn;

    public float radiusToSpawn = 50;

    public float amountToSpawn = 3;
    public GameObject spawnEffect;//todo

    public void SpawnGroup()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randPos = Random.insideUnitSphere * radiusToSpawn;//random in circle
            Vector3 randSpawnPos = randPos + this.transform.position;//around center of obj

            GameObject spawnedObj = GameObject.Instantiate(shipToSpawn, randSpawnPos, this.transform.rotation);

        }
        

    }
}
