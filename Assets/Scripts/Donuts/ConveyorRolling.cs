using UnityEngine;

public class ConveyorRolling : MonoBehaviour
{
    [SerializeField] private Transform[] Rollers;

    public float _angularSpeed;

    public void SetRollSpeed(float linearSpeed)
    {
        _angularSpeed = linearSpeed / (Mathf.PI) * 360.0f;
    }

    private void Update()
    {
        // Don't roll if the game isn't going
        if (GameManager.Instance.IsLevelOngoing != 1 && !GameManager.Instance.IsWaitingToStart)
            return;
        
        foreach (Transform t in Rollers)
            RotateRoller(t, Time.deltaTime * _angularSpeed);
    }

    private void RotateRoller(Transform roller, float dtheta)
    {
        Vector3 rotation = roller.eulerAngles;
        rotation.z -= dtheta;
        roller.eulerAngles = rotation;
    }
}
