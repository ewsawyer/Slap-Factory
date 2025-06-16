using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager2 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        bool yellowCircle = dq.Circle == Circle.Yellow;
        bool greenSquare = dq.Square == Square.Green;

        if (StageNum == 0)
            return yellowCircle || greenSquare;
        
        return (yellowCircle || greenSquare) && !(yellowCircle && greenSquare);
    }
}
