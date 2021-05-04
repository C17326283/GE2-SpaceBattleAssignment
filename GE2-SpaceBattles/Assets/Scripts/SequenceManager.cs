using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceManager : MonoBehaviour
{
    public float timeToWaitBeforeStarting = 2;
    public SpawnManager spawnManager;
    public CameraPointManager camPointManager;
    public CameraTargeting camTargeting;
    public GameObject normandy;

    public UnityEvent[] events;
    public Vector3[] cameraOffsets;//for having cam a certain distance away 
    
    public int curEvent = 0;
    public int curOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Sequence());
        //events[curEvent].Invoke();
        StartCoroutine(NextSeq(timeToWaitBeforeStarting));
    }

    
    public Vector3 GetOffset()
    {
        Vector3 selectedOffset = cameraOffsets[curOffset];
        Debug.Log("Get offset: "+curOffset+", "+selectedOffset);
        curOffset++;
        return selectedOffset;
    }
    
    IEnumerator NextSeq(float waitBeforeTrigger)
    {
//        Debug.Log("NextSeq");
        yield return new WaitForSeconds(waitBeforeTrigger);
        events[curEvent].Invoke();
        curEvent++;
    }

    
    public void Seq1_1(float waitBeforeNext)
    {
        Debug.Log("Reapers arrive and camera follows them as they move toward citadel");
        spawnManager.SpawnNextGroup();//spawn reapers
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq1_2(float waitBeforeNext)
    {
        Debug.Log("Spawn Geth after reaper");
        spawnManager.SpawnNextGroup();
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq1_3(float waitBeforeNext)
    {
        Debug.Log("Reaper Side view");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camTargeting.gameObjectToLookAt.transform.position + camTargeting.gameObjectToLookAt.transform.InverseTransformDirection(GetOffset());
        camTargeting.transform.parent = camTargeting.gameObjectToLookAt.transform;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq2_1(float waitBeforeNext)
    {
        Debug.Log("Destiny ascension tries to escape");
        GameObject ascension = GameObject.Find("Flagship");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = ascension.transform.position + ascension.transform.InverseTransformDirection(GetOffset());
        camTargeting.gameObjectToLookAt = ascension;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq2_2(float waitBeforeNext)
    {
        Debug.Log("Citadel Starts closing");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("CitadelDefencePoint");
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq2_3(float waitBeforeNext)
    {
        Debug.Log("Reaper ramming ships");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + Reaper.transform.InverseTransformDirection(GetOffset());

        camTargeting.gameObjectToLookAt = Reaper;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq2_4(float waitBeforeNext)
    {
        Debug.Log("Reaper entering citadel");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + Reaper.transform.InverseTransformDirection(GetOffset());

        camTargeting.gameObjectToLookAt = Reaper;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq2_5(float waitBeforeNext)
    {
        Debug.Log("Reaper attaching to spire");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + Reaper.transform.InverseTransformDirection(GetOffset());

        camTargeting.gameObjectToLookAt = Reaper;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq3_1(float waitBeforeNext)
    {
        Debug.Log("Focus on teleporter Arrives");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Teleporter");
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq3_2(float waitBeforeNext)
    {
        Debug.Log("Normandy Arrives");
        
        spawnManager.SpawnNextGroup();//normandy
        normandy = GameObject.Find("Normandy(Clone)");
        camTargeting.gameObjectToLookAt = normandy;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq3_3(float waitBeforeNext)
    {
        Debug.Log("Alliance Arrives");
        spawnManager.SpawnNextGroup();//alliance ships
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq4_1(float waitBeforeNext)
    {
        Debug.Log("Watch geth around alliance blow up");
        
        GameObject ascension = GameObject.Find("Flagship");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = ascension.transform.position + ascension.transform.InverseTransformDirection(GetOffset());
        camTargeting.gameObjectToLookAt = ascension;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq4_2(float waitBeforeNext)
    {
        Debug.Log("Alliance flies in through explosions");
        camTargeting.transform.parent = normandy.transform;
        camTargeting.transform.position = normandy.transform.position + normandy.transform.InverseTransformDirection(GetOffset());
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq4_3(float waitBeforeNext)
    {
        Debug.Log("Ascension starts leaving");
        
        GameObject ascension = GameObject.Find("Flagship");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = ascension.transform.position + ascension.transform.InverseTransformDirection(GetOffset());
        camTargeting.gameObjectToLookAt = ascension;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq5_1(float waitBeforeNext)
    {
        Debug.Log("Citadel opens and ships move in");
        camTargeting.transform.parent = normandy.transform;
        camTargeting.transform.position = normandy.transform.position + normandy.transform.InverseTransformDirection(GetOffset());
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq5_2(float waitBeforeNext)
    {
        Debug.Log("Sovereign fights back as its getting attacked while attached");
        
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + Reaper.transform.InverseTransformDirection(GetOffset());

        camTargeting.gameObjectToLookAt = Reaper;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq5_3(float waitBeforeNext)
    {
        Debug.Log("Sovereign shields down");
        
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + Reaper.transform.InverseTransformDirection(GetOffset());

        camTargeting.gameObjectToLookAt = Reaper;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq5_4(float waitBeforeNext)
    {
        Debug.Log("Everyone starts firing");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //todo look at alliance ship instead of reaper
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq6_1(float waitBeforeNext)
    {
        Debug.Log("Normandy dips for big attack");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = normandy;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq6_2(float waitBeforeNext)
    {
        Debug.Log("Normandy shoots big rocket");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //look at normandy rocket
        camTargeting.gameObjectToLookAt = normandy;
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq6_3(float waitBeforeNext)
    {
        Debug.Log("Sovereign explodes");
        
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(waitBeforeNext));
    }
    
    public void Seq7_1(float waitBeforeNext)
    {
        Debug.Log("Normandy flys away");
        
        camTargeting.gameObjectToLookAt = normandy;
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        
    }
    
    
    
    
    /*
     
     public void SetOffsetToObject(String gameObjectName, Vector3 offset)
    {
        Transform mainObj = GameObject.Find(gameObjectName).transform;
        camTargeting.transform.position = mainObj.position+offset;
    }

    
    //just for calling coroutine from event
    public void TriggerNextSeq(float wait)
    {
        Debug.Log("TriggerNextSeq");
        StartCoroutine(NextSeq(wait));
    }
     
     
    //this is mainly a debug for keeping track in the unity events
    public void PrintSequenceTask(String message)
    {
        Debug.Log("Reapers arrive and camera follows them as they move toward citadel");
    }

    public void SpecialOffsetReaperSide()
    {
        SetOffsetToObject("Reaper(Clone)",reaperSideOffset);
    }
     
     IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2);//wait 2 seconds
        //StartCoroutine(Seq1());
        
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
    
    //Citadel starts closing
    public void Seq3()
    {
        Debug.Log("Seq3() Citadel starts closing");
        //citadelAnim.SetTrigger();
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Flagship");
    }
    
    //Reaper ramming ships
    public void Seq4()
    {
        Debug.Log("Seq4() Reaper ramming ships");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.transform.parent = Reaper.transform;
        camTargeting.transform.position = Reaper.transform.position + (Reaper.transform.forward*500)+(-Reaper.transform.up*100);

        camTargeting.gameObjectToLookAt = Reaper;
    }
    
    
    
    
    //Reaper inside citadel as it closes
    public void Seq5()
    {
        Debug.Log("Seq5() Reaper inside citadel as it closes");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
    }

    
    
    
    //reaper attaches to spire
    
    //alliance arrives
    IEnumerator Seq6()
    {
        Debug.Log("Seq6() alliance arrives");
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Reaper(Clone)");
        yield return new WaitForSeconds(5);
        
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
        Debug.Log("Seq7() alliance defend ascension");
        camTargeting.transform.position = normandy.transform.position + (-normandy.transform.forward*50)+(normandy.transform.up*16);
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
    
    
    
    
    //Destiny ascension tries to escape
    public void Seq2()//move cam closer
    {
        Debug.Log("Seq2() Destiny ascension tries to escape");
        
        GameObject ascension = GameObject.Find("Flagship");
        camTargeting.transform.position = camTargeting.gameObjectToLookAt.transform.position + (-camTargeting.gameObjectToLookAt.transform.right*1000);

        
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        camTargeting.gameObjectToLookAt = GameObject.Find("Flagship");
    }
    
    public void Seq60()//move cam closer
    {
        camTargeting.transform.parent = null;
        camTargeting.transform.position = camPointManager.GetPoint();
        //spawnManager.SpawnNextGroup();
    }
    
    */
}
