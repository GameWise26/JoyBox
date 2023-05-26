using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRimerCuerpo : MonoBehaviour
{
    public static PRimerCuerpo instancia;
    private float actual;

    private void Awake(){
        if(PRimerCuerpo.instancia == null){
            PRimerCuerpo.instancia = this;    
        } else if(PRimerCuerpo.instancia != this){
            Destroy(gameObject);
            Debug.LogWarning("La primera parte ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        actual = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoverSnake.instancia.fin){
            if(MoverSnake.instancia.ultimo)
                transform.rotation = Quaternion.Euler(0,0,MoverSnake.instancia.angle);
            return;
        }
        if(MoverSnake.instancia.angle != actual){
            actual = MoverSnake.instancia.angle;
        }
    }
}
