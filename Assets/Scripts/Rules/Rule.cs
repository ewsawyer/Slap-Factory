using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Rule
{
    [Tooltip("The bases that violate this rule")] [SerializeField]
    public Qualities.Circle[] BaseRule;

    [Tooltip("The frostings that violate this rule")] [SerializeField]
    public Qualities.Square[] FrostingRule;
    
    [Tooltip("The sprinkles that violate this rule")] [SerializeField]
    public Qualities.Triangle[] SprinklesRule;

    public bool IsValid(BoxQualities box)
    {
        bool validBase = !BaseRule.Contains(box.Circle);
        bool validFrosting = !FrostingRule.Contains(box.Square);
        bool validSprinkles = !SprinklesRule.Contains(box.Triangle);

        return validBase || validFrosting || validSprinkles;
    }
}
