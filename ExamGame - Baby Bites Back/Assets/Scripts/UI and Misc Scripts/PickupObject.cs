using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public PlayerMovement PM;
    public PlayerHealthSystem PHS;

    public bool  CanPickup = false;

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PHS = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PM.CurrentLevel == "BossLevel")
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        if(CanPickup && Input.GetKeyDown(KeyCode.C))
        {
            PHS.CurrentHP += Mathf.Round(PHS.MaxPlayerHP/2);

            if(PHS.CurrentHP >= PHS.MaxPlayerHP)
            {
                PHS.CurrentHP = PHS.MaxPlayerHP;
            }

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
