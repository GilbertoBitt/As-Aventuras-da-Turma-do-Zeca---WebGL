using UnityEngine;
using UnityEngine.EventSystems;

public class characterSelect :  MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

	private Vector2 startPoint;
	private Vector2 endPoint;
	private float delta;
	public gameSelection gSelection;
	public int characterID;

	public void ButtonDown(){
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	public void ButtonUp(){
		this.transform.localScale = new Vector3 (0.95f, 0.95f, 0.95f);
	}

	public void OnPointerUp(PointerEventData eventData){
		this.transform.localScale = new Vector3 (0.95f, 0.95f, 0.95f);
		endPoint = Input.mousePosition;
		delta = Vector2.Distance (startPoint, endPoint);
		if (delta < 0.1f) {
			//gSelection.gameConfigs.allMinigames [gSelection.selectedMinigame].OldCharacterSelected = characterID;
			gSelection.selectedChars = characterID;
			//Debug.Log ("Character Selected" + characterID);
		}
	}
	public void OnPointerDown(PointerEventData eventData){
		startPoint = Input.mousePosition;
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

}
