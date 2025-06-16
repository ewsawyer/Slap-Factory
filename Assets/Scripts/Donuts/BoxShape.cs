using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BoxShape : MonoBehaviour
{

    [SerializeField] private Vector2 ForceDir;
    [SerializeField] private float MinForce;
    [SerializeField] private float MaxForce;
    [SerializeField] private float Torque;
    [SerializeField] private Juice JuiceCollide;
    [SerializeField] private Juice JuiceBreakOff;
    
    private Rigidbody2D _rb;
    private Collider2D _collider;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void BreakOff()
    {
        // Separate from box
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _collider.enabled = true;
        transform.parent = null;
        // Apply forces
        Vector2 f = Random.Range(MinForce, MaxForce) * ForceDir.normalized;
        _rb.AddForce(f, ForceMode2D.Impulse);
        _rb.AddTorque(Torque);
        // Juice
        JuiceBreakOff.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // JuiceCollide.Play();
    }
}
