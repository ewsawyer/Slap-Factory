using System;
using UnityEngine;

public class PositionSodLerp : TransformSodLerp
{
    [SerializeField] private bool UseWorldPosition;
    
    public override void OnDynamicsUpdate(SecondOrderDynamics sod)
    {
        // Lerp to find the new position based on the output from the SOD
        float val = sod.y.x;
        Vector3 pos = Vector3.LerpUnclamped(StartValue, EndValue, val);
       
        // Set the position
        if (UseWorldPosition)
            Target.position = pos;
        else
            Target.localPosition = pos;
    }
}
