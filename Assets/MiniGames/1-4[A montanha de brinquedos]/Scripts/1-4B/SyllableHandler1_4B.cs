using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SyllableHandler1_4B : OverridableMonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler {

	Outline outlineComp;
	public CanvasGroup thisCanvasGroup;
	Vector3 startDragPos;
	public Manager1_4B manager;
	public Text textComp;
	public string syllable;
	public bool isOverBlank = false;
	public bool hasDroped = false;
	public bool isBeenDrag = false;
	public BlankSpace1_4B blankSpaceDroped;
	public Image imageComp;
	private float zValue;

    public void ResetToDefault() {
        thisCanvasGroup.alpha = 0;
        textComp.text = "";
        syllable = "";
        isOverBlank = false;
        hasDroped = false;
        isBeenDrag = false;
        blankSpaceDroped = null;
        transform.SetParent(manager.poolParent);
        transform.position = new Vector3(5000, 5000, 0);
    }
	// Use this for initialization
	void Start () {
		if (outlineComp == null) {
			outlineComp = this.GetComponent<Outline>();
		}

		if (thisCanvasGroup == null) {
			thisCanvasGroup = this.GetComponent<CanvasGroup>();
		}

		if (imageComp == null) {
			imageComp = this.GetComponent<Image>();
		}
	}

    public override void UpdateMe() {
        if (blankSpaceDroped == null && !isBeenDrag && (hasDroped || isOverBlank)) {
            thisCanvasGroup.blocksRaycasts = true;
            manager.isDragging = false;
            hasDroped = false;
            isOverBlank = false;
            this.transform.SetParent(manager.syllablesParent);
        }
    }

    public void OnBeginDrag (PointerEventData eventData){
		if(manager.isPlaying && !manager.isDragging){
			thisCanvasGroup.blocksRaycasts = false;
			manager.isDragging = true;
			if (hasDroped && blankSpaceDroped != null) {				
				blankSpaceDroped.RaycastTargetUpdate(true);
				blankSpaceDroped.hasDrop = false;
				blankSpaceDroped.thisSyllable = null;
				blankSpaceDroped = null;
			}
			hasDroped = false;
			zValue = this.transform.position.z;
			this.transform.SetParent(manager.dragParent);
			isBeenDrag = true;
		}
	}

	public void OnDrag (PointerEventData eventData){
		if (manager.isPlaying && manager.isDragging) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(thisCanvasGroup.transform as RectTransform, Input.mousePosition, Camera.main, out pos);
			transform.position = thisCanvasGroup.transform.TransformPoint(pos);
			/*Vector3 pos = Input.mousePosition;
			pos.z = zValue;*/
			//this.transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag (PointerEventData eventData){
		thisCanvasGroup.blocksRaycasts = true;
		manager.isDragging = false;
		if (!isOverBlank) {
			this.transform.SetParent(manager.syllablesParent);
		}
		isBeenDrag = false;
	}

	public void OnPointerEnter(PointerEventData eventData){
		//outlineComp.enabled = true;
	}

	public void OnPointerExit(PointerEventData eventData){
		//outlineComp.enabled = false;
	}

	public void UpdateTextContent(){
		textComp.text = syllable;
	}

    public void DoFade(float alpha) {
        thisCanvasGroup.DOFade(alpha, 0.3f);
    }
}
