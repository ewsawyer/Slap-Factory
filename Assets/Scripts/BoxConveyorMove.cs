using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxConveyorMove : MonoBehaviour
{
    [Tooltip("Speed that the box will move")] [SerializeField]
    public float Speed;

    [Tooltip("If true, this object will move regardless of whether the level is in progress")] [SerializeField]
    private bool ShouldAlwaysMove;
    
    // Update is called once per frame
    void Update()
    {
        // Don't move if the level's over
        if (GameManager.Instance.IsLevelOngoing != 1 && !ShouldAlwaysMove)
            return;
        
        Vector2 pos = transform.position;
        pos.x += Speed * Time.deltaTime;
        transform.position = pos;
    }
}
