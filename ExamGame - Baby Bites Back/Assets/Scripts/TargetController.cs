using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public PhaseManager PM;
    private CircleCollider2D TargetCol;
    public bool DangerReady;
    public ParticleSystem Danger;
    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();

        DangerReady = false;
        TargetCol = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine(DangerDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(DangerReady)
        {
            Danger.Play();
            TargetCol.enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(DangerZoneDestruction());
        }
    }

    public IEnumerator DangerDelay()
    {
        yield return new WaitForSeconds(2f);
        DangerReady = true;
    }

    public IEnumerator DangerZoneDestruction()
    {
        yield return new WaitForSeconds(2f);
        PM.BarrageComplete = true;
        Destroy(gameObject);
    }
}
