using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlPC : MonoBehaviour
{
    public GameManager gameManager; // Asegúrate de asignar el GameManager en el Inspector

    private bool turnoPC = false; // Variable para controlar el turno de la PC
    private float tiempoEspera = 2.0f; // Tiempo de espera antes de que la PC realice una acción (ajusta según tu juego)

    private void Start()
    {
        // Aquí puedes inicializar cualquier cosa que necesites para la lógica de la PC
        // Por ejemplo, cargar estrategias, comportamientos, etc.
    }

    private void Update()
    {
        if (turnoPC)
        {
            // La PC tiene un turno, así que aquí puedes implementar la lógica de su acción

            // Ejemplo: Jugar una carta aleatoria después de un tiempo de espera
            StartCoroutine(JugarCartaDespuesDeEspera());

            turnoPC = false; // Marcar que la PC ya ha jugado su turno
        }
    }

    // Ejemplo de jugar una carta aleatoria después de un tiempo de espera
    private IEnumerator JugarCartaDespuesDeEspera()
{
    yield return new WaitForSeconds(tiempoEspera);

    List<Carta> manoPC = gameManager.GetManoJugador1(); // Obtén la mano de la PC

    if (manoPC.Count > 0)
    {
        // Encuentra la carta con la segunda mejor ValorJuego en la mano
        int mejorValorJuego = -1;
        int segundoMejorValorJuego = -1;
        int indiceMejorCarta = -1;
        int indiceSegundaMejorCarta = -1;

        for (int i = 0; i < manoPC.Count; i++)
        {
            if (manoPC[i].ValorJuego > mejorValorJuego)
            {
                segundoMejorValorJuego = mejorValorJuego;
                indiceSegundaMejorCarta = indiceMejorCarta;

                mejorValorJuego = manoPC[i].ValorJuego;
                indiceMejorCarta = i;
            }
            else if (manoPC[i].ValorJuego > segundoMejorValorJuego)
            {
                segundoMejorValorJuego = manoPC[i].ValorJuego;
                indiceSegundaMejorCarta = i;
            }
        }

        Carta cartaAReproducir = manoPC[indiceSegundaMejorCarta];
        // gameManager.JugarCartaPC(cartaAReproducir);

        // Asegúrate de eliminar la carta de la mano de la PC después de jugarla
        manoPC.RemoveAt(indiceSegundaMejorCarta);
    }
}

    // Llamar a esta función para permitir que la PC juegue su turno
    public void PermitirTurnoPC()
    {
        turnoPC = true;
    }
}
///voy a explotarte ia de los cojones
