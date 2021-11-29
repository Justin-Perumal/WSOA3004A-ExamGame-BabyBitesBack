using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP;

    [SerializeField] private float HealthThreshold1;
    [SerializeField] private float HealthThreshold2;
    public bool EnterPhase1 = false;
    public bool EnterPhase2 = false;
    public bool EnterPhase3 = false;

    // Start is called before the first frame update
    void Start()
    {
        EnterPhase1 = true;
        HealthThreshold1 = Mathf.Round((MaxHP/3)*2);
        HealthThreshold2 = Mathf.Round(MaxHP/3);
        CurrentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHP <= HealthThreshold1 && CurrentHP > HealthThreshold2)
        {
            EnterPhase2 = true;
        }
        else if(CurrentHP <= HealthThreshold2)
        {
            EnterPhase3 = true;
        }

        if(CurrentHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Mom defeated");
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
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
}
