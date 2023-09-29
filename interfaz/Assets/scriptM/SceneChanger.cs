using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private bool hasInteracted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted)
        {
            hasInteracted = true;
            GameManager.Instance.GuardarComidas();
            GameManager.Instance.GuardarPuntaje();
            Debug.Log("Cambio de nivel");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            if (currentSceneIndex == 6) 
            {
                SceneManager.LoadScene(nextSceneIndex);
                Invoke("CargarFinalScene", 0.5f); 
            }
            else
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }

    private void CargarFinalScene()
    {
        SceneManager.LoadScene("FinalManagerScene");
    }
}
