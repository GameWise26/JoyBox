using UnityEngine;
using System.Collections;

public class MostrarCartas : MonoBehaviour
{
    public SpriteRenderer mazoSpriteRenderer;
    public Sprite[] spritesDeCartas; 
    public float tiempoEntreCartas = 1.0f; 

    void Start()
    {
        StartCoroutine(MostrarCartasSecuencialmente());
    }

    IEnumerator MostrarCartasSecuencialmente()
    {
        foreach (Sprite spriteCarta in spritesDeCartas)
        {
            mazoSpriteRenderer.sprite = spriteCarta;
            yield return new WaitForSeconds(tiempoEntreCartas);
        }
    }
}