using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameManager.Instance.LoadLevel(0);
    }
}
