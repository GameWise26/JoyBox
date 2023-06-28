using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collider){
        if(collider.CompareTag("Player"))
        {
            MoverSnake.instancia.fin = true;
            MoverSnake.instancia.Terminar();
        }
    }
}
