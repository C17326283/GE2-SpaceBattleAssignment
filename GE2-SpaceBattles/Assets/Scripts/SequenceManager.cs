using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.Events;

public class SequenceManager : MonoBehaviour
{
    public float timeToWaitBeforeStarting = 2;
    public SpawnManager spawnManager;
    public PointManager camPointManager;
    public PointManager triggerPointManager;
    public CameraTargeting camTargeting;
    public GameObject normandy;

    public AudioManager audioManager;

    //Having all the sequences in events lets you better manage and trigger them from anywhere or sequentially
    public UnityEvent[] events;

    public int curEvent = 0;

    public Animator citadelAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        curEvent = 0;
        camTargeting.curOffsetIndex = 0;
        StartCoroutine(NextSeq(timeToWaitBeforeStarting));
    }


    IEnumerator NextSeq(float waitBeforeTrigger)
    {
//        Debug.Log("NextSeq");
        yield return new WaitForSeconds(waitBeforeTrigger);
        events[curEvent].Invoke();
        curEvent++;
    }


    //sequences with a parameter play for that length and then trigger the next
    //triggered from start invoke
    public void Seq1_1(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq1_1 Reapers arrive and camera follows them as they move toward citadel");
        spawnManager.SpawnNextGroup(); //spawn reapers
        camTargeting.SetCamLookAt(GameObject.Find("Reaper(Clone)").transform);
        //camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());//dont need because you start at point
        audioManager.PlayNextMusic(); //reaper entry music

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq1_2()
    {
        Debug.Log("Seq1_2 Spawn Geth after reaper");
        spawnManager.SpawnNextGroup();

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from TriggerSideViewReaper world trigger
    public void Seq1_3()
    {
        Debug.Log("Seq1_3 Reaper Side view");
        //camTargeting.SetCameraWithRelativeOffset(camTargeting.gameObjectToLookAt.transform,GetOffset());
        camTargeting.SetCamFollow(GameObject.Find("Reaper(Clone)").transform, 0);

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger

    }

    //triggered from TriggerRocketsFiringAtAscention world trigger
    public void Seq2_1()
    {
        Debug.Log("Seq2_1 Destiny ascension tries to escape");
        GameObject ascension = GameObject.Find("Flagship");
        ascension.GetComponent<BehaviourTree>().enabled = true;
        camTargeting.SetCameraWithRelativeOffset(ascension.transform, 1);
        camTargeting.SetCamLookAt(ascension.transform);

        audioManager.PlayNextVoice(); //evac council

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger

    }

    //triggered from CitadelStartsClosing world Trigger
    public void Seq2_2()
    {
        Debug.Log("Seq2_2 Citadel Starts closing");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());

        citadelAnim.SetBool("Closing", true);
        //camTargeting.gameObjectToLookAt = GameObject.Find("CitadelDefencePoint");

        audioManager.PlayNextVoice(); //theyre sealing the station

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from ReaperRammingShips world trigger
    public void Seq2_3()
    {
        Debug.Log("Seq2_3 Reaper ramming ships");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 2);
        camTargeting.SetCamLookAt(Reaper.transform);

        audioManager.PlayNextVoice(); //dont let the enemy inside

        //geth stop following reaper
        GameObject shipsHolder = GameObject.Find("-ActiveShips-");
        foreach (Transform ship in shipsHolder.transform) //search all immediate children of shipholder
        {
            if (ship.transform.CompareTag("Reaper") && ship.GetComponent<OffsetPursueBehaviour>())
            {
                OffsetPursueBehaviour shipFollowB = ship.GetComponent<OffsetPursueBehaviour>();
                //maybe send some after ascension?
                shipFollowB.followObjName = "CitadelDefencePoint";
                shipFollowB.followObj = null;
            }
        }

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from ReaperEnteringCitadel world trigger
    public void Seq2_4()
    {
        Debug.Log("Seq2_4 Reaper entering citadel");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 3);
        camTargeting.SetCamLookAt(Reaper.transform);


        audioManager.PlayNextMusic(); //reaper in citadel

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from ReaperInsideCitadel world trigger
    public void Seq2_5()
    {
        Debug.Log("Seq2_4 Reaper inside citadel");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 4);
        camTargeting.SetCamLookAt(Reaper.transform);

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from WatchOutsideBattle world trigger
    public void Seq2_6()
    {
        Debug.Log("Seq2_4 Watch battle outside citadel");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        camTargeting.SetCamLookAt(GameObject.Find("CitadelDefencePoint").transform);

        audioManager.PlayNextVoiceDelay(12); //caught the distress

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from ReaperAttachingToSpire world trigger
    public void Seq2_7(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq2_5 Reaper attaching to spire");
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 5);
        camTargeting.SetCamLookAt(Reaper.transform);

        audioManager.PlayNextVoice(); //ive regained control

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq3_1(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq3_1 Focus on teleporter Arrives");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        camTargeting.SetCamLookAt(GameObject.Find("Teleporter").transform);

        audioManager.PlayNextMusic(); //final battle

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq3_2(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq3_2 Normandy Arrives");

        spawnManager.SpawnNextGroup(); //normandy
        normandy = GameObject.Find("Normandy(Clone)");

        camTargeting.SetCamFollowAndLook(normandy.transform,13);//13 because i added after

        audioManager.PlayNextVoice(); //picking up reinforcements

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq3_3()
    {
        Debug.Log("Seq3_3 Alliance Arrives");
        spawnManager.SpawnNextGroup(); //alliance ships
        //camTargeting.moveLerpSpeed = 100;//really fast to keep up with the normandy
        
        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from AllianceBlowingUpGeth world trigger
    public void Seq4_1(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq4_1 Watch geth around alliance blow up");

        GameObject ascension = GameObject.Find("Flagship");
        camTargeting.SetCamFollow(ascension.transform, 6);
        camTargeting.SetCamLookAt(ascension.transform);

        audioManager.PlayNextVoice(); //alliance moves in

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq4_2(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq4_2 Alliance flies in through explosions");
        camTargeting.SetCamFollow(normandy.transform, 7);
        camTargeting.SetCamLookAt(normandy.transform);

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq4_3(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq4_3 Ascension starts leaving");

        GameObject ascension = GameObject.Find("Flagship");
        //camTargeting.SetCamFollow(ascension.transform, 8);
        camTargeting.SetCamLookAt(ascension.transform);

        audioManager.PlayNextVoice(); //ascension clear

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq5_1()
    {
        Debug.Log("Seq5_1 Citadel opens and ships move in");
        citadelAnim.SetBool("Closing", false);
        camTargeting.SetCamFollow(normandy.transform, 9);
        camTargeting.SetCamLookAt(normandy.transform);

        //geth stop following reaper
        GameObject shipsHolder = GameObject.Find("-ActiveShips-");
        foreach (Transform ship in shipsHolder.transform) //search all immediate children of shipholder
        {
            if (ship.transform.CompareTag("Alliance") && ship.GetComponent<OffsetPursueBehaviour>())
            {
                OffsetPursueBehaviour shipFollowB = ship.GetComponent<OffsetPursueBehaviour>();
                //maybe send some after ascension?
                shipFollowB.followObjName = "ReaperAttackPoint";
                shipFollowB.followObj = null;

                ship.GetComponent<CombatBehaviour>().arrivedDist = 300;
                ship.GetComponent<CombatBehaviour>().divertDistance = 600;// reaper is big so dont get as close
            }
        }

        OffsetPursueBehaviour normandyFollowB = normandy.GetComponent<OffsetPursueBehaviour>();
        //maybe send some after ascension?
        //normandyFollowB.followOffsetDistance = Mathf.Infinity;
        normandyFollowB.followObjName = "ReaperAttackPoint";
        normandyFollowB.followObj = null;

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from Alliance&NormandyIsShootingReaper
    public void Seq5_2(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq5_2 Sovereign fights back as its getting attacked while attached");

        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 10);
        camTargeting.SetCamLookAt(Reaper.transform);

        //enable side weapons
        Reaper.GetComponent<ReaperWeaponManager>().EnableWeapons();

        audioManager.PlayNextVoice(); //take that monster down

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq5_3(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq5_3 Sovereign shields down");

        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        camTargeting.SetCamFollow(Reaper.transform, 11);
        camTargeting.SetCamLookAt(Reaper.transform);



        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq5_4()
    {
        Debug.Log("Seq5_4 Everyone starts firing");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        camTargeting.SetCamLookAt(GameObject.Find("Reaper(Clone)").transform);
        //todo look at alliance ship instead of reaper

        Transform flankPoint = GameObject.Find("NormandyFlankPoint").transform;

        //Fly to flank position by setting a divert point which is considered before other behaviours
        normandy.GetComponent<CombatBehaviour>().divertTarget = flankPoint.position;
        
        audioManager.PlayNextVoice(); //shields are down
        audioManager.PlayNextMusic(); //victory

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from NormandyReadyForBigAttack world trigger
    public void Seq6_1()
    {
        Debug.Log("Seq6_1 Normandy dips for big attack");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        camTargeting.SetCamLookAt(normandy.transform);
        //normandy.GetComponent<OffsetPursueBehaviour>().maxDistAway = Mathf.Infinity;//allow normandy to move far for dive

        audioManager.PlayNextVoice(); //flank

        normandy.transform.Find("SuperGun").gameObject.SetActive(true); //turn on super weapon

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from SuperRocketTargetingReaper world trigger
    public void Seq6_2()
    {
        Debug.Log("Seq6_2 Normandy shoots big rocket");
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());


        //look at normandy rocket
        Transform SuperRocket = GameObject.Find("SuperSeekingRocket(Clone)").transform;
        camTargeting.SetCamFollowAndLook(SuperRocket, 12);
        camTargeting.moveLerpSpeed = 100; //move a lot faster to keep up with rocket

        

        triggerPointManager.GetNextPoint().gameObject.SetActive(true); //turn on next world trigger
    }

    //triggered from SovereignExplodes world trigger
    public void Seq6_3(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq6_3 Sovereign explodes");

        //disable weapons
        GameObject Reaper = GameObject.Find("Reaper(Clone)");
        Reaper.GetComponent<ReaperWeaponManager>().DisableWeapons();
        
        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        //camTargeting.SetCamLookAt(GameObject.Find("Normandy(Clone)").transform);

        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }

    //triggered from previous
    public void Seq7_1(float TriggerNextSequenceTime)
    {
        Debug.Log("Seq7_1 Normandy flys away");
        
        //make ships fly back out
        GameObject shipsHolder = GameObject.Find("-ActiveShips-");
        foreach (Transform ship in shipsHolder.transform) //search all immediate children of shipholder
        {
            if (ship.transform.CompareTag("Alliance") && ship.GetComponent<OffsetPursueBehaviour>())
            {
                OffsetPursueBehaviour shipFollowB = ship.GetComponent<OffsetPursueBehaviour>();
                //maybe send some after ascension?
                shipFollowB.followObjName = "CitadelDefencePoint";
                shipFollowB.followObj = null;
            }
        }
        
        audioManager.PlayNextVoice(); //final speach

        camTargeting.SetCameraMatchPoint(camPointManager.GetNextPoint());
        
        
        //continue to next event which trigger next seq
        StartCoroutine(NextSeq(TriggerNextSequenceTime));
    }
    
    public void Seq7_2()
    {
        Debug.Log("Seq7_2 End");

        GameObject.Find("GameManager").GetComponent<GameManager>().Quit();

    }
}