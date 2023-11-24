using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputsRegister : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    public Image panelImage;
    public Color selectColor = new Color(0.9176f, 0.1686f, 0.1686f);
    public Color hoverColor = new Color(248f / 255f, 108f / 255f, 128f / 255f);
    public Color reposo = new Color(0xA6 / 255f, 0xA6 / 255f, 0xA6 / 255f);

    // Implemento las interfaces
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NoHover(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Select(eventData);
    }

    //Hago uso de las interfaces
    public void Hover(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            panelImage.color = hoverColor;
        }
    }

    public void NoHover(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            panelImage.color = reposo;
        }
    }

    public void Select(BaseEventData eventData)
    {
        var inputsRegisters = FindObjectsOfType<InputsRegister>();
        foreach (var input in inputsRegisters)
        {
            if (input != this)
            {
                input.panelImage.color = reposo;
            }
        }

        panelImage.color = selectColor;
    }
}
