using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;

public class RuleManager8 : RuleManager
{

    private int _numGreen;
    private int _numYellow;
    
    public override bool IsValid(BoxQualities dq)
    {
        if (dq.Triangle == Triangle.Green)
            return _numGreen % 2 != 1;

        if (dq.Triangle == Triangle.Yellow)
            return _numYellow % 3 != 2;
            
        return true;
    }

    public override void OnEndBox(BoxQualities dq)
    {
        base.OnEndBox(dq);
        
        if (dq.Triangle == Triangle.Green)
            _numGreen++;

        if (dq.Triangle == Triangle.Yellow)
            _numYellow++;
    }
}
