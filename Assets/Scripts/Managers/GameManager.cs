using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int IsLevelOngoing;
    public string MyNameIs;

    [Tooltip("The starting level. Useful for debugging")] [SerializeField]
    private int StartingLevel;
    
    public int _level;
    private GameObject _winUI;
    private GameObject _loseUI;
    public bool IsWaitingToStart { get; private set; }

    void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        // _level = StartingLevel;
        IsWaitingToStart = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void SetIsLevelOngoing(int x)
    {
        IsLevelOngoing = x;
    }

    public void HandleSlap()
    {
        if (IsWaitingToStart)
            StartLevel();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Tab))
        //     WinLevel();
        
        // Restart level
        // if (Input.GetKeyDown(KeyCode.R))
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // Quit
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadLevel(0);
    }

    public void StartLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        
        IsWaitingToStart = false;
        IsLevelOngoing = 1;
        GameObject.FindGameObjectWithTag("Slap to Start").SetActive(false);
    }

    public int Level()
    {
        return _level;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        LoadLevel(1);
        yield break;
    }

    public void WinLevel()
    {
        if (IsLevelOngoing == 1)
            StartCoroutine(WinLevelCoroutine());
    }

    public void LoseLevel()
    {
        if (IsLevelOngoing != 1)
            return;

        SetIsLevelOngoing(0);
        _loseUI = GameObject.FindFirstObjectByType<LoseScreen>(FindObjectsInactive.Include).gameObject;
        _loseUI.SetActive(true);
    }

    public void RetryLevel()
    {
        StartCoroutine(RetryLevelCoroutine());
    }

    private IEnumerator RetryLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        IsWaitingToStart = true;
    }

    public void ReturnToMenu()
    {
        print("ReturnToMenu()");
    }

    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        _level = buildIndex;

        if (buildIndex != 0)
            IsWaitingToStart = true;
    }

    private IEnumerator WinLevelCoroutine()
    {
        SetIsLevelOngoing(0);
        RuleManager.Instance.gameObject.SetActive(false);
        _winUI = GameObject.FindFirstObjectByType<WinScreen>(FindObjectsInactive.Include).gameObject;
        _winUI.SetActive(true);
        yield break;
    }

    public void NextLevel()
    {
        _level++;
        SceneManager.LoadScene(_level);
        IsWaitingToStart = true;
    }
}
