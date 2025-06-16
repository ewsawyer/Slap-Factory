using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager1 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    { 
        if (dq.Circle != Circle.Red)
            return true;

        if (StageNum == 0)
            return false;

        return dq.Square == Square.Blue;
    }
}
