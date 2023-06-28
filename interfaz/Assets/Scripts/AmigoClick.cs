using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmigoClick : MonoBehaviour
{
    public string nombre;
    public string juego;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Intermedio(){
        CargarAmigo.instancia.MostrarDatos(nombre,juego);
    }
}
