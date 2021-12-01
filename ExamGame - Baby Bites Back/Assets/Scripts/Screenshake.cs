using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Screenshake : MonoBehaviour
{
    public static Screenshake Instance { get; private set; }
    private CinemachineVirtualCamera VCam;
    private float ShakeTimer;
 
    private void Awake()
    {
        Instance = this;
        VCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CinemachineBMCP = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CinemachineBMCP.m_AmplitudeGain = intensity;
        ShakeTimer = time;
    }

    private void Update()
    {
        if(ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
            if(ShakeTimer <=0)
            {
            CinemachineBasicMultiChannelPerlin CinemachineBMCP = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            CinemachineBMCP.m_AmplitudeGain = 0f;
            }
        }
    }
}
