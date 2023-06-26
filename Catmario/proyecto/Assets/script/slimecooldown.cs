using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimecooldown : MonoBehaviour
{
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer en el objeto slimecooldown.");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar)
            {
                return;
            }

            puedeAtacar = false;

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0.5f;
                spriteRenderer.color = color;
            }

            GameManager.Instance?.PerderVida();

            michimovimiento michiMovimiento = other.gameObject.GetComponent<michimovimiento>();
            if (michiMovimiento != null)
            {
                michiMovimiento.AplicarGolpe();
            }

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    private void Update()
    {

    }
}
