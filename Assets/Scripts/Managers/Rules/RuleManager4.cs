using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager4 : RuleManager
{
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return false;
        
        DateTime now = DateTime.Now;
        bool isLeapYear = DateTime.IsLeapYear(now.Year);
        bool isTuesday = now.DayOfWeek == DayOfWeek.Tuesday;
        bool isAfter3pm = now.Hour > 15 || (now.Hour == 15 && now.Minute > 0);

        if (StageNum == 1)
            return !isLeapYear;

        if (StageNum == 2)
            return !isLeapYear || isTuesday;

        if (StageNum == 3)
            return !isAfter3pm && (!isLeapYear || isTuesday);

        return true;
    }
}
