using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeting : MonoBehaviour
{
    public Transform gameObjectToLookAt;
    public Transform gameObjectToFollow;
    public Vector3 objectFollowOffset;
    public float moveLerpSpeed = 2f;
    public float rotLerpSpeed = 2f;
    
    public Vector3 panningAmount = new Vector3(0,0,0);
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (gameObjectToFollow)
        {
            Vector3 toPos = gameObjectToFollow.position + gameObjectToFollow.TransformDirection(objectFollowOffset);
            transform.position = Vector3.Lerp(transform.position, toPos, Time.deltaTime * moveLerpSpeed);
        }
        else
        {
            Vector3 toPos = transform.position + transform.TransformDirection(panningAmount);
            transform.position = Vector3.Slerp(transform.position, toPos, Time.deltaTime * moveLerpSpeed);
        }
        
        if (gameObjectToLookAt)
        {
            Vector3 relativePos = gameObjectToLookAt.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, rotLerpSpeed * Time.deltaTime );
        }
    }
    public void SetCameraWithRelativeOffset(Transform obj,Vector3 offset)
    {
        SetCamNotFollowing();
        transform.position = obj.transform.TransformPoint(offset);
        //transform.position = obj.transform.position+offset;
        print("obj"+obj.transform.position+",offset:"+offset+", with offset:"+(obj.transform.position+obj.transform.TransformPoint(offset)));
    }
    
    public void SetCameraMatchPoint(Transform cameraPoint)
    {
        SetCamNotFollowing();
        transform.position = cameraPoint.transform.position;
        transform.rotation = cameraPoint.transform.rotation;
    }
    
    public void SetCamFollowAndLook(Transform obj, Vector3 offset)
    {
        SetCamFollow(obj, offset);
        SetCamLookAt(obj);
    }

    public void SetCamFollow(Transform obj, Vector3 offset)
    {
        gameObjectToFollow = obj;
        objectFollowOffset = offset;
    }
    public void SetCamLookAt(Transform obj)
    {
        gameObjectToLookAt = obj;
    }

    public void SetCamNotFollowing()
    {
        gameObjectToFollow = null;
        objectFollowOffset = Vector3.zero;
    }
    
}
