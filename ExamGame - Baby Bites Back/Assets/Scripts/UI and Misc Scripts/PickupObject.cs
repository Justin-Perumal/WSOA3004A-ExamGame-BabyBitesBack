using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public PlayerHealthSystem PHS;

    public bool  CanPickup = false;

    // Start is called before the first frame update
    void Start()
    {
        PHS = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanPickup && Input.GetKeyDown(KeyCode.C))
        {
            PHS.CurrentHP = PHS.MaxPlayerHP;
            PHS.PlayHealthEffect();
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            CanPickup = true;
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            CanPickup = false;
        }
    }
}
