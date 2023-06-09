using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morir : MonoBehaviour
{

    public GameObject boton;
    public bool muerto = false;
    public Iniciar inicio;
    public Image img;

    public void OnMuerteButtonClick(){
        if (inicio.iniciado == true) {
            img.enabled = false;
            muerto = true;
        }      
    }
}
