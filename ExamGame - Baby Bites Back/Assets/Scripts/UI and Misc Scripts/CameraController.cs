using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameManager GM;
    public PlayerMovement PM;
    public ZoneDetection ZoneOneDetect;
    public ZoneDetection ZoneTwoDetect;
    public ZoneDetection ZoneThreeDetect;

    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera Cam_ZoneOne;
    public CinemachineVirtualCamera Cam_ZoneTwo;
    public CinemachineVirtualCamera Cam_ZoneThree;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ZoneOneDetect.ZoneOneEntered)
        {
            MainCamera.Priority = 1;
            Cam_ZoneOne.Priority = 10;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            PM.MinMoveX = -9.5f;
            PM.MaxMoveX = 17.5f;
            GM.SpawnActivationZone1();
            GM.Zone1Panel.SetActive(true);
        }

        if(GM.Zone1Complete)
        {
            MainCamera.Priority = 10;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            ZoneOneDetect.ZoneOneEntered = false;
        }

        if(ZoneTwoDetect.ZoneTwoEntered && !GM.Zone2Complete)
        {
            MainCamera.Priority = 2;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 10;
            Cam_ZoneThree.Priority = 3;
            PM.MinMoveX = 21f;
            PM.MaxMoveX = 54f;
            GM.SpawnActivationZone2();
            GM.Zone2Panel.SetActive(true);
        }

        if(GM.Zone2Complete)
        {
            MainCamera.Priority = 10;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            ZoneTwoDetect.ZoneOneEntered = false;
        }

        if(ZoneThreeDetect.ZoneThreeEntered && !GM.Zone3Complete)
        {
            MainCamera.Priority = 3;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 10;
            PM.MinMoveX = 57.5f;
            PM.MaxMoveX = 88f;
            GM.SpawnActivationZone3();
            GM.Zone3Panel.SetActive(true);
        }

        if(GM.Zone3Complete)
        {
            MainCamera.Priority = 10;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            ZoneThreeDetect.ZoneTwoEntered = false;
        }
    }

}
