using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject goodEndingCanvas;
    public GameObject neutralEndingCanvas;
    public GameObject badEndingCanvas;
    public GameObject secretEndingCanvas;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el objeto GameManager en la escena.");
        }

        ActivarFinal();
    }

    private void ActivarFinal()
    {
        int totalComidas = gameManager.GetComidas();
        int totalMonedas = gameManager.GetPuntosTotales();

        if (totalComidas >= 25)
        {
            goodEndingCanvas.SetActive(true);
        }
        else if (totalComidas >= 15 && totalComidas <= 24)
        {
            neutralEndingCanvas.SetActive(true);
        }
        else if (totalComidas < 14)
        {
            badEndingCanvas.SetActive(true);
        }

        if (totalMonedas == GameManager.MAX_PUNTOS)
        {
            secretEndingCanvas.SetActive(true);
        }
    }
}

