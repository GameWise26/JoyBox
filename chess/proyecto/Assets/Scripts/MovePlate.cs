using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controlador;

    GameObject referencia = null;

    // posiciones DEL TABLERO
    int matrizX;
    int matrizY;

    // false = movimento, true = ataque
    public bool ataque = false;

    public void Start()
    {
        if (ataque) { 
            // Cambiar a rojo
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnMouseUp()
    {
        controlador = GameObject.FindGameObjectWithTag("GameController");

        if (ataque) { 
            GameObject piezaDestruida = controlador.GetComponent<Juego>().GetPos(matrizX, matrizY);

            if (piezaDestruida.name == "B_rey") {
                controlador.GetComponent<Juego>().Ganador("Negras");
            }
            if (piezaDestruida.name == "N_rey")
            {
                controlador.GetComponent<Juego>().Ganador("Blancas");
            }

            Destroy(piezaDestruida);
        }

        controlador.GetComponent<Juego>().SetPosVacio(referencia.GetComponent<chicoAjedrez>().GetCordX(), referencia.GetComponent<chicoAjedrez>().GetCordY());

        referencia.GetComponent<chicoAjedrez>().SetCordX(matrizX);
        referencia.GetComponent<chicoAjedrez>().SetCordY(matrizY);
        referencia.GetComponent<chicoAjedrez>().SetCoords();

        controlador.GetComponent<Juego>().SetPos(referencia);
        referencia.GetComponent<chicoAjedrez>().UpdateMovm();

        referencia.GetComponent<chicoAjedrez>().DestroyMovePlates();
        controlador.GetComponent<Juego>().SiguienteTurno();
        controlador.GetComponent<Juego>().updateActivo();
    }

    public void SetCords(int x, int y)
    {
        matrizX = x;
        matrizY = y;
    }

    public void SetReferencia(GameObject obj)
    {
        referencia = obj;
    }

    public GameObject GetReferencia() { 
        return referencia;
    }
}
