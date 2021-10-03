using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float MoveSpeed;

    private float Horizontal;
    private float Vertical;

    public Animator PlayerAnimator;

    public GameObject AttackHitBox;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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


        //Attacking will be put in its own script. This is for now for testing purposes
        if(Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimator.SetBool("Attack",true);
            AttackHitBox.SetActive(true);
            StartCoroutine(Attack());

            //Need to add a way that the player cannot run around madly while attacking
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        AttackHitBox.SetActive(false);
        PlayerAnimator.SetBool("Attack", false);
    }
}
