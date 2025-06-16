using System.Collections;
using UnityEngine;

public class SquashStretchJuice : JuiceEffect
{
    public enum SquashMode
    {
        Absolute,
        Relative
    }

    public enum SquashAxis
    {
        X,
        Y
    }

    [Tooltip("The transform to apply the squashing/stretching to")] [SerializeField]
    private Transform Target;
    
    [Tooltip("The value that determines how much squash/stretch is happening. Positive for stretch, negative for squash")] [SerializeField]
    private float Scale;

    [Tooltip("Absolute will stretch until reaching the scale value. Relative adds to the scale of the object when played")] [SerializeField]
    private SquashMode Mode;

    [Tooltip("Select the axis to change the scale along")] [SerializeField]
    private SquashAxis Axis;

    [Tooltip("How much time to spend going from the original scale to the final scale")] [SerializeField]
    private float DurationOutward;

    [Tooltip("The animation curve that controls the speed of squashing/stretching to the final scale")] [SerializeField]
    private AnimationCurve CurveOutward;

    [Tooltip("How much time to spend going from the final scale to the original scale")] [SerializeField]
    private float DurationReturn;

    [Tooltip("The animation curve that controls the speed of squashing/stretching back to the original scale")] [SerializeField]
    private AnimationCurve CurveReturn;

    private float _ogPrimaryScale;
    private float _ogSecondaryScale;

    private void Start()
    {
        _ogPrimaryScale = PrimaryAxisScale();
        _ogSecondaryScale = SecondaryAxisScale();
    }

    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    public override void Stop()
    {
        base.Stop();
        StopAllCoroutines();
    }

    private IEnumerator PlayCoroutine()
    {
        IsPlaying = true;
        
        // Get start and end scales
        float startScale = PrimaryAxisScale();
        float endScale = Scale;
        if (Mode == SquashMode.Relative)
            endScale += startScale;

        // Do squash and return to original scale
        yield return StartCoroutine(SquashCoroutine(startScale, endScale, DurationOutward, CurveOutward));
        yield return StartCoroutine(SquashCoroutine(endScale, _ogPrimaryScale, DurationReturn, CurveReturn));
        Target.transform.localScale = VectorizeScale(_ogPrimaryScale, _ogSecondaryScale);

        IsPlaying = false;
    }
    
    private IEnumerator SquashCoroutine(float startScale, float endScale, float duration, AnimationCurve curve)
    {
        // Get starting secondary scale
        float scaleSec = SecondaryAxisScale();
        // Get end scale of primary axis
        
        // Interpolate between start and end scales
        float timer = 0.0f;
        while (timer < DurationOutward)
        {
            // Get the current scale based on the time and animation curve
            float t = CurveOutward.Evaluate(timer / DurationOutward);
            float scaleCurPri = Mathf.Lerp(startScale, endScale, t);

            // Get the current value of scale along secondary axis
            float squashFactor = startScale / scaleCurPri;
            float scaleCurSec = squashFactor * scaleSec;

            // Get the current scale as a vector
            Vector2 finalScale = VectorizeScale(scaleCurPri, scaleCurSec);

            // Apply scale to target
            Target.transform.localScale = finalScale;
            
            // Repeat process on next frame
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private Vector2 VectorizeScale(float primary, float secondary)
    {
        // Get the scale as a vector
        Vector2 scale = new Vector2(primary, secondary);
        if (Axis == SquashAxis.Y)
            scale = new Vector2(secondary, primary);

        return scale;
    }
    
    // private IEnumerator SquashCoroutine(float startScale, float endScale, float duration, AnimationCurve curve)
    // {
    //     // Get starting secondary scale
    //     float scaleSec = SecondaryAxisScale();
    //     // Get end scale of primary axis
    //     
    //     // Interpolate between start and end scales
    //     float timer = 0.0f;
    //     while (timer < DurationOutward)
    //     {
    //         // Get the current scale based on the time and animation curve
    //         float t = CurveOutward.Evaluate(timer / DurationOutward);
    //         float scaleCurPri = Mathf.Lerp(startScale, endScale, t);
    //
    //         // Get the current value of scale along secondary axis
    //         float squashFactor = startScale / scaleCurPri;
    //         float scaleCurSec = squashFactor * scaleSec;
    //
    //         // Get the current scale as a vector
    //         Vector2 finalScale = new Vector2(scaleCurPri, scaleCurSec);
    //         if (Axis == SquashAxis.Y)
    //             finalScale = new Vector2(scaleCurSec, scaleCurPri);
    //
    //         // Apply scale to target
    //         Target.transform.localScale = finalScale;
    //         
    //         // Repeat process on next frame
    //         timer += Time.deltaTime;
    //         yield return null;
    //     }
    // }

    private float PrimaryAxisScale()
    {
        return Axis == SquashAxis.X ? Target.transform.localScale.x : Target.transform.localScale.y;
    }

    private float SecondaryAxisScale()
    {
       return Axis == SquashAxis.X ? Target.transform.localScale.y : Target.transform.localScale.x; 
    }

    public static void InstantSquash(float scale, Transform target, SquashAxis axis, SquashMode mode = SquashMode.Absolute)
    {
        // Get the current values of the scale along primary and secondary axes
        float scalePri = axis == SquashAxis.X ? target.localScale.x : target.localScale.y;
        float scaleSec = axis == SquashAxis.X ? target.localScale.y : target.localScale.x;

        // Get final value of scale along primary axis
        float scaleFinalPri = scale;
        if (mode == SquashMode.Relative)
            scaleFinalPri += scalePri;

        // Get the final value of scale along secondary axis
        float squashFactor = scalePri / scaleFinalPri;
        float scaleFinalSec = squashFactor * scaleSec;

        // Get final scale as a vector
        Vector2 finalScale = new Vector2(scaleFinalPri, scaleFinalSec);
        if (axis == SquashAxis.Y)
            finalScale = new Vector2(scaleFinalSec, scaleFinalPri);

        target.transform.localScale = finalScale;
    }
    
    
}
