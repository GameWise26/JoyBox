using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 5;
    public GameObject columnPrefab;

    public float columnMin = -2.9f;
    public float columnMax = 1.4f;
    private float spawnXPosition = 8.31f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-14,0);

    private float timeSinceLastSpawned;
    public float spawnRate;

    private int currentColumn;
    private bool bandera = false;

    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++){
            columns[i] = Instantiate(columnPrefab,objectPoolPosition,Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Bird.instance.rb2d.IsAwake()) return;
        timeSinceLastSpawned += Time.deltaTime;
        if(!GameController.instance.gameOver && timeSinceLastSpawned >= spawnRate){
            timeSinceLastSpawned = 0;
            SpawnColumn();
            if(!bandera){
                bandera = true;
                SocketManager.instancia.Emit("ficols",new Dictionary<string,bool>(){{"empieza",true}});
            }
        }
    }

    void SpawnColumn(){
        float spawnYPosition = Random.Range(columnMin,columnMax);
        columns[currentColumn].transform.position = new Vector2(spawnXPosition,spawnYPosition);
        float x = currentColumn;
        currentColumn++;
        if(currentColumn >= columnPoolSize){
            currentColumn = 0;
        }
        SocketManager.instancia.Emit("fcols",new Dictionary<string,float>(){{"y",spawnYPosition},{"pos",currentColumn}});
    }
}
