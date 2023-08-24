using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuscarUG : MonoBehaviour
{
    public TMP_InputField busqueda;
    public GameObject prefab;
    private bool xd = false, dx = true, tipoBuscar = true;
    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.socket.OnUnityThread("losUsuarios", (response) =>
        {
            List<string> usuarios = SocketManager.instancia.pasarLista(response);
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
               Destroy(transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < usuarios.Count; i += 2)
            {
                GameObject f = Instantiate(prefab, transform.position, transform.rotation, transform);
                f.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = usuarios[i + 1];
                f.transform.GetComponent<AmigoClick>().id = usuarios[i];
                f.transform.GetComponent<AmigoClick>().nombre = usuarios[i+1];
            }
        });
        SocketManager.instancia.socket.OnUnityThread("añadioAmigo", (response) => {
            List<string> resultado = SocketManager.instancia.pasarLista(response);
            for(var i = 0; i < transform.childCount; i++){
                if(resultado[0] == transform.GetChild(i).GetComponent<AmigoClick>().id){
                    transform.GetChild(i).GetComponent<AmigoClick>().imgAñadir();
                }   
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            dx = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && xd && dx) buscar();
    }
    public void select()
    {
        xd = true;
    }
    public void deselect()
    {
        xd = false;
    }
    public void buscar()
    {
        dx = false;
        if (tipoBuscar)
        {
            SocketManager.instancia.socket.Emit("buscarUsuarios", new { datos = new string[] { busqueda.text } });
        }
        else
        {
            SocketManager.instancia.socket.Emit("buscarGrupos", new { datos = new string[] { busqueda.text } });
        }
    }
    public void usuarios()
    {
        tipoBuscar = true;
    }
    public void grupos()
    {
        tipoBuscar = false;
    }
}
