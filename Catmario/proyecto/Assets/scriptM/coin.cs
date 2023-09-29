using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public int valor = 1;
    public GameManager Gameman;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Gameman.SumarPuntos(valor);
            Destroy(this.gameObject);
        }
    }
}
