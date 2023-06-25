using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_mov_salto : MonoBehaviour
{
    public float speed;
    public bool esDerecha;
    public float contadorT;
    public float tiempoParaCambiar = 4f;
    public float FuerzaSalto = 4f;
    public Rigidbody2D rbd;

    // Start is called before the first frame update
    void Start()
    {
        contadorT = tiempoParaCambiar;
        InvokeRepeating("Saltar" , 2f , 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (esDerecha == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (esDerecha == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }

        contadorT -= Time.deltaTime;
        if (contadorT <= 0)
        {
            contadorT = tiempoParaCambiar;
            esDerecha = !esDerecha;
        }
    }

    public void Saltar()
    {
        rbd.AddForce(Vector2.up * FuerzaSalto, ForceMode2D.Impulse);
    }
}
