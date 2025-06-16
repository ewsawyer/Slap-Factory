using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    
    [Tooltip("The timescale to return to after a timescale change")] [SerializeField]
    private float OriginalScale;

    public float Scale;
    public float Duration;

    private float _timer;
    public bool IsTimeScaleAltered { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ResetTimeScale();
        }
        else
            Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void ChangeTimeScale(float scale, float duration)
    {
        if (IsTimeScaleAltered && duration > Duration - _timer)
        {
            Duration = _timer + duration;
            this.Scale = scale;
        }
        else if (!IsTimeScaleAltered)
        {
            Duration = duration;
            Scale = scale;
            StartCoroutine(Slowdown());
        }
    }

    public void ChangeTimeScale(float scale)
    {
        this.Scale = scale;
        Time.timeScale = Scale;
        Time.fixedDeltaTime = 0.02f * Scale;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        IsTimeScaleAltered = false;
    }
    
    private IEnumerator Slowdown()
    {
        IsTimeScaleAltered = true;
        _timer = 0.0f;

        while (_timer < Duration)
        {
            // if (PauseManager.Paused)
            // {
            //     yield return null;
            //     continue;
            // }
            
            Time.timeScale = Scale;
            Time.fixedDeltaTime = 0.02f * Scale;
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        IsTimeScaleAltered = false;
    }

}