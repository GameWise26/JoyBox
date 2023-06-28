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
            string rest = response.ToString();
            Dictionary<string,string> res = JsonConvert.DeserializeObject<Dictionary<string,string>>(rest.Substring(1,rest.Length-2));
            if(res.ContainsKey("nombre")){
                SocketManager.instancia.nombre = res["nombre"];
                SceneManager.LoadScene("interfaz_home");
            }
            else{
                Debug.Log("Error: "+response);
            }
        });
        SocketManager.instancia.socket.OnUnityThread("camigos", (response) =>{
            string rest = response.ToString();
            SocketManager.instancia.amigos = JsonConvert.DeserializeObject<List<string>>(rest.Substring(1,rest.Length-2));
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
