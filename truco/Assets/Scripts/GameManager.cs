using UnityEngine;
using System.Collections.Generic;

public enum Palo
{
    Espada,
    Basto,
    Copa,
    Oro
}

public enum Valor
{
    Uno,
    Dos,
    Tres,
    Cuatro,
    Cinco,
    Seis,
    Siete,
    Diez,
    Once,
    Doce
}

[System.Serializable]
public class Carta
{
    public Sprite sprite;
    public Palo palo;
    public Valor valor;
}

public class GameManager : MonoBehaviour
{
    public GameObject cartaPrefab;
    public List<Carta> cartas;

    public float espacioHorizontal = 2.0f;
    public float espacioVertical = 2.0f;
    public float separacionEntreGrupos = 1.0f;
    private Vector3 _posicionDeseada = new Vector3(0.0f, -4.93f, 0.0f);
    public Vector3 PosicionDeseada => _posicionDeseada;
    private bool allCardsArrived = false;
    private GameObject lastArrivedCard = null;

    public List<GameObject> cartasGameObject = new List<GameObject>();
    public Dictionary<Carta, Sprite> cartaSpriteDict = new Dictionary<Carta, Sprite>(); // Diccionario para asociar cartas con sprites
    public static GameManager Instance { get; private set; }

    public List<Carta> GetCartasBarajadas()
    {
        List<Carta> cartasBarajadas = new List<Carta>();
        for (int i = 0; i < cartasGameObject.Count; i++)
        {
            cartasBarajadas.Add(cartas[i]);
        }
        return cartasBarajadas;
    }

    public List<Carta> GetManoJugador1()
    {
        List<Carta> mano = new List<Carta>();
        for (int i = 0; i < 3; i++)
        {
            mano.Add(cartas[i]);
        }
        return mano;
    }

    public List<Carta> GetManoJugador2()
    {
        List<Carta> mano = new List<Carta>();
        for (int i = 3; i < 6; i++)
        {
            mano.Add(cartas[i]);
        }
        return mano;
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       DestruirCartasAnteriores();
       RealizarInicio();
    }

    public void RealizarInicio()
    {
        cartasGameObject = new List<GameObject>();
        GenerarYBarajarCartas();
        RepartirCartas();

        for (int i = 3; i < 6; i++)
        {
            cartasGameObject[i].AddComponent<CartaBehavior>();
        }
    }

    public void DestruirCartasAnteriores()
    {
        foreach (var carta in cartasGameObject)
        {
            Destroy(carta);
        }
        cartasGameObject.Clear();
    }


public void GenerarYBarajarCartas()
{
    // Limpiar listas y diccionario antes de generar nuevas cartas
    cartasGameObject.Clear();
    cartaSpriteDict.Clear();

    for (int i = cartas.Count - 1; i > 0; i--)
    {
        int j = Random.Range(0, i + 1);
        Carta temp = cartas[i];
        cartas[i] = cartas[j];
        cartas[j] = temp;
    }

    for (int i = 0; i < 6; i++)
    {
        GameObject nuevaCarta = Instantiate(cartaPrefab, Vector3.zero, Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCarta.GetComponent<SpriteRenderer>();

        if (i < 3)
        {
            // Escala diferente para las primeras tres cartas (0, 1, 2)
            nuevaCarta.transform.localScale = new Vector3(0.742592f, 0.8768119f, 1f);
            nuevaCarta.name = "Carta " + i;
        }
        else
        {
            nuevaCarta.transform.localScale = new Vector3(1.64f, 1.66f, 1f);
            spriteRenderer.sprite = cartas[i].sprite;

            // Agregar o ajustar el único BoxCollider2D
            BoxCollider2D boxCollider = nuevaCarta.GetComponent<BoxCollider2D>();
            if (boxCollider == null)
            {
                boxCollider = nuevaCarta.AddComponent<BoxCollider2D>();
            }

            // Establecer offset y tamaño específicos
            boxCollider.offset = new Vector2(0.007541239f, 0.01489973f);
            boxCollider.size = new Vector2(2.033966f, 3.258996f);
        }

        cartasGameObject.Add(nuevaCarta);

        // Agregar el sprite al diccionario
        if (!cartaSpriteDict.ContainsKey(cartas[i]))
        {
            cartaSpriteDict.Add(cartas[i], spriteRenderer.sprite);
        }

        // Asignar la carta al comportamiento de la carta
        CartaBehavior cartaBehavior = nuevaCarta.GetComponent<CartaBehavior>();
        if (cartaBehavior != null)
        {
            cartaBehavior.carta = cartas[i];
        }
    }
}



    void RepartirCartas()
    {
        int totalCartas = cartasGameObject.Count;
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

            cartasGameObject[i].transform.position = new Vector3(x, y, i * separacionEntreGrupos);
        }
    }
    public void MoveCardUp(CartaBehavior cardBehavior)
    {
        foreach (var carta in cartasGameObject)
        {
            if (carta.GetComponent<CartaBehavior>() == cardBehavior)
            {
                cardBehavior.IsMoving = true;
                cardBehavior.TargetHeight = 2.0f; // Cambia el valor según lo necesario
            }
            else if (lastArrivedCard != null)
            {
                Vector3 targetPosition = carta.transform.position + Vector3.up * 2.0f; // Cambia el 2.0f según lo necesario
                carta.transform.position = Vector3.Lerp(carta.transform.position, targetPosition, Time.deltaTime * 5.0f); // Cambia la velocidad de desplazamiento según lo necesario
            }
        }
    }

    public void CardArrived()
    {
        int arrivedCount = cartasGameObject.FindAll(carta => carta != null && carta.GetComponent<CartaBehavior>() != null && !carta.GetComponent<CartaBehavior>().IsMoving).Count;
        if (arrivedCount == cartasGameObject.Count)
        {
            allCardsArrived = true;
            ReorderCards();
        }
    }

    void ReorderCards()
    {
        int totalCartas = cartasGameObject.Count;
        float offsetCenter = ((totalCartas - 1) * espacioHorizontal) / 2;

        for (int i = 0; i < totalCartas; i++)
        {
            float x = -offsetCenter + i * espacioHorizontal;
            float y = -4.93f; // Posición vertical en el centro

            cartasGameObject[i].transform.position = new Vector3(x, y, i * separacionEntreGrupos);
        }
    }

    void Update()
    {
        if (allCardsArrived)
        {
            bool allCardsArrived = true;
            foreach (var carta in cartasGameObject)
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

    public Sprite GetSpriteForCard(Carta carta)
{
    if (cartaSpriteDict.ContainsKey(carta))
    {
        return cartaSpriteDict[carta];
    }
    return null; // O un sprite por defecto en caso de no encontrar la carta
}

public int GetIndexOfCard(GameObject cartaObject)
{
    return cartasGameObject.IndexOf(cartaObject);
}
}

