using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManzanaComer : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collider){
        if(collider.CompareTag("Player"))
        {
            MoverSnake.instancia.puntos++;
            MoverSnake.instancia.texto.text = "Score: "+MoverSnake.instancia.puntos;
            Ultimo.instancia.Crearparte();
            Instantiate(prefab,new Vector2(-0.025f+(80.3f * (1f / Screen.dpi)*Random.Range(-10,10)),0.375f+(80.3f * (1f / Screen.dpi)*Random.Range(-6,4))),Quaternion.identity);
            Destroy(gameObject);
        }
        else{
            transform.position = new Vector2(-0.025f+(80.3f * (1f / Screen.dpi)*Random.Range(-10,10)),0.375f+(80.3f * (1f / Screen.dpi)*Random.Range(-6,4)));
        }
    }
}
