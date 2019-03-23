using System.Collections.Generic;
using UnityEngine;

public class ShowTouches1_2A : MonoBehaviour {

	public GameObject touchPrefabIcon;
	public Transform touchParent;
	public List<GameObject> touchesInstance = new List<GameObject>();
	public float smoothMovementOffset = 1f;

	// Use this for initialization
	
	
	// Update is called once per frame
	/*void Update () {

		if(Input.touchCount > 0){
			if(touchesInstance.Count >= Input.touchCount){
				for (int i = 0; i < Input.touchCount; i++){
					if(!touchesInstance[i].activeInHierarchy){
						touchesInstance[i].SetActive(true);
					}
					//touchesInstance[i].transform.position = Input.GetTouch(i).position;
					Vector3 screenPoint = Input.GetTouch(i).position;
					screenPoint.z = 10.0f; //distance of the plane from the camera
					touchesInstance[i].transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
				}
			} else if(touchesInstance.Count < Input.touchCount){
				GameObject touchIcon = Instantiate(touchPrefabIcon,touchParent) as GameObject;
				touchesInstance.Add(touchIcon);
				touchIcon.transform.localScale = new Vector3(1f,1f,1f);
				return;
			}
		} else {
			for (int i = 0; i < touchesInstance.Count; i++){
				if(touchesInstance[i].activeInHierarchy){
					touchesInstance[i].SetActive(false);
				}
			}
		}
		
	}*/

}
