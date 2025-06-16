using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager15 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        return dq.Triangle != Triangle.Blue;
    }
}
