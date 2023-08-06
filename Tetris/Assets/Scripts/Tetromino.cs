using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 1;

    public bool allowRotation = true;
    public bool limitRotation = false;
    public bool rectangulo = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallspeed)
        {
            Move(Vector3.down);
            fall = Time.time;
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;

        if (!CheckIsValidPosition())
        {
            transform.position -= direction;
        }
    }

    void Rotate()
    {

        if (!allowRotation)
            return;

        int rotationAngle = !limitRotation || (int)transform.eulerAngles.z == 90 ? 90 : -90;

        transform.Rotate(0, 0, rotationAngle);
       

        if (!CheckIsValidPosition())
        {
            bool rotated = false;

            // Try moving one unit to the right and rotate again
            if (CheckMoveValidity(Vector3.right))
            {
                Move(Vector3.right);
                rotated = true;
            }
            // If it's still invalid, try moving one unit to the left and rotate again
            else if (CheckMoveValidity(Vector3.left))
            {
                Move(Vector3.left);
                rotated = true;
            }
            // If the rotation is not possible after the possible horizontal moves, try moving up one unit and rotate again
            else if (CheckMoveValidity(Vector3.up))
            {
                Move(Vector3.up);
                rotated = true;
            }

            // If the rotation is not possible after the possible moves, revert the rotation
            if (!rotated)
            {
                transform.Rotate(0, 0, -rotationAngle);
                Debug.Log("Se rechazo la rotacion");
            }
        }
    }


    bool CheckMoveValidity(Vector3 direction)
    {
        transform.position += direction;
        bool isValid = CheckIsValidPosition();
        transform.position -= direction;
        return isValid;
    }


    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 position = FindObjectOfType<Game>().Round(mino.position);

            if (FindObjectOfType<Game>().CheckIsInsideGrid(position) == false)
            {
                return false;
            }
        }
        return true;
    }
}