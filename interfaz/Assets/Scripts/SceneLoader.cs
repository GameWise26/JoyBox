using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene(string scene)
    {
        SceneManager.LoadScene("interfaz_" + scene);
    }
}
