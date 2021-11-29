using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [Header("Phase Control")]
    public string CurrentPhase;
    public int PhasePicker; //Randomize which phase is being used
    public int PhasesComplete;

    [Header("Healing Phase")]
    public GameObject HealthBlocks;

    [Header("Boss Control")]
    public GameObject Boss;
    public BossController BC;

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

    // Start is called before the first frame update
    void Start()
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

        ManageBarragePhase();
        ManageBrockSpawnPhase();
        ManageTedSpawnPhase();
    }

    public void SpawnHealthBlocks()
    {
        RandomisePhase();
        ImplementPhase();
    }

    public void ManageBossVulnerability()
    {
        Boss.SetActive(true);
        StartCoroutine(BossVulnerabilityTimer());
    }

    public void ManageBarragePhase()
    {
        if(PhasePicker == 1 && BarrageComplete)
        {
            PhasesComplete++;
            if(PhasesComplete < 3)
            {
                RandomisePhase();
                while(PhasePicker == 1)
                {
                    Debug.Log("MWAHAHA NO BARRAGE");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 3)
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
            if(PhasesComplete < 3)
            {
                RandomisePhase();
                while(PhasePicker == 2)
                {
                    Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 3)
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
            if(PhasesComplete < 3)
            {
                RandomisePhase();
                while(PhasePicker == 3)
                {
                    Debug.Log("Working again");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 3)
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
            if(PhasesComplete < 3)
            {
                RandomisePhase();
                while(PhasePicker == 4)
                {
                    //Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 3)
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
            if(PhasesComplete < 3)
            {
                RandomisePhase();
                while(PhasePicker == 4)
                {
                    //Debug.Log("HAHAHA IT WORKS");
                    RandomisePhase();
                }
            }
            else if(PhasesComplete == 3)
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
            PhasePicker = Random.Range(7,10);
        }
    }

    void ImplementPhase() //Ideally each phase will be seperately controlled by its own script
    {
        switch(PhasePicker)
        {
            case -1:
            //Heal Phase --> After Boss phase. Spawn alphabet blocks 
            //Cookies appear (Microwave shake and cookie falls from sky)
            break;

            case 0: 
            ManageBossVulnerability();
            Debug.Log("Implement Boss vulnerability phase"); //Allows the player to attack the mom
            break;

            case 1:
            BarrageComplete = false;
            Instantiate(BarragePattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement barrage phase"); //Cause certain areas of the level to be 'dangerous'
            break;

            case 2:
            SpawnsDestroyed = 0;
            Instantiate(BrockSpawnPattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Immense Brock spawns"); //Cause a lot of brocks to spawn
            break;

            case 3:
            TedsDestroyed = 0;
            Instantiate(TedSpawnPattern,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Ted Barrage"); //Cause a lot of teds to spawn and attack player
            break;

            case 4:
            TedsDestroyed = 0;
            SpawnsDestroyed = 0;
            Instantiate(BrockAndTed,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Brock and Ted phase");
            break;
            //etc etc etc;

            case 5:
            SpawnsDestroyed = 0;
            BarrageComplete = false;
            Instantiate(BrockAndBarrage,gameObject.transform.position, Quaternion.identity);
            Debug.Log("Implement Brock and Barrage Phase");
            break;
        }
    }

    public IEnumerator PhaseDowntime()
    {
        yield return new WaitForSeconds(3f);
        ImplementPhase();
    }

    public IEnumerator BossVulnerabilityTimer()
    {
        yield return new WaitForSeconds(5f);
        Boss.SetActive(false);
        PhasesComplete = 0;
        PhasePicker = -1;
        ImplementPhase();
    }
}
