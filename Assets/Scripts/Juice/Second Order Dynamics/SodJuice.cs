using UnityEngine;

public class SodJuice : JuiceEffect
{
    [SerializeField] private SecondOrderDynamics Sod;
    [Header("Nudge")]
    [SerializeField] private Vector3 NudgeForce;
    [SerializeField] private bool IgnoreNudge;
    [Header("Input")]
    [SerializeField] private Vector3 X;
    [SerializeField] private bool IgnoreX;

    public override void Play()
    {
        if (!IgnoreNudge)
            Sod.yd += NudgeForce;
        if (!IgnoreX)
            Sod.X = X;
    }
}
