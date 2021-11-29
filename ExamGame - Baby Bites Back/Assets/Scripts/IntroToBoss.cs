using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroToBoss : MonoBehaviour
{
    public CinemachineVirtualCamera InitialCam;
    public CinemachineVirtualCamera BossCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.CompareTag("Player"))
        {

        }
    }
}
