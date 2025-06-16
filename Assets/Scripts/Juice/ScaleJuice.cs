using System.Collections;
using UnityEngine;

public class ScaleJuice : JuiceEffect
{
    public enum Style
    {
        Offset,
        Absolute,
        ToTargetScale
    }

    [SerializeField] private Transform ObjectToScale;
    [SerializeField] private Transform Target;
    [SerializeField] private float Delay;
    [SerializeField] private float Duration;
    [SerializeField] private Vector3 ScaleTo;
    [SerializeField] private Style ScaleStyle;
    [SerializeField] private AnimationCurve Curve;

    private float _timer;
    
    public override void Play()
    {
        StartCoroutine(ScaleCoroutine());
    }

    public override void Stop()
    {
        base.Stop();
        StopAllCoroutines();
    }

    public void PlayReversed()
    {
        StartCoroutine(ScaleCoroutine(true));
    }

    public float GetDelay()
    {
        return Delay;
    }

    public float GetDuration()
    {
        return Duration;
    }

    public void SetObjectToScale(Transform t)
    {
        ObjectToScale = t;
    }
    public void SetTarget(Transform t)
    {
        Target = t;
    }
    
    public void SetScaleTo(Vector3 v)
    {
        ScaleTo = v;
    }

    public IEnumerator ScaleCoroutine(bool reversed=false)
    {
        IsPlaying = true;
        
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);

        _timer = Duration;
        
        Vector3 startScale = ObjectToScale.transform.localScale;
        Vector3 endScale = reversed ? -ScaleTo : ScaleTo;
        if (ScaleStyle == Style.Offset)
            endScale = startScale + endScale;
        
        while (_timer > 0.0f)
        {
            Vector3 scale;
            if (ScaleStyle == Style.ToTargetScale)
            {
                startScale = ObjectToScale.localScale;
                endScale = Target.localScale;
                Vector3 dir = (endScale - startScale).normalized;
                float dist = Vector3.Distance(startScale, endScale);
                float remainingTime = Duration - _timer;
                scale = startScale + (dist / remainingTime) * Time.deltaTime * dir;
            }
            else
            {
                scale = Vector3.Lerp(startScale, endScale, Curve.Evaluate(1.0f - _timer / Duration));
            }
            
            ObjectToScale.transform.localScale = scale;
            
            _timer -= Time.deltaTime;
            yield return null;
        }

        ObjectToScale.transform.localScale = endScale;

        IsPlaying = false;
    }
}
