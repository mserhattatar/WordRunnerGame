using Cinemachine;
using UnityEngine;


public class CineMachineManager : MonoBehaviour
{
    private float _shakeTimer;
    private bool _stopShake;
    private CinemachineBasicMultiChannelPerlin _cineMPerlin;

    public delegate void CineMachineDelegate();

    public static CineMachineDelegate CineMachineShakeDelegate;


    private void Start()
    {
        _shakeTimer = 0f;
        _cineMPerlin = gameObject.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CineMachineShakeDelegate += ShakeCamera;
    }

    private void Update()
    {
        if (!_stopShake) return;
        ResetCameraShake();
    }

    private void ShakeCamera()
    {
        _cineMPerlin.m_AmplitudeGain = 1.2f;
        _shakeTimer = 0.5f;
        _stopShake = true;
    }

    private void ResetCameraShake()
    {
        if (_shakeTimer > 0f)
            _shakeTimer -= Time.deltaTime;
        else if (_shakeTimer < 0f)
        {
            _shakeTimer = 0f;
            _cineMPerlin.m_AmplitudeGain = 0f;
            _stopShake = false;
        }
    }
}