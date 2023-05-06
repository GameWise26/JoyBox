using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

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
        
    }


    public void BirdDie(){
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void OnDestroy(){
        if(GameController.instance == this){
            GameController.instance = null;    
        }
    }
}
