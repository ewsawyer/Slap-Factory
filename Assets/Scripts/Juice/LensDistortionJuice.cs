using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistortionJuice : JuiceEffect
{
    [SerializeField] private float Intensity;
    [SerializeField] private float Duration;
    [SerializeField] private bool CenterOnTransform;
    [SerializeField] private float Delay;

    [SerializeField] private AnimationCurve CurveIn;
    [SerializeField] private AnimationCurve CurveOut;
    
    private Volume _volume;
    private LensDistortion _distortion;
    private Vector2 _ogCenter;
    private float _timer;

    public void Start()
    {
        _volume = GameObject.FindGameObjectWithTag("Global Volume").GetComponent<Volume>();
        LensDistortion temp;
        if(_volume.profile.TryGet(out temp))
            _distortion = temp;
        _ogCenter = _distortion.center.value;
    }

    public override void Play()
    {
        StartCoroutine(LensDistortionCoroutine());
    }

    private IEnumerator LensDistortionCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        IsPlaying = true;
        float original = _distortion.intensity.value;

        if (CenterOnTransform)
            _distortion.center.value = Camera.main.WorldToViewportPoint(transform.position);
        
        // Ramp up
        _timer = 0.0f;
        while (_timer < Duration / 2.0f)
        {
            float t = _timer / (Duration / 2.0f);
            _distortion.intensity.value = Mathf.Lerp(original, Intensity, CurveIn.Evaluate(t));
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ramp down
        _timer = 0.0f;
        while (_timer < Duration / 2.0f)
        {
            float t = _timer / (Duration / 2.0f);
            _distortion.intensity.value = Mathf.Lerp(Intensity, original, CurveOut.Evaluate(t));
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _distortion.center.value = _ogCenter;

        _distortion.intensity.value = original;
        IsPlaying = false;
    }
}
