using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager19 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        string currentDay = DateTime.Now.DayOfWeek.ToString().ToLower();
        if (StageNum == 0)
            return !currentDay.Contains(dq.LeftChar);
        
        if (StageNum == 1)
            return !currentDay.Contains(dq.LeftChar) || "ethan sawyer".Contains(dq.LeftChar);

        return true;
    }
}
