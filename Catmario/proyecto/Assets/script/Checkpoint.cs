using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool interacted = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !interacted)
        {
            michimovimiento player = collision.GetComponent<michimovimiento>();
            if (player != null)
            {
                player.SetCheckpoint();
            }

            animator.SetTrigger("checkpoint2"); // Dispara la animación "Interact" en el animator
            interacted = true; // Marca el checkpoint como interactuado
        }
    }
}
