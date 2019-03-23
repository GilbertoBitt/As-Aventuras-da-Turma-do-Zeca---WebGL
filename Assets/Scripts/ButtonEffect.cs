using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private Button _button;

    public void Start() {
        _button = GetComponent(typeof(Button)) as Button;
    }

    public void ButtonDown(){

        if (_button.IsInteractable()) {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
	}

	public void ButtonUp(){
        if (_button.IsInteractable()) {
            this.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        }
	}

	public void OnPointerUp(PointerEventData eventData){
        if (_button.IsInteractable()) {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
	}
	public void OnPointerDown(PointerEventData eventData){
        if (_button.IsInteractable()) {
            this.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        }
	}
}
