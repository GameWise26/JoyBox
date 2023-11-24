using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class Configuracion_Controller : MonoBehaviour
{
    char[] CaracteresEspaciales = { '!', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };
    public TMP_InputField contrasenia, ncontrasenia, ccontrasenia;
    public TextMeshProUGUI MsgBox;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("cambiarContra", (response) =>{
            List<string> res = SocketManager.instancia.pasarLista(response);
            //string rest = response.ToString();
            //Dictionary<string,string> res = JsonConvert.DeserializeObject<Dictionary<string,string>>(rest.Substring(1,rest.Length-2));
            MsgBox.text = res[0] == "1" ? "Contraseña cambiada con exito" : "No se pudo cambiar la contraseña";
            if(res[0] == "1" && PlayerPrefs.HasKey("contraseña")){
                PlayerPrefs.SetString("contraseña", res[1]);
                PlayerPrefs.Save();
            }
        });
        SocketManager.instancia.socket.OnUnityThread("logout", (response) =>{
            SceneManager.LoadScene("interfaz_inicio_sesion");
        });
        SocketManager.instancia.socket.OnUnityThread("eliminar", (response) =>{
            CerrarSesion();
        });
    }

    // Update is called once per frame

    public void CerrarSesion(){
        SocketManager.instancia.socket.Emit("logout",new {datos=new string[]{}});
        if(PlayerPrefs.HasKey("nombre") && PlayerPrefs.HasKey("contraseña")){
            PlayerPrefs.SetString("nombre", "");
            PlayerPrefs.SetString("contraseña", "");
            PlayerPrefs.Save();
        }
    }

    public void Eliminar(){
        SocketManager.instancia.socket.Emit("eliminar",new {datos=new string[]{}});
    }

    public void CambiarContra(){
        if(ccontrasenia.text == "" || ncontrasenia.text == "" || contrasenia.text == "") return;
        if(!ValidarSeguridadContrasenia(ccontrasenia.text) || !ValidarSeguridadContrasenia(ncontrasenia.text)) MsgBox.text = "Se requiere al menos una mayuscula, miniscula, numero y caracter especial. Máximo de caracteres 20 y minimo 9";
        else if(ncontrasenia.text != ccontrasenia.text) MsgBox.text = "Las contraseñas no coinciden";
        else SocketManager.instancia.socket.Emit("cambiarContra",new {datos=new string[]{contrasenia.text,ncontrasenia.text}});
    }
    private bool ValidarSeguridadContrasenia(string contrasenia)
    {
        if(Regex.IsMatch(contrasenia, @"[A-Z]") && Regex.IsMatch(contrasenia, @"[a-z]") && Regex.IsMatch(contrasenia, @"\d") && contrasenia.Any(c => CaracteresEspaciales.Contains(c)) && contrasenia.Length >= 9 && contrasenia.Length <= 20) return true;
        return false;
    }
}
