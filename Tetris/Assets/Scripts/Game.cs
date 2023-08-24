using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    private Queue<string> tetrominoBag = new Queue<string>();
    private List<string> listTetrominos = new List< string>();
    public List<SpriteRenderer> NextPanel = new List<SpriteRenderer>();
    public List<Sprite> spritesTetrominos= new List<Sprite>();

    void Start()
    {
        FillTetrominoBag();
        SpawnNextTetromino();
    }

    void Update()
    {
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

    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(ListTetrominos()[0], typeof(GameObject)), new Vector2(5.0f,18.0f), Quaternion.identity);

    }

    public List<string> ListTetrominos()
    {
        
        if (listTetrominos.Count == 0) {
            Debug.Log("Llenando lista");
            for (int i = 0; i < 6; i++)
            {
                listTetrominos.Add(GetNextTetromino());
            }
        }
        else
        {
            Debug.Log("Remover agregar");
            listTetrominos.RemoveAt(0);
            listTetrominos.Add(GetNextTetromino());
        }

        for (int i = 0; i < 5; i++)
        {
            NextPanel[i].sprite = spritesTetrominos.FirstOrDefault(s => s.name[s.name.Length - 1] == listTetrominos[i + 1][listTetrominos[i + 1].Length - 1]);
        }

        return listTetrominos;
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
