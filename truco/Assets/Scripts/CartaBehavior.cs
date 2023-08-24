using UnityEngine;

public class CartaBehavior : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 originalPosition;
    private float targetHeight = 0.0f;
    private bool isRevealed = false;
    private SpriteRenderer spriteRenderer;

    public Carta carta; // Agregar propiedad para almacenar la carta

    public float TargetHeight
    {
        get { return targetHeight; }
        set { targetHeight = value; }
    }

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = 5.0f * Time.deltaTime;
            Vector3 targetPosition = originalPosition + Vector3.up * targetHeight;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

private void OnMouseDown()
{
    if (!IsMoving && !isRevealed)
    {
        IsMoving = true;
        gameManager.MoveCardUp(this);

        // Revelar la carta
        isRevealed = true;
        int index = gameManager.cartasGameObject.IndexOf(gameObject); // Obtener el índice en cartasGameObject
        if (index != -1 && index < gameManager.cartas.Count)
        {
            Carta carta = gameManager.cartas[index];
            spriteRenderer.sprite = carta.sprite;

            // Muestra el valor y el palo en la consola
            Debug.Log("Valor: " + carta.valor + ", Palo: " + carta.palo);
        }
        else
        {
            Debug.LogError("No se pudo encontrar la carta en CartaBehavior.");
        }
    }
}







}

