using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The audio manager triggers the correct voice lines and music to play when the sequence manager needs them.
 This can be done in a cycle or for specific lines. Voice lines can be triggered with a delay and music has
  a volume fade on start and end.*/
public class AudioManager : MonoBehaviour
{
    public AudioSource[] voiceClips;
    public AudioSource[] MusicClips;

    public GameObject parentOfVoiceClips;
    public GameObject parentOfMusicClips;
    

    public int currVoiceClip = 0;
    public int currMusic = 0;

    public float volumeLerpDuration = 2;

    private void Start()
    {
        voiceClips = parentOfVoiceClips.GetComponents<AudioSource>();
        MusicClips = parentOfMusicClips.GetComponents<AudioSource>();
    }


    //Different functions to trigger audio in specific ways
    public void PlayNextVoice()
    {
        PlayVoice(currVoiceClip);
        currVoiceClip++;
    }
    
    public void PlayVoice(int elementNum)
    {
        voiceClips[elementNum].Play();
    }
    
    public void PlayNextVoiceDelay(float delay)
    {
        Invoke("PlayNextVoice",delay);
    }
    
    public void PlayNextMusic()
    {
        //fade out last music
        if (currMusic > 0)
        {
            StartCoroutine(StartFade(MusicClips[currMusic-1], MusicClips[currMusic-1].volume, 0,false));
        }
            
        PlayMusic(currMusic);
        currMusic++;
    }
    
    
    
    
    public void PlayMusic(int elementNum)
    {
        //fade in from 0 to whats set in editor
        StartCoroutine(StartFade(MusicClips[elementNum], 0, MusicClips[elementNum].volume,false));
        MusicClips[elementNum].Play();
    }
    
    // fade audio in/out, method modified from https://gamedevbeginner.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
    IEnumerator StartFade(AudioSource audioSource, float startVolume, float targetVolume, bool stopAfter)
    {
        float currentTime = 0;
        
        while (currentTime < volumeLerpDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / volumeLerpDuration);
            yield return null;
        }

        if (stopAfter)
        {
            audioSource.Stop();
        }
        yield break;
    }
}