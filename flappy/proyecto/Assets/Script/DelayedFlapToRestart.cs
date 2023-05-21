using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFlapToRestart : MonoBehaviour
{
    public GameObject flapToRestart;
    public float delay = 1f;
    
    void OnEnable(){
        Invoke("EnableFlapToRestart",delay);
    }

    void EnableFlapToRestart(){
        flapToRestart.SetActive(true);
    }
}
