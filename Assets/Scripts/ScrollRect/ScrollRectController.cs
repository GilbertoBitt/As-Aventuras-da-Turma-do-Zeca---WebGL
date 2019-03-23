using UnityEngine;
using UnityEngine.UI;

public class ScrollRectController : MonoBehaviour {

    public ScrollRect scrollRect;
    public bool mouseDown, buttonDown, buttonUp;
    [SerializeField] private int referenceDPI = 100;
    [SerializeField] private float referencePixelDrag = 8f;
    [SerializeField] private float dragValue;

    // Use this for initialization
    void Start () {
        scrollRect = GetComponent<ScrollRect>();        
        dragValue = Screen.dpi / referenceDPI * referencePixelDrag;
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseDown) {
            if (buttonDown) {
                ScrollDown();
            } else {
                ScrollUp();
            }
        }
		
	}

    public void ButtonDownIsPressed() {
        mouseDown = true;
        buttonDown = true;
    }

    public void ButtonUpIsPressed() {
        mouseDown = true;
        buttonUp = true;
    }

    private void ScrollDown() {
        if (Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            buttonDown = false;
        } else {
            scrollRect.horizontalNormalizedPosition -= dragValue;
        }
    }

    private void ScrollUp() {
        if (Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            buttonUp = false;
        } else {
            scrollRect.horizontalNormalizedPosition += dragValue;
        }
    }
}
