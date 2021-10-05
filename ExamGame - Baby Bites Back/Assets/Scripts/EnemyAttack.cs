using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAttack : MonoBehaviour
{
    [Header("Objects and Components")]
    public GameObject EnemyAtkHitBox;
    public GameObject AttackText; //Temporary until enemy has an attack animation

    [Header("Attack stuff")]
    public bool Attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AttackText.transform.eulerAngles = new Vector3(0f,0f,0f);
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            EnemyAtkHitBox.SetActive(true);
            Attacking = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyAtkHitBox.SetActive(false);
        Attacking = false;
    }
}
