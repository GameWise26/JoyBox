using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AmigoClick : MonoBehaviour
{
    public string nombre;
    public string juego;
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Intermedio(){
        CargarAmigo.instancia.MostrarDatos(nombre,juego,id);
    }
    public void irChat(){
        SocketManager.instancia.id_chat = id;
        SocketManager.instancia.nombre_chat = nombre;
        SceneManager.LoadScene("Interfaz_social_chat");
    }
}
