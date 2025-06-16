using UnityEngine;

public class PositionSod : SecondOrderDynamics
{
    [SerializeField] private Transform ObjectToMove;
    [SerializeField] private bool SetLocalPosition;
    
    public override Vector3 UpdateDynamics(float T, Vector3 x, Vector3 xd)
    {
        base.UpdateDynamics(T, x, xd);
        
        if (SetLocalPosition)
            ObjectToMove.localPosition = y;
        else
            ObjectToMove.position = y;

        return y;
    }
}
