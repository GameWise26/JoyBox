using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem1 : MonoBehaviour
{
    public static SoundSystem1 instance;

    public AudioClip audioClipPoint;
    public AudioClip audioClipFlap;
    public AudioClip audioClipHit;

    public AudioSource audioSource;
    public AudioSource audioBackground;

    private void Awake(){
        if(SoundSystem1.instance == null){
            SoundSystem1.instance = this;    
        } else if(SoundSystem1.instance != this){
            Destroy(gameObject);
            Debug.LogWarning("SoundSystem ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }

    public void PlayPoint(){
        PlayAudioClip(audioClipPoint);
    }
    public void PlayFlap(){
        PlayAudioClip(audioClipFlap);
    }
    public void PlayHit(){
        PlayAudioClip(audioClipHit);
    }
    
    public void PlayAudioClip(AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void OnDestroy(){
        if(SoundSystem1.instance == this){
            SoundSystem1.instance = null;    
        }
    }
}
