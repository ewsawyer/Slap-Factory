using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : JuiceEffect
{
    [Tooltip("The amplitude of the camera shake")] [SerializeField]
    private float Amplitude;

    [Tooltip("Delays the shake by this many seconds")] [SerializeField]
    private float Delay;

    private PositionShaker _shaker;
    
    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    public override void Stop()
    {
        base.Stop();
        _shaker.Stop();
    }

    private IEnumerator PlayCoroutine()
    {
        IsPlaying = true;
        _shaker = Camera.main.GetComponent<PositionShaker>();
        _shaker.Amplitude = Amplitude;

        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        if (_shaker.IsShaking())
            _shaker.ReplenishShakeTime();
        else
            _shaker.Play();

        IsPlaying = false;
    }
}
