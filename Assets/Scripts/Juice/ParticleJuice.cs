using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleJuice : JuiceEffect
{
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private bool MatchParentRotation2D;
    [Tooltip("If set, the particles will attach themselves to this transform and move along with it")]
    [SerializeField] private Transform ParentTransform;
    [SerializeField] private float Delay;

    private ParticleSystem _particles;
    
    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    public override void Stop()
    {
        base.Stop();
        StopAllCoroutines();
        _particles.Stop();
    }

    private IEnumerator PlayCoroutine()
    {
        IsPlaying = true;
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);

        _particles = Instantiate(Particles);

        if (ParentTransform)
            _particles.transform.parent = ParentTransform;
        
        _particles.transform.position = transform.position;
        
        if (MatchParentRotation2D)
        {
            Vector3 parentRight = transform.parent.right;
            parentRight.z = 0;
            _particles.transform.right = parentRight;
        }
        
        IsPlaying = false;
    }
}
