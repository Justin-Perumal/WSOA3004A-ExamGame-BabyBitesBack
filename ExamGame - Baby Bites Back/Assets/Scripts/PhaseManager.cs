using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [Header("Phase Control")]
    public string CurrentPhase;
    public int PhasePicker; //Randomize which phase is being used
    public int PhasesComplete;

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

    // Start is called before the first frame update
    void Start()
    {
        PhasePicker = Random.Range(1,4);
        ImplementPhase();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) //Testing
        {
            PhasePicker = Random.Range(1,4);
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
                PhasePicker = Random.Range(1,4);
                while(PhasePicker == 1)
                {
                    Debug.Log("MWAHAHA NO BARRAGE");
                    PhasePicker = Random.Range(1,4);
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
                PhasePicker = Random.Range(1,4);
                while(PhasePicker == 2)
                {
                    Debug.Log("HAHAHA IT WORKS");
                    PhasePicker = Random.Range(1,4);
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
                PhasePicker = Random.Range(1,4);
                while(PhasePicker == 3)
                {
                    Debug.Log("Working again");
                    PhasePicker = Random.Range(1,4);
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
        // Combination phase of both brock spawns and ted spawns
        // ETC for rest of combination phases
    }

    void ImplementPhase() //Ideally each phase will be seperately controlled by its own script
    {
        switch(PhasePicker)
        {
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

            //etc etc etc;
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
        PhasePicker = Random.Range(1,4);
        ImplementPhase();
    }
}
