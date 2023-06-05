using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;


public class DataItem
{
    public string idUser;
}

public class UserDatabase : MonoBehaviour
{
    public static UserDatabase instancia;
    public string apiUrl = "https://joyboxapp.000webhostapp.com/seleccionarPuntaje.php";
    public string apiUrl1 = "https://joyboxapp.000webhostapp.com/actualizarPuntaje.php";
    public Dictionary<string,string> aver;
    public int id;

    private void Awake(){
        if(UserDatabase.instancia == null){
            UserDatabase.instancia = this;
            DontDestroyOnLoad(this);
        }
    }

    async void Start()
    {
        id = 1;
        aver = await UploadUserData(apiUrl,new string[]{"idUser"},new string[]{id.ToString()});
    }

    public async Task<Dictionary<string,string>> UploadUserData(string url,string[] campos, string[] valores)
    {
        Dictionary<string,string> response = new Dictionary<string,string>();
        WWWForm form = new WWWForm();
        for(int i = 0; i < campos.Length; i++){
            form.AddField(campos[i], valores[i]);
        }

        UnityWebRequest webRequest = UnityWebRequest.Post(url,form);

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la solicitud: " + webRequest.error);
        }
        else
        {
            string texto = webRequest.downloadHandler.text;
            string[] subs = texto.Split("|");
            for(int i = 0; i < subs.Length/2; i+=2){
                response[subs[i]] = subs[i+1];
            }

        }
        return response;
    }
}