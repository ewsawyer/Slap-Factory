using UnityEngine;

public class RotationSodLerp : TransformSodLerp
{
    public override void OnDynamicsUpdate(SecondOrderDynamics sod)
    {
        Target.eulerAngles = Vector3.LerpUnclamped(StartValue, EndValue, sod.y.x);
    }
}
