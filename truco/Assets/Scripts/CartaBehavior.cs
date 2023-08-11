using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaBehavior : MonoBehaviour
{
    private bool isSelected = false;
    private bool isMoving = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = 5.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, gameManager.PosicionDeseada, step);

            if (transform.position == gameManager.PosicionDeseada)
            {
                isMoving = false;
                gameManager.CardArrived();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            isSelected = true;
            gameManager.MoveCardToCenter(this);
        }
        else
        {
            isMoving = true;
        }
    }
}

