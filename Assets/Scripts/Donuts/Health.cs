using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Tooltip("Amount of hits this object can take in total")] [SerializeField]
    private int StartingHealth;

    // Commented out because all boxes have 1 hitpoint
    // [Tooltip("Juice to play on every hit")] [SerializeField]
    // private Juice HitJuice;

    [Tooltip("Juice to play when destroyed")] [SerializeField]
    private Juice DestroyJuice;

    private int _health;

    private BoxQualities _dq;
    private Collider2D _collider;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = StartingHealth;
        _dq = GetComponent<BoxQualities>();
        _collider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int dmg = 1)
    {
        _health -= dmg;
        // HitJuice.Play();
        
        // Nothing more to do if it still has health
        if (_health > 0)
            return;
        
        // Box has been destroyed
        DestroyJuice.Play();

        // Check if this box was invalid
        if (!RuleManager.Instance.IsValid(_dq))
            ScoreManager.Instance.Good();
        else
            ScoreManager.Instance.Faulty();
        
        // Turn off all the important stuff and destroy after juice is done
        StopAndHide();
        _dq.OnEndBox();
        Destroy(gameObject, 10.0f);
    }

    protected virtual void StopAndHide()
    {
        if (_dq)
            _dq.DestroyBox();
        
        _collider.enabled = false;
        this.enabled = false;
    }
}
