using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

/* sway the ship left and right for more natural movement, also avoid issue with pursuing target directly behind causing fats turn*/
public class HarmonicSwayBehaviour : BaseShipBehaviour
{
    public float swaySpeed = 0.5f;//bigger is faster
    public float swayAmount = 0.1f;

    private void Start()
    {
        swaySpeed += Random.Range(swaySpeed / 10,-swaySpeed / 10);//add a little variance so everything doesnt sway at same time

    }

    /* add left and right sway based on pingpong function*/
    [Task]
    public void AddSway()
    {
        //goes back and forth between 0 and 1 over swaySpeed time
        float swayValswayTime =  Mathf.PingPong(Time.time * swaySpeed, 1);
        //get a left or right force to add based on that amount by lerping betwen 2 values and setting the time as the pingpong var
        Vector3 swayForce = Vector3.Lerp(new Vector3(-this.swayAmount,0,0), new Vector3(swayAmount,0,0), swayValswayTime);
//        print(shipBoid);
        //add the random force based on the current speed traveling
        base.shipBoid.AddToForce(swayForce,1f);
        
        Task.current.Succeed();
    }
}
