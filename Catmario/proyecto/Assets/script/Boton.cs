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
        Debug.Log("Salir");
        Application.Quit();
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
