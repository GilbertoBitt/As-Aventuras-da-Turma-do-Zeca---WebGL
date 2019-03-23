using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MEC;

public class backgroundImage1_2A : MonoBehaviour, IPointerClickHandler {

	public Manager_1_2A manager;

	public GameObject touchPrefabIcon;
	public Transform touchParent;
	public List<GameObject> touchesInstance = new List<GameObject>();
    public Queue<GameObject> poolTouch = new Queue<GameObject>();
    public int sizeOfPool;
	public float smoothMovementOffset = 1f;

    // Use this for initialization

    private void Start() {
        for (int i = 0; i < sizeOfPool; i++) {
            GameObject touchIcon = Instantiate(touchPrefabIcon, touchParent) as GameObject;
            poolTouch.Enqueue(touchIcon);
            touchIcon.SetActive(false);
        }
    }


    public void OnPointerClick(PointerEventData eventData){
		if(Input.touchCount <= 1 && manager.isGameRunning && !eventData.dragging){
			manager.NothingFinded ();
			manager.actualCombo = 1;
			TouchHandler();
		}

	}

	public void TouchHandler(){
        if (poolTouch.Count >= 1) {
            GameObject touchIcon = poolTouch.Dequeue();
            if (touchIcon != null) {
                touchIcon.SetActive(true);
                touchIcon.transform.localScale = new Vector3(1f, 1f, 1f);
                Vector3 screenPoint = Input.mousePosition;
                screenPoint.z = 10.0f; //distance of the plane from the camera
                touchIcon.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
                Timing.RunCoroutine(HideItem(touchIcon));
            }
        }
	}

	public IEnumerator<float> HideItem(GameObject itemToHide){
		yield return Timing.WaitForSeconds(manager.errorTextEffectDuration);
        poolTouch.Enqueue(itemToHide);
		itemToHide.SetActive(false);
	}


}
