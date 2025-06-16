using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    [FormerlySerializedAs("BaseColors")] [Tooltip("The colors to use for the different bases, in the same order as the enum")] [SerializeField]
    private Color[] CircleColors;
    [FormerlySerializedAs("FrostingColors")] [Tooltip("The colors to use for the different frostings, in the same order as the enum")] [SerializeField]
    private Color[] SquareColors;
    [FormerlySerializedAs("SprinkleColors")] [Tooltip("The colors to use for the different sprinkles, in the same order as the enum")] [SerializeField]
    private Color[] TriangleColors;

    void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(this);
    }
    
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    
    public Color GetCircleColor(Qualities.Circle c)
    {
        return CircleColors[(int)c];
    }
    
    public Color GetSquareColor(Qualities.Square s)
    {
        return SquareColors[(int)s];
    }

    public Color GetTriangleColor(Qualities.Triangle t)
    {
        return TriangleColors[(int)t];
    }

}
