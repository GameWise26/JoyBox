using UnityEngine;

public class CartaBehavior : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 originalPosition;
    private bool isRevealed = false;
    private SpriteRenderer spriteRenderer;

    public float TargetHeight { get; private set; } = 3.0f; // Altura del destino

    private GameManager gameManager;

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = 5.0f * Time.deltaTime;
            Vector3 targetPosition = originalPosition + Vector3.up * TargetHeight;
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
            gameManager.MoveCardToDestination(this); // Mover hacia el objeto destino
        }
    }
}
