using UnityEngine;

public class CartaBehavior : MonoBehaviour
{
    public bool IsMoving { get; set; } = false;
    private Vector3 originalPosition;
    private bool isRevealed = false;
    private SpriteRenderer spriteRenderer;

    public float TargetHeight { get; private set; } = 3.0f; // Altura del destino

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            float step = 5.0f * Time.deltaTime;
            Vector3 targetPosition = originalPosition + Vector3.up * TargetHeight;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                IsMoving = false;
            }
        }
    }

    private void OnMouseDown()
{
    if (!IsMoving && !isRevealed)
    {
        if ((gameManager.TurnoJugador1 && gameManager.GetIndexOfCard(gameObject) >= 3) ||
            (!gameManager.TurnoJugador1 && gameManager.GetIndexOfCard(gameObject) < 3))
        {
            IsMoving = true;
            gameManager.MoveCardToDestination(this); // Mover hacia el objeto destino

            // Comprueba si la carta es la 1, 2 o 3
            int indexOfCard = gameManager.GetIndexOfCard(gameObject);
            if (indexOfCard >= 0 && indexOfCard <= 2)
            {
                // Si es la carta 1, 2 o 3, ajusta la altura a un valor negativo para que se mueva hacia atrás
                TargetHeight = -3.0f;
            }

            // Marcar la carta como movida en esta ronda
            gameManager.CardMoved();
        }
    }
}
}