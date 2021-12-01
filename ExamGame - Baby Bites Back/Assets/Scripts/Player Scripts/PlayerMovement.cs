using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("External Scripts")]
    public GameManager GM;

    [Header("Components")]
    public Rigidbody2D rb;

    [Header("Movement")]
    public bool PlayerIsAsleep;
    public string CurrentLevel;
    public float MoveSpeed;
    private Vector3 Movement;
    public bool IsLeft;
    public bool IsRight;
    [SerializeField] public float MinMoveX;
    [SerializeField] private float MinMoveY;
    [SerializeField] public float MaxMoveX;
    [SerializeField] private float MaxMoveY;

    [Header("Animations")]
    public Animator PlayerAnimator;
    [SerializeField]private float AttackTimer;
    [SerializeField]private float HitBoxActivationTimer;

    [Header("Attacking stuff")] //Will be moved to its own script later on
    public GameObject AttackHitBox;
    private bool Attacking = false;
    private bool HammerAttack = false;
    private bool QuickAttack = false;
    public float HammerCooldown;
    public float HammerTimer;
    public Image HammerImage;
    public bool HammerReady = true;
    [SerializeField] private bool UltimateReady = false;
    public bool UltimateInUse = false;
    public float MaxUlt;
    public GameObject UltimateAttack;
    public GameObject UltimateReadyUI;
    [SerializeField] public float CurrentUlt;
    public Slider UltimateBar;

    public static PlayerMovement instance;

    [Header("Audio Stuff")]
    public AudioSource AudioSrc;
    public AudioClip UltClip;
    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        PlayerIsAsleep = false;

        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();

        UltimateBar.maxValue = MaxUlt;
        UltimateBar.value = MaxUlt;

        if(CurrentLevel == "Tutorial")
        {
            CurrentUlt = MaxUlt;
        }

        //Sets the bounds for the first level
        if(CurrentLevel == "Level 1")
        {
            MinMoveX = -9.5f;
            MaxMoveX = 16.5f;

            MinMoveY = -9f;
            MaxMoveY = 2.1f;
        }
        else if(CurrentLevel == "BossLevel")
        {
            MinMoveX = -52f;
            MaxMoveX = -12f;
            
            MinMoveY = -14f;
            MaxMoveY = -0.3f;
        }
        else if(CurrentLevel == "Tutorial")
        {
            MinMoveX = -9.5f;
            MaxMoveX = 120;

            MinMoveY = -9f;
            MaxMoveY = 2.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UltimateBar.value = CurrentUlt;

        if(CurrentUlt >= MaxUlt)
        {
            UltimateReady = true;
            if(CurrentLevel != "Tutorial")
            {
                UltimateReadyUI.SetActive(true);
            }
        }

        if(UltimateReady && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.V)))
        {
            PlayerAnimator.SetTrigger("Ultimate");
            //AudioSrc.PlayOneShot(UltClip);
            if(CurrentLevel != "Tutorial")
            {
                UltimateReady = false;
            }
            UltimateReadyUI.SetActive(false);
            GameObject UltimateRing = Instantiate(UltimateAttack, transform.position, Quaternion.identity);
            StartCoroutine(FreezeEnemies());
            CurrentUlt = 0;
        }

        if(!PlayerIsAsleep)
        {
            Move();

            //Attacking will be put in its own script. This is for now for testing purposes
            if((Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1)) && !Attacking && HammerReady && !QuickAttack)
            {
                PlayerAnimator.SetBool("Attack",true);
                //AttackHitBox.SetActive(true);
                Attacking = true;
                HammerAttack = true;
                MoveSpeed = 1.25f;
                StartCoroutine(Attack());

                HammerTimer = 0f;
                HammerReady = false;

                //Need to add a way that the player cannot run around madly while attacking
            }

            if(HammerTimer < HammerCooldown)
            {
                HammerImage.fillAmount = HammerTimer/HammerCooldown;
                HammerTimer += Time.deltaTime;
            }
            else if(HammerTimer >= HammerCooldown)
            {
                HammerReady = true;
            }

            if((Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)) && !Attacking && !HammerAttack)
            {
                PlayerAnimator.SetBool("Q_Attack", true);
                QuickAttack = true;
                Attacking = true;
                StartCoroutine(Q_Attack());
            }
        }

        if(CurrentLevel == "Level 1")
        {
            LimitMovement();
        }
    }

    public void LimitMovement()
    {
        if(GM.Zone1Complete)
        {
            MinMoveX = -9.5f;
            MaxMoveX = 54f;
        }

        if(GM.Zone2Complete)
        {
            MinMoveX = -9.5f;
            MaxMoveX = 85f;
        }

        if(GM.Zone3Complete)
        {
            MinMoveX = -9.5f;
            MaxMoveX = 120f;
        }
    }

    public void Move()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Vector3 pos = transform.position + Movement * MoveSpeed * Time.fixedDeltaTime;

        pos.x = Mathf.Clamp(pos.x, MinMoveX, MaxMoveX);
        pos.y = Mathf.Clamp(pos.y, MinMoveY, MaxMoveY);

        rb.MovePosition(pos);

        if(Movement.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
            IsLeft = true;
            IsRight = false;
        }

        if(Movement.x > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);
            IsLeft = false;
            IsRight = true;
        }

        if(Movement.x != 0 || Movement.y != 0)
        {
            PlayerAnimator.SetTrigger("Walking");
        }
        else PlayerAnimator.ResetTrigger("Walking");
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(HitBoxActivationTimer);
        //AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(AttackTimer - HitBoxActivationTimer);
        //AttackHitBox.SetActive(false);
        HammerAttack = false;
        PlayerAnimator.SetBool("Attack", false);
        MoveSpeed = 5.5f;
        Attacking = false;
    }

    private IEnumerator Q_Attack()
    {
        yield return new WaitForSeconds(HitBoxActivationTimer);
        //AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(AttackTimer - HitBoxActivationTimer);
        //AttackHitBox.SetActive(false);
        QuickAttack = false;
        PlayerAnimator.SetBool("Q_Attack", false);
        //MoveSpeed = 5.5f;
        Attacking = false;
    }

    private IEnumerator FreezeEnemies()
    {
        UltimateInUse = true;
        yield return new WaitForSeconds(4f);
        UltimateInUse = false;
    }
}
