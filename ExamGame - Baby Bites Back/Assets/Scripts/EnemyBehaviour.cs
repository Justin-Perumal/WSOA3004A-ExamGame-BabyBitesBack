using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int EnemyMaxHP;
    private int CurrentHP;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = EnemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            CurrentHP--;
            Debug.Log("Hit Enemy " + "; Enemy HP = " + CurrentHP);

            //Need to add a way to check repetitive attacks as well as enemy flinch
        }
    }
}
