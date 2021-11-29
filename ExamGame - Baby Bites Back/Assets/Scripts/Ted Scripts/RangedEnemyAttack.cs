using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public PlayerMovement PM;
    public GameObject Projectile;
    public int ProjectileCounter;
    public RangedEnemyBehaviour REB;
    private Rigidbody2D ShotRB;
    public bool ProjectileDelayed = false;
    public bool Attacking = false;
    public Animator TedAnim;

    void Awake()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(ProjectileDelayed)
        {
            LaunchProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player") && !Attacking && !PM.UltimateInUse)
        {
            Attacking = true;

            StartCoroutine(Attack());
        }
    }

    public void LaunchProjectile()
    {
        if(REB.FacingLeft)
        {
            ShotRB.velocity = new Vector2(-5f,0f);
        }

        if(REB.FacingRight)
        {
            ShotRB.velocity = new Vector2(5f,0f);
        }
    }

    private IEnumerator Attack()
    {
        TedAnim.SetTrigger("Throwing");
        yield return new WaitForSeconds(0.9f);

        GameObject Shot = Instantiate(Projectile, transform.position, Quaternion.identity);
        ShotRB = Shot.GetComponent<Rigidbody2D>(); //Need to properly do this entire script
        
        yield return new WaitForSeconds(1.1f);
        Attacking = false;
    }

}
