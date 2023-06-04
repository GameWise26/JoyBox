using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UserDatabase : MonoBehaviour
{
    public static UserDatabase instancia;
    public int id;
    private bool bandera;
    public Dictionary<string,string> res;
    public Text recordText;
    public string apiUrl = "https://joyboxapp.000webhostapp.com/seleccionarPuntaje.php";
    public string apiUrl1 = "https://joyboxapp.000webhostapp.com/actualizarPuntaje.php";

    private void Awake(){
        if(UserDatabase.instancia == null){
            UserDatabase.instancia = this;    
        }
    }

    void Start()
    {
        res = new Dictionary<string,string>();
        id = 1;
        StartCoroutine(UploadUserData(apiUrl,new string[]{"idUser"},new string[]{id.ToString()},valor => res = valor));
    }

    void Update(){
        if(!bandera && res.Where(v => v.Key == "puntaje").Any()){
            bandera = true;
            recordText.text = "Record: "+res["puntaje"];
        }
    }

    public IEnumerator UploadUserData(string url,string[] campos, string[] valores, System.Action<Dictionary<string,string>> callback)
    {
        WWWForm form = new WWWForm();
        for(int i = 0; i < campos.Length; i++){
            form.AddField(campos[i], valores[i]);
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud: " + webRequest.error);
            }
            else
            {
                // La solicitud fue exitosa, procesa la respuesta recibida
                string response = webRequest.downloadHandler.text;
                Dictionary<string,string> resultados = new Dictionary<string,string>();
                string[] subs = response.Split("|");
                for(int i = 0; i < subs.Length/2; i+=2){
                    resultados[subs[i]] = subs[i+1];
                }
                callback.Invoke(resultados);
                // Puedes realizar cualquier procesamiento adicional aquí
            }
        }
    }
}