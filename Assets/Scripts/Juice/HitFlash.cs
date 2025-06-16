using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : JuiceEffect
{
    [Tooltip("The sprite renderers to change the color of")] [SerializeField]
    private SpriteRenderer[] Targets;
    
    [Tooltip("Color to flash to when hit")] [SerializeField]
    private Color FlashColor;

    [Tooltip("Number of times to flash the desired color")] [SerializeField]
    private int NumFlashes;
    
    [Tooltip("Total duration of the effect")]
    [SerializeField] private float Duration;
    
    private Color[] _originalColors;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalColors = new Color[Targets.Length];
    }
    
    public override void Play()
    {
        // Save original colors
        for (int i = 0; i < Targets.Length; i++)
            _originalColors[i] = Targets[i].color;
        
        // Start all targets flashing
        if (Targets.Length > 0)
            for (int i = 0; i < Targets.Length; i++)
                StartCoroutine(FlashCoroutine(i));
    }

    public override void Stop()
    {
        base.Stop();
        StopAllCoroutines();

        // Reset colors back to their original
        for (int i = 0; i < Targets.Length; i++)
            Targets[i].color = _originalColors[i];
    }
    
    private IEnumerator FlashCoroutine(int index)
    {
        IsPlaying = true;
        
        SpriteRenderer target = Targets[index];
        Color originalColor = _originalColors[index];
        
        for (int i = 0; i < NumFlashes; i++)
        {
            target.color = FlashColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
            
            target.color = originalColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
        }
        
        target.color = originalColor;

        IsPlaying = false;
    }
}
