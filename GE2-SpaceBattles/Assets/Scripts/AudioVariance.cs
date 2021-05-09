using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* For changing pitch on instantiated gameobjects, ie explosions*/
public class AudioVariance : MonoBehaviour
{
    //for allowing explosions to have audio pitch variance
    public AudioSource audio;
    public float minPitch = .8f;
    public float maxPitch = 1.1f;
        
    // Start is called before the first frame update
    void Awake()
    {
        audio.playOnAwake = false;
        audio.pitch = Random.Range(minPitch, maxPitch);
        audio.Play();
    }
    
}
