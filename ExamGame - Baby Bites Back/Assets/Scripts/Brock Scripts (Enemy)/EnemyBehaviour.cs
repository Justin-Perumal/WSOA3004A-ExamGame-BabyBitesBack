using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    [Header("External Scripts")]
    public GameManager GM;
    public EnemyAttack EnemyAtk;

    [Header("Objects and components")]
    public GameObject Player;
    public Animator EnemyAnimator;

    [Header("Enemy Movement")]
    public float MoveSpeed;

    [Header("Variables")]
    public float EnemyMaxHP;
    private float CurrentHP;
    public bool Flinch;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        CurrentHP = EnemyMaxHP;
        EnemyAnimator = gameObject.GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
         if(!EnemyAtk.Attacking && !Flinch)
         {
            EnemyMovement();
         }

         if(CurrentHP <= 0)
         {
            GM.EnemiesKilled++;
            Destroy(gameObject);
         }
    }

    public void EnemyMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed*Time.deltaTime); //Makes enemy chase player

        if((gameObject.transform.position.x - Player.transform.position.x) < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if((gameObject.transform.position.x - Player.transform.position.x) > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            CurrentHP--;
            Debug.Log("Hit Enemy " + "; Enemy HP = " + CurrentHP);

            Flinch = true;
            StartCoroutine(EnemyFlinch());
            EnemyAnimator.SetBool("Flinched",true);

            //Need to add a way to check repetitive attacks as well as enemy flinch
        }
    }

    private IEnumerator EnemyFlinch()
    {
        /*if(Flinch) --> Knockback
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x-7f, transform.position.y), 69*Time.deltaTime);
        } */ 

        yield return new WaitForSeconds(1.5f);
        Flinch = false;
        EnemyAnimator.SetBool("Flinched",false);
    }
}