using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool1 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++){
            columns[i] = Instantiate(columnPrefab,objectPoolPosition,Quaternion.identity);
        }
        SocketManager.instancia.socket.OnUnityThread("spawnColumn",(response) => {
            SpawnColumn(3);
        });
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (!Bird1.instance.rb2d.IsAwake()) return;
        timeSinceLastSpawned += Time.deltaTime;
        if(!GameController1.instance.gameOver && timeSinceLastSpawned >= spawnRate){
            timeSinceLastSpawned = 0;
            SpawnColumn();
        }
    }*/

    void SpawnColumn(int response){
        float spawnYPosition = Random.Range(columnMin,columnMax);
            columns[currentColumn].transform.position = new Vector2(spawnXPosition,spawnYPosition);
            currentColumn++;
            if(currentColumn >= columnPoolSize){
                currentColumn = 0;
            }
    }
}
