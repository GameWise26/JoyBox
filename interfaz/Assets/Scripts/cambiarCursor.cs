using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cambiarCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D handCursorTexture;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(handCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
