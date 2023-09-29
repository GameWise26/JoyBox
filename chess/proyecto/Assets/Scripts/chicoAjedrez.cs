using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicoAjedrez : MonoBehaviour
{
    // Referencias
    public GameObject controlador;
    public GameObject movePlate;
    public AudioSource SonidoMovimiento;
    public AudioSource SonidoCaptura;

    // Posiciones
    private int cordX = -1;
    private int cordY = -1;

    // Mantiene el rastro de su color, blancas o negras. Tambien mantiene rastro de cuantas veces se ha movido
    private string equipo;
    private int movimientos = 0;

    // Referencias a sprites posibles
    public Sprite N_peon, N_torre, N_caballo, N_alfil, N_reina, N_rey;
    public Sprite B_peon, B_torre, B_caballo, B_alfil, B_reina, B_rey;

    public void SetCoords() {
        float x = cordX;
        float y = cordY;

        x *= 0.66f;  // Esto capaz explota, que sea lo primero a revisar en caso de apocalipsis
        y *= 0.66f;

        x += -2.3f;  // (esto tambien)
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public void Activar() {
        // Asigna el controlador al atributo controlador
        controlador = GameObject.FindGameObjectWithTag("GameController");

        // Ajustar el transform del objeto instanciado
        SetCoords();

        switch (this.name) {
            case "N_peon": this.GetComponent<SpriteRenderer>().sprite = N_peon; equipo = "Negro"; break;
            case "N_torre": this.GetComponent<SpriteRenderer>().sprite = N_torre; equipo = "Negro"; break;
            case "N_caballo": this.GetComponent<SpriteRenderer>().sprite = N_caballo; equipo = "Negro"; break;
            case "N_alfil": this.GetComponent<SpriteRenderer>().sprite = N_alfil; equipo = "Negro"; break;
            case "N_reina": this.GetComponent<SpriteRenderer>().sprite = N_reina; equipo = "Negro"; break;
            case "N_rey": this.GetComponent<SpriteRenderer>().sprite = N_rey; equipo = "Negro"; break;

            case "B_peon": this.GetComponent<SpriteRenderer>().sprite = B_peon; equipo = "Blanco"; break;
            case "B_torre": this.GetComponent<SpriteRenderer>().sprite = B_torre; equipo = "Blanco"; break;
            case "B_caballo": this.GetComponent<SpriteRenderer>().sprite = B_caballo; equipo = "Blanco"; break;
            case "B_alfil": this.GetComponent<SpriteRenderer>().sprite = B_alfil; equipo = "Blanco"; break;
            case "B_reina": this.GetComponent<SpriteRenderer>().sprite = B_reina; equipo = "Blanco"; break;
            case "B_rey": this.GetComponent<SpriteRenderer>().sprite = B_rey; equipo = "Blanco"; break;
        }
    }

    public void OnMouseUp() {
        if (!controlador.GetComponent<Juego>().isGameOver() && controlador.GetComponent<Juego>().GetJugadorActivo() == equipo) {
            DestroyMovePlates();

            IniciarMovePlates();
        }
    }

    public void DestroyMovePlates() {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i= 0; i < movePlates.Length; i++) {
            Destroy(movePlates[i]);
        }
    }

    public void IniciarMovePlates() {
        switch (this.name) {
            case "N_peon":
                PawnMovePlate(cordX, cordY - 1);
                break;

            case "B_peon":
                PawnMovePlate(cordX, cordY + 1);
                break;

            case "N_torre":
            case "B_torre":
                LineaMovePlates(0, 1);
                LineaMovePlates(0, -1);

                LineaMovePlates(1, 0);
                LineaMovePlates(-1, 0);
                break;

            case "N_caballo":
            case "B_caballo":
                LMovePlates();
                break;

            case "N_alfil":
            case "B_alfil":
                LineaMovePlates(1, 1);
                LineaMovePlates(1, -1);

                LineaMovePlates(-1, 1);
                LineaMovePlates(-1, -1);
                break;

            case "N_reina":
            case "B_reina":
                LineaMovePlates(1, 0);
                LineaMovePlates(-1, 0);

                LineaMovePlates(1, 1);
                LineaMovePlates(0, 1);
                LineaMovePlates(-1, 1);

                LineaMovePlates(1, -1);
                LineaMovePlates(0, -1);
                LineaMovePlates(-1, -1);
                break;

            case "N_rey":
            case "B_rey":
                RodearMovePlates();
                break;   
        }
    }

    public void LineaMovePlates(int xIncremento, int yIncremento) {
        Juego c = controlador.GetComponent<Juego>();

        int x = cordX + xIncremento;
        int y = cordY + yIncremento;

        while (c.PosicionEnTablero(x, y) && c.GetPos(x, y) == null) {
            SpawnMovePlate(x, y);
            x += xIncremento;
            y += yIncremento;
        }

        if (c.PosicionEnTablero(x, y) && c.GetPos(x, y).GetComponent<chicoAjedrez>().equipo != this.equipo) {
            SpawnMovePlate(x, y, true);
        }
    }

    public void LMovePlates() {
        SpawnPuntoMovePlate(cordX + 1, cordY + 2);
        SpawnPuntoMovePlate(cordX + 2, cordY + 1);

        SpawnPuntoMovePlate(cordX - 1, cordY + 2);
        SpawnPuntoMovePlate(cordX - 2, cordY + 1);

        SpawnPuntoMovePlate(cordX + 2, cordY - 1);
        SpawnPuntoMovePlate(cordX + 1, cordY - 2);

        SpawnPuntoMovePlate(cordX - 2, cordY - 1);
        SpawnPuntoMovePlate(cordX - 1, cordY - 2);
    }

    public void RodearMovePlates() {
        SpawnPuntoMovePlate(cordX, cordY + 1);
        SpawnPuntoMovePlate(cordX + 1, cordY + 1);
        SpawnPuntoMovePlate(cordX + 1, cordY);
        SpawnPuntoMovePlate(cordX + 1, cordY - 1);
        SpawnPuntoMovePlate(cordX, cordY - 1);
        SpawnPuntoMovePlate(cordX - 1, cordY - 1);
        SpawnPuntoMovePlate(cordX - 1, cordY);
        SpawnPuntoMovePlate(cordX - 1, cordY + 1);
    }

    public void SpawnPuntoMovePlate(int x, int y) { 
        Juego c = controlador.GetComponent<Juego>();

        if (c.PosicionEnTablero(x, y)) {
            if (c.GetPos(x, y) == null)
            {
                SpawnMovePlate(x, y);
            }
            else if (c.GetPos(x, y).GetComponent<chicoAjedrez>().equipo != this.equipo) {
                SpawnMovePlate(x, y, true);
            }
        }
    }

    public void PawnMovePlate(int x, int y) { 
        Juego c = controlador.GetComponent<Juego>();
        if (c.PosicionEnTablero(x, y)) {
            if (this.movimientos == 0 && c.GetPos(x, y + 1) == null) {
                SpawnMovePlate(x, y + 1);
            }

            if (this.movimientos == 0 && c.GetPos(x, y - 1) == null) { 
                SpawnMovePlate(x, y - 1);
            }

            if (c.GetPos(x, y) == null) {
                SpawnMovePlate(x, y);
            }
            
            if (c.PosicionEnTablero(x + 1, y) && c.GetPos(x + 1, y) != null && c.GetPos(x + 1, y).GetComponent<chicoAjedrez>().equipo != this.equipo)
            {
                SpawnMovePlate(x + 1, y, true);
            }

            if (c.PosicionEnTablero(x - 1, y) && c.GetPos(x - 1, y) != null && c.GetPos(x - 1, y).GetComponent<chicoAjedrez>().equipo != this.equipo)
            {
                SpawnMovePlate(x - 1, y, true);
            }
        }
    }

    public void SpawnMovePlate(int matrizX, int matrizY, bool ataque = false) {
        float x = matrizX;
        float y = matrizY;  

        x *= 0.66f;  // Esto capaz explota, que sea lo primero a revisar en caso de apocalipsis
        y *= 0.66f;

        x += -2.3f;  // (esto tambien)
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.ataque = ataque == true ? true : false;
        mpScript.SetReferencia(gameObject);
        mpScript.SetCords(matrizX, matrizY);
    }

    public void MoveSound() {
        SonidoMovimiento.Play();
    }

    public void CaptureSound()
    {
        SonidoCaptura.Play();
    }

    public int GetCordX() {
        return cordX;
    }

    public int GetCordY() {  
        return cordY;
    }

    public void SetCordX(int X) { 
        cordX = X;
    }

    public void SetCordY(int Y) { 
        cordY = Y;
    }

    public int GetMovm() {
        return movimientos;
    }

    public void UpdateMovm() {
        movimientos++;
    }
}
