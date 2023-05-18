using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerDoblado : MonoBehaviour
{
    private int puntos,contador;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = MoverSnake.instancia.angle;
        puntos = MoverSnake.instancia.puntos;
        contador = MoverSnake.instancia.puntos+2;
    }

    void Update(){
        if(puntos != MoverSnake.instancia.puntos){
            puntos++;
            contador++;
        }
        if(contador == 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("parte"))
        {
            contador--;
        }
    }
}
