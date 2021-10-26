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
    public float MaxUlt;
    [SerializeField] private float CurrentUlt;
    public Slider UltimateBar;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();

        UltimateBar.maxValue = MaxUlt;
        UltimateBar.value = MaxUlt;

        MinMoveX = -9.5f;
        MaxMoveX = 16.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UltimateBar.value = CurrentUlt;

        Move();

        //Attacking will be put in its own script. This is for now for testing purposes
        if(Input.GetKeyDown(KeyCode.Z) && !Attacking )
        {
            PlayerAnimator.SetBool("Attack",true);
            //AttackHitBox.SetActive(true);
            Attacking = true;
            MoveSpeed = 1.15f;
            CurrentUlt ++;
            StartCoroutine(Attack());

            //Need to add a way that the player cannot run around madly while attacking
        }

        LimitMovement();

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
            //Application.Quit();
        //}
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

        pos.x = Mathf.Clamp(pos.x, MinMoveX, MaxMoveX); //Create variables for this so can be manipulated easily 
        pos.y = Mathf.Clamp(pos.y, -9f, 2.1f);

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
        PlayerAnimator.SetBool("Attack", false);
        MoveSpeed = 4;
        Attacking = false;
    }
}
