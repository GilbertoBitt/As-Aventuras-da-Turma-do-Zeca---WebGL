using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BlankSpace1_4B : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public Manager1_4B manager;
	public SyllableHandler1_4B thisSyllable;
	public bool hasDrop;
	public Image thisImageComp;

	public Image traceImageComp;
	// Use this for initialization

    public void ResetToDefault() {
        transform.SetParent(manager.poolParent);
        transform.position = new Vector3(10000, 10000, 0);
        hasDrop = false;
        thisSyllable = null;
        thisImageComp.DOFade(0f, 0.1f);
        RaycastTargetUpdate(true);
    }

	void Start () {
		thisImageComp = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		/*if (thisSyllable != null && hasDrop == true) {
			if (thisSyllable.transform.parent != manager.dragParent) {
				thisSyllable = null;
				hasDrop = false;
			}
		}*/

	}

	public void OnDrop (PointerEventData eventData){
		if (eventData.pointerDrag != null) {
			//Debug.Log(eventData.pointerDrag.name);
			SyllableHandler1_4B syllable = eventData.pointerDrag.GetComponent<SyllableHandler1_4B>();
			if (syllable != null && thisSyllable == null) {
				syllable.transform.position = this.transform.position;
                //syllable.transform.SetParent(this.transform);
                thisSyllable = syllable;
				syllable.hasDroped = true;
				hasDrop = true;
				syllable.blankSpaceDroped = this;
				RaycastTargetUpdate(false);
                manager.DropCardSound();
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (eventData.pointerDrag != null) {
			//Debug.Log(eventData.pointerDrag.name);
			SyllableHandler1_4B syllable = eventData.pointerDrag.GetComponent<SyllableHandler1_4B>();
			if (syllable != null) {
				syllable.isOverBlank = true;
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		if (eventData.pointerDrag != null) {
			SyllableHandler1_4B syllable = eventData.pointerDrag.GetComponent<SyllableHandler1_4B>();
			if (syllable != null) {
				syllable.isOverBlank = false;
			}
		}
	}


	public void RaycastTargetUpdate(bool isEnable){
		thisImageComp.raycastTarget = isEnable;
	}

    public void DoFade(float _alpha) {
        thisImageComp.DOFade(_alpha, 0.3f);
    }
}
