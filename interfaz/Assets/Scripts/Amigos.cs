using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Amigos : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SocketManager.instancia.amigos.Count;i+=5){
            GameObject f = Instantiate(prefab, transform.position, transform.rotation, transform);
            f.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SocketManager.instancia.amigos[i+1];
            f.transform.GetComponent<AmigoClick>().id = SocketManager.instancia.amigos[i];
            f.transform.GetComponent<AmigoClick>().nombre = SocketManager.instancia.amigos[i+1];
            f.transform.GetComponent<AmigoClick>().juego = SocketManager.instancia.amigos[i+2];
            if(SocketManager.instancia.amigos[i+3] != "defecto"){
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(System.Convert.FromBase64String(SocketManager.instancia.amigos[i+3]));
                f.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            }
            if(SocketManager.instancia.amigos[i+4] == "n")
                f.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color32(255,1,1,255);
        }
    }

}
