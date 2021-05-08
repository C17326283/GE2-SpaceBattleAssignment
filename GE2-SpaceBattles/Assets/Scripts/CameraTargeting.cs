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
    
    public Vector3[] cameraOffsets; //for having cam a certain distance away from a target
    public int curOffsetIndex = 0;
    
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
    public void SetCameraWithRelativeOffset(Transform obj,int offsetIndex)
    {
        Vector3 offset = GetOffset(offsetIndex);
        SetCamNotFollowing();
        transform.position = obj.transform.TransformPoint(offset);
        //transform.position = obj.transform.position+offset;
        print("obj"+obj.transform.position+",offset: "+offsetIndex+","+offset+", with offset:"+(obj.transform.position+obj.transform.TransformPoint(offset)));
    }
    
    public void SetCameraMatchPoint(Transform cameraPoint)
    {
        SetCamNotFollowing();
        transform.position = cameraPoint.transform.position;
        transform.rotation = cameraPoint.transform.rotation;
    }
    
    public void SetCamFollowAndLook(Transform obj, int offsetIndex)
    {
        SetCamFollow(obj, offsetIndex);
        SetCamLookAt(obj);
    }

    public void SetCamFollow(Transform obj, int offsetIndex)
    {
        Vector3 offset = GetOffset(offsetIndex);
        
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
    
    public Vector3 GetOffset(int index)
    {
        Vector3 selectedOffset = cameraOffsets[index];
        curOffsetIndex = index + 1;

        return selectedOffset;
    }

    public Vector3 GetNextOffset()
    {

        Vector3 selectedOffset = GetOffset(curOffsetIndex);

        Debug.Log("Get offset: " + curOffsetIndex + ", " + selectedOffset);
        curOffsetIndex++;

        return selectedOffset;
    }
    
}
