using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;

    public int MinSpawnTime;
    public int SpawnTimer;
    public int MaxSpawnTime;
    public bool CanSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        CanSpawn = true;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        SpawnTimer = Random.Range(MinSpawnTime,MaxSpawnTime);
        CanSpawn = false;
        Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(SpawnTimer);
        CanSpawn = true;
    }
}
