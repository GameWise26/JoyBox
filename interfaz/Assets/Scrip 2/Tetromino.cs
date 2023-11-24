using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 1f;
    public bool allowRotation = true;
    public bool limitRotation = false;
    public bool rectangulo = false;
    private bool rote;
    public string prefabPath;
    public Tetromino currentTetromino ;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<Game>().pausa)
        {
            if (FindObjectOfType<Game>().afterHold)
            {
                if (HasTouchedGround() == true)
                {
                    FindObjectOfType<Game>().afterHold = false;
                    FindObjectOfType<Game>().holdEnable = true;
                }
                else
                {
                    FindObjectOfType<Game>().holdEnable = false;
                }
            }
            CheckUserInput();
        }
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
            if (Input.GetKey(KeyCode.DownArrow))
            {
                fallspeed = 0.1f;
            }
            else
            {
                fallspeed = 1f;
            }

            Move(Vector3.down);
            fall = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (FindObjectOfType<Game>().holdEnable)
            {
                currentTetromino = FindObjectOfType<Tetromino>();

                if (FindObjectOfType<Game>().piezaEnHold == false)
                {
                    FindObjectOfType<Game>().currentHeldTetromino = currentTetromino;
                    FindObjectOfType<Game>().piezaEnHold = true;
                    FindObjectOfType<Game>().seCambiaPorHold = false;
                }
                else
                {
                    FindObjectOfType<Game>().seCambiaPorHold = true;
                    FindObjectOfType<Game>().piezaRTomada = true;
                   FindObjectOfType<Game>().piezaRecienTomada = currentTetromino;
                }               
                    FindObjectOfType<Game>().holdEnable = false;
                    FindObjectOfType<Game>().afterHold = true;
                    Destroy(currentTetromino.gameObject);
                    FindObjectOfType<Game>().SpawnNextTetromino();
                    FindObjectOfType<Game>().HoldPanel();
            }
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 currentPosition = transform.position;
        transform.position += direction + (rectangulo && transform.position.x == 0 && rote == true ? direction : Vector3.zero);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position -= direction + (rectangulo && transform.position.x == 0 && rote == true ? direction : Vector3.zero);
            rote = false;

            if (direction == Vector3.down)
            {

                FindObjectOfType<Game>().llegoAlFinal = currentPosition.y >= 19 ? true : false;

                for (int yMino = 0; yMino < 20; yMino++)
                {
                    if (FindObjectOfType<Game>().IsFullRowAt(yMino))
                    {
                        FindObjectOfType<Game>().DeleteRow();
                    }
                }

                enabled = false;
                FindObjectOfType<Game>().afterHold = false;
                FindObjectOfType<Game>().seCambiaPorHold = false;
                FindObjectOfType<Game>().SpawnNextTetromino();
            }
        }
    }

    bool HasTouchedGround()
    {
        Vector3 currentPosition = transform.position;

        transform.position += Vector3.down;
        if (CheckIsValidPosition())
        {
            transform.position = currentPosition;
            return false;
        }
        else
        {
            transform.position = currentPosition;
            return true;
        }
    }


    void Rotate()
    {
        if (!allowRotation)
            return;

        int rotationAngle = !limitRotation || (int)transform.eulerAngles.z == 90 ? -90 : 90;

        transform.Rotate(0, 0, rotationAngle);

        if (!CheckIsValidPosition())
        {
            bool rotated = false;

            if (CheckMoveValidity(Vector3.right))
            {
                rote = true;
                Move(Vector3.right);
                rotated = true;

            }

            else if (CheckMoveValidity(Vector3.left))
            {
                Move(Vector3.left);
                rotated = true;
            }

            else if (CheckMoveValidity(Vector3.up))
            {
                Move(Vector3.up);
                rotated = true;
            }

            if (!rotated)
            {
                transform.Rotate(0, 0, -rotationAngle);
            }
        }
        foreach (Transform mino in transform)
        {
            mino.Rotate(0, 0, -rotationAngle);
        }
    }


    bool CheckMoveValidity(Vector3 direction)
    {
        transform.position += direction + (rectangulo ? direction : Vector3.zero);

        bool isValid = CheckIsValidPosition();

        transform.position -= direction + (rectangulo ? direction : Vector3.zero);

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

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(position) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(position).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}