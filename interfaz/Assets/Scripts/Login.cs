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
    public TMP_InputField usuario, contrasenia;
    public TextMeshProUGUI MsgBox;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("login", (response) =>
        {
            List<string> res = SocketManager.instancia.pasarLista(response);
            //string rest = response.ToString();
            //Dictionary<string,string> res = JsonConvert.DeserializeObject<Dictionary<string,string>>(rest.Substring(1,rest.Length-2));
            if(res[1] == "1"){
                SocketManager.instancia.nombre = res[0];
                SceneManager.LoadScene("interfaz_home");
            }
            /*else if(res["exito"] == "EnUso"){
                MsgBoxError.text = "La cuenta a la que intentas acceder ya se encuentra en uso actualmente";
            }
            else if(res["exito"] == "CI"){
                MsgBoxError.text = "El usuario o la contraseña son incorrectos";
            }
            else if(res["exito"] == "IX"){
                MsgBoxError.text = "Error inesperado. Vuelva a intentarlo mas tarde";
            }*/
        });
        SocketManager.instancia.socket.OnUnityThread("camigos", (response) =>{
            string rest = response.ToString();
            SocketManager.instancia.amigos = JsonConvert.DeserializeObject<List<string>>(rest.Substring(1,rest.Length-2));
        });
    }
    public void Enviar(){
        SocketManager.instancia.socket.Emit("login",new {datos = new string[]{usuario.text,contrasenia.text}});
    }
}
