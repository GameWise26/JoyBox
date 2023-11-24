using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HappyFace : MonoBehaviour
{
    public List<Sprite> sprites;
    private Image image;
    public static HappyFace instancia;

    private void Awake(){
        HappyFace.instancia = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gane(){
        image.sprite = sprites[0];
    }
    public void perdi(){
        image.sprite = sprites[1];
    }
}
   