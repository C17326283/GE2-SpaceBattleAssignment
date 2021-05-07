using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] voiceClips;
    public AudioSource[] MusicClips;
    

    public int currVoiceClip = 0;
    public int currMusic = 0;
    public void PlayNextVoice()
    {
        PlayVoice(currVoiceClip);
        currVoiceClip++;
    }
    
    public void PlayVoice(int elementNum)
    {
        voiceClips[elementNum].Play();
    }
    
    public void PlayNextMusic()
    {
        if(currMusic>0)
            MusicClips[currMusic-1].Stop();
        PlayMusic(currMusic);
        currMusic++;
    }
    
    public void PlayMusic(int elementNum)
    {
        MusicClips[elementNum].Play();
    }
}
