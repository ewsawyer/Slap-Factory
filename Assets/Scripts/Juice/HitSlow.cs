using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlow : JuiceEffect
{
    public override bool IsPlaying => TimeManager.Instance.IsTimeScaleAltered;

    [Tooltip("Amount of time to slow down for")] [SerializeField]
    public float Duration;

    [Tooltip("Scale of time during slowdown")] [SerializeField]
    private float Scale;

    private float _timer;
    
    public override void Play()
    {
        TimeManager.Instance.ChangeTimeScale(Scale, Duration);
    }

    public override void Stop()
    {
        base.Stop();
        TimeManager.Instance.ResetTimeScale();
    }
}