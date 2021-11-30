using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroToBoss : MonoBehaviour
{
    public PlayerMovement PM;
    public PhaseManager PhaseMan;

    public GameObject IntroBrockSpawn;
    public GameObject IntroTed;

    public GameObject Mom;
    public GameObject MomBarrage;

    public CinemachineVirtualCamera InitialCam;
    public CinemachineVirtualCamera BossAppearsCam;
    public CinemachineVirtualCamera BossCam;

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IntroBrockSpawn == null && IntroTed == null)
        {
            PM.MaxMoveX = 14f;
        }
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {
            BossAppearsCam.Priority = 15;
            PM.MinMoveX = -24f;
            Mom.SetActive(true);
            StartCoroutine(MomAppears());
        }
    }

    public IEnumerator MomAppears()
    {
        Mom.GetComponent<Animator>().SetBool("Intro", true);
        yield return new WaitForSeconds(2.3f);
        BossCam.Priority = 25;
        Instantiate(MomBarrage, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Laser?");
        yield return new WaitForSeconds(5f);
        Mom.GetComponent<Animator>().SetBool("Intro", false);
        PhaseMan.GetComponent<PhaseManager>().enabled = true;
        Destroy(gameObject);
    }
}
