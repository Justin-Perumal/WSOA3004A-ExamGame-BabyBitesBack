using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement PMove;
    public PhaseManager PM;
    public RangedEnemyAttack REA;
    public GameObject DamageEffect;
    public float MoveSpeed;
    public Transform[] Waypoints;
    public int CurrentMovePos;

    public int MaxHP;
    public int CurrentHP;
    private bool HitByUlt = false;
    public bool FacingRight= false;
    public bool FacingLeft =true;

    public Animator TedEnemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TedEnemyAnimator.SetBool("WalkFront", true);
        PM = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
        Player = GameObject.Find("Player");
        PMove = Player.GetComponent<PlayerMovement>();
        CurrentHP = MaxHP;
        TedEnemyAnimator = gameObject.GetComponent<Animator>();

        if(PMove.CurrentLevel == "BossLevel")
        {
            transform.localScale = new Vector3(2.25f, 2.25f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!PMove.UltimateInUse)
        {
            if(!REA.Attacking)
            {
                EnemyMovement();
            }
        }

        if(CurrentHP <= 0)
        {
            Instantiate(DamageEffect, transform.position, Quaternion.identity);

            if(PM != null)
            {
                PM.TedsDestroyed++;
            }

            if(HitByUlt)
            {
                PMove.CurrentUlt++;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            CurrentHP -= 2;
            //Debug.Log("Hit Enemy " + "; Enemy HP = " + CurrentHP);
            //Need to add a way to check repetitive attacks as well as enemy flinch
        }

        if(Col.CompareTag("QAttackHitBox"))
        {
            TedEnemyAnimator.SetTrigger("Hurt");
            CurrentHP--;
        }

        if(Col.CompareTag("UltimateAttack"))
        {
            CurrentHP -= 10;
            HitByUlt = true;
        }
    }

    public void EnemyMovement()
    {
        //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed*Time.deltaTime); //Makes enemy chase player

        if((gameObject.transform.position.x - Player.transform.position.x) < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
            FacingRight = true;
            FacingLeft = false;
        }

        if((gameObject.transform.position.x - Player.transform.position.x) > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);
            FacingRight = false;
            FacingLeft = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, Waypoints[CurrentMovePos].position, MoveSpeed*Time.deltaTime);

        if(Vector2.Distance(transform.position, Waypoints[CurrentMovePos].position) < 0.2f)
        {
            if(CurrentMovePos == Waypoints.Length-1)
            {
                CurrentMovePos = 0;
                TedEnemyAnimator.SetBool("WalkFront", false);
                TedEnemyAnimator.SetBool("WalkBack", true);
            }
            else
            {
                TedEnemyAnimator.SetBool("WalkBack", false);
                TedEnemyAnimator.SetBool("WalkFront", true);
                CurrentMovePos++;
            }
        }
    }
}
