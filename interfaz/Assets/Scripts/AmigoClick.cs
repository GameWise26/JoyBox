using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AmigoClick : MonoBehaviour
{
    public string nombre;
    public string juego;
    public string id;
    public Sprite listo;
    private bool bandera = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void imgAñadir(){
        transform.GetChild(3).GetComponent<Image>().sprite = listo;
    }
    public void AñadirAmigo(){
        if(bandera){
            bandera = false;
            SocketManager.instancia.socket.Emit("añadirAmigo", new {datos = new string[] {id}});
        }
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
