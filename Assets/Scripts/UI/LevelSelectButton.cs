using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour, IPointerClickHandler
{

    private int _levelBuildIndex;
    
    private void Awake()
    {
        _levelBuildIndex = int.Parse(name.Substring(name.LastIndexOf(' ') + 1));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.LoadLevel(_levelBuildIndex);
    }
}
