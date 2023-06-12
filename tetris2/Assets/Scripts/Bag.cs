using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public TetrominoData[] tetrominos;
    public List<TetrominoData> bag;
    public List<TetrominoData> bagExtra;
    public List<TetrominoData> bagUsadas;


    public TetrominoData GetNextPiece()
    {
        if (bag.Count == 0)
        {
            tetrominos = FindObjectOfType<Board>().tetrominos;

            for (int i = 0; i < tetrominos.Length; i++)
            {
                bag.Add(tetrominos[i]);
                bagExtra.Add(tetrominos[i]);
                bagUsadas.RemoveAt(i);
            }

            for (int i = 0; i < bag.Count; i++)
            {
                int rnd = Random.Range(0, bagExtra.Count);
                bag[i] = bagExtra[rnd];
                bagExtra.RemoveAt(rnd);
            }
            
        }
        bagUsadas.Insert(0, bag[0]);
        bag.RemoveAt(0);
        return bagUsadas[0];
    }
}
