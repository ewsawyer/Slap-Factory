using UnityEngine;

public class RandomSodNudgeJuice : JuiceEffect
{
    [SerializeField] private SecondOrderDynamics Sod;
    [SerializeField] private float MinForce;
    [SerializeField] private float MaxForce;
    [Tooltip("If true, the z component of the nudge will be set to zero.")]
    [SerializeField] private bool Is2D;

    public override void Play()
    {
        // pick random direction
        Vector3 dir = new Vector3(
            Random.Range(-1.0f, 1.0f), 
            Random.Range(-1.0f, 1.0f), 
            Random.Range(-1.0f, 1.0f))
            .normalized;
        
        if (Is2D)
            dir.z = 0.0f;

        float force = Random.Range(MinForce, MaxForce);
        Sod.yd += force * dir;
    }
}