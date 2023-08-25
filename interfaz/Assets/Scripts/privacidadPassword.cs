using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_InputField contraseniaInputField;
    public TMP_InputField rcontraseniaInputField;
    public TMP_InputField confirmarInputField;
    public Image botonImage;
    public Sprite ojoAbiertoSprite;
    public Sprite ojoCerradoSprite;
    private bool showPassword = false;

    public void BotonCambioEstado()
    {
        showPassword = !showPassword;
        CambiarVisibilidad(contraseniaInputField);
        CambiarVisibilidad(rcontraseniaInputField);
        CambiarVisibilidad(confirmarInputField);
        CambiarImagenBoton();
    }

    public void BotonCambioEstadoLogin(){
        showPassword = !showPassword;
        CambiarVisibilidad(contraseniaInputField);
        CambiarImagenBoton();
    }

    private void CambiarVisibilidad(TMP_InputField inputField)
    {
        inputField.contentType = showPassword ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        inputField.ForceLabelUpdate();
    }

    private void CambiarImagenBoton()
    {
        botonImage.sprite = showPassword ? ojoAbiertoSprite : ojoCerradoSprite;
    }
}
