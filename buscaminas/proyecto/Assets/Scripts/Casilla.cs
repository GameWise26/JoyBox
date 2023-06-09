using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public int id;
    public bool isBandera = false;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }
    public void mostrar(){
        if(Minas.instancia.fin) return;
        if(isBandera) return;
        if(!Minas.instancia.inicio){
            Minas.instancia.inicio = true;
            Minas.instancia.crearMatch(id);
            img.sprite = Minas.instancia.sprites[Minas.instancia.childs[id].numero];
            Minas.instancia.childs[id].mostrar = true;
            Minas.instancia.mostrarVaciasArriba(id,0,0);
            Minas.instancia.ori = DateTime.Now;
            return;
        }
        if(Minas.instancia.childs[id].isBomb) Minas.instancia.mostrarMinas(id);

        img.sprite = Minas.instancia.sprites[Minas.instancia.childs[id].numero];
        Minas.instancia.childs[id].mostrar = true;
        if(Minas.instancia.total == 0 && comprobar()){
            HappyFace.instancia.gane();
            Minas.instancia.fin = true;
        }
    }
    public void bandera(){
        if(Minas.instancia.childs[id].mostrar) return;
        isBandera = isBandera ? false:true;
        img.sprite = isBandera ? Minas.instancia.sprites[12]:Minas.instancia.sprites[13];
        if(isBandera) Minas.instancia.total--;
        else Minas.instancia.total++;
        Minas.instancia.obj.text = "Minas: "+Minas.instancia.total;
        if(Minas.instancia.total == 0 && comprobar()){
            HappyFace.instancia.gane();
            Minas.instancia.fin = true;
        }
    }
    public bool comprobar(){
        foreach(Mina m in Minas.instancia.childs){
            if(!m.mostrar && !m.isBomb){
                return false;
            }
        }
        return true;
    }
    public void enterMouse(){
        Minas.instancia.actual = id;
    }
    public void exitMouse(){
        Minas.instancia.actual = -1;
    }
}
