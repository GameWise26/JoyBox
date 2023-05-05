using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public float upForce = 200f;
    // Start is called before the first frame update
    
    private void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDead) return;
        if(Input.GetMouseButtonDown(0) && transform.position.y < 5){
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * upForce);
            anim.SetTrigger("Flap");
        }
    }
    private void OnCollisionEnter2D (Collision2D collision){
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.BirdDie();
        rb2d.velocity = Vector2.zero;
    }
}
