using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    public Button botonIzquierdo;
    public Button botonDerecho;
    public GameObject content;
    public RectTransform viewPort;

    void Start()
    {
        if (botonIzquierdo != null)
            botonIzquierdo.onClick.AddListener(DesplazarIzquierda);
        else
            Debug.LogError("El botón izquierdo no está asignado en el Inspector. ");

        if (botonDerecho != null)
            botonDerecho.onClick.AddListener(DesplazarDerecha);
        else
            Debug.LogError("El botón derecho no está asignado en el Inspector.>:/");
    }

    void DesplazarIzquierda()
    {
        if (viewPort == null)
        {
            Debug.LogError("El ViewPort no está asignada en el Inspector. >:/");
            return;
        }

        float desplazamiento = viewPort.rect.width / 2f;
        content.transform.position += new Vector3(desplazamiento, 0f, 0f);
    }

    void DesplazarDerecha()
    {
        if (viewPort == null)
        {
            Debug.LogError("El ViewPort no está asignada en el Inspector. >:/");
            return;
        }
        float desplazamiento = viewPort.rect.width / 2f;
        content.transform.position -= new Vector3(desplazamiento, 0f, 0f);
 
    }
}
