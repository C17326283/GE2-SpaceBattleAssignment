using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public CameraPointManager camPointManager;
    public CameraTargeting camTargeting;
    public GameObject normandy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    
    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);//wait 2 seconds
        Seq1();
        yield return new WaitForSeconds(.1f);
        Seq2();
        yield return new WaitForSeconds(15f);
        Seq3();
        yield return new WaitForSeconds(.1f);
        Seq4();
        yield return new WaitForSeconds(10f);
        Seq5();
        yield return new WaitForSeconds(20f);
        Seq6();
    }



    public void Seq1()//Reaper spawns
    {
        spawnManager.SpawnNextGroup();//spawn reapers
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
    }
    public void Seq2()//geth army arrives
    {
        spawnManager.SpawnNextGroup();
    }
    public void Seq3()//Alliance arrives
    {
        spawnManager.SpawnNextGroup();
        normandy = GameObject.Find("Normandy(Clone)");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = normandy;
    }
    public void Seq4()
    {
        spawnManager.SpawnNextGroup();
    }
    
    public void Seq5()//move cam closer
    {
        camTargeting.transform.position = normandy.transform.position + (-normandy.transform.forward*20)+(normandy.transform.up*5);
        camTargeting.transform.parent = normandy.transform;
        //spawnManager.SpawnNextGroup();
    }
    public void Seq6()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //spawnManager.SpawnNextGroup();
    }
}
