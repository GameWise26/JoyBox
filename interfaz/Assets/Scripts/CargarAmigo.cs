using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CargarAmigo : MonoBehaviour
{
    public TextMeshProUGUI nombre,juego;
    public string juegod;
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

    public void MostrarDatos(string nombre, string juego){
        mismo.SetActive(true);
        this.nombre.text = nombre;
        if(juego == "defecto"){
            ojo.SetActive(false);
            estado.SetActive(false);
        }
        else{
            this.juego.text = "Jugando al " + juego;
            juegod = juego;
        }
    }
    public void irPartida(){
        SceneManager.LoadScene(juegod);
    }
}
