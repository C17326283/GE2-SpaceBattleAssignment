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
        StartCoroutine(Seq1x());
        
        yield return new WaitForSeconds(10f);
        Seq2x();
        
        
        //yield return new WaitForSeconds(20f);
        //StartCoroutine(Seq3x());
        
        /*
        yield return new WaitForSeconds(.1f);
        Seq4();
        yield return new WaitForSeconds(10f);
        Seq5();
        yield return new WaitForSeconds(60f);
        Seq6();
        */
    }


    //Start with calm shot of relay

    //Reapers arrive and camera follows them as they move toward citadel
    IEnumerator Seq1x()//Reaper spawns
    {
        Debug.Log("Reapers arrive and camera follows them as they move toward citadel");
        
        spawnManager.SpawnNextGroup();//spawn reapers
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        yield return new WaitForSeconds(timeGethAfterReaper);
        //spawn in geth
        spawnManager.SpawnNextGroup();
    }
    
    public void Seq2x()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Flagship");
        //spawnManager.SpawnNextGroup();
    }
    
    //Destiny ascension tries to escape
    
    //Citadel starts closing
    
    //Reaper ramming ships
    
    //Reaper inside citadel as it closes
    
    //reaper attaches to spire
    
    //alliance arrives
    IEnumerator Seq3x()//Alliance arrives
    {
        spawnManager.SpawnNextGroup();//normandy
        normandy = GameObject.Find("Normandy(Clone)");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = normandy;
        yield return new WaitForSeconds(timeGethAfterReaper);
        spawnManager.SpawnNextGroup();//alliance ships
    }
    
    
    
    //alliance defend ascension
    
    //follow normandy as things are getting blown up
    
    //cut to wide of everyone exploding
    
    //citadel opens
    
    //ships stop and bomb sovereign as he fights back
    
    //shields down everyone fires
    
    //normandy dive bomb
    
    //sovereign falls off and dies
    
    // normandy flying away
    
    
    
    
    
    public void Seq50()//move cam closer
    {
        camTargeting.transform.position = normandy.transform.position + (-normandy.transform.forward*25)+(normandy.transform.up*8);
        camTargeting.transform.parent = normandy.transform;
        //spawnManager.SpawnNextGroup();
    }
    public void Seq60()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //spawnManager.SpawnNextGroup();
    }
}
