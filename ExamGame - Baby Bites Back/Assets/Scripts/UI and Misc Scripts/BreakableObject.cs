using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakableObject : MonoBehaviour
{
    public float ObjectHealth;
    public GameObject HealthPickUp;
    public GameObject PickUpText;

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
            if(PickUpText != null)
            {
                PickUpText.GetComponent<TextMeshProUGUI>().text = "Press C to pick up the cookie. Cookies heal you to full health";
            }
            gameObject.SetActive(false);
        }

        if(PickUpText == null)
        {
            return;
        }
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox") || Col.CompareTag("QAttackHitBox"))
        {
            ObjectHealth--;
        }
    }
}
