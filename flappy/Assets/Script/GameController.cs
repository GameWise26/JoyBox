using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

    private void Awake(){
        if(GameController.instance == null){
            GameController.instance = this;    
        } else if(GameController.instance != this){
            Destroy(gameObject);
            Debug.LogWarning("GameController ha sido instanciado por segunda vez. Esto no debería ocurrir.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
