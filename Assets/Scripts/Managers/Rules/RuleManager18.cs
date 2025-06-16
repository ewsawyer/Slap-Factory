using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager18 : RuleManager
{

    [SerializeField] private TextMeshProUGUI Rule1;
    [SerializeField] private TextMeshProUGUI Rule2;
    
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return !Rule1.text.Contains(dq.LeftChar);
        if (StageNum == 1)
            return !Rule1.text.Contains(dq.LeftChar) && !Rule2.text.Contains(dq.LeftChar);

        return true;
    }
}
