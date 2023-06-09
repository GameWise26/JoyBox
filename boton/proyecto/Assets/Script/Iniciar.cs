using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iniciar : MonoBehaviour
{
    public bool iniciado = false;
    public Text Puntaje;

    public Morir morir;
    public void OnInicioButtonClick(){
        Puntaje.text = "Puntaje: 0";
        iniciado = true;
        morir.muerto = false;
    }
}
