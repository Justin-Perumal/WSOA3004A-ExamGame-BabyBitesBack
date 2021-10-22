using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int EnemiesKilled;
    public float Points;

    public bool Zone1Complete;
    public GameObject Zone1Wall;
    public bool Zone2Complete;
    public GameObject Zone2Wall;
    public bool Zone3Complete;

    public GameObject Pointer1;
    public GameObject Pointer2;
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
        if(EnemiesKilled >= 5)
        {
            Pointer1.SetActive(true);
            Zone1Complete = true;
            Zone1Wall.SetActive(false);
        }

        if(EnemiesKilled >= 10)
        {
            Pointer2.SetActive(true);
            Zone2Complete = true;
            Zone2Wall.SetActive(false);
        }
        
    }
}
