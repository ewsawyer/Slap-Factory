using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class ButtonMashing : MonoBehaviour
{
    [FormerlySerializedAs("SlapNothingJuice")] [Tooltip("The juice to play when the player slaps the table")] [SerializeField]
    private Juice SlapJuice;

    [Tooltip("The juice to play when the player's arm goes back up")] [SerializeField]
    private Juice ArmUpJuice;

    [Tooltip("The sprite to show when the arm is up")] [SerializeField]
    private GameObject ArmUpSprite;

    [Tooltip("The sprite to show when the arm is down")] [SerializeField]
    private GameObject ArmDownSprite;
    
    private BoxCollider2D _collider;
    private bool _hitSpace;
    private List<Health> _targets;
    private bool _isArmDown;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _targets = new List<Health>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        //     Slap();
        // if (Input.GetKeyUp(KeyCode.Space))
        //     ArmUp();
    }

    public void HandleSlapInput(InputAction.CallbackContext context)
    {
        if (!_isArmDown && context.performed)
            Slap();
    }

    public void HandleArmUpInput(InputAction.CallbackContext context)
    {
        if (_isArmDown && context.performed)
            ArmUp();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        UpdateTarget(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (!h)
            return;
        
        if (_targets.Contains(h))
            _targets.Remove(h);
    }

    private void UpdateTarget(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (!h)
            return;

        _targets.Add(h);
    }

    private void Slap()
    {
        _isArmDown = true;
        // Add some stuff here for mashing with no target
        ArmUpSprite.SetActive(false);
        ArmDownSprite.SetActive(true);
        SlapJuice.Play();
        
        if (_targets.Count == 0)
            return;
        
        GetClosestTarget().TakeDamage();
    }

    private Health GetClosestTarget()
    {
        float minDist = float.MaxValue;
        Health closest = null;
        
        for (int i = 0; i < _targets.Count; i++)
        {
            Health h = _targets[i];
            float dist = Vector2.Distance(transform.position, h.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = h;
            }
        }

        return closest;
    }

    private void ArmUp()
    {
        ArmUpSprite.SetActive(true);
        ArmDownSprite.SetActive(false);
        ArmUpJuice.Play();
        _isArmDown = false;
    }
}
