using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    public GameObject Player;
    public RangedEnemyAttack REA;
    public float MoveSpeed;
    public bool FacingRight= false;
    public bool FacingLeft =true;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!REA.Attacking)
        {
            //EnemyMovement();
        }
    }

    public void EnemyMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed*Time.deltaTime); //Makes enemy chase player

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
    }
}
