using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* for destroying instantiated objects by adding this instead of destroying from this that spawned it*/
public class DestroyAfterTime : MonoBehaviour
{
    public float timeBeforeDestroy = 2;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,timeBeforeDestroy);
    }

}
