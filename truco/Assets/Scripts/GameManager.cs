using UnityEngine;
using System.Collections;
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
    public int ValorJuego;
    public int ValorNumerico;
}

public class GameManager : MonoBehaviour, IEnumerable<Carta>
{
    public GameObject cartaPrefab;
    public List<Carta> cartas;
    public Transform[] objetosDestino;

    public float espacioHorizontal = 2.0f;
    public float espacioVertical = 2.0f;
    public float separacionEntreGrupos = 1.0f;
    private Vector3 _posicionDeseada = new Vector3(0.0f, -4.93f, 0.0f);
    public Vector3 PosicionDeseada => _posicionDeseada;
    private bool allCardsArrived = false;
    private GameObject lastArrivedCard;

    public List<GameObject> cartasGameObject = new List<GameObject>();
    public Dictionary<Carta, Sprite> cartaSpriteDict = new Dictionary<Carta, Sprite>();

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
            GameObject nuevaCarta = Instantiate(cartaPrefab, objetosDestino[i].position, objetosDestino[i].rotation); // Posición y rotación del objeto destino
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

    public void MoveCardToDestination(CartaBehavior cardBehavior, Vector3 destination)
    {
        StartCoroutine(MoveCardToDestinationCoroutine(cardBehavior, destination));
    }

    public IEnumerator<Carta> GetEnumerator()
    {
        foreach (var carta in cartas)
        {
            yield return carta;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerator MoveCardToDestinationCoroutine(CartaBehavior cardBehavior, Vector3 destination)
    {
        float journeyLength = Vector3.Distance(cardBehavior.transform.position, destination);
        float journeyStartTime = Time.time;
        float journeyDuration = 1.0f;

        while (cardBehavior.transform.position != destination)
        {
            float distanceCovered = (Time.time - journeyStartTime) * journeyDuration;
            float fractionOfJourney = distanceCovered / journeyLength;
            cardBehavior.transform.position = Vector3.Lerp(cardBehavior.transform.position, destination, fractionOfJourney);

            yield return null;
        }

        cardBehavior.IsMoving = false;
    }

    void Update()
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
    }

    public Sprite GetSpriteForCard(Carta carta)
    {
        if (cartaSpriteDict.ContainsKey(carta))
        {
            return cartaSpriteDict[carta];
        }
        return null;
    }

    public int GetIndexOfCard(GameObject cartaObject)
    {
        return cartasGameObject.IndexOf(cartaObject);
    }

    public void MoveCardToDestination(CartaBehavior cardBehavior)
    {
        cardBehavior.IsMoving = true;
    }
}
