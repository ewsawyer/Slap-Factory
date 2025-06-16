using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JuiceEffect : MonoBehaviour
{
    public virtual bool IsPlaying { get; protected set; }
    
    public abstract void Play();

    // Not all JuiceEffects need to implement this
    public virtual void Stop()
    {
        IsPlaying = false;
    }
}
