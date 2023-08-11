using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject[] cartaPrefabArray;
    public Carta[] imagenesDeCartas;

    private List<GameObject> cartas;

    public float espacioHorizontal = 2.0f;
    public float espacioVertical = 2.0f;
    public float separacionEntreGrupos = 1.0f;

    private Vector3 _posicionDeseada = new Vector3(0.0f, -4.93f, 0.0f);
    public Vector3 PosicionDeseada => _posicionDeseada;

    private bool allCardsArrived = false;
    private GameObject lastArrivedCard = null;

    void Start()
    {
        cartas = new List<GameObject>();
        GenerarYBarajarCartas();
        RepartirCartas();

        for (int i = 3; i < 6; i++)
        {
            cartas[i].AddComponent<CartaBehavior>();
        }
    }

    void GenerarYBarajarCartas()
    {
        for (int i = imagenesDeCartas.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Carta temp = imagenesDeCartas[i];
            imagenesDeCartas[i] = imagenesDeCartas[j];
            imagenesDeCartas[j] = temp;
        }

        for (int i = 0; i < 6; i++)
        {
            GameObject nuevaCarta = Instantiate(cartaPrefabArray[i], Vector3.zero, Quaternion.identity);
            nuevaCarta.transform.localScale = new Vector3(1.64f, 1.66f, 1f);
            SpriteRenderer spriteRenderer = nuevaCarta.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = imagenesDeCartas[i].sprite;
            cartas.Add(nuevaCarta);
        }
    }

    void RepartirCartas()
    {
        int totalCartas = cartas.Count;
        int cartasPorGrupo = totalCartas / 2;
        float offsetTop = ((cartasPorGrupo - 1) * espacioHorizontal) / 2;
        float offsetBottom = ((cartasPorGrupo - 1) * espacioHorizontal) / 2;

        float yOffsetTop = 7.0f; 
        float yOffsetBottom = -11.0f; 

        for (int i = 0; i < totalCartas; i++)
        {
            float x;
            float y;

            if (i < cartasPorGrupo)
            {
                x = -offsetTop + i * espacioHorizontal;
                y = yOffsetTop;
            }
            else
            {
                x = -offsetBottom + (i - cartasPorGrupo) * espacioHorizontal;
                y = yOffsetBottom;
            }

            cartas[i].transform.position = new Vector3(x, y, i * separacionEntreGrupos);
        }
    }

    public void MoveCardToCenter(CartaBehavior cardBehavior)
    {
        if (!allCardsArrived)
            return;

        foreach (var carta in cartas)
        {
            if (carta.GetComponent<CartaBehavior>() == cardBehavior)
            {
                cardBehavior.IsMoving = true;
                lastArrivedCard = carta;
            }
            else if (lastArrivedCard != null)
            {
                carta.transform.position = new Vector3(lastArrivedCard.transform.position.x, lastArrivedCard.transform.position.y - 0.1f, lastArrivedCard.transform.position.z);
                lastArrivedCard = carta;
            }
        }
    }

    public void CardArrived()
    {
        int arrivedCount = cartas.FindAll(carta => carta != null && carta.GetComponent<CartaBehavior>() != null && !carta.GetComponent<CartaBehavior>().IsMoving).Count;
        if (arrivedCount == cartas.Count)
        {
            allCardsArrived = true;
            ReorderCards();
        }
    }

    void ReorderCards()
    {
        int totalCartas = cartas.Count;
        float offsetCenter = ((totalCartas - 1) * espacioHorizontal) / 2;

        for (int i = 0; i < totalCartas; i++)
        {
            float x = -offsetCenter + i * espacioHorizontal;
            float y = -4.93f; // Posición vertical en el centro

            cartas[i].transform.position = new Vector3(x, y, i * separacionEntreGrupos);
        }
    }

void Update()
{
    if (allCardsArrived)
    {
        bool allCardsArrived = true;
        foreach (var carta in cartas)
        {
            if (carta != null && carta.GetComponent<CartaBehavior>() != null && carta.GetComponent<CartaBehavior>().IsMoving)
            {
                allCardsArrived = false;
                break;
            }
        }
        if (allCardsArrived)
        {
            ReorderCards();
        }
    }
}

[System.Serializable]
public class Carta
{
    public Sprite sprite;
    public Palo palo;
    public Valor valor;
    public int peso; 

    public Carta(Sprite sprite, Palo palo, Valor valor)
    {
        this.sprite = sprite;
        this.palo = palo;
        this.valor = valor;
        switch (valor)
        {
            case Valor.Uno:
                if (palo == Palo.Copa || palo == Palo.Oro)
                    peso = 8;
                else if(palo == Palo.Basto )
                    peso = 500;
                else
                    peso = 1000;
                break;
            case Valor.Siete:
                if (palo == Palo.Copa || palo == Palo.Basto)
                    peso = 4;
                else if(palo == Palo.Oro)
                    peso = 200;
                else
                    peso = 300;
                break;
            case Valor.Tres:
                peso = 10;
                break;
            case Valor.Dos:
                peso = 9;
                break;
            case Valor.Doce:
                peso = 7;
                break;
            case Valor.Once:
                peso = 6;
                break;
            case Valor.Diez:
                peso = 5;
                break;
            case Valor.Seis:
                peso = 3;
                break;
            case Valor.Cinco:
                peso = 2;
                break;
            case Valor.Cuatro:
                peso = 1;
                break;
            default:
                peso = 0;
                break;
        }
    }
}
}
