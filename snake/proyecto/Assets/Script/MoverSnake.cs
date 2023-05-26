using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoverSnake : MonoBehaviour
{
    public GameObject rotado;
    public static MoverSnake instancia;
    public float x, y, angle,ant,probable;
    private Rigidbody2D rb2d;
    private Vector2 vector;
    public int puntos, tiempo, tiempo2, volver;
    public Text texto, puntosOver;
    public Image mxd;
    public GameObject gameOver,teclas;
    public bool fin,compensar,comienza,ultimo,siguiente;
    public Sprite murio;
    public SpriteRenderer siu;

    private void Awake(){
        if(MoverSnake.instancia == null){
            MoverSnake.instancia = this;    
        } else if(MoverSnake.instancia != this){
            Destroy(gameObject);
            Debug.LogWarning("La cabeza ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        x = 80.3f * (1f / Screen.dpi);
        y = 0;
        vector = new Vector2(x,y);
        angle = 0f;
        ant = 0f;
        puntos = 0;
        tiempo2 = 5;
        volver = 5;
        fin = true;
        comienza = true;
        siu = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && comienza){
            comienza = false;
            fin = false;
            teclas.SetActive(false);
        }
        if(fin && Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else if(fin)
            return;
        if(Input.GetKeyDown(KeyCode.RightArrow) && angle != 0f && angle != 180 && tiempo <= 0){
            siguiente = true;
            ant = angle;
            angle = 0f;
            transform.rotation = Quaternion.Euler(0,0,angle);
            if(ant == 270){
                //AparecerDoblado.instancia.Aparecer(transform.position.x,transform.position.y,0,90);
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(0,0,90));
            }else{
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(180,0,90));
            }
            tiempo = volver;
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && angle != 90f && angle != 270 && tiempo <= 0){
            siguiente = true;
            ant = angle;
            angle = 90f;
            transform.rotation = Quaternion.Euler(0,0,angle);
            if(ant == 180){
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(180,0,0));
            } else{
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(0,0,180));
            }
            tiempo = volver;
        } else if(Input.GetKeyDown(KeyCode.DownArrow) && angle != 270f && angle != 90 && tiempo <= 0){
            siguiente = true;
            ant = angle;
            angle = 270f;
            transform.rotation = Quaternion.Euler(0,0,angle);
            if(ant == 180){
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(0,0,0));
            }else{
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(180,0,180));
            }
            tiempo = volver;
        } else if(Input.GetKeyDown(KeyCode.LeftArrow) && angle != 180f && angle != 0 && tiempo <= 0){
            siguiente = true;
            ant = angle;
            angle = 180f;
            transform.rotation = Quaternion.Euler(0,0,angle);
            if(ant == 270){
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(180,0,270));
            }else{
                Instantiate(rotado,new Vector2(transform.position.x,transform.position.y),Quaternion.Euler(0,0,270));
            }
            tiempo = volver;
        }
        tiempo2--;
        if(tiempo2 == 0){
            if(siguiente)
                siguiente = false;
            if(ultimo)
                ultimo = false;
            transform.Translate(vector);
            PRimerCuerpo.instancia.transform.Translate(vector);
            tiempo2 = volver;
        }
        tiempo--;
    }
    public void OnDestroy(){
        if(MoverSnake.instancia == this){
            MoverSnake.instancia = null;    
        }
    }
    private void OnTriggerEnter2D (Collider2D collider){
        if(!collider.CompareTag("fruta") && !collider.CompareTag("barrera")){
            fin = true;
            ultimo = true;
            Terminar();
        }
    }
    public void Terminar(){
        texto.gameObject.SetActive(false);
        mxd.enabled = false;
        gameOver.SetActive(true);
        puntosOver.text = ""+puntos;
        siu.sprite = murio;
    }
}
