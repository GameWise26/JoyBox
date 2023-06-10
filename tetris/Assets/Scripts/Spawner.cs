using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    int piezasUsadas;
    public List<GameObject> bolsa = new List<GameObject>();
    int piezasppawn = 0;
    bool existe = true;
    //bool existe2 = true;

    public List<GameObject> crearBolsa(List<GameObject> bolsa)
    {
        int numer = 0;
        List<int> nume = new List<int>();
        for (int i = 0; i < 7; i++)
            nume.Add(i);

        for (int i = 0; i < nume.Count + 6; i++)
        {
            numer = nume[Random.Range(0, nume.Count)];
            bolsa.Add(groups[numer]);
            nume.Remove(numer);
        }
        for (int i = 0; i < bolsa.Count; i++)
            Debug.Log(bolsa[i]);

        return bolsa;
    }

    public void spawnNext(List<GameObject> bolsa, int pieza)
    {
        //if (pieza < 7 && pieza >= 0)
        //for (int i = 0; i< 7 ; i++){
        Instantiate(bolsa[pieza], transform.position, Quaternion.identity);

        if (pieza == bolsa.Count)
        {
            crearBolsa(bolsa);
            pieza = 0;
        }
        //else
        ////}
        
    }

    void Start()
    {
        if (existe)
            crearBolsa(bolsa);
        existe = false;
        spawnNext(bolsa, piezasppawn);
    }
}
