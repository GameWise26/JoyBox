using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comidas : MonoBehaviour
{
    public int valor = 1;
   private GameManager gameManager;

    private void Start()
    {
    gameManager = GameManager.Instance;
    if (gameManager == null)
    {
        Debug.LogError("No se encontró el objeto GameManager en la escena.");
    }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.SumarComidas(valor);
            }
            else
            {
                Debug.LogError("No se encontró el objeto GameManager.");
            }
            
            Destroy(gameObject);
        }
    }
}
