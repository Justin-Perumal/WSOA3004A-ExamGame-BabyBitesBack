using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Sprite[] MomSprites;

    public float MaxHP;
    public float CurrentHP;
    public GameObject HealthDisplay;

    [SerializeField] private float HealthThreshold1;
    [SerializeField] private float HealthThreshold2;
    public bool EnterPhase1 = false;
    public bool EnterPhase2 = false;
    public bool EnterPhase3 = false;

    public Transform LazerPoint;
    public GameObject Lazer;
    public bool LazerReady;
    public bool LazerPhase;
    // Start is called before the first frame update
    void Start()
    {
        EnterPhase1 = true;
        HealthThreshold1 = Mathf.Round((MaxHP/3)*2);
        HealthThreshold2 = Mathf.Round(MaxHP/3);
        CurrentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HealthDisplay.GetComponent<Image>().fillAmount = CurrentHP/MaxHP;

        if(EnterPhase1 && !EnterPhase2 && !EnterPhase3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = MomSprites[0];
        }

        if(CurrentHP <= HealthThreshold1 && CurrentHP > HealthThreshold2)
        {
            EnterPhase2 = true;
        }
        else if(CurrentHP <= HealthThreshold2)
        {
            EnterPhase3 = true;
            LazerPhase = true;
        }

        if(CurrentHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Mom defeated");
        }

        if(LazerReady && LazerPhase)
        {
            Instantiate(Lazer, LazerPoint.position, Quaternion.identity);
            StartCoroutine(LazerCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("PlayerAtkHitBox"))
        {
            CurrentHP -= 2;
        }

        if(Col.CompareTag("QAttackHitBox"))
        {
            CurrentHP--;
        }

        if(Col.CompareTag("UltimateAttack"))
        {
            CurrentHP -= 5;
        }
    }

    public void LazerEyes()
    {
        
    }

    public IEnumerator LazerCooldown()
    {
        LazerReady = false;
        yield return new WaitForSeconds(5f);
        LazerReady = true;
    }
}
