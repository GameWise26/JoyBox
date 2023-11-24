using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI comidas;
    public GameObject[] vidas;

    private void Start()
    {
    GameManager.Instance.CargarPuntaje();
    GameManager.Instance.CargarComidas();

    // Llama al método InicializarHUD() al inicio de la escena
    InicializarHUD();
    }


    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void ActualizarComidas(int comidas)
    {
        this.comidas.text = comidas.ToString();
    }

    public void DesactivarVida(int indice)
    {
        if (indice >= 0 && indice < vidas.Length)
        {
            vidas[indice].SetActive(false);
        }
    }

    public void ActivarVida(int indice)
    {
        if (indice >= 0 && indice < vidas.Length)
        {
            vidas[indice].SetActive(true);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += EscenaCargada;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= EscenaCargada;
    }

    private void EscenaCargada(Scene scene, LoadSceneMode mode)
    {
        InicializarHUD();
    }

    private void InicializarHUD()
    {
        if (SceneManager.GetActiveScene().name == "Nivel 01")
        {
            // Establecer puntajes en 0 si la escena es "Nivel 01"
            GameManager.Instance.SetPuntosTotales(0);
            GameManager.Instance.SetComidas(0);
        }

        ActualizarPuntos(GameManager.Instance.GetPuntosTotales());
        ActualizarComidas(GameManager.Instance.GetComidas());
    }
}

