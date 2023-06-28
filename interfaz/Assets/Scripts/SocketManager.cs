﻿using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
//using Debug = System.Diagnostics.Debug;

public class SocketManager : MonoBehaviour
{
    public static SocketManager instancia;
    public SocketIOUnity socket;
    

    //Datos del usuario
    public string nombre;
    public List<string> amigos;
    public string juego;

    public GameObject objectToSpin;

    private void Awake(){
        if(SocketManager.instancia == null){
            SocketManager.instancia = this;
            DontDestroyOnLoad(this);
        }else if(SocketManager.instancia != this){
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //TODO: check the Uri if Valid.
        var uri = new Uri("https://joyboxapp.onrender.com");
        //var uri = new Uri("http://localhost:3000");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            //Debug.Print("socket.OnConnected");
        };
        socket.OnPing += (sender, e) =>
        {
            //Debug.Print("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            //Debug.Print("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            //Debug.Print("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            //Debug.Print($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        ////
        juego = "ningun juego";
        //Debug.Print("Connecting...");
        socket.Connect();
        SocketManager.instancia.socket.OnUnityThread("inicioJuego", (response) =>{
            Dictionary<string,string> dict = new Dictionary<string,string>();
            List<string> obj = JsonConvert.DeserializeObject<List<string>>(response.ToString().Substring(1,response.ToString().Length-2));
            dict.Add("nombre",juego);
            dict.Add("id",obj[0]);
            dict.Add("id_amigo",obj[1]);
            socket.Emit("esteJuego",JsonConvert.SerializeObject(dict));
        });
    }
}