using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird1 : MonoBehaviour
{
    private bool isDead = false, listo = false;
    public Rigidbody2D rb2d;
    private Animator anim;
    public float upForce = 200f;
    private RotateBird rotateBird;
    public static Bird1 instance;
    //skin
    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;
    //animation
    public Animator animator;
    public RuntimeAnimatorController nuevoControlador0;
    public RuntimeAnimatorController nuevoControlador1;
    public RuntimeAnimatorController nuevoControlador2;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotateBird = GetComponent<RotateBird>();
        if (Bird1.instance == null)
        {
            Bird1.instance = this;
        }
        else if (Bird1.instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Bird1 ha sido instanciado por segunda vez. Esto no deber�a ocurrir.");
        }
    }

    // Update is called once per frame
    /*private void Update()
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
                SoundSystem1.instance.PlayFlap();
            }
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        listo = false;
        isDead = true;
        anim.SetTrigger("Die");
        rotateBird.enabled = false;
        GameController1.instance.BirdDie();
        rb2d.velocity = Vector2.zero;
        SoundSystem.instance.PlayHit();
        SoundSystem.instance.audioBackground.Stop();
    }

    //skin
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = PlayerPrefs.GetInt("selectedOption");
        }
        if (selectedOption == 1)
        {
            anim.runtimeAnimatorController = nuevoControlador1;
        }
        else if (selectedOption == 2)
        {
            anim.runtimeAnimatorController = nuevoControlador2;
        }
        else
        {
            anim.runtimeAnimatorController = nuevoControlador0;
        }
        SocketManager.instancia.socket.OnUnityThread("impulso", (response) =>{
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * upForce);
            anim.SetTrigger("Flap");
            SoundSystem.instance.PlayFlap();
            Debug.Log("o");
        });
    }
}
