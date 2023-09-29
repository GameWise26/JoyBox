using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boton : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel 01");
    }
    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void Salir()
    {
       SceneManager.LoadScene("Interfaz_juegos");
    }
    public void Inicio()
    {
         SceneManager.LoadScene("Inicio");
    }
    public void Historia()
    {
         SceneManager.LoadScene("Historia");
    }
}
