using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The spawn manager triggers the specific teleporting group when the sequence manager needs it */
public class SpawnManager : MonoBehaviour
{
    public TeleportSpawner[] spawnerGroupsInOrder;
    public int spawnedIndex = 0;

    public void SpawnNextGroup()
    {
        spawnerGroupsInOrder[spawnedIndex].TriggerSpawn();

        spawnedIndex += 1;
    }
}
