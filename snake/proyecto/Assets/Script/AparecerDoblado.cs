using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerDoblado : MonoBehaviour
{
    public int puntos,contador;
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
            collider.gameObject.transform.rotation = Quaternion.Euler(0,0,angle);
        }
        else if(collider.CompareTag("cola")){
            if(collider.gameObject.transform.position.x < transform.position.x - 0.1f || collider.gameObject.transform.position.x > transform.position.x + 0.1f || collider.gameObject.transform.position.y < transform.position.y -0.1f || collider.gameObject.transform.position.y > transform.position.y + 0.1f) return;
            contador--;
            collider.gameObject.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
