using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollButton : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    private ScrollRectController scrollRectController;

    [SerializeField]
    private bool isDownButton;

    public void OnPointerDown(PointerEventData eventData) {
        if (isDownButton) {
            scrollRectController.ButtonDownIsPressed();
        } else {
            scrollRectController.ButtonUpIsPressed();
        }
    }
}
