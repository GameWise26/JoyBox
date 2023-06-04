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
    private Dictionary<string,string> aver;

    private int score;
    public Text scoreText;

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
    void Start()
    {
        aver = new Dictionary<string,string>();
    }


    public void BirdDie(){
        if(gameOver)
            return;
        gameOverText.SetActive(true);
        gameOver = true;
        if(score > int.Parse(UserDatabase.instancia.res["puntaje"]))
            StartCoroutine(UserDatabase.instancia.UploadUserData(UserDatabase.instancia.apiUrl1,new string[]{"idUser","puntaje"},new string[]{UserDatabase.instancia.id.ToString(),score.ToString()},valor => aver = valor));
    }

    public void OnDestroy(){
        if(GameController.instance == this){
            GameController.instance = null;    
        }
    }
}
