using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject1 : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    
    private void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.velocity = new Vector2(GameController1.instance.scrollSpeed,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController1.instance.gameOver){
            rb2d.velocity = Vector2.zero;
        }
    }
}
