using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el objeto GameManager en la escena.");
        }
        else
        {
            gameManager.GameOver += HandleGameOver;
        }
    }

    private void HandleGameOver()
    {
        menuGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Inicio");
    }
}

