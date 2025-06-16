using UnityEngine;

public class MistakeLight : MonoBehaviour
{
    public bool IsUp { get; private set; }

    [SerializeField] private Juice GoUpJuice;
    [SerializeField] private Juice GoDownJuice;
    
    public void SetIsUp(bool up)
    {
        IsUp = up;
        
        if (up)
            GoUpJuice.Play();
        else
            GoDownJuice.Play();
    }
}
