using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mina{
    public Transform mina;
    public bool isBomb = false,mostrar = false;
    public int numero;

    public Mina(Transform mina){
        this.mina = mina;
    }
}
public class Minas : MonoBehaviour
{
    public static Minas instancia;
    public List<Mina> childs;
    public List<Sprite> sprites;
    public bool inicio = false,fin = false;
    public int actual;

    private void Awake(){
        Minas.instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        childs = new List<Mina>();
        int numHijos = transform.childCount;
        for (int i = 0; i < numHijos; i++)
        {
            Transform child = transform.GetChild(i);
            childs.Add(new Mina(child));
            child.GetComponent<Casilla>().id = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && actual != -1 && !fin)
            childs[actual].mina.GetComponent<Casilla>().bandera();
    }
    public void crearMatch(int index){
        List<int> posibles = new List<int>{0,8,16,24,32,40,48,56};
        int minas = 10;
        for (int i = 0; i < childs.Count; i++){
            if(Random.Range(0,40) > 30 && minas > 0 && !childs[i].isBomb){
                minas--;
                childs[i].isBomb = true;
            } else if(!(minas > 0))
                break;
            if(i == childs.Count-1)
                i = 0;
        }
        for(int i = 0; i < childs.Count; i++){
            if(childs[i].isBomb){
                childs[i].numero = 10;
                continue;
            }
            int count = 0;
            int si = posibles.Find(v => v - i > -8);
            if(i-8 >= 0 && childs[i-8].isBomb)
                count++;
            if(i-9 >= 0 && i-9 >= si - 8 && childs[i-9].isBomb)
                count++;
            if(i-7 >= 0 && i-7 < si && childs[i-7].isBomb)
                count++;
            if(i-1 >= 0 && i-1 >= si && childs[i-1].isBomb)
                count++;
            if(i+1 < childs.Count && i+1 < si+8 && childs[i+1].isBomb)
                count++;
            if(i+7 < childs.Count && i+7 >= si+8 && childs[i+7].isBomb)
                count++;
            if(i+8 < childs.Count && childs[i+8].isBomb)
                count++;
            if(i+9 < childs.Count && i+9 <= si+15 && childs[i+9].isBomb)
                count++;
            childs[i].numero = count;
        }
        if(childs[index].numero != 0){
            for(int i = 0; i < childs.Count;i++){
                childs[i].isBomb = false;
            }
            crearMatch(index);
        }
    }
    public void mostrarMinas(int index){
        foreach(Mina m in childs){
            if(m.mina.GetComponent<Casilla>().id == index) continue;
            if(m.isBomb && !m.mina.GetComponent<Casilla>().isBandera){
                m.mina.GetComponent<Image>().sprite = sprites[9];
            }
            else if(m.mina.GetComponent<Casilla>().isBandera && !m.isBomb)
                m.mina.GetComponent<Image>().sprite = sprites[11];
        }
        fin = true;
    }
    public void mostrarVaciasArriba(int index, int pos){
        for(int i = pos; i < childs.Count; i++){
            int si = posibles.Find(v => v - i > -8);
            int count = 0;
            if(i-8 >= 0 && childs[i-8].isBomb)
                mostrarVaciasArriba(index,i-8);
            if(i-9 >= 0 && i-9 >= si - 8 && childs[i-9].isBomb)
                mostrarVaciasArriba(index);
            if(i-7 >= 0 && i-7 < si && childs[i-7].isBomb)
                count++;
            if(i-1 >= 0 && i-1 >= si && childs[i-1].isBomb)
                count++;
            if(i+1 < childs.Count && i+1 < si+8 && childs[i+1].isBomb)
                count++;
            if(i+7 < childs.Count && i+7 >= si+8 && childs[i+7].isBomb)
                count++;
            if(i+8 < childs.Count && childs[i+8].isBomb)
                count++;
            if(i+9 < childs.Count && i+9 <= si+15 && childs[i+9].isBomb)
                count++;
            /*if(index-(8*(i+1)) >= 0 && childs[index-(8*(i+1))].numero == 0){
                childs[index-(8*(i+1))].mina.GetComponent<Casilla>().mostrar();
                mostrarVaciasIzquierda(index-(8*(i+1)),0);
                if(index-(8*(i+2)) > 0 && childs[index-(8*(i+2))].numero != 0)
                    childs[index-(8*(i+2))].mina.GetComponent<Casilla>().mostrar();
            }
            if(index-(8*(i+1)) > 0 && childs[index-(8*(i+1))].numero != 0){
                if(intentos == 4)
                    break;
                intentos++;
                mostrarVaciasIzquierda(index,intentos);
                break;
            }*/

        }
    }
}
