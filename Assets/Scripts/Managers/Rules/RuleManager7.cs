using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager7 : RuleManager
{

    
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return NumScoredThisStage % 2 != 1;
        
        if (StageNum == 1)
            return NumScoredThisStage % 3 != 2;
        
        if (StageNum == 2)
            return NumScoredThisStage % 4 != 3;
        
        if (StageNum == 3)
            return NumScoredThisStage % 6 != 5;

        return true;
    }
}
