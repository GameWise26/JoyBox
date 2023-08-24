using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour
{
    public static GameController1 instance;
    public GameObject gameOverText;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

    //Banda, siempre que quieran hacer consultas a la bd desde algun script, declaren primero este tipo de Dictionary

    private int score;
    public Text scoreText, recordText;

    private void Awake(){
        if(GameController1.instance == null){
            GameController1.instance = this;    
        } else if(GameController1.instance != this){
            Destroy(gameObject);
            Debug.LogWarning("GameController1 ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }

    public void BirdScored(){
        if(gameOver) return;

        score++;
        scoreText.text = "Score: "+score;
        SoundSystem.instance.PlayPoint();
    }

    // Start is called before the first frame update
    void Start()
    {
        SocketManager.instancia.Emit("fentro", new Dictionary<string,string>(){{"id",SocketManager.instancia.id_espec}});
        SocketManager.instancia.socket.OnUnityThread("salidas",(response)=>{
            //Bird1.instance.
        });
    }


    public void BirdDie(){
        if(gameOver)
            return;
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void OnDestroy(){
        if(GameController1.instance == this){
            GameController1.instance = null;    
        }
    }
}
