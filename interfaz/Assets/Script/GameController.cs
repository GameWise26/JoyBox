using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

    //Banda, siempre que quieran hacer consultas a la bd desde algun script, declaren primero este tipo de Dictionary

    private int score;
    public Text scoreText, recordText;

    private void Awake(){
        if(GameController.instance == null){
            GameController.instance = this;    
        } else if(GameController.instance != this){
            Destroy(gameObject);
            Debug.LogWarning("GameController ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }

    public void BirdScored(){
        if(gameOver) return;

        score++;
        scoreText.text = "Score: "+score;
        SoundSystem.instance.PlayPoint();
    }

    // Start is called before the first frame update
    //Deben usar el modificador async en cada función que usen el UploadUserData
    void Start()
    {
        if(SocketManager.instancia.juego != "flappy"){
            /*SocketManager.instancia.socket.OnUnityThread("fPuntos", (response) =>{
                int pts = int.Parse(SocketManager.instancia.pasarDict(response)["puntos"]);
                recordText.text = "Record: " + pts;
                SocketManager.instancia.flappyPuntos = pts;
            });*/
            Juego juego = new Juego{
                juego = "flappy"
            };
            SocketManager.instancia.Emit("esteJuego",juego);
            SocketManager.instancia.juego = "flappy";
            SocketManager.instancia.salirJuego = true;
            SocketManager.instancia.Emit("fPuntos",juego);
        }
        else{
            recordText.text = "Record: " + SocketManager.instancia.flappyPuntos;
        }
        SocketManager.instancia.socket.OnUnityThread("valores",(response)=>{
            
            //SocketManager.instancia.Emit("");
        });
    }


    public void BirdDie(){
        if(gameOver)
            return;
        gameOverText.SetActive(true);
        gameOver = true;
        SocketManager.instancia.Emit("faPuntos",new Dictionary<string,string>(){{"puntos",score.ToString()}});
        SocketManager.instancia.Emit("ffinal",new Dictionary<string,bool>(){{"fin",true}});
        if(SocketManager.instancia.flappyPuntos < score) SocketManager.instancia.flappyPuntos = score;
    }

    public void OnDestroy(){
        if(GameController.instance == this){
            GameController.instance = null;    
        }
    }
}
