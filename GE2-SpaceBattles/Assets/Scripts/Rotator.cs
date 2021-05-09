using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* simple rotate around axis script */
public class Rotator : MonoBehaviour
{
    public float rotateSpeedX = .5f;
    public float rotateSpeedY = .1f;
    public float rotateSpeedZ = 0f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-rotateSpeedX * Time.deltaTime ,-rotateSpeedY * Time.deltaTime,-rotateSpeedZ * Time.deltaTime );
    }
}
