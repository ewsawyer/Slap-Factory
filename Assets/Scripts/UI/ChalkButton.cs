using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChalkButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler
{
    [SerializeField] private Juice JuiceHighlight;
    [SerializeField] private Juice JuiceDehighlight;
    [SerializeField] private TextMeshProUGUI Text;

    public void OnSelect(BaseEventData eventData)
    {
        HandleHighlight();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HandleDehighlight();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleHighlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HandleDehighlight();
    }

    private void HandleHighlight()
    {
        JuiceHighlight.Play();
        Text.fontStyle |= FontStyles.Underline;
    }

    private void HandleDehighlight()
    {
        JuiceDehighlight.Play();
        Text.fontStyle &= ~FontStyles.Underline;
    }
}
