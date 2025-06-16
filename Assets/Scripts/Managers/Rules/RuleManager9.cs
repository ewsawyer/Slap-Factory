using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager9 : RuleManager
{
    
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return dq.Circle != Circle.Red;

        if (StageNum == 1)
            return dq.Circle != Circle.Red || dq.Square == Square.Blue;

        return true;
    }
}
