using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOverCuerpo : MonoBehaviour
{
    private float x, y, actual;
    private Rigidbody2D rb2d;
    private Vector2 vector;
    public Rigidbody2D crb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        x = 80.3f * (1f / Screen.dpi);
        y = 0;
        actual = 0f;
        vector = new Vector2(x,y);
    }

    // Update is called once per frame
    void Update()
    {
        if(MoverSnake.instancia.angle != actual){
            actual = MoverSnake.instancia.angle;
        }
        if(MoverSnake.instancia.tiempo2 == 0){
            transform.Translate(vector);
        }
    }
}
