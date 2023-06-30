using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HUD hud;

    private int puntosTotales;
    private int comidas;
    private int vidas = 3;
    public event Action GameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado, hay más de un GameManager activo");
        }
    }

    public int GetPuntosTotales()
    {
        return puntosTotales;
    }

    public int GetComidas()
    {
        return comidas;
    }

    public void SetPuntosTotales(int value)
    {
        puntosTotales = value;
        hud.ActualizarPuntos(puntosTotales);
    }

    public void SetComidas(int value)
    {
        comidas = value;
        hud.ActualizarComidas(comidas);
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        hud.ActualizarPuntos(puntosTotales);
    }

    public void SumarComidas(int comidaASumar)
    {
    comidas += comidaASumar;
    hud.ActualizarComidas(comidas);
    }


    public void PerderVida()
    {
    vidas -= 1;
    if (vidas == 0)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        else
        {
            Debug.LogError("No se encontró el objeto con la etiqueta 'Player'.");
        }

        GameOver?.Invoke();
        Destroy(gameObject);
    }
    hud.DesactivarVida(vidas);
}


    public bool RecuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }

        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

    public void GuardarPuntaje()
    {
    PlayerPrefs.SetInt("Puntaje", puntosTotales);
    PlayerPrefs.Save();
    }   

public void CargarPuntaje()
{
    if (PlayerPrefs.HasKey("Puntaje"))
    {
        puntosTotales = PlayerPrefs.GetInt("Puntaje");
    }
    else
    {
        puntosTotales = 0;
    }
    hud.ActualizarPuntos(puntosTotales);
}

    public void GuardarComidas()
    {
    PlayerPrefs.SetInt("Comidas", comidas);
    PlayerPrefs.Save();
    }

    public void CargarComidas()
    {
    if (PlayerPrefs.HasKey("Comidas"))
    {
        comidas = PlayerPrefs.GetInt("Comidas");
    }
    else
    {
        comidas = 0;
    }
    hud.ActualizarComidas(comidas);
    }

    private void OnApplicationQuit()
    {
    GuardarPuntaje();
    GuardarComidas();
    }

}
