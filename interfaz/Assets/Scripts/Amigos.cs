using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Amigos : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SocketManager.instancia.amigos.Count;i+=3){
            GameObject f = Instantiate(prefab, transform.position, transform.rotation, transform);
            f.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SocketManager.instancia.amigos[i+1];
            f.transform.GetComponent<AmigoClick>().nombre = SocketManager.instancia.amigos[i+1];
            f.transform.GetComponent<AmigoClick>().juego = SocketManager.instancia.amigos[i+2];
        }
    }

}
