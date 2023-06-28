using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargarAmigo : MonoBehaviour
{
    public TextMeshProUGUI nombre,juego;
    public GameObject mismo;
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
        this.juego.text = juego;
    }
}
