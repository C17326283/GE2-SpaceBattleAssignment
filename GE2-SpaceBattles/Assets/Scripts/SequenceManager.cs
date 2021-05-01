using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public CameraPointManager camPointManager;
    public CameraTargeting camTargeting;
    public GameObject normandy;

    public float seq1wait = 2;

    public float timeGethAfterReaper = .1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    
    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);//wait 2 seconds
        StartCoroutine(Seq1());
        
        yield return new WaitForSeconds(20f);
        Seq2();
        yield return new WaitForSeconds(30f);
        Seq3();
        yield return new WaitForSeconds(20f);
        Seq4();
        yield return new WaitForSeconds(5f);
        Seq5();
        
        yield return new WaitForSeconds(5);
        StartCoroutine(Seq6());
        yield return new WaitForSeconds(5);
        Seq7();
        
    }


    //Start with calm shot of relay

    //Reapers arrive and camera follows them as they move toward citadel
    IEnumerator Seq1()//Reaper spawns
    {
        Debug.Log("Reapers arrive and camera follows them as they move toward citadel");
        
        spawnManager.SpawnNextGroup();//spawn reapers
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        yield return new WaitForSeconds(timeGethAfterReaper);
        //spawn in geth
        spawnManager.SpawnNextGroup();
    }
    
    //Destiny ascension tries to escape
    public void Seq2()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Flagship");
    }
    
    //Citadel starts closing
    public void Seq3()
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Flagship");
    }
    
    //Reaper ramming ships
    public void Seq4()
    {
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
    }
    
    
    
    
    //Reaper inside citadel as it closes
    public void Seq5()
    {
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
    }

    
    
    
    //reaper attaches to spire
    
    //alliance arrives
    IEnumerator Seq6()//Alliance arrives
    {
        spawnManager.SpawnNextGroup();//normandy
        normandy = GameObject.Find("Normandy(Clone)");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = normandy;
        yield return new WaitForSeconds(timeGethAfterReaper);
        spawnManager.SpawnNextGroup();//alliance ships
    }
    
    //alliance defend ascension
    //todo add more
    public void Seq7()//move cam closer
    {
        camTargeting.transform.position = normandy.transform.position + (-normandy.transform.forward*25)+(normandy.transform.up*8);
        camTargeting.transform.parent = normandy.transform;
        //spawnManager.SpawnNextGroup();
    }
    
    //follow normandy as things are getting blown up
    
    //cut to wide of everyone exploding
    
    //citadel opens
    
    //ships stop and bomb sovereign as he fights back
    
    //shields down everyone fires
    
    //normandy dive bomb
    
    //sovereign falls off and dies
    
    
    // normandy flying away
    
    
    
    
    
    
    public void Seq60()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //spawnManager.SpawnNextGroup();
    }
}
