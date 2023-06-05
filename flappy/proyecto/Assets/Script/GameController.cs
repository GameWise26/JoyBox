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
        // Tambien no se olviden de usar el modificador await como a continuación
        recordText.text = "Record: "+UserDatabase.instancia.aver["puntaje"];
    }


    public async void BirdDie(){
        if(gameOver)
            return;
        gameOverText.SetActive(true);
        gameOver = true;
        Dictionary<string,string> a;
        if(score > int.Parse(UserDatabase.instancia.aver["puntaje"])){
            a = await UserDatabase.instancia.UploadUserData(UserDatabase.instancia.apiUrl1,new string[] {"idUser","puntaje"},new string[]{UserDatabase.instancia.id.ToString(),score.ToString()});
        if(bool.Parse(a["exito"])){
            UserDatabase.instancia.aver["puntaje"] = score.ToString();
            recordText.text = "Record: "+score;
        }
        }
    }

    public void OnDestroy(){
        if(GameController.instance == this){
            GameController.instance = null;    
        }
    }
}
