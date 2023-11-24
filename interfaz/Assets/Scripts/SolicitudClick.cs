using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolicitudClick : MonoBehaviour
{
    public string nombre;
    public string id;
    private bool bandera = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AñadirAmigo(){
        SocketManager.instancia.amigos.Add(id);
        SocketManager.instancia.amigos.Add(nombre);
        SocketManager.instancia.amigos.Add("defecto");
    }

    public void procesarSolicitud(string evento){
        SocketManager.instancia.socket.Emit(evento,new { datos = new string[]{id}});
    }
}
