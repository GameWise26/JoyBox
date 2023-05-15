using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuinicial : MonoBehaviour
{
    public void PLAY()
    {
        SceneManager.LoadScene("main");
    }

    public void OPCIONES()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SALIR()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void BACK()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
