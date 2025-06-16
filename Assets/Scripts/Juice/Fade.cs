using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Fade : JuiceEffect
{
    [SerializeField] protected SpriteRenderer[] Targets;
    
    [SerializeField] private bool PlayOnStart;
    [SerializeField] private float Delay;
    [SerializeField] private bool DestroyAfterFade;
    [SerializeField] private float Duration;
    [SerializeField] public Color TargetColor;

    private float _timer;
    private float _currentDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayOnStart)
            Play();
    }

    public override void Play()
    {
        _currentDelay = Delay;
        StartCoroutine(FadeCoroutine());
    }

    public void SkipDelay(bool skip)
    {
        _currentDelay = skip ? 0.0f : Delay;
    }
    
    private IEnumerator FadeCoroutine()
    {
        IsPlaying = true;
        if (_currentDelay > 0.0f)
            yield return new WaitForSecondsRealtime(_currentDelay);
        
        _timer = 0.0f;
        
        // Save the starting colors
        Color[] startColors = new Color[Targets.Length];
        for (int i = 0; i < Targets.Length; i++)
            startColors[i] = Targets[i].color;

        // Fade between the start and target colors
        while (_timer < Duration)
        {
            for (int i = 0; i < Targets.Length; i++)
                Targets[i].color = Color.Lerp(startColors[i], TargetColor, _timer / Duration);
            
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Set the color equal to the final one
        foreach (SpriteRenderer t in Targets)
        {
            t.color = TargetColor;
            yield return null;
        }
        
        if (DestroyAfterFade)
            Destroy(gameObject);

        IsPlaying = false;
    }
}