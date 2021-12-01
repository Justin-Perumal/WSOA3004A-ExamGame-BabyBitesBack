using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("Objects and Components")]
    public PlayerMovement PM;
    public GameObject HealthImage;
    public GameObject LostMenu;
    public GameObject Player;
    public GameObject PlayerAvatarImage; // --> Replace the current test with actual images
    public Sprite[] HealthStates;
    public GameObject HealthEffect;
    public Animator PlayerAnim;

    [Header("Variables")]
    public float MaxPlayerHP;
    [SerializeField] private float MidHealthThreshold; 
    [SerializeField] private float LowHealthThreshold;
    [SerializeField] private bool PlayerInvincible = false;
    [SerializeField] public float CurrentHP;
    [SerializeField] private float InvincibilityTime;
    public bool PlayerSleeping = false;

    [Header("Sliders")]
    public Slider HealthBar;

    [Header("Audio Stuff")]
    public AudioSource AudioSrc;
    public AudioClip UltClip;

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
        PlayerAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //HealthImage.GetComponent<Image>().fillAmount = CurrentHP/MaxPlayerHP;

        HealthBar.value = CurrentHP;

        if(CurrentHP <= MaxPlayerHP && CurrentHP > MidHealthThreshold)
        {
            PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[0];
        }
            else if(CurrentHP <= MidHealthThreshold && CurrentHP > LowHealthThreshold)
            {
                PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[1];
            }
                else if(CurrentHP <= LowHealthThreshold)
                {
                    PlayerAvatarImage.GetComponent<Image>().sprite = HealthStates[2];
                }  

        if(CurrentHP <= 0)
        {
            PlayerAnim.SetTrigger("Death");
            PM.PlayerIsAsleep = true;
            //gameObject.SetActive(false);
        }

        if(PlayerSleeping)
        {
            LostMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PlayHealthEffect()
    {
        Instantiate(HealthEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if((Col.CompareTag("EnemyHitBox") || Col.CompareTag("Projectile") || Col.CompareTag("DangerZone")) && !PlayerInvincible)
        {
            CurrentHP--;
            PlayerInvincible = true;
            Debug.Log("Player HP: " + CurrentHP);

            if(Player.GetComponent<PlayerMovement>().IsRight)
            {
                Player.transform.position = new Vector2(Player.transform.position.x-0.75f, Player.transform.position.y);
            }
            if(Player.GetComponent<PlayerMovement>().IsLeft)
            {
                Player.transform.position = new Vector2(Player.transform.position.x+0.75f, Player.transform.position.y);
            }

            StartCoroutine(InvincibileTimer());
        }
    }

    private IEnumerator InvincibileTimer()
    {
        PlayerAnim.SetTrigger("Hurt");
        yield return new WaitForSeconds(InvincibilityTime);
        PlayerInvincible = false;
    }
}
