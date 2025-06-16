using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EyeTracking : MonoBehaviour
{
    [Tooltip("The force to accelerate the eyes by")] [SerializeField]
    private float EyeMovementForce;

    [Tooltip("The size of the maximum displacement from the eyes' original position")] [SerializeField]
    private float MaxDisplacement;

    [Tooltip("The eyes to move around while tracking")] [SerializeField]
    private Transform Eyes;

    [Tooltip("The speed by which to move the eyes towards the max displacement")] [SerializeField]
    private float Speed;
    
    private Vector2 _velocity;
    private Vector2 _originalPos;
    private Vector2 _displacement;
    private Vector2 _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = Eyes.transform.position;
        _target = _originalPos;
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsTarget();
    }

    public void SetTarget(Transform t)
    {
        _target = t.position;
    }

    public void ReturnToNeutral()
    {
        _target = _originalPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _target = other.transform.position;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _target = other.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ReturnToNeutral();
    }

    private void LookTowardsTarget()
    {
        if (((Vector2)Eyes.position - _target).magnitude < 0.025f)
        {
            Eyes.position = _target;
            return;
        }

        Vector2 dirToTarget = (_target - (Vector2)Eyes.position).normalized;
        Vector2 displacement = (Vector2)Eyes.position - _originalPos;
        displacement += (Speed * Time.deltaTime * dirToTarget);
        displacement = Vector2.ClampMagnitude(displacement, MaxDisplacement);
        Eyes.position = _originalPos + displacement;
    }
}
