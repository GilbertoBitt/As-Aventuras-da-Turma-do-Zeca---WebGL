using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlacesToHide_1_2A : MonoBehaviour, IPointerClickHandler {

	public Sprite Image;
	public Image imageComponent;
	public Manager_1_2A manager;
	public bool isDone = false;
	public Image imagePanel;
	public GameObject touchPrefabIcon;
	public Transform touchParent;
	public List<GameObject> touchesInstance = new List<GameObject>();
	public float smoothMovementOffset = 1f;
    public Vector3[] scales;

	// Use this for initialization
	void Start () {

		if (imageComponent == null) {
			//imageComponent = this.GetComponent<Image> ();
		}

		//imageComponent.preserveAspect = true;
		
	}

    [ContextMenu("Reorganize Scale")]
    public void ChangeScale() {

        this.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
        if(transform.GetChild(0).localScale.x > 0) {
            transform.GetChild(0).localScale = new Vector3(.7f,.7f,.7f);
        } else {
            transform.GetChild(0).localScale = new Vector3(-.7f,.7f,.7f); ;
        }
    }
	
	// Update is called once per frame

	public void UpdateImage(Sprite hideImage){
		Image = hideImage;
		ImageComponentUpdate (hideImage);
	}
		
	public void ImageComponentUpdate(Sprite hideImage){
		imageComponent.sprite = hideImage;
		//imageComponent.preserveAspect = true;
	}

	public void OnPointerClick(PointerEventData eventData){
		if(Input.touchCount <= 1 && manager.isChoosingFound == false && manager.isGameRunning == true && manager.pauseManager.isOnPause == false){
			if (Image == manager.pauloSprite) {
				if (manager.foundPaulo) {
					clicked ();
					//manager.soundManager.startSoundFX(manager.sfx[0]);
				} else {
					manager.NothingFinded ();
					//Debug.Log ("Achou nada!");
					manager.actualCombo = 1;
					TouchHandler();
				}
			} else {
				clicked ();
			}
		}

		
	}

	public void clicked(){

		if (isDone == false) {
			if (Image != null) {
				//Debug.Log (Image.name);
				//manager.amountsFinded++;
				if(manager.foundPaulo){
					manager.FoundedPaulo (Image, calculatePosition(),this.GetComponent<RectTransform>());
				} else {
					manager.Founded (Image, calculatePosition(),this.GetComponent<RectTransform>());
				}
				imagePanel.material = null;
				imageComponent.color = Color.clear;
				isDone = true;
			} else {
				manager.NothingFinded ();
				//Debug.Log ("Achou nada!");
				manager.actualCombo = 1;
				TouchHandler();
			}
		}
	}

	public Vector3 calculatePosition(){
	
		return this.transform.position;;
	}

	public void TouchHandler(){
		if(Input.touchCount > 0){
			GameObject touchIcon = Instantiate(touchPrefabIcon,touchParent) as GameObject;
			touchIcon.transform.localScale = new Vector3(1f,1f,1f);
			Vector3 screenPoint = Input.GetTouch(0).position;
			screenPoint.z = 10.0f; //distance of the plane from the camera
			touchIcon.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
			Timing.RunCoroutine(HideItem(touchIcon), Segment.SlowUpdate);
		}

	}

	public IEnumerator<float> HideItem(GameObject itemToHide){
		yield return Timing.WaitForSeconds(manager.errorTextEffectDuration);
		itemToHide.SetActive(false);
	}



}
