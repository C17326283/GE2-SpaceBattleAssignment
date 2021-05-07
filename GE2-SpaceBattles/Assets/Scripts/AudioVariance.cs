using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioVariance : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
