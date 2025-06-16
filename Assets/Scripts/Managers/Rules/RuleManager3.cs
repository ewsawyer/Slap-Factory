using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager3 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0 || StageNum == 4)
            return dq.Circle != Circle.Red && dq.Square != Square.Blue;

        if (StageNum == 1)
            return dq.Square != Square.Blue;

        if (StageNum == 2)
            return true;

        if (StageNum == 3)
            return dq.Circle != Circle.Red;
        
        return true;
    }
}
