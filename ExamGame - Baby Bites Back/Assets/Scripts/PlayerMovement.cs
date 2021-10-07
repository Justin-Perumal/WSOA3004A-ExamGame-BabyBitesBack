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
    private Vector3 Movement;

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

        Move();

        //Attacking will be put in its own script. This is for now for testing purposes
        if(Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimator.SetBool("Attack",true);
            //AttackHitBox.SetActive(true);
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
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Vector3 pos = transform.position + Movement * MoveSpeed * Time.fixedDeltaTime;

        pos.x = Mathf.Clamp(pos.x, -9.5f, 52f); //Create variables for this so can be manipulated easily 
        pos.y = Mathf.Clamp(pos.y, -7f, 2.1f);

        rb.MovePosition(pos);

        if(Movement.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Movement.x > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Movement.x != 0 || Movement.y != 0)
        {
            PlayerAnimator.SetTrigger("Walking");
        }
        else PlayerAnimator.ResetTrigger("Walking");
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.25f);
        AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        AttackHitBox.SetActive(false);
        PlayerAnimator.SetBool("Attack", false);
        Attacking = false;
    }
}
