using System.Collections;
using UnityEngine;

public class InstantiateJuice : JuiceEffect
{

    [SerializeField] private GameObject ToInstantiate;
    [SerializeField] private Transform Parent;
    [SerializeField] private float Delay;

    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);

        Instantiate(ToInstantiate);

        if (Parent)
            ToInstantiate.transform.parent = Parent;
    }
    
}
