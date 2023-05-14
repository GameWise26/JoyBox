using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManzanaComer : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}
