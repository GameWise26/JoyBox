using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 1f;
    public bool allowRotation = true;
    public bool limitRotation = false;
    public bool rectangulo = false;
    private bool rote;



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
           //StartFastMove(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
            //StartFastMove(Vector3.left);
        }
        /*else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StopFastMove();
        }*/
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
    }

    //private Coroutine fastMoveCoroutine;

    /*void StartFastMove(Vector3 direction)
    {
        if (fastMoveCoroutine != null)
        {
            StopCoroutine(fastMoveCoroutine);
        }
        fastMoveCoroutine = StartCoroutine(FastMove(direction));
    }

    void StopFastMove()
    {
        if (fastMoveCoroutine != null)
        {
            StopCoroutine(fastMoveCoroutine);
        }
    }

    IEnumerator FastMove(Vector3 direction)
    {
        float moveInterval = 0.1f; // Cambioooooooo de velocidadddadad
        float timer = 0f;

        while (true)
        {
            if (timer >= moveInterval)
            {
                Move(direction);
                timer = 0f;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }*/

    void Move(Vector3 direction)
    {
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

                for (int yMino = 0; yMino < 20; yMino++  )
                {
                    if (FindObjectOfType<Game>().IsFullRowAt(yMino))
                    {
                        Debug.Log("Se encontro fila completa");
                        FindObjectOfType<Game>().DeleteRow();
                    }
                }

                enabled = false;
                FindObjectOfType<Game>().SpawnNextTetromino();
            }
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
                Debug.Log("Se rechazo la rotacion");
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