using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    [Header("Phase Control")]
    public string CurrentPhase;
    public int PhasePicker; //Randomize which phase is being used
    public int PhasesComplete;
    public GameObject[] SpawnsToBeDestroyed;

    [Header("Healing Phase")]
    public GameObject HealthBlocks;

    [Header("Boss Control")]
    public GameObject Boss;
    public BossController BC;
    public GameObject AttackBossText;
    public GameObject BossHealthDisplay;

    [Header("Barrage Phase")]
    public GameObject BarragePattern;
    public bool BarrageComplete;

    [Header("Brock Spawn Phase")]
    public GameObject BrockSpawnPattern;
    public int SpawnsDestroyed;

    [Header("Ted Spawn Phase")]
    public GameObject TedSpawnPattern;
    public int TedsDestroyed;

    [Header("Brock + Ted Spawn Phase")]
    public GameObject BrockAndTed;

    [Header("Brock + Barrage Phase")]
    public GameObject BrockAndBarrage;

    [Header("Ted + Barrage Phase")]
    public GameObject TedAndBarrage;

    // Start is called before the first frame update
    void OnEnable()
    {
        RandomisePhase();
        ImplementPhase();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) //Testing
        {
            RandomisePhase();
            ImplementPhase();
        }   

        if(Input.GetKeyDown(KeyCode.P)) //Testing
        {
            PhasePicker = 0;
            ImplementPhase();
        }

        if(PhasePicker == 1)
        {
            ManageBarragePhase();
        }

        if(PhasePicker == 2)
        {
            ManageBrockSpawnPhase();
        }

        if(PhasePicker == 3)
        {
            ManageTedSpawnPhase();
        }

        if(PhasePicker == 4)
        {
            ManageComboBrockAndTedPhase();
        }

        if(PhasePicker == 5)
        {
            ManageBrockAndBarragePhase();
        }

        if(PhasePicker == 6)
        {
            ManageTedAndBarragePhase();
        }
    }

    public void SpawnHealthBlocks()
    {
        RandomisePhase();
        StartCoroutine(PhaseDowntime());
    }

    public void ManageBossVulnerability()
    {
        Boss.GetComponent<BoxCollider2D>().enabled = true;
        Boss.GetComponent<Animator>().SetBool("MomVulnerable", true);
        StartCoroutine(BossVulnerabilityTimer());
    }

    public void ManageBarragePhase()
    {
        if(PhasePicker == 1 && BarrageComplete)
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 1)
                {
                    Debug.Log("MWAHAHA NO BARRAGE");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }
            
            Debug.Log("Brock Phase complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void PickBarragePattern()
    {
        //When array is implemented --> Use a loop to pick which pattern needs to be instantiated
    }

    public void ManageBrockSpawnPhase()
    {
        if(PhasePicker == 2 && SpawnsDestroyed == 2)
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 2)
                {
                    Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }
            
            Debug.Log("Brock Phase complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void PickBrockSpawnPattern()
    {
        //When array is implemented --> Use a loop to pick which pattern needs to be instantiated
    }

    public void ManageTedSpawnPhase()
    {
        if(PhasePicker == 3 && TedsDestroyed == 4)
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 3)
                {
                    Debug.Log("Working again");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }

            Debug.Log("Ted Phase Complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void PickTedSpawnPattern()
    {
        //When array is implemented --> Use a loop to pick which pattern needs to be instantiated
    }

    public void ManageComboBrockAndTedPhase()
    {
        if(PhasePicker == 4 && ((SpawnsDestroyed == 2 && TedsDestroyed == 1) || (SpawnsDestroyed == 1 && TedsDestroyed == 2)))
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 4)
                {
                    //Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }
            
            Debug.Log("Brock Phase complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void ManageBrockAndBarragePhase()
    {
        if(PhasePicker == 5 && SpawnsDestroyed == 2 && BarrageComplete)
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 5)
                {
                    //Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }
            
            Debug.Log("Brock Phase complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void ManageTedAndBarragePhase()
    {
        if(PhasePicker == 6 && TedsDestroyed == 2 && BarrageComplete)
        {
            PhasesComplete++;
            if(PhasesComplete < 2)
            {
                RandomisePhase();
                while(PhasePicker == 6)
                {
                    //Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 2)
            {
                PhasePicker = 0;
            }
            
            Debug.Log("Brock Phase complete");
            StartCoroutine(PhaseDowntime());
        }
    }

    public void RandomisePhase()
    {
        if(BC.EnterPhase1 && !BC.EnterPhase2 && !BC.EnterPhase3)
        {
            PhasePicker = Random.Range(1,4);
        }
        else if(BC.EnterPhase1 && BC.EnterPhase2 && !BC.EnterPhase3)
        {
            PhasePicker = Random.Range(4,7);
        }
        else if(BC.EnterPhase1 && BC.EnterPhase2 && BC.EnterPhase3)
        {
            PhasePicker = Random.Range(1,7);
        }
    }

    void ImplementPhase() //Ideally each phase will be seperately controlled by its own script
    {
        SpawnsDestroyed = 0;
        TedsDestroyed = 0;
        BarrageComplete = false;
        DestroyObjects();

        switch(PhasePicker)
        {
            case -1:
            Instantiate(HealthBlocks,gameObject.transform.position, Quaternion.identity);
            SpawnHealthBlocks();
            //Heal Phase --> After Boss phase. Spawn alphabet blocks 
            //Cookies appear (Microwave shake and cookie falls from sky)
            break;

            case 0: 
            ManageBossVulnerability();
            Debug.Log("Implement Boss vulnerability phase"); //Allows the player to attack the mom
            break;

            case 1:
            Instantiate(BarragePattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement barrage phase"); //Cause certain areas of the level to be 'dangerous'
            break;

            case 2:
            Instantiate(BrockSpawnPattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Immense Brock spawns"); //Cause a lot of brocks to spawn
            break;

            case 3:
            Instantiate(TedSpawnPattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Ted Barrage"); //Cause a lot of teds to spawn and attack player
            break;

            case 4:
            Boss.GetComponent<Animator>().SetTrigger("Wag");
            Instantiate(BrockAndTed,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Brock and Ted phase");
            break;
            //etc etc etc;

            case 5:
            Boss.GetComponent<Animator>().SetTrigger("Wag");
            Instantiate(BrockAndBarrage,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Brock and Barrage Phase");
            break;

            case 6:
            Boss.GetComponent<Animator>().SetTrigger("Wag");
            Instantiate(TedAndBarrage, gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Ted and Barrage Phase");
            break;
        }
    }

    void DestroyObjects()
    {
        SpawnsToBeDestroyed = GameObject.FindGameObjectsWithTag("BossSpawns");

        for(int i=0; i<SpawnsToBeDestroyed.Length; i++)
        {
            Destroy(SpawnsToBeDestroyed[i]);
        }
    }

    public IEnumerator PhaseDowntime()
    {
        yield return new WaitForSeconds(3f);
        ImplementPhase();
    }

    public IEnumerator BossVulnerabilityTimer()
    {
        AttackBossText.SetActive(true);
        BossHealthDisplay.SetActive(true);
        yield return new WaitForSeconds(5f);
        AttackBossText.SetActive(false);
        BossHealthDisplay.SetActive(false);
        Boss.GetComponent<BoxCollider2D>().enabled = false;
        Boss.GetComponent<Animator>().SetBool("MomVulnerable", false);
        PhasesComplete = 0;
        PhasePicker = -1;
        ImplementPhase();
    }
}
