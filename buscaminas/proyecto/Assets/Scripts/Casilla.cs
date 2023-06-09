using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void mostrar(){
        if(Minas.instancia.fin) return;
        if(!Minas.instancia.inicio){
            Minas.instancia.inicio = true;
            Minas.instancia.crearMatch(id);
            Minas.instancia.mostrarVacias(id);
        }
        if(Minas.instancia.childs[id].isBomb) Minas.instancia.mostrarMinas(id);

        img.sprite = Minas.instancia.sprites[Minas.instancia.childs[id].numero];
        Minas.instancia.childs[id].mostrar = true;
    }
    public void bandera(){
        if(Minas.instancia.childs[id].mostrar) return;
        isBandera = isBandera ? false:true;
        img.sprite = isBandera ? Minas.instancia.sprites[12]:Minas.instancia.sprites[13];
    }
    public void enterMouse(){
        Minas.instancia.actual = id;
    }
    public void exitMouse(){
        Minas.instancia.actual = -1;
    }
}
