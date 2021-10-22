using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameManager GM;
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
        }

        if(GM.Zone1Complete)
        {
            MainCamera.Priority = 10;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            ZoneOneDetect.ZoneOneEntered = false;
        }

        if(ZoneTwoDetect.ZoneTwoEntered)
        {
            MainCamera.Priority = 2;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 10;
            Cam_ZoneThree.Priority = 3;
        }

        if(GM.Zone2Complete)
        {
            MainCamera.Priority = 10;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 3;
            ZoneTwoDetect.ZoneOneEntered = false;
        }

        if(ZoneThreeDetect.ZoneThreeEntered)
        {
            MainCamera.Priority = 3;
            Cam_ZoneOne.Priority = 1;
            Cam_ZoneTwo.Priority = 2;
            Cam_ZoneThree.Priority = 10;
        }
    }

}
