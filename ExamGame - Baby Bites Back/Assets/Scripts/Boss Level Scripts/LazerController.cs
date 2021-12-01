using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public PlayerMovement PM;
    public float MoveSpeed;
    private Transform PlayerPoint;
    public Rigidbody2D rb;

    public Vector3 RandomLazer;
    public int RandomDistanceX;
    public int RandomDistanceY;
    // Start is called before the first frame update
    void Start()
    {
        RandomDistanceX = Random.Range(-5,6);
        RandomDistanceY = Random.Range(-5,6);
        RandomLazer = new Vector3(RandomDistanceX, RandomDistanceY, 0);

        PlayerPoint = GameObject.Find("Player").transform;
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Vector3 dir = PlayerPoint.position - transform.position;
        dir = PlayerPoint.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if(PM.IsLeft)
        {
            transform.eulerAngles = new Vector3(0f,0f,angle+90f);
        }
        if(PM.IsRight)
        {
            transform.eulerAngles = new Vector3(0f,0f,angle+45f);
        }

        rb.velocity = (PlayerPoint.transform.position - transform.position + RandomLazer) * MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, PlayerPoint.position, MoveSpeed*Time.deltaTime);
    }

    public IEnumerator LazerTimeout()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
