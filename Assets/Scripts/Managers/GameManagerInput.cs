using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerInput : MonoBehaviour
{
    public void OnSlap(InputAction.CallbackContext context)
    {
        if (context.performed)
            GameManager.Instance.HandleSlap();
    }
}
