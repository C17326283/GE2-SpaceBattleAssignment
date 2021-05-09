using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The point manager is used for paths, trigger and camera points which allows access to cycling through or selecting individual objects from the lists.*/
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
    
    public Transform GetCurrentPoint()
    {
        Transform pointToReturn = points[current];
        return pointToReturn;//the one before advanced
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
    
    public bool IsLast()
    {
        return current == points.Count - 1;
    }
}
