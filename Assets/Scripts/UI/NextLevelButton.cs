using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
}
