using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimo : MonoBehaviour
{
    public static Ultimo instancia;
    public GameObject parte;
    private void Awake(){
        if(Ultimo.instancia == null){
            Ultimo.instancia = this;    
        } else if(Ultimo.instancia != this){
            Destroy(gameObject);
            Debug.LogWarning("La cola ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Crearparte(){
        //Instantiate(parte,new Vector2(transform.position.x + MoverSnake.instancia.puntos * 80.3f * (1f / Screen.dpi),transform.position.y));
    }
    public void OnDestroy(){
        if(MoverSnake.instancia == this){
            MoverSnake.instancia = null;    
        }
    }
}
