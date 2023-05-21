using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            GameController.instance.BirdScored();
        }
    }
}
