using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public GameObject Projectile;
    public RangedEnemyBehaviour REB;
    public bool Attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player") && !Attacking)
        {
            Attacking = true;
            GameObject Shot = Instantiate(Projectile, transform.position, Quaternion.identity);
            var ShotRB = Shot.GetComponent<Rigidbody2D>(); //Need to properly do this entire script
            
            if(REB.FacingLeft)
            {
                ShotRB.velocity = new Vector2(-5f,0f);
            }

            if(REB.FacingRight)
            {
                ShotRB.velocity = new Vector2(5f,0f);
            }

            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        Attacking = false;
    }
}
