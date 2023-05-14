using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverSnake : MonoBehaviour
{
    public GameObject rotado;
    public static MoverSnake instancia;
    public float x, y, angle,ant;
    private Rigidbody2D rb2d;
    private Vector2 vector;
    public int puntos = 0, tiempo = 0, tiempo2 = 0, volver;
    public Text texto;

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
        tiempo2 = 25;
        volver = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && angle != 0f && angle != 180 && tiempo <= 0){
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
            transform.Translate(vector);
            tiempo2 = 25;
        }
        tiempo--;
    }
    public void OnDestroy(){
        if(MoverSnake.instancia == this){
            MoverSnake.instancia = null;    
        }
    }
}
