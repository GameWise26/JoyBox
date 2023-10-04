using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.Linq;

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
    //aceptar o negar
    //ACEPATPC Y NEGARPC -> LOS CANTOS FUERON CANTADOS POR LA PC
    //LOS OTROS LOS CANTAMOSNOSOTROS
    private bool Aceptado = false;
    private bool Negado = false;
    private bool AceptarPC = false;
    private bool NegarPC = false;
    //cosas
    private bool Mazo = false;
    private bool turnojugador1 = true; 
    private GameManager gameManager;
    public GameObject canvascantos;
    public GameObject menunormal;
    public GameObject menuenvidos;
    public GameObject ResultadoEnvido;
    private const int puntosvictoria = 30;
    private int puntosJugador = 0;
    private int puntosPC = 0;

    private int EnvidoJugador = 0;
    private int EnvidoPC = 0;

    private int puntosganador = 0;
    private List<Carta> ManoPC = new List<Carta>(); 
    private List<Carta> ManoJugador = new List<Carta>();
    public TMPro.TextMeshProUGUI textotruco;
    public TMPro.TextMeshProUGUI TextoTitulo;
    public TMPro.TextMeshProUGUI Texto1;
    public TMPro.TextMeshProUGUI Texto2;
    public TMPro.TextMeshProUGUI TextoPuntajespara;
    

    void Start()
    {   
        ManoPC.Clear();
        ManoJugador.Clear();

        gameManager = GameManager.Instance;
        RepartirCartas();

        menuenvidos.SetActive(false);
        ResultadoEnvido.SetActive(false);
        ManoPC = gameManager.GetManoJugador1();
        ManoJugador = gameManager.GetManoJugador2();
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

        ManoPC.Clear();
        ManoJugador.Clear();

        GameManager.Instance.DestruirCartasAnteriores();
        GameManager.Instance.RealizarInicio();

        List<Carta> cartasBarajadas = gameManager.GetCartasBarajadas(); 
        ManoPC = gameManager.GetManoJugador1();
        ManoJugador = gameManager.GetManoJugador2();
        
    }

    public void Atras()
    {
        menunormal.SetActive(true);
        menuenvidos.SetActive(false);
    }

    private void RepartirCartas()
    {
        List<Carta> cartasBarajadas = gameManager.GetCartasBarajadas(); 
        ManoPC = gameManager.GetManoJugador1();
        ManoJugador = gameManager.GetManoJugador2();
    }

//////////////ENVIDOS////////////////////
    public void Envidos()
    {
        menunormal.SetActive(false);
        menuenvidos.SetActive(true);
    }

    // Llamado cuando el jugador actual canta "Envido"
    public void CantarEnvido()
{
    if (!Envido)
    {
        Envido = true;
        AceptarPC = true;
        Debug.Log("Envido");
    }
    else
    {
        Envido = false;
        EnvidoEnvido = true;
        AceptarPC = true;
        Debug.Log("Envido Envido");
    }
    AnunciarGanador();
}

    // Llamado cuando el jugador actual canta "Real Envido"
    public void CantarRealEnvido()
    {
        RealEnvido = true;
        AceptarPC = true;
        Debug.Log("Real Envido");
        AnunciarGanador();
    }

    // Llamado cuando el jugador actual canta "Falta Envido"
    public void CantarFaltaEnvido()
    {
        FaltaEnvido = true;
        AceptarPC = true;
        Debug.Log("Falta Envido");
        AnunciarGanador();
    }

    // Llamado cuando el jugador responde al canto (acepta o niega)
    public void ResponderEnvido(bool acepta)
    {
        if (turnojugador1)
        {
            if (acepta)
            {
                
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
        int PuntosEnvidos = 0;

        // Diccionario para contar las cartas por palo
        Dictionary<Palo, List<Carta>> cartasPorPalo = new Dictionary<Palo, List<Carta>>();
        foreach (Palo palo in Enum.GetValues(typeof(Palo)))
        {
            cartasPorPalo[palo] = new List<Carta>();
        }
        foreach (Carta carta in manoDelJugador)
        {
            cartasPorPalo[carta.palo].Add(carta);
        }

        // Buscar si hay 3 cartas del mismo palo
        foreach (Palo palo in Enum.GetValues(typeof(Palo)))
        {
            if (cartasPorPalo[palo].Count >= 3)
            {
                int sumaValores = cartasPorPalo[palo]
                    .OrderByDescending(carta => carta.ValorNumerico)
                    .Take(3) 
                    .Sum(carta => (carta.ValorNumerico > 9 && carta.ValorNumerico <= 12) ? 0 : carta.ValorNumerico);

                PuntosEnvidos = sumaValores + 20;
                Debug.Log("PuntosEnvidos (3 cartas iguales): " + PuntosEnvidos);
                return PuntosEnvidos;
            }
        }

        // Buscar si hay 2 cartas del mismo palo
        foreach (Palo palo in Enum.GetValues(typeof(Palo)))
        {
            if (cartasPorPalo[palo].Count == 2)
            {
                int sumaValores = cartasPorPalo[palo]
                    .Sum(carta => (carta.ValorNumerico > 9) ? 0 : carta.ValorNumerico);

                PuntosEnvidos = sumaValores + 20;
                Debug.Log("PuntosEnvidos (2 cartas iguales): " + PuntosEnvidos);
                return PuntosEnvidos;
            }
        }

        // No hay cartas iguales
        int maxValorNumerico = manoDelJugador
            .Where(carta => carta.ValorNumerico <= 9) 
            .Select(carta => carta.ValorNumerico)
            .DefaultIfEmpty(0) 
            .Max();

        PuntosEnvidos = (maxValorNumerico > 9) ? 0 : maxValorNumerico;
        Debug.Log("PuntosEnvidos después del cálculo: " + PuntosEnvidos);

        return PuntosEnvidos;
    }

    public void AnunciarGanador()
    {
        ManoPC = gameManager.GetManoJugador1();
        ManoJugador = gameManager.GetManoJugador2();
        ResultadoEnvido.SetActive(true);
        EnvidoPC = CalcularPuntosEnvido(ManoPC);
        EnvidoJugador = CalcularPuntosEnvido(ManoJugador); 

        if(EnvidoJugador > EnvidoPC)
        {
            if(Aceptado)
            {
                TextoTitulo.text = "GANASTE";
                Texto1.text = "jugador : " + EnvidoJugador;
                Texto2.text = "PC : Son buenas";
                TextoPuntajespara.text = "Los puntos van para el JUGADOR";

            }
            else if(AceptarPC)
            {
                TextoTitulo.text = "GANASTE";
                Texto1.text = "PC : " + EnvidoPC;
                Texto2.text = "jugador : " + EnvidoJugador;
                TextoPuntajespara.text = "Los puntos van para el JUGADOR";
            }
        }
        else if(EnvidoPC > EnvidoJugador)
        {
            if(Aceptado)
            {
                TextoTitulo.text = "PERDISTE";
                Texto1.text = "jugador : " + EnvidoJugador;
                Texto2.text = "PC : " + EnvidoPC + " son mejores";
                TextoPuntajespara.text = "Los puntos van para la PC";

            }
            else if(AceptarPC)
            {
                TextoTitulo.text = "GANASTE";
                Texto1.text = "PC : " + EnvidoPC;
                Texto2.text = "jugador : Son buenas";
                TextoPuntajespara.text = "Los puntos van para la PC";
            }
        }
         else if(EnvidoPC == EnvidoJugador)
        {
            if(turnojugador1 == true )
            {
                TextoTitulo.text = "EMPATE";
                Texto1.text = "jugador : " + EnvidoJugador;
                Texto2.text = "PC : " + EnvidoPC;
                TextoPuntajespara.text = "Los puntos van para el JUGADOR";   
            }
            else
            {
                TextoTitulo.text = "EMPATE";
                Texto1.text = "jugador : " + EnvidoJugador;
                Texto2.text = "PC : " + EnvidoPC;
                TextoPuntajespara.text = "Los puntos van para la PC"; 
            }
        }
    }

    public void AceptarResultado()
    {
        ResultadoEnvido.SetActive(false);
    }

    private void EnvidoCompleto()
    {
        
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
