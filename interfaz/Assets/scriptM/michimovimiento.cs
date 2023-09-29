using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class michimovimiento : MonoBehaviour
{
    public float velocidad;
    public float fuerzasalto;
    public float fuerzaGolpe;
    public LayerMask capasuelo;
    public LayerMask capaplataforma;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;
    private bool puedeMoverse = true;
    private GameManager gameManager;

    private Vector3 checkpointPosition;
    float Xinicial, Yinicial;
    private void Start()
    {
        Xinicial = transform.position.x;
        Yinicial = transform.position.y;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el objeto GameManager en la escena.");
        }

        checkpointPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    public bool taSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capasuelo);
        return raycastHit.collider != null;
    }

    public bool taEncimaPlataforma()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.up, 0.2f, capaplataforma);
        return raycastHit.collider != null;
    }

    private bool saltoRealizado = false;

    void ProcesarSalto()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (taSuelo() || taEncimaPlataforma()) && !saltoRealizado)
        {
            rigidBody.AddForce(Vector2.up * fuerzasalto, ForceMode2D.Impulse);
            saltoRealizado = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            saltoRealizado = false;
        }
    }

    void ProcesarMovimiento()
    {
        // Si no puede moverse, salimos de la función
        if (!puedeMoverse) return;

        // Lógica de movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // Si se cumple condición
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            // Ejecutar código de volteado
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void AplicarGolpe()
    {
        puedeMoverse = false;

        Vector2 direccionGolpe;

        if (rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);

        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperamos antes de comprobar si esta en el suelo.
        yield return new WaitForSeconds(0.1f);

        while (!taSuelo())
        {
            // Esperamos al siguiente frame.
            yield return null;
        }

        // Si ya está en suelo activamos el movimiento.
        puedeMoverse = true;
    }

    public void SetCheckpoint()
    {
        checkpointPosition = transform.position;
    }

    public void RespawnAtCheckpoint()
    {
        transform.position = checkpointPosition;
        puedeMoverse = true; 
    }

    public void RecolocarPersonaje()
    {
        transform.position = new Vector3(Xinicial, Yinicial, 0);
        puedeMoverse = true; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = null;
        }
    }
}
