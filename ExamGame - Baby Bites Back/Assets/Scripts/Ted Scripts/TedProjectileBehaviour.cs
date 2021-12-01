using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TedProjectileBehaviour : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D ShotRB;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        ShotRB = gameObject.GetComponent<Rigidbody2D>(); //Need to properly do this entire script
    
        if((gameObject.transform.position.x - Player.transform.position.x) > 0)
        {
            ShotRB.velocity = new Vector2(-5f,0f);
        }

        if((gameObject.transform.position.x - Player.transform.position.x) < 0)
        {
            ShotRB.velocity = new Vector2(5f,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
