using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager20 : RuleManager
{
    [SerializeField] private TextMeshProUGUI Xpm;
    [SerializeField] private TextMeshProUGUI Yam;
    [SerializeField] private TextMeshProUGUI Oddness;

    private int _x;
    private int _y;
    private bool _oddness;

    protected override void Start()
    {
        base.Start();
        _x = Random.Range(1, 12);
        _y = Random.Range(5, 12);
        _oddness = Random.Range(0, 2) == 1;

        Xpm.text = _x + "PM";
        Yam.text = _y + "AM";
        Oddness.text = (_oddness ? "odd" : "even") + "-numbered";
    }
    
    public override bool IsValid(BoxQualities dq)
    {
        DateTime now = DateTime.Now;
        bool isAfterX = now.Hour > _x + 12 || (now.Hour == _x + 12 && now.Minute > 0);
        bool isBeforeY = now.Hour < _y;
        bool isOnDay = _oddness == (now.Day % 2 == 1);

        bool shouldDestroyRedCircle = dq.Circle == Circle.Red && ((isAfterX || isBeforeY) && isOnDay); 

        if (StageNum == 0)
            return !shouldDestroyRedCircle;

        if (StageNum == 1)
            return !shouldDestroyRedCircle || dq.Triangle == Triangle.Yellow;

        if (StageNum == 2)
            return (!shouldDestroyRedCircle || dq.Triangle == Triangle.Yellow) && dq.Square != Square.Blue &&
                   dq.Triangle != Triangle.Green && dq.Square != Square.Red && dq.Circle != Circle.Yellow;

        return true;
    }
}
