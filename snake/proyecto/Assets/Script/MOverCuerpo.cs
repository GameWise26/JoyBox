using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOverCuerpo : MonoBehaviour
{
    private float x, y, actual;
    private Rigidbody2D rb2d;
    private Vector2 vector;
    public Rigidbody2D crb2d;
    private int tiempo2;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        x = 80.3f * (1f / Screen.dpi);
        y = 0;
        actual = 0f;
        vector = new Vector2(x,y);
        tiempo2 = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoverSnake.instancia.angle != actual){
            actual = MoverSnake.instancia.angle;
        }
        tiempo2--;
        if(tiempo2 == 0){
            transform.Translate(vector);
            tiempo2 = 25;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("rotadp"))
        {
            transform.rotation = Quaternion.Euler(0,0,collider.gameObject.GetComponent<AparecerDoblado>().angle);
        }
    }
}
