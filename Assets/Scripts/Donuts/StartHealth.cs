using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHealth : Health
{ 
    protected override void StopAndHide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
