using System.Collections;
using System;
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
    public int actual,total,tiempo;
    public List<int> posibles = new List<int>{0,8,16,24,32,40,48,56};
    public DateTime ori,ti;
    private TimeSpan di;
    public Text obj,obj1;

    private void Awake(){
        Minas.instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        total = 10;
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
        ti = DateTime.Now;
        if(ori != null && !fin){
            di = ti - ori;
            if(di.Seconds == 1){
                tiempo++;
                obj1.text = "Tiempo: "+tiempo;
                ori = DateTime.Now;
            }
        }
        if(Input.GetMouseButtonDown(1) && actual != -1 && !fin)
            childs[actual].mina.GetComponent<Casilla>().bandera();
    }
    public void crearMatch(int index){
        int minas = 10;
        for (int i = 0; i < childs.Count; i++){
            if(UnityEngine.Random.Range(0,40) > 30 && minas > 0 && !childs[i].isBomb){
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
        HappyFace.instancia.perdi();
    }
    public void mostrarVaciasArriba(int index, int pos, int original){
        for(int i = pos; i < childs.Count; i++){
            List<int> usados = new List<int>();
            int si = posibles.Find(v => v - i > -8);
            if(i-8 >= 0 && childs[i-8].numero == 0){
                usados.Add(i-8);
                mostrarVaciasArribaa(index,i-8,original,usados);
            }
            if(i-9 >= 0 && i-9 >= si - 8 && childs[i-9].numero == 0){
                usados.Add(i-9);
                mostrarVaciasArribaa(index,i-9,original,usados);
            }
            if(i-7 >= 0 && i-7 < si && childs[i-7].numero == 0){
                usados.Add(i-7);
                mostrarVaciasArribaa(index,i-7,original,usados);
            }
            if(i-1 >= 0 && i-1 >= si && childs[i-1].numero == 0){
                usados.Add(i-1);
                mostrarVaciasArribaa(index,i-1,original,usados);
            }
            if(i+1 < childs.Count && i+1 < si+8 && childs[i+1].numero == 0){
                usados.Add(i+1);
                mostrarVaciasArribaa(index,i+1,original,usados);
            }
            if(i+7 < childs.Count && i+7 >= si+8 && childs[i+7].numero == 0){
                usados.Add(i+7);
                mostrarVaciasArribaa(index,i+7,original,usados);
            }
            if(i+8 < childs.Count && childs[i+8].numero == 0){
                usados.Add(i+8);
                mostrarVaciasArribaa(index,i+8,original,usados);
            }
            if(i+9 < childs.Count && i+9 <= si+15 && childs[i+9].numero == 0){
                usados.Add(i+9);
                mostrarVaciasArribaa(index,i+9,original,usados);
            }
            original++;
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
    private void mostrarVaciasArribaa(int index,int pos, int original, List<int> usados){
            if(pos == index)
                childs[original].mina.GetComponent<Casilla>().mostrar();
            int si = posibles.Find(v => v - pos > -8);
            if(!usados.Exists(v => v == pos-8) && pos-8 >= 0 && childs[pos-8].numero == 0){
                usados.Add(pos-8);
                mostrarVaciasArribaa(index,pos-8,original,usados);
            }
            if(!usados.Exists(v => v == pos-9) && pos-9 >= 0 && pos-9 >= si - 8 && childs[pos-9].numero == 0){
                usados.Add(pos-9);
                mostrarVaciasArribaa(index,pos-9,original,usados);
            }
            if(!usados.Exists(v => v == pos-7) && pos-7 >= 0 && pos-7 < si && childs[pos-7].numero == 0){
                usados.Add(pos-7);
                mostrarVaciasArribaa(index,pos-7,original,usados);
            }
            if(!usados.Exists(v => v == pos-1) && pos-1 >= 0 && pos-1 >= si && childs[pos-1].numero == 0){
                usados.Add(pos-1);
                mostrarVaciasArribaa(index,pos-1,original,usados);
            }
            if(!usados.Exists(v => v == pos+1) && pos+1 < childs.Count && pos+1 < si+8 && childs[pos+1].numero == 0){
                usados.Add(pos+1);
                mostrarVaciasArribaa(index,pos+1,original,usados);
            }
            if(!usados.Exists(v => v == pos+7) && pos+7 < childs.Count && pos+7 >= si+8 && childs[pos+7].numero == 0){
                usados.Add(pos+7);
                mostrarVaciasArribaa(index,pos+7,original,usados);
            }
            if(!usados.Exists(v => v == pos+8) && pos+8 < childs.Count && childs[pos+8].numero == 0){
                usados.Add(pos+8);
                mostrarVaciasArribaa(index,pos+8,original,usados);
            }
            if(!usados.Exists(v => v == pos+9) && pos+9 < childs.Count && pos+9 <= si+15 && childs[pos+9].numero == 0){
                usados.Add(pos+9);
                mostrarVaciasArribaa(index,pos+9,original,usados);
            }
            if(original != pos)
                return;
    }
}
