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

public class Register : MonoBehaviour
{
    public TextMeshProUGUI usuario,contrasenia,email,rcontrasenia,edad,msgbox;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("registro", (response) =>
        {
            Dictionary<string,bool> res = JsonConvert.DeserializeObject<Dictionary<string,bool>>(response.ToString().Split('[')[1].Split(']')[0]);
            if(res["exito"]) msgbox.text = "Se registro correctamente, ahora inicie sesion";
            else msgbox.text = "No se pudo registrar, verifique los datos ingresados";
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
