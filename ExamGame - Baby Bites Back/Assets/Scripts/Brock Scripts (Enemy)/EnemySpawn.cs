using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject BrockEnemy;
    public PhaseManager PM;

    public int MinSpawnTime;
    public int SpawnTimer;
    public int MaxSpawnTime;
    public bool CanSpawn = true;
    public bool InitialSpawn = true;
    public float  MaxHP;
    [SerializeField] private float CurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        PM = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
        StartCoroutine(SpawnEnemy());

        if(InitialSpawn)
        {
            Instantiate(BrockEnemy, gameObject.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PM == null)
        {
            return;
        }

        if(CanSpawn)
        {
            StartCoroutine(SpawnEnemy());
            Instantiate(BrockEnemy, gameObject.transform.position, Quaternion.identity);
        }

        if(CurrentHP <= 0)
        {
            Destroy(gameObject);
            if(PM != null)
            {
                PM.SpawnsDestroyed++;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            CurrentHP -= 2;
        }

        if(Col.CompareTag("QAttackHitBox"))
        {
            CurrentHP--;
        }

        if(Col.CompareTag("UltimateAttack"))
        {
            CurrentHP -= 10;
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
