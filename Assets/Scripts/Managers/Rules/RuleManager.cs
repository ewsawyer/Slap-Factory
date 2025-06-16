using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class RuleManager : MonoBehaviour
{
    [Serializable]
    public struct Stage
    {
        public int Duration;
        public BoxSpawner Spawner;
        public List<GameObject> Rules;
        public List<GameObject> ActivateElements;
        public List<GameObject> DeactivateElements;
    }
    
    public static RuleManager Instance;

    [Tooltip("The schedule for this level")] [SerializeField]
    protected List<Stage> Stages;

    [Tooltip("The delay between handling the last box of a stage and the new rule appearing")] [SerializeField]
    private float RuleAppearanceDelay;

    private Juice _juiceNextStage;
    
    [Tooltip("(DEBUG) Changing this will allow you to start at a stage other than the first one")] [SerializeField]
    protected int StageNum;
    protected int NumScoredThisStage;
    private float _timer;
    private int _numScoredBeforeStartOfThisStage;
    
    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    protected virtual void Start()
    {
        foreach (Stage s in Stages)
        {
            foreach(GameObject go in s.ActivateElements)
                go.SetActive(false);
        }
        
        StageNum -= 1;
        NextStage();

        _juiceNextStage = GameObject.FindGameObjectWithTag("Next Stage Juice").GetComponent<Juice>();
    }

    void Update()
    {
        // Do nothing if level is not in progress
        if (GameManager.Instance.IsLevelOngoing != 1)
            return;
        
        _timer += Time.deltaTime;
        
        // If we've destroyed enough boxes to move to the next stage of the level 
        if (ScoreManager.Instance.NumScored >= Stages[StageNum].Duration + _numScoredBeforeStartOfThisStage)
        {
            if (StageNum < Stages.Count - 1)
                NextStage();
            else   // No stages left. Win the level
                GameManager.Instance.WinLevel();
        }
    }

    public int GetTotalNumberOfBoxes()
    {
        int total = 0;
        foreach (Stage s in Stages)
            total += s.Duration;

        return total;
    }

    private void NextStage()
    {
        StartCoroutine(NextStageCoroutine());
    }

    private IEnumerator NextStageCoroutine()
    {
        StageNum++;
        NumScoredThisStage = 0;
        // _timer = 0.0f;
        _numScoredBeforeStartOfThisStage = ScoreManager.Instance.NumScored;
        
        // Wait a bit so the player can recover from the last box of the stage
        yield return new WaitForSeconds(0.5f);
        
        if (StageNum > 0)
            _juiceNextStage.Play();
        
        if (StageNum > 0)
        {
            // Deactivate old rules
            foreach (GameObject go in Stages[StageNum - 1].Rules)
                go.SetActive(false);
            
            // Deactivate old spawner
            Stages[StageNum - 1].Spawner.gameObject.SetActive(false);
        }
        
        // Change spawners
        if (Stages[StageNum].Spawner)
        {
            Stages[StageNum].Spawner.gameObject.SetActive(true);
            Stages[StageNum].Spawner.NumToSpawn = Stages[StageNum].Duration;
        }
        
        // Activate new rules
        foreach (GameObject go in Stages[StageNum].Rules)
            go.SetActive(true);
        
        // Deactivate miscellaneous things
        foreach (GameObject go in Stages[StageNum].DeactivateElements)
            go.SetActive(false);
        
        // Activate miscellaneous things
        foreach (GameObject go in Stages[StageNum].ActivateElements)
            go.SetActive(true);
    }

    public abstract bool IsValid(BoxQualities box);

    public virtual void OnEndBox(BoxQualities box)
    {
        NumScoredThisStage++;
    }
}
