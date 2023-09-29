using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callate : MonoBehaviour
{
    public bool mute = false;
    public Sprite muteOn, muteOff;
    GameObject controlador;
    GameObject camera;

    public void OnMouseUp()
    {
        controlador = GameObject.FindGameObjectWithTag("GameController");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        if (!mute) {
            mute = !mute;
            GetComponent<SpriteRenderer>().sprite = muteOn;

            camera.GetComponent<AudioSource>().mute = true;
            controlador.GetComponent<Juego>().SwitchMute();
        }
        else if (mute)
        {
            mute = !mute;
            GetComponent<SpriteRenderer>().sprite = muteOff;

            camera.GetComponent<AudioSource>().mute = false;
            controlador.GetComponent<Juego>().SwitchMute();
        }
    }
}
