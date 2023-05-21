using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isDead = false, listo = false;
    public Rigidbody2D rb2d;
    private Animator anim;
    public float upForce = 200f;
    private RotateBird rotateBird;
    public static Bird instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotateBird = GetComponent<RotateBird>();
        if (Bird.instance == null)
        {
            Bird.instance = this;
        }
        else if (Bird.instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Bird ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }
    

    private void Start()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || listo == true)
        {
            listo = true;
            if (isDead) return;
            if (Input.GetMouseButtonDown(0) && transform.position.y < 5)
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(Vector2.up * upForce);
                anim.SetTrigger("Flap");
                SoundSystem.instance.PlayFlap();
            }
        }
       /* else
        {
            rb2d.isDynamic = false;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        listo = false;
        isDead = true;
        anim.SetTrigger("Die");
        rotateBird.enabled = false;
        GameController.instance.BirdDie();
        rb2d.velocity = Vector2.zero;
        SoundSystem.instance.PlayHit();
        SoundSystem.instance.audioBackground.Stop();
    }
}
