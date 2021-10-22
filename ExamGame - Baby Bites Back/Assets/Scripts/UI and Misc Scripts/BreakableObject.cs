using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public float ObjectHealth;
    public GameObject HealthPickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectHealth <= 0)
        {
            Instantiate(HealthPickUp, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            ObjectHealth--;
        }
    }
}
