using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public bool isMainCam;

    CinemachineFreeLook mainCam;
    CinemachineVirtualCamera lockOnCam;

    float shakeTimer;

    private void Awake()
    {
        if (isMainCam)
        {
            mainCam = GetComponent<CinemachineFreeLook>();
        }
        else
        {
            lockOnCam = GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void Shake(float intensity, float duration)
    {
        if(!isMainCam)
        {
            CinemachineBasicMultiChannelPerlin noise = lockOnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = intensity;
            shakeTimer = duration;
        }
        else
        {
            mainCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
            mainCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
            mainCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
            shakeTimer = duration;
        }
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                if (!isMainCam)
                {
                    CinemachineBasicMultiChannelPerlin noise = lockOnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    noise.m_AmplitudeGain = 0;
                }
                else
                {
                    mainCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                    mainCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                    mainCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                }
            }
        }
    }
}
