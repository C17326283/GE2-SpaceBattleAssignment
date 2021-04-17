using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public TeleportSpawner[] spawnerGroupsInOrder;

    public int spawnedIndex = 0;


    public void SpawnNextGroup()
    {
        spawnerGroupsInOrder[spawnedIndex].SpawnGroup();

        spawnedIndex += 1;
    }
    
}
