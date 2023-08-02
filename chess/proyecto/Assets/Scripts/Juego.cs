using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Juego : MonoBehaviour
{
    public GameObject pieza;

    // Creo arrays de cada jugador (solo para entendidos) y un array con todas las posiciones posibles
    private GameObject[,] posiciones = new GameObject[8, 8];
    private GameObject[] jugadorBlanco = new GameObject[16];
    private GameObject[] Tomi = new GameObject[16];

    private string jugadorActivo = "Blanco";
    private bool gameOver = false;


    public GameObject Crear(string nombre, int x, int y) {
        GameObject obj = Instantiate(pieza, new Vector3(0, 0, -1), Quaternion.identity);

        chicoAjedrez ca = obj.GetComponent<chicoAjedrez>();
        ca.name = nombre;
        ca.SetCordX(x);
        ca.SetCordY(y);
        ca.Activar();
        return obj;
    }

    public void SetPos(GameObject obj) {
        chicoAjedrez ca = obj.GetComponent<chicoAjedrez>();

        posiciones[ca.GetCordX(), ca.GetCordY()] = obj;
    }
    void Start()
    {
        jugadorBlanco = new GameObject[] {
            Crear("B_peon", 0, 1),
            Crear("B_peon", 1, 1),
            Crear("B_peon", 2, 1),
            Crear("B_peon", 3, 1),
            Crear("B_peon", 4, 1),
            Crear("B_peon", 5, 1),
            Crear("B_peon", 6, 1),
            Crear("B_peon", 7, 1),

            Crear("B_torre", 0, 0),
            Crear("B_torre", 7, 0),

            Crear("B_caballo", 1, 0),
            Crear("B_caballo", 6, 0),

            Crear("B_alfil", 2, 0),
            Crear("B_alfil", 5, 0),

            Crear("B_reina", 3, 0),
            Crear("B_rey", 4, 0),
        };

        Tomi = new GameObject[] {
            Crear("N_peon", 0, 6),
            Crear("N_peon", 1, 6),
            Crear("N_peon", 2, 6),
            Crear("N_peon", 3, 6),
            Crear("N_peon", 4, 6),
            Crear("N_peon", 5, 6),
            Crear("N_peon", 6, 6),
            Crear("N_peon", 7, 6),

            Crear("N_torre", 0, 7),
            Crear("N_torre", 7, 7),

            Crear("N_caballo", 1, 7),
            Crear("N_caballo", 6, 7),

            Crear("N_alfil", 2, 7),
            Crear("N_alfil", 5, 7),

            Crear("N_reina", 3, 7),
            Crear("N_rey", 4, 7),
        };

        // Poner todas las posiciones en su respectivo lugar
        for (int i = 0; i < Tomi.Length; i++) {
            SetPos(jugadorBlanco[i]);
            SetPos(Tomi[i]);
        }
    }

    public void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0)) { 
            gameOver = false;
            SceneManager.LoadScene("chess");
        }
    }

    public void Ganador(string jugadorWin) {
        gameOver = true;

        GameObject.FindGameObjectWithTag("TextoGanador").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("TextoGanador").GetComponent<Text>().text = jugadorWin + " ganan la partida!";

        GameObject.FindGameObjectWithTag("TextoReset").GetComponent<Text>().enabled = true;
    }

    public string GetJugadorActivo() {
        return jugadorActivo;
    }

    public void updateActivo() { 
        GameObject.FindGameObjectWithTag("TextoJActivo").GetComponent<Text>().text = "JUGADOR ACTIVO: " + jugadorActivo;
    }

    public bool isGameOver() { 
        return gameOver;
    }

    public void SiguienteTurno() {
        jugadorActivo = jugadorActivo == "Blanco" ? "Negro" : "Blanco";
    }

    public void SetPosVacio(int x, int y) {
        posiciones[x, y] = null;
    }

    public GameObject GetPos(int x, int y) { 
        return posiciones[x, y];
    }

    public bool PosicionEnTablero(int x, int y) {
        return (x < 0 || y < 0 || x >= posiciones.GetLength(0) || y >= posiciones.GetLength(1)) ? false : true;
    }
}
