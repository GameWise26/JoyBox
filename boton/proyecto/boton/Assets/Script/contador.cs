using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class contador : MonoBehaviour
{
    private float   timer = 0;
    private int PuntajeSegundos;
    public Text Puntaje;
    public Iniciar iniciar;

    public Morir morir;
    public Image img;
    public Image imgMorir;

    // Update is called once per frame
    void Update()
    {
        if(iniciar.iniciado == true){
            if(morir.muerto == false){
                img.enabled = false;
                imgMorir.enabled = true;
                timer += Time.deltaTime;
                PuntajeSegundos = Convert.ToInt32(timer % 60);

                Puntaje.text = $"Puntaje: {PuntajeSegundos}";
            }
            else{
                timer = 0;
                Puntaje.text = "Moriste, Presiona el boton para empezar";
                img.enabled = true;
            }
        }
        else{
            Puntaje.text = "Presiona el boton para empezar";
        }    
    }
}
