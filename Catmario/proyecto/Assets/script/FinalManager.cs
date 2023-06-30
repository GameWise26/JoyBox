using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    private void Start()
    {
        int comidasTotales = GameManager.Instance.GetComidas();
        int puntosTotales = GameManager.Instance.GetPuntosTotales();

        if (comidasTotales >= 20)
        {
            CargarFinalBueno();
        }
        else if (comidasTotales >= 12 && comidasTotales <= 19)
        {
            CargarFinalNeutro();
        }
        else if (comidasTotales < 11 && puntosTotales > 300)
        {
            CargarFinalSecreto();
        }
        else
        {
            CargarFinalMalo();
        }
    }

    private void CargarFinalBueno()
    {
        SceneManager.LoadScene("FinalBuenoScene");
    }

    private void CargarFinalNeutro()
    {
        SceneManager.LoadScene("FinalNeutroScene");
    }

    private void CargarFinalSecreto()
    {
        SceneManager.LoadScene("FinalSecretoScene");
    }

    private void CargarFinalMalo()
    {
        SceneManager.LoadScene("FinalMaloScene");
    }
}
