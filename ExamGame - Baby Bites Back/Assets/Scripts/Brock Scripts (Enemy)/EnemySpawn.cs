using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject BrockEnemy;

    public int MinSpawnTime;
    public int SpawnTimer;
    public int MaxSpawnTime;
    public bool CanSpawn = true;
    public bool InitialSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());

        if(InitialSpawn)
        {
            Instantiate(BrockEnemy, gameObject.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSpawn)
        {
            StartCoroutine(SpawnEnemy());
            Instantiate(BrockEnemy, gameObject.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        SpawnTimer = Random.Range(MinSpawnTime,MaxSpawnTime);
        CanSpawn = false;
        yield return new WaitForSeconds(SpawnTimer);
        CanSpawn = true;
    }
}
