using UnityEngine;

public abstract class SodLerpBase : MonoBehaviour, IDynamicsListener
{
    [SerializeField] private SecondOrderDynamics Sod;
    
    
    private void OnEnable()
    {
        Sod.AddListener(this);
    }

    private void OnDisable()
    {
        Sod.RemoveListener(this);
    }

    public abstract void OnDynamicsUpdate(SecondOrderDynamics sod);
}
