using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager11 : RuleManager
{
    
    public override bool IsValid(BoxQualities dq)
    {
        return StageNum == 1 || StageNum == 3 || StageNum == 4;
    }
}
