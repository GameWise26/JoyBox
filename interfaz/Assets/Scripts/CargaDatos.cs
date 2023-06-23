using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargaDatos : MonoBehaviour
{
    public Text nombre;
    // Start is called before the first frame update
    void Start()
    {
        nombre.text = "Hola "+SocketManager.instancia.nombre;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
