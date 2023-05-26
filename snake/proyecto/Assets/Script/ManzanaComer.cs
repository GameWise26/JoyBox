using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManzanaComer : MonoBehaviour
{
    private bool estable;
    private int tiempo;
    private GameObject prefab;
    public Sprite manzana;
    private SpriteRenderer siu;
    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load<GameObject>("manzana");
        siu = GetComponent<SpriteRenderer>();
        tiempo = 20;
    }
    void Update(){
        tiempo--;
        if(tiempo == 0 && !estable){
            siu.sprite = manzana;
        }
        else if(tiempo == 0){
            Instantiate(prefab,new Vector2(-0.025f+(80.3f * (1f / Screen.dpi)*Random.Range(-9,9)),0.375f+(80.3f * (1f / Screen.dpi)*Random.Range(-5,4))),Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D collider){
        if(collider.CompareTag("Player"))
        {
            MoverSnake.instancia.puntos++;
            MoverSnake.instancia.texto.text = ""+MoverSnake.instancia.puntos;
            Ultimo.instancia.Crearparte();
            Instantiate(prefab,new Vector2(-0.025f+(80.3f * (1f / Screen.dpi)*Random.Range(-9,9)),0.375f+(80.3f * (1f / Screen.dpi)*Random.Range(-5,4))),Quaternion.identity);
            Destroy(gameObject);
        }
        else if(!collider.CompareTag("barrera")){
            estable = true;
        }
    }
}
