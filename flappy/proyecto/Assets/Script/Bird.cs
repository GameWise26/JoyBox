using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public float upForce = 200f;
    private RotateBird rotateBird;
    //skin
    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;


    private void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotateBird = GetComponent<RotateBird>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDead) return;
        if(Input.GetMouseButtonDown(0) && transform.position.y < 5){
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * upForce);
            anim.SetTrigger("Flap");
            SoundSystem.instance.PlayFlap();
        }
    }
    private void OnCollisionEnter2D (Collision2D collision){
        isDead = true;
        anim.SetTrigger("Die");
        rotateBird.enabled = false;
        GameController.instance.BirdDie();
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

    private void load()
    {   
    selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    void Start()
    {
    if(PlayerPrefs.HasKey("selectedOption"))
    {
        selectedOption = 0;
    }
    else
    {
        load();
    }
    UpdateCharacter(selectedOption);
    }
}
