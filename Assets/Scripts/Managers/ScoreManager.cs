using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Tooltip("The lights that will turn on to indicate that the player got something wrong")] [SerializeField]
    private MistakeLight[] Lights;

    [Tooltip("The color to turn the lights when a faulty product passes through")] [SerializeField]
    private Color FaultyColor;

    [Tooltip("The juice to play when the player makes a correct choice")] [SerializeField]
    private Juice GoodJuice;
    
    [Tooltip("The juice to play when the player makes an incorrect choice")] [SerializeField]
    private Juice FaultyJuice;

    public int Lives;
    public int NumScored;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public virtual void Good()
    {
        ScoreBox();
        GoodJuice.Play();
    }

    public virtual void Faulty()
    {
        ScoreBox();
        FaultyJuice.Play();
        
        if (Lives == 1)
            GameManager.Instance.LoseLevel();
        
        int index = Lights.Length - Lives;
        
        if (index < 0 || index > Lights.Length)
            return;
        
        Lights[index].SetIsUp(true);
        Lives--;
    }

    private void ScoreBox()
    {
        NumScored++;
    }

}
