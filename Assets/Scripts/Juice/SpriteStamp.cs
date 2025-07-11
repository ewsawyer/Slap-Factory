using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteStamp : JuiceEffect
{
    [Tooltip("The sprite to leave behind after the shot was made")] [SerializeField]
    private GameObject Stamp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Play()
    {
        IsPlaying = true;
        GameObject stamp = Instantiate(Stamp);
        stamp.transform.position = transform.position;
        IsPlaying = false;
    }
}
