using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventJuice : JuiceEffect
{
    [Tooltip("Delays the invocation of the event by this much")] [SerializeField]
    private float Delay;
    
    [Tooltip("The unity event to invoke when this juice plays")] [SerializeField]
    private UnityEvent Event;
    
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
        
        if (Delay > 0.0f)
            yield return new WaitForSeconds(Delay);
        
        Event.Invoke();

        IsPlaying = false;
    }
}
