using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CargarAmigo : MonoBehaviour
{
    public TextMeshProUGUI nombre,juego;
    public string juegod,id;
    public GameObject mismo,ojo,estado;
    public static CargarAmigo instancia;
    private void Awake(){
        if(CargarAmigo.instancia == null){
            CargarAmigo.instancia = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MostrarDatos(string nombre, string juego, string id){
        mismo.SetActive(true);
        this.nombre.text = nombre;
        if(juego == "defecto"){
            ojo.SetActive(false);
            estado.SetActive(false);
        }
        else{
            this.juego.text = "Jugando al " + juego;
            juegod = juego;
            this.id = id;
        }
    }
    public void irPartida(){
        SocketManager.instancia.id_espec = id;
        SceneManager.LoadScene(juegod);
    }
}
