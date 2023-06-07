using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    int piezasUsadas;

    public List<GameObject> crearBolsa()
    {
        List<GameObject> bolsa = new List<GameObject>();
        for (int i = 0; i < groups.Length; i++)
        {
            bolsa.Add(groups[i]);
        }
        return bolsa;
    }
    
    public void spawnNext(List<GameObject> bolsa)
    {
        piezasUsadas++;
        int tetromino = Random.Range(0, bolsa.Count);
        Instantiate(groups[tetromino],
                    transform.position,
                    Quaternion.identity);
        
        bolsa.RemoveAt(tetromino);
        if (piezasUsadas == 7) { crearBolsa(); }
    }

    void Start()
    {
        List<GameObject> bolsa = crearBolsa();
        spawnNext(bolsa);
    }
}



