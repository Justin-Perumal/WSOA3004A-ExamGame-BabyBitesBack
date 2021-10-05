using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("Objects and Components")]
    public GameObject HealthImage;

    [Header("Variables")]
    public float MaxPlayerHP;
    [SerializeField] private float CurrentHP;

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
        CurrentHP = MaxPlayerHP;
        HealthBar.maxValue = MaxPlayerHP;
        HealthBar.value = MaxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        //HealthImage.GetComponent<Image>().fillAmount = CurrentHP/MaxPlayerHP;

        HealthBar.value = CurrentHP;

        if(CurrentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("EnemyHitBox"))
        {
            CurrentHP--;
            Debug.Log("Player HP: " + CurrentHP);
        }
    }
}
