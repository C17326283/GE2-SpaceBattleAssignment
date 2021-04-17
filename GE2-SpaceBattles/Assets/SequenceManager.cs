using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    
    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);//wait 2 seconds
        spawnManager.SpawnNextGroup();//spawn reapers
        spawnManager.SpawnNextGroup();//spawn reapers
        yield return new WaitForSeconds(10);
        spawnManager.SpawnNextGroup();//spawn alliance
    }
}
