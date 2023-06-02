using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D bc2d;

    private void Awake(){
        bc2d = GetComponent<BoxCollider2D>();
    }
    
    private void RepositionBackground(){
        transform.Translate(Vector2.right * bc2d.size.x * 2000/1005);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -bc2d.size.x){
            RepositionBackground();
        }
    }
}
