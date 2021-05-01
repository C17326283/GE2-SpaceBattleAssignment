using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointManager : MonoBehaviour
{
    
    public List<Vector3> points = new List<Vector3>();
    public int current = 0;


    
    void Start () {
        points.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            points.Add(transform.GetChild(i).position);
        }
    }
    
    public Vector3 GetPoint()
    {
        Vector3 pointToReturn = points[current];
        AdvanceToNext();
        return pointToReturn;//the one before advanced
    }
    
    
    public void AdvanceToNext()
    {
        if (current != points.Count - 1)
        {
            current++;
        }
    }
}
