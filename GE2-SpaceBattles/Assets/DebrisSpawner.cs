using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebrisSpawner : MonoBehaviour
{
    public GameObject[] debrisObjs;

    public int minAmountOfDebrisToSpawn = 2;
    public int maxAmountOfDebrisToSpawn = 10;
    
    public int minScale = 2;
    public int maxScale = 10;
    
    public int maxForce;

    public float destroyAfter = 30;
    
    public void SpawnDebris()
    {
        int amountOfDebrisToSpawn = Random.Range(minAmountOfDebrisToSpawn, maxAmountOfDebrisToSpawn);
        //make all the debris objects
        for (int i = 0; i < amountOfDebrisToSpawn; i++)
        {
            
            GameObject debrisObj = GameObject.Instantiate(debrisObjs[Random.Range(0,debrisObjs.Length-1)], transform.position, this.transform.rotation);
            //make random scale for variance
            float randScale = Random.Range(-minScale, maxScale);
            debrisObj.transform.localScale = new Vector3(randScale,randScale,randScale);
            //add random explosive force
            Rigidbody debrisRb = debrisObj.GetComponent<Rigidbody>();
            debrisRb.velocity = new Vector3(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));
            debrisRb.angularVelocity = new Vector3(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));

            //destroy after a while too avoid too many scene objs
            Destroy(debrisObj,destroyAfter);
        }
        

    }
    
}
