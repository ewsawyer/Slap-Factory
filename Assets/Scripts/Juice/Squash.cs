using UnityEngine;
using UnityEngine.Serialization;

public class Squash : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }
    
    [SerializeField] private Transform Target;
    [SerializeField] private Axis PrimaryAxis;
    [SerializeField] private float SquashedPrimaryScale;
    [SerializeField] private float Softness;
    
    private float _neutralPrimaryScale;
    private float[] _neutralSecondaryScales;
    
    void OnEnable()
    {
        _neutralPrimaryScale = GetPrimaryScale();
        _neutralSecondaryScales = GetSecondaryScales();
    }


    public void SetSquash(float x)
    {
        float primary = Mathf.LerpUnclamped(_neutralPrimaryScale, SquashedPrimaryScale, x);
        SetPrimaryScale(primary);
        
        float s = (1.0f - primary / _neutralPrimaryScale) * Softness + 1;
        float[] secondary = new float[] { _neutralSecondaryScales[0] * s, _neutralSecondaryScales[1] * s };
        SetSecondaryScales(secondary);
    }

    private float GetPrimaryScale()
    {
        if (PrimaryAxis == Axis.X)
            return Target.localScale.x;
        if (PrimaryAxis == Axis.Y)
            return Target.localScale.y;
        return Target.localScale.z;
    }

    private float[] GetSecondaryScales()
    {
        if (PrimaryAxis == Axis.X)
            return new float[] { Target.localScale.y, Target.localScale.z };
        if (PrimaryAxis == Axis.Y)
            return new float[] { Target.localScale.x, Target.localScale.z };
        return new float[] { Target.localScale.x, Target.localScale.y };
    }

    private void SetPrimaryScale(float scale)
    {
        Vector3 localScale = Target.localScale;
        
        if (PrimaryAxis == Axis.X)
            localScale.x = scale;
        if (PrimaryAxis == Axis.Y)
            localScale.y = scale;
        localScale.z = scale;

        Target.localScale = localScale;
    }

    private void SetSecondaryScales(float[] scales)
    {
        Vector3 localScale = Target.localScale;

        if (PrimaryAxis == Axis.X)
        {
            localScale.y = scales[0];
            localScale.z = scales[1];
        }
        if (PrimaryAxis == Axis.Y)
        {
            localScale.x = scales[0];
            localScale.z = scales[1];
        }
        else
        {
            localScale.x = scales[0];
            localScale.y = scales[1];
        }

        Target.localScale = localScale;
    }
}
