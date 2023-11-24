using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class TraerChat : MonoBehaviour
{
    public TextMeshProUGUI nombre;
    public TMP_InputField msg;
    private bool xd = false, dx = true;
    public GameObject amigo,yo;
    public Scrollbar scrollBar;
    private RectTransform rt;
    private Vector2 prt;
    private string miMsg;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("losMensajes",(response)=>{
            List<string> mensajes = JsonConvert.DeserializeObject<List<string>>(response.ToString().Substring(1,response.ToString().Length-2));
            for(int i = 0; i < mensajes.Count; i+=2){
                GameObject f;
                if(mensajes[i] == SocketManager.instancia.id_chat){
                    f = Instantiate(amigo, transform.position, transform.rotation, transform);
                }
                else{
                    f = Instantiate(yo, transform.position, transform.rotation, transform);
                }
                f.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = mensajes[i+1];
            }
        });
        SocketManager.instancia.socket.OnUnityThread("nuevoMensaje",(response)=>{
            Dictionary<string,string> dict = SocketManager.instancia.pasarDict(response);
            if(SocketManager.instancia.id_chat == dict["id"]){
                GameObject f = Instantiate(amigo, transform.position, transform.rotation, transform);
                f.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = dict["msg"];
            }
        });
        SocketManager.instancia.socket.OnUnityThread("mensajeEnviado",(response)=>{
            Dictionary<string,string> dict = SocketManager.instancia.pasarDict(response);
            if(dict["exito"] == "true"){
                GameObject f = Instantiate(yo, transform.position, transform.rotation, transform);
                f.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = miMsg;
            }
        });
        SocketManager.instancia.socket.Emit("mensajes",new {datos = new string[]{SocketManager.instancia.id_chat}});
        nombre.text = SocketManager.instancia.nombre_chat;
        rt = GetComponent<RectTransform>();
        prt = rt.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyUp(KeyCode.Return)){
            dx = true;
        }*/
        if (Input.GetKeyUp(KeyCode.Return) && xd)
        {
            miMsg = msg.text;
            msg.text = "";
            SocketManager.instancia.socket.Emit("mandar",new {datos = new string[]{SocketManager.instancia.id_chat,miMsg}});
        }
        if(prt != rt.sizeDelta){
            prt =  rt.sizeDelta;
            scrollBar.value = 0;
        }
    }
    public void select(){
        xd = true;
    }
    public void deselect(){
        xd = false;
    }
}
