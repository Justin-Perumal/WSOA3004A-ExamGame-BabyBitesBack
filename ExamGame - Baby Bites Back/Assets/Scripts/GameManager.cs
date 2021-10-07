using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int EnemiesKilled;
    public float Points;

    public GameObject Pointer;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesKilled >= 5)
        {
            Pointer.SetActive(true);
        }
    }
}
