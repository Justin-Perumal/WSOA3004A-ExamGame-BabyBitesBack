using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int MaxPlayerHP;
    private int CurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Enemy"))
        {
            CurrentHP--;
            Debug.Log("Player HP: " + CurrentHP);
        }
    }
}
