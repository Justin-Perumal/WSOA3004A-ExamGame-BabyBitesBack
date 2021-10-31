using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("Objects and Components")]
    public GameObject HealthImage;
    public GameObject LostMenu;
    public GameObject Player;
    public GameObject PlayerAvatarImage; // --> Replace the current test with actual images
    public Sprite[] HealthStates;
    public GameObject HealthEffect;

    [Header("Variables")]
    public float MaxPlayerHP;
    [SerializeField] private float MidHealthThreshold; 
    [SerializeField] private float LowHealthThreshold;
    [SerializeField] private bool PlayerInvincible = false;
    [SerializeField] public float CurrentHP;
    [SerializeField] private float InvincibilityTime;

    [Header("Sliders")]
    public Slider HealthBar;

    public static PlayerHealthSystem instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        CurrentHP = MaxPlayerHP;
        HealthBar.maxValue = MaxPlayerHP;
        HealthBar.value = MaxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        //HealthImage.GetComponent<Image>().fillAmount = CurrentHP/MaxPlayerHP;

        HealthBar.value = CurrentHP;

        if(CurrentHP <= 5 && CurrentHP > 3)
        {
            PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[0];
        }
            else if(CurrentHP <= 3 && CurrentHP > 1)
            {
                PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[1];
            }
                else if(CurrentHP <= 1)
                {
                    PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[2];
                }  

        if(CurrentHP <= 0)
        {
            Time.timeScale = 0f;
            LostMenu.SetActive(true);
            //gameObject.SetActive(false);
        }
    }

    public void PlayHealthEffect()
    {
        Instantiate(HealthEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if((Col.CompareTag("EnemyHitBox") || Col.CompareTag("Projectile")) && !PlayerInvincible)
        {
            CurrentHP--;
            PlayerInvincible = true;
            Debug.Log("Player HP: " + CurrentHP);

            StartCoroutine(InvincibileTimer());

           /* if(Player.GetComponent<PlayerMovement>().IsRight)
            {
                Player.transform.position = new Vector2(Player.transform.position.x-1f, Player.transform.positon.y);
            } 
                --> Knockback 
            */
        }
    }

    private IEnumerator InvincibileTimer()
    {
        yield return new WaitForSeconds(InvincibilityTime);
        PlayerInvincible = false;
    }
}
