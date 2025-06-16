using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfConveyor : MonoBehaviour
{
    [Tooltip("The color to turn the lights when a good product passes through")] [SerializeField]
    private Color GoodColor;

    [Tooltip("The juice to play when a good box is scanned")] [SerializeField]
    private Juice JuiceGoodScan;

    [Tooltip("The juice to play when a faulty box is scanned")] [SerializeField]
    private Juice JuiceFaultyScan;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxQualities dq = other.GetComponent<BoxQualities>();
        if (!dq)
            return;

        if (dq.WasValidWhenPassingPlayer)
            Good();
        else
            Faulty();
        
        dq.OnEndBox();
        Destroy(dq.gameObject);
    }

    private void Good()
    {
        JuiceGoodScan.Play();
        ScoreManager.Instance.Good();
    }

    private void Faulty()
    {
        JuiceFaultyScan.Play();
        ScoreManager.Instance.Faulty();
    }
}
