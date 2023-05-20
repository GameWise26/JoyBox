using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class conte : MonoBehaviour
{
    public RectTransform content;
    public Button leftButton;
    public Button rightButton;
    public float scrollSpeed = 10f;
    private Vector2 targetPosition;

    private void Start()
    {
        leftButton.onClick.AddListener(ScrollLeft);
        rightButton.onClick.AddListener(ScrollRight);
        targetPosition = content.anchoredPosition;
    }

    private void ScrollLeft()
    {
        targetPosition += new Vector2(scrollSpeed, 0f);
        targetPosition.x = Mathf.Clamp(targetPosition.x, -455.8845f, 455.881f);
    }

    private void ScrollRight()
    {
        targetPosition -= new Vector2(scrollSpeed, 0f);
        targetPosition.x = Mathf.Clamp(targetPosition.x, -455.8845f, 455.881f);
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetPosition += new Vector2(-scroll * scrollSpeed, 0f);
        targetPosition.x = Mathf.Clamp(targetPosition.x, -455.8845f, 455.881f);

        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * 5f);
    }

}