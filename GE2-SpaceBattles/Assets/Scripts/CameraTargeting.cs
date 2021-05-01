using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeting : MonoBehaviour
{
    public GameObject gameObjectToLookAt;
    public float lerpSpeed = 2f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObjectToLookAt != null)
        {
            Vector3 relativePos = gameObjectToLookAt.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, lerpSpeed * Time.deltaTime );
                
        }
        
    }
}
