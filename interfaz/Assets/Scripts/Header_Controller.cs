using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Header_Controller : MonoBehaviour
{
    public Image fdp;
    // Start is called before the first frame update
    void Start()
    {
        if(SocketManager.instancia.fdp != null)
        fdp.sprite = SocketManager.instancia.fdp;
    }
}
