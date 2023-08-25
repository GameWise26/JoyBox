using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Juego : MonoBehaviour
{
    //envidos
    private bool Envido = false;
    private bool EnvidoEnvido = false;
    private bool RealEnvido = false;
    private bool FaltaEnvido = false;
    //trucos
    private bool Truco = false;
    private bool Retruco = false;
    private bool Vale4 = false;
    //cosas
    private bool Mazo = false;
    private bool turnojugador1 = true; 
    private GameManager gameManager;
    public GameObject canvascantos;
    public GameObject menunormal;
    public GameObject menuenvidos;
    private const int puntosganador = 30;
    private int puntosJugador1 = 0;
    private int puntosJugador2 = 0;
    private List<Carta> manoJugador1 = new List<Carta>(); 
    private List<Carta> manoJugador2 = new List<Carta>();
    public TMPro.TextMeshProUGUI textotruco;
 

    void Start()
    {
        gameManager = GameManager.Instance;
        RepartirCartas();
        menuenvidos.SetActive(false);

    }

    public void Envidos()
    {
        menunormal.SetActive(false);
        menuenvidos.SetActive(true);
    }
    public void Atras()
    {
        menunormal.SetActive(true);
        menuenvidos.SetActive(false);
    }

    private void RepartirCartas()
    {
        List<Carta> cartasBarajadas = gameManager.GetCartasBarajadas(); 
        manoJugador1 = gameManager.GetManoJugador1();
        manoJugador2 = gameManager.GetManoJugador2();
    }

    // Llamado cuando el jugador actual canta "Envido"
    public void CantarEnvido()
    {
      if (!Envido)
        {
            Envido = true;
            Debug.Log("Envido");
        }
        else
        {
            Envido = false;
            EnvidoEnvido = true;
            Debug.Log("Envido Envido");
        }
    }

    // Llamado cuando el jugador actual canta "Real Envido"
    public void CantarRealEnvido()
    {
        RealEnvido = true;
        Debug.Log("Real Envido");
    }

    // Llamado cuando el jugador actual canta "Falta Envido"
    public void CantarFaltaEnvido()
    {
        FaltaEnvido = true;
        Debug.Log("Falta Envido");
    }

    // Llamado cuando el jugador responde al canto (acepta o niega)
    public void ResponderEnvido(bool acepta)
    {
        if (turnojugador1)
        {
            if (acepta)
            {
                // Implementa la lógica para aceptar el canto
            }
            else
            {
                // Implementa la lógica para negar el canto
            }

            // Implementa la lógica para actualizar marcadores y manejar fin de partida si alguien llega a 30 puntos
            // ...

            // Implementa la lógica para reiniciar estado de los cantos, cambiar de turno y activar el canvas de canto
            // ...
        }
    }

private int CalcularPuntosEnvido(List<Carta> manoDelJugador)
{
    int puntosEnvido = 0;
    Dictionary<Palo, List<Valor>> palosYValores = new Dictionary<Palo, List<Valor>>();

    foreach (Palo palo in Enum.GetValues(typeof(Palo)))
    {
        palosYValores[palo] = new List<Valor>();
    }

    foreach (Carta carta in manoDelJugador)
    {
        palosYValores[carta.palo].Add(carta.valor);
    }

    int maxPuntaje = 0;
    foreach (KeyValuePair<Palo, List<Valor>> kvp in palosYValores)
    {
        int puntajePalo = 0;

        foreach (Valor valor in kvp.Value)
        {
            int valorNumerico = (int)valor;
            //condición ? valor_si_verdadero : valor_si_falso;
            valorNumerico = (valorNumerico >= 10 && valorNumerico <= 12) ? 0 : valorNumerico;
            puntajePalo += valorNumerico;
        }

        maxPuntaje = Mathf.Max(maxPuntaje, puntajePalo);
    }

    puntosEnvido = maxPuntaje + 20;

    return puntosEnvido;
}



public void VolverARepartir()
{
    Envido = false;
    EnvidoEnvido = false;
    RealEnvido = false;
    FaltaEnvido = false;
    Truco = false;
    Retruco = false;
    Vale4 = false;
    textotruco.text = "Truco";

    manoJugador1.Clear();
    manoJugador2.Clear();

    GameManager.Instance.DestruirCartasAnteriores();

    GameManager.Instance.RealizarInicio();
}


//////////////////////////////TRUCO//////////////////////////////
public void CantarTrucos()
{
    if (!Truco)
    {
            Truco = true;
            Debug.Log("TRUCO");
            textotruco.text = "Re Truco";
    }
    else if(Truco && !Retruco)
    {
            Truco = true;
            Retruco = true;
            Debug.Log("Re truco");
            textotruco.text = "Vale 4";
    }
    else if(Retruco && Truco)
    {
            Truco = true;
            Retruco = true;
            Vale4 = true;
            Debug.Log("Quiero Vale 4");
    }
}
}
