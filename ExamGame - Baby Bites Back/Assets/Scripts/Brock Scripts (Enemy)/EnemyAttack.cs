using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAttack : MonoBehaviour
{
    [Header("Objects and Components")]
    public EnemyBehaviour EB;
    public PlayerMovement PM;
    public GameObject EnemyAtkHitBox;
    public GameObject Enemy;
    public Animator EnemyAnimator;

    [Header("Attack stuff")]
    public bool Attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        EnemyAnimator = Enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D Col)
    {
        if(Col.CompareTag("Player") && !Attacking && !EB.Flinch && !PM.UltimateInUse)
        {
            //EnemyAtkHitBox.SetActive(true);
            Attacking = true;
            EnemyAnimator.SetBool("Attack",true);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        //EnemyAtkHitBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        EnemyAnimator.SetBool("Attack",false);
        //EnemyAtkHitBox.SetActive(false);
        Attacking = false;
    }
}
