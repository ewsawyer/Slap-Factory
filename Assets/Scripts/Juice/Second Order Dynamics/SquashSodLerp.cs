using UnityEngine;

public class SquashSodLerp : SodLerpBase
{

    [SerializeField] private Squash Squasher;
    
    public override void OnDynamicsUpdate(SecondOrderDynamics sod)
    {
        Squasher.SetSquash(sod.y.x);
    }
}
