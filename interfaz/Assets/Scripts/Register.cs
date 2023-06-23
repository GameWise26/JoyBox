using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

class Formulario{
    public string nombre { get; set; }
    public string edad{get;set;}
    public string contrasenia{get;set;}
    public string rcontrasenia{get;set;}
    public string correo{get;set;}
}
class Resultado{
    public bool exito;
}

public class Register : MonoBehaviour
{
    public TextMeshProUGUI usuario,contrasenia,email,rcontrasenia,edad;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("registro", (response) =>
        {
            Debug.Log(JsonUtility.FromJson<Resultado>(response.ToString()).exito);
        });
    }

    public void Enviar(){
        Formulario form = new Formulario{
            nombre = usuario.text,
            edad = edad.text,
            contrasenia = contrasenia.text,
            rcontrasenia = rcontrasenia.text,
            correo = email.text
        };
        SocketManager.instancia.socket.Emit("registro",JsonConvert.SerializeObject(form));
    }
    public void irAlLogin(){
        SceneManager.LoadScene("interfaz_inicio_sesion");
    }
}
