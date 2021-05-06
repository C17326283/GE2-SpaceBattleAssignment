using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    
    public List<Transform> points = new List<Transform>();
    public int current = 0;


    
    void Start () {
        points.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            points.Add(transform.GetChild(i));
        }
    }
    
    public Transform GetNextPoint()
    {
        Transform pointToReturn = points[current];
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
