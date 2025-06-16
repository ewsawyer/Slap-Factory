using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager10 : RuleManager
{
    
    public override bool IsValid(BoxQualities dq)
    {
        bool redAndBlue = dq.Circle == Circle.Red && dq.Square == Square.Blue;
        bool greenTri = dq.Triangle == Triangle.Green;
        
        if (StageNum == 0)
            return !redAndBlue || greenTri;

        DateTime now = DateTime.Now;
        bool is5pmOrLater = now.Hour >= 17;
        bool oddDate = now.Day % 2 == 1;

        if (StageNum == 1)
            return !redAndBlue || (greenTri && !is5pmOrLater && !oddDate);

        return true;
    }
}
