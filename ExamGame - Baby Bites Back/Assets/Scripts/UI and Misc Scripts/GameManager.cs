using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string CurrentLevel;

    public int EnemiesKilled;
    public int RangedEnemiesKilled;
    public float Points;

    [Header("Zone Components and variables")]
    public bool Zone1Complete;
    public GameObject Zone1Wall;
    public int Zone1Enemies;
    public bool Zone2Complete;
    public GameObject Zone2Wall;
    public int Zone2Enemies;
    public bool Zone3Complete;
    public GameObject Zone3Wall;
    public int Zone3Enemies;
    public GameObject Pointer1;
    public GameObject Pointer2;
    public GameObject Pointer3;

    [Header("Zone Spawn Objects")]
    public GameObject Zone1Spawns;
    public GameObject Zone2Spawns;
    public GameObject Zone3Spawns;

    [Header("Zone Completion Panels")]
    public GameObject Zone1Panel;
    public GameObject Zone1PanelText;
    public GameObject Zone2Panel;
    public GameObject Zone2PanelText;
    public GameObject Zone3Panel;
    public GameObject Zone3PanelText;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesKilled = 0;
        Zone1Complete = false;
        Zone2Complete = false;
        Zone3Complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesKilled >= Zone1Enemies)
        {
            Pointer1.SetActive(true);
            Zone1Complete = true;
            Zone1Wall.SetActive(false);
            Zone1Spawns.SetActive(false);
            Zone1Panel.SetActive(false);
        }

        if(EnemiesKilled >= Zone2Enemies)
        {
            Pointer2.SetActive(true);
            Zone2Complete = true;
            Zone2Wall.SetActive(false);
            Zone2Spawns.SetActive(false);
            Zone2Panel.SetActive(false);
        }

        if(EnemiesKilled >= Zone3Enemies)
        {
            Pointer3.SetActive(true);
            Zone3Complete = true;
            Zone3Wall.SetActive(false);
            Zone3Spawns.SetActive(false);
            Zone3Panel.SetActive(false);
        }

        Zone1PanelText.GetComponent<TextMeshProUGUI>().text = "Brocks defeated: " + EnemiesKilled.ToString() + "/" + Zone1Enemies.ToString();
        Zone2PanelText.GetComponent<TextMeshProUGUI>().text = "Brocks defeated: " + (EnemiesKilled-Zone1Enemies).ToString() + "/" + (Zone2Enemies-Zone1Enemies).ToString();
        Zone3PanelText.GetComponent<TextMeshProUGUI>().text = "Brocks defeated: " + (EnemiesKilled-Zone2Enemies).ToString() + "/" + (Zone3Enemies-Zone2Enemies).ToString();
        
    }

    public void SpawnActivationZone1()
    {
        Zone1Spawns.SetActive(true);
    }

    public void SpawnActivationZone2()
    {
        Zone2Spawns.SetActive(true);
    }

    public void SpawnActivationZone3()
    {
        Zone3Spawns.SetActive(true);
    }
}
