using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovimiento : MonoBehaviour
{
    public float velocidad;
    public float distancia;

    private Vector3 startPos;
    private bool movingRight = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }

        if (transform.position.x >= startPos.x + distancia)
        {
            movingRight = false;
        }
        else if (transform.position.x <= startPos.x)
        {
            movingRight = true;
        }
    }
}
