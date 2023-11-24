using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvasGameOver;

    private void Start()
    {
        canvasGameOver.SetActive(false);
        GameManager.Instance.GameOver += ShowGameOverCanvas;
    }

    private void ShowGameOverCanvas()
    {
        canvasGameOver.SetActive(true);
    }
}