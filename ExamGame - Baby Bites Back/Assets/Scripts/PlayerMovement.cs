using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;

    [Header("Movement")]
    public float MoveSpeed;
    private float Horizontal;
    private float Vertical;

    [Header("Animations")]
    public Animator PlayerAnimator;

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
        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();

        UltimateBar.maxValue = MaxUlt;
        UltimateBar.value = MaxUlt;
    }

    // Update is called once per frame
    void Update()
    {
        UltimateBar.value = CurrentUlt;

        if (!Attacking)
        {
            Move();
        }

        //Attacking will be put in its own script. This is for now for testing purposes
        if(Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimator.SetBool("Attack",true);
            AttackHitBox.SetActive(true);
            Attacking = true;
            CurrentUlt ++;
            StartCoroutine(Attack());

            //Need to add a way that the player cannot run around madly while attacking
        }

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
            //Application.Quit();
        //}
    }

    public void Move()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(Horizontal * MoveSpeed, Vertical * MoveSpeed, 0.0f);
        transform.position = transform.position + Movement * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Horizontal != 0 || Vertical != 0)
        {
            PlayerAnimator.SetTrigger("Walking");
        }
        else PlayerAnimator.ResetTrigger("Walking");
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        AttackHitBox.SetActive(false);
        PlayerAnimator.SetBool("Attack", false);
        Attacking = false;
    }
}
