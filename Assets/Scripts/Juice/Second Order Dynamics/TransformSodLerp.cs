using UnityEngine;
using UnityEngine.Serialization;

public abstract class TransformSodLerp : SodLerpBase
{
    [SerializeField] protected Transform Target;
    [FormerlySerializedAs("StartPosition")] [SerializeField] protected Vector3 StartValue;
    [FormerlySerializedAs("EndPosition")] [SerializeField] protected Vector3 EndValue;

    public abstract override void OnDynamicsUpdate(SecondOrderDynamics sod);
}
