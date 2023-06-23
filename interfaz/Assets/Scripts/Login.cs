using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

class Iniciar{
    public string nombre { get; set; }
    public string contrasenia{get;set;}
}
class Nombre{
    public string nombre { get; set; }
}
public class Login : MonoBehaviour
{
    public TextMeshProUGUI usuario, contrasenia;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("login", (response) =>
        {
            Dictionary<string,string> res = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.ToString().Split('[')[1].Split(']')[0]);
            if(res.ContainsKey("nombre")){
                SocketManager.instancia.nombre = res["nombre"];
                SceneManager.LoadScene("interfaz_home");
            }
            else{
                Debug.Log("Error: "+response);
            }
        });
    }
    public void Enviar(){
        Iniciar form = new Iniciar{
            nombre = usuario.text,
            contrasenia = contrasenia.text
        };
        SocketManager.instancia.socket.Emit("login",JsonConvert.SerializeObject(form));
    }
}
