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
        float x = transform.position.x;
        float y = transform.position.y;
        if(transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == -360 || transform.rotation.eulerAngles.z == 360){
            transform.position = new Vector2(transform.position.x-80.3f * (1f / Screen.dpi),transform.position.y);
        }
        else if(transform.rotation.eulerAngles.z == 180 || transform.rotation.eulerAngles.z == -180){
            transform.position = new Vector2(transform.position.x+80.3f * (1f / Screen.dpi),transform.position.y);
        }
        else if(transform.rotation.eulerAngles.z == 270 || transform.rotation.eulerAngles.z == -90){
            transform.position = new Vector2(transform.position.x,transform.position.y+80.3f * (1f / Screen.dpi));
        }
        else{
            transform.position = new Vector2(transform.position.x,transform.position.y-80.3f * (1f / Screen.dpi));
        }
        Debug.Log("xd");
        Instantiate(parte,new Vector2(x,y),Quaternion.Euler(0,0,transform.rotation.eulerAngles.z));
    }
    public void OnDestroy(){
        if(MoverSnake.instancia == this){
            MoverSnake.instancia = null;
        }
    }
}
