using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxValidator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxQualities bq = other.GetComponent<BoxQualities>();
        if (!bq)
            return;

        bq.WasValidWhenPassingPlayer = RuleManager.Instance.IsValid(bq);
    }
}
