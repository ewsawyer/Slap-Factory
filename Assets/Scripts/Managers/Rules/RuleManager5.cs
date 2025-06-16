using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager5 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return dq.Square != Square.Red;

        if (StageNum == 1)
            return dq.Square != Square.Red || dq.Circle != Circle.Red;

        return true;
    }
}
