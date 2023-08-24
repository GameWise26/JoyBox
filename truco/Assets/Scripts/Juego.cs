using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Lógica para calcular los puntos del Envido
    private int CalcularPuntosEnvido(List<Carta> manoDelJugador)
    {
        int puntosEnvido = 0;

        // Implementa aquí la lógica para calcular los puntos del Envido según las reglas que proporcionaste
        return puntosEnvido;
    }

    private int CalcularPuntosEnvidoEnvido(List<Carta> manoDelJugador)
    {
        int puntosEnvidoEnvido = 0;

        // Implementa aquí la lógica para calcular los puntos del Envido Envido según las reglas que proporcionaste

        return puntosEnvidoEnvido; 
    }

    private int CalcularPuntosRealEnvido(List<Carta> manoDelJugador)
    {
        int puntosRealEnvido = 0;

        // Implementa aquí la lógica para calcular los puntos del Real Envido según las reglas que proporcionaste

        return puntosRealEnvido; 
    }

    private int CalcularPuntosFaltaEnvido(List<Carta> manoDelJugador)
    {
        int puntosFaltaEnvido = 0;
        return puntosFaltaEnvido; 
    }

   public void VolverARepartir()
    {
    
    // Resetea las variables de canto (si es necesario)
    Envido = false;
    EnvidoEnvido = false;
    RealEnvido = false;
    FaltaEnvido = false;
    Truco = false;
    Retruco = false;
    Vale4 = false;
        
    // Limpiar las manos de ambos jugadores
    manoJugador1.Clear();
    manoJugador2.Clear();
    
    // Volver a repartir las cartas
    
    GameManager.Instance.RealizarInicio();
    }   

}
