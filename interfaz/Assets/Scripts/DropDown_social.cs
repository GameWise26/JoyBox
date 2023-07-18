using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropDown_social : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Interfaz_social");
    }

    public void ChangeScene2()
    {
        SceneManager.LoadScene("Interfaz_social_g");
    }
}
