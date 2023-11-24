using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Solicitudes : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("lasSolicitudes", (response) =>{
            List<string> usuarios = SocketManager.instancia.pasarLista(response);
            for(int i = 0; i < usuarios.Count;i+=2){
                GameObject f = Instantiate(prefab, transform.position, transform.rotation, transform);
                f.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = usuarios[i+1];
                f.transform.GetComponent<SolicitudClick>().id = usuarios[i];
                f.transform.GetComponent<SolicitudClick>().nombre = usuarios[i+1];
            }
        });
        SocketManager.instancia.socket.OnUnityThread("solicitudRechazada", (response) =>{
            List<string> resultado = SocketManager.instancia.pasarLista(response);
            for(var i = 0; i < transform.childCount; i++){
                if(resultado[0] == transform.GetChild(i).GetComponent<SolicitudClick>().id){
                    Destroy(transform.GetChild(i).gameObject);
                }   
            }
        });
        SocketManager.instancia.socket.OnUnityThread("solicitudAceptada", (response) =>{
            List<string> resultado = SocketManager.instancia.pasarLista(response);
            for(var i = 0; i < transform.childCount; i++){
                if(resultado[0] == transform.GetChild(i).GetComponent<SolicitudClick>().id){
                    transform.GetChild(i).GetComponent<SolicitudClick>().AñadirAmigo();
                    Destroy(transform.GetChild(i).gameObject);
                }   
            }
        });
        SocketManager.instancia.socket.Emit("pedirSolicitudes",new { datos = new string[]{}});
    }
}
