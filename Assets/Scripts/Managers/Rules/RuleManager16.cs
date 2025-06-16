using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager16 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return dq.LeftChar < dq.RightChar;

        if (StageNum == 1)
            return dq.LeftChar < dq.RightChar || dq.LeftChar == dq.RightChar + 1;

        return true;
    }
}
