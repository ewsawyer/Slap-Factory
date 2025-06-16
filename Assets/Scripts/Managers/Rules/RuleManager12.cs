using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager12 : RuleManager
{
    
    public override bool IsValid(BoxQualities dq)
    {
        // Poetry in motion
        // Couldn't have written this if it weren't for Mr. Turner 6 years ago
        
        // Create a bitmask for all the tests of certain aspects of the box
        int qualityMask =
            (dq.Circle == Circle.Red ? 1 : 0)
            | (dq.Square == Square.Blue ? 1 : 0) << 1
            | (dq.Triangle == Triangle.Blue ? 1 : 0) << 2
            | (dq.Square == Square.Yellow ? 1 : 0) << 3
            | (dq.Circle == Circle.Green ? 1 : 0) << 4
            | (dq.Circle == Circle.Blue ? 1 : 0) << 5
            | (dq.Triangle == Triangle.Red ? 1 : 0) << 6;

        // Create a bitmask that is filled right to left with as many 1's as the stage number we're on
        int stageMask = 0;
        for (int i = 0; i < StageNum; i++)
            stageMask += (int)Mathf.Pow(2, i);
        
        // print(Convert.ToString(qualityMask, toBase: 2));
        // print(Convert.ToString(stageMask, toBase: 2));

        return (qualityMask & stageMask) > 0;
    }
}
