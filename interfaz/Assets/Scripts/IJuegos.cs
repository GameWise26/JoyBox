using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IJuegos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SocketManager.instancia.salirJuego){
            Juego juego = new Juego{
                juego = "defecto"
            };
            SocketManager.instancia.Emit("esteJuego",juego);
            SocketManager.instancia.juego = "defecto";
            SocketManager.instancia.salirJuego = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
