using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : MonoBehaviour
{
    public float timeToWait=1;

    public float radius = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ChangeTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToWait);
            transform.position = Random.insideUnitSphere * radius;
            print(radius);
        }
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        print("WaitAndPrint " + Time.time);
        
    }
}
