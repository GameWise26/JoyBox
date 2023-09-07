using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


public class GameController_PantallaCarga : MonoBehaviour
{
    private bool banSaveSession = false, dou = true, dou1 = true, llave1 = false, llave2 = false;
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
                llave1 = true;
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
            llave2 = true;
        });
        if(PlayerPrefs.HasKey("nombre") && PlayerPrefs.HasKey("contraseña") && PlayerPrefs.GetString("nombre") != "" && PlayerPrefs.GetString("contraseña") != ""){
            banSaveSession = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!banSaveSession && SocketManager.instancia.banInicio && dou)
        {
            dou = false;
            SceneManager.LoadScene("interfaz_inicio_sesion");
        }
        else if(banSaveSession && SocketManager.instancia.banInicio && dou1){
            dou1 = false;
            SocketManager.instancia.socket.Emit("login",new {datos = new string[]{PlayerPrefs.GetString("nombre"),PlayerPrefs.GetString("contraseña")}});
        }
        if(SocketManager.instancia.banInicio && dou && llave1 && llave2){
            dou = false;
            SceneManager.LoadScene("interfaz_home");
        }
    }
}
