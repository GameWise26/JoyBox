using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigoperseguidor : MonoBehaviour
{
    public Transform objetivo;
    public float speed;
    public bool deboPerseguir;
    public float distancia; //lo lejos que estamos
    public float distanciaAbsoluta;
    private bool enElAire;

    // Start is called before the first frame update
    void Start()
    {
        if(objetivo == null)
        {
            deboPerseguir = false; // No hay objetivo, así que no perseguimos
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo == null)
        {
            deboPerseguir = false; // No hay objetivo, así que no perseguimos
            return; // Salir del método Update
        }

        distancia = objetivo.position.x - transform.position.x;

        distanciaAbsoluta = Mathf.Abs(distancia);

        if (deboPerseguir && !enElAire)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }

        if (distancia > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (distancia < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (distanciaAbsoluta < 5)
        {
            deboPerseguir = true;
        }
        else
        {
            deboPerseguir = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            enElAire = false; // Está en el suelo
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            enElAire = true; // Está en el aire
        }
    }
}

