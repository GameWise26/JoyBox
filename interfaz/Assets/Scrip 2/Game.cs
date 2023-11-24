using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    private Queue<string> tetrominoBag = new Queue<string>();
    private List<string> listTetrominos = new List< string>();
    public List<SpriteRenderer> NextPanel = new List<SpriteRenderer>();
    public List<Sprite> spritesTetrominos= new List<Sprite>();
    public Tetromino currentHeldTetromino, piezaRecienTomada = null;
    public bool piezaRTomada = false;
    public bool piezaEnHold = false;
    public SpriteRenderer holdPanel;
    public bool holdEnable = true;
    public bool afterHold = false;
    public bool seCambiaPorHold = false;
    public bool llegoAlFinal = false;
    public int currentScore = 0;
    public TextMeshProUGUI puntosMostrar;
    public bool pausa = false;
    public GameObject panel;
    //public Button pausa;


    void Start()
    {
        FillTetrominoBag();
        SpawnNextTetromino();
        afterHold = false;
        Debug.Log("Start: CT = " + currentHeldTetromino == null ? "null" : "tiene un valor");
    }

    void Update()
    {
        puntosMostrar.text = "Puntos: " + currentScore;
        Debug.Log("en pausa:" + pausa);
    }

    public void pausaBtn()
    {
        pausa = !pausa;
        panel.transform.position += new Vector3( pausa ? 1920.0f : -1920.0f, 0.0f, 0.0f);
    }

    void FillTetrominoBag()
    {
        string[] tetrominos = { "Tetromino_T", "Tetromino_I", "Tetromino_J", "Tetromino_L", "Tetromino_O", "Tetromino_S", "Tetromino_Z" };

        List<string> tetrominoList = new List<string>(tetrominos);
        while (tetrominoList.Count > 0)
        {
            int randomIndex = Random.Range(0, tetrominoList.Count);
            tetrominoBag.Enqueue(tetrominoList[randomIndex]);
            tetrominoList.RemoveAt(randomIndex);
        }
    }

    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x,y] = null;
        }
    }


    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for( int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                currentScore += 100;
                MoveAllRowsDown(y + 1);
                --y;
            }
        }
    }

    public void UpdateGrid(Tetromino tetromino)
    {
        for (int y=0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y ] != null)
                {
                    if (grid[x,y].parent == tetromino.transform)
                    {
                        grid[x, y] = null; 
                    }
                }
            }
        }

        foreach(Transform mino in tetromino.transform)
        {
            Vector2 pos = Round (mino.position);

            if(pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public List<string> ListTetrominos()
    {
        
        if (listTetrominos.Count == 0) {
            //Debug.Log("Llenando lista");
            for (int i = 0; i < 6; i++)
            {
                listTetrominos.Add(GetNextTetromino());
            }
        }
        else
        {
            //Debug.Log("Remover agregar");
            listTetrominos.RemoveAt(0);
            listTetrominos.Add(GetNextTetromino());
        }

        for (int i = 0; i < 5; i++)
        {
            NextPanel[i].sprite = spritesTetrominos.FirstOrDefault(s => s.name[s.name.Length - 1] == listTetrominos[i + 1][listTetrominos[i + 1].Length - 1]);
        }

        return listTetrominos;
    }

    public void SpawnNextTetromino()
    {
        if (!llegoAlFinal)
        {
            string nextTetrominoPath = seCambiaPorHold ? currentHeldTetromino.prefabPath : ListTetrominos()[0];
            GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(nextTetrominoPath, typeof(GameObject)), new Vector2(5.0f, 19.0f), Quaternion.identity);
            nextTetromino.GetComponent<Tetromino>().prefabPath = nextTetrominoPath;
        }
    }

    public void HoldPanel()
    {
        if (!llegoAlFinal)
        {
            if (piezaRTomada)
            {
                currentHeldTetromino = piezaRecienTomada;
            }
            string prefabPath = currentHeldTetromino.prefabPath;
            UpdateHoldPanel(prefabPath);

        }
    }

    void UpdateHoldPanel(string tetrominoPath)
    {
        holdPanel.sprite = spritesTetrominos.FirstOrDefault(s => s.name[s.name.Length - 1] == tetrominoPath[tetrominoPath.Length - 1]);
    }

    public bool CheckIsInsideGrid(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x < gridWidth && (int)position.y >= 0);
    }

    public Vector2 Round (Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x), Mathf.Round(position.y));
    }

    string GetNextTetromino()
    {
        if (tetrominoBag.Count == 0)
        {
            FillTetrominoBag();
        }

        return "Prefabs/" + tetrominoBag.Dequeue();
    }


}
