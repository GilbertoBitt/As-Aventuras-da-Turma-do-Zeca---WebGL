using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestHandler1_4A : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

//	[SeparatorAttribute("Referencias")]
	public Manager1_4A manager;
	//[SeparatorAttribute("Configurações")]
	public ItemType1_4A chestGroup;
	public Image imageComp;

	public Image imageTamp;
	public Image imageBau;
	public Text textComp;
	public bool isChestBonus = false;
	public bool isChestClose = false;
	public GameObject particleBonusChest;
	public RectTransform rectComp;
	// Use this for initialization
	public Sprite[] tampBauA;

	public GameObject brinqEnter;

	int numbSprB;

	public Animator jucaFecharBau;

	int numJ;

	public Animator zecaFecharBau;

	public int bauSort;

	bool checkIni;
	bool checkIni2;

 
	public void BauAbrindo1 () {
	imageTamp.sprite = tampBauA[0];	
	if(numJ==0){

	manager.checkJucaBau=false;
	jucaFecharBau.SetBool("fecharBau",manager.checkJucaBau);

	}
	numJ=numJ+1;

	}
	public void BauAbrindo2 () {
	imageTamp.sprite = tampBauA[1];	
	}
	public void BauAbrindo3 () {
	imageTamp.sprite = tampBauA[2];	
	}
	public void BauAbrindo4 () {
	imageTamp.sprite = tampBauA[3];	
	}

	public void ItemBau () {

	GetComponent<Animator>().SetBool("itemBau",false);	
	brinqEnter.GetComponent<Image>().enabled=false;
		if(imageComp.raycastTarget==false)
			GetComponent<Animator> ().enabled = true;
		
			//GetComponent<Animator> ().enabled = false;
		//StartCoroutine (TimeBau);
	}

	void Start () {
		
			checkIni2 = true;
			jucaFecharBau.enabled = false;
			zecaFecharBau.enabled = false;
			if (rectComp == null) {
				rectComp = this.GetComponent<RectTransform> ();
			}
			StartCoroutine (TimeBau ());

	}

	IEnumerator TimeBau(){
		if (checkIni == false) {
			yield return Yielders.Get(0.5f);
			GetComponent<Animator> ().enabled = true;
			checkIni = true;
		}
		yield return Yielders.Get(2.1f);
		if(imageComp.raycastTarget==false)
			GetComponent<Animator> ().enabled = true;
		
		//	GetComponent<Animator> ().enabled = false;


	

	}


	public void OnDrop (PointerEventData eventData){
		//Debug.Log ("Drop Item!");
		ItemHandler1_4A itemHandler = null;
		if (eventData.pointerDrag.gameObject != null) {
			itemHandler = eventData.pointerDrag.GetComponent<ItemHandler1_4A> ();
		}
		ItemGroup1_4A item = null;
		if (itemHandler != null && itemHandler.itemToDrag != null) {
			item = itemHandler.itemToDrag.GetComponent<ItemGroup1_4A> ();
		}
		if (manager.isDragging == true && !manager.isRemoving) {			
			//Debug.Log ("Drop Item! and is Dragging");
			if (item != null) {
                item.DisableBackgroundImage();
                itemHandler.blockDrag = true;
                item.canvasGroupComp.blocksRaycasts = false;
                //item.canvasGroupComp.interactable = false;
                //Debug.Log ("item não é nulo");
                if (item.itemInfo.itemGroup == chestGroup) {
					//item.itemInfo.itemImage
					manager.ItemGroupIsCorrect (itemHandler, isChestBonus);
					ItemGroup1_4A item2 = itemHandler.itemToDrag.GetComponent<ItemGroup1_4A> ();
					if (!item2.hasObjectOnLeft && !item2.hasObjectOnRight) {
						brinqEnter.GetComponent<Image>().enabled = true;
						brinqEnter.GetComponent<Image>().sprite = item.itemInfo.itemImage;
						GetComponent<Animator>().enabled = true;
						GetComponent<Animator>().SetBool("itemBau", true);
						//Debug.Log ("Item Organizado com Sucesso!");
						item.DisableBackgroundImage();
					} else {
						manager.ItemGroupIsWrong (itemHandler);
						//Debug.Log ("Item Organizado está no local errado!");
						item.DisableBackgroundImage();
					}
				} else {
					manager.ItemGroupIsWrong (itemHandler);
					//Debug.Log ("Item Organizado está no local errado!");
					item.DisableBackgroundImage();
				}
			}
		}

		if(item.isBonusItem){
			item.isBonusItem = false;
		}
		item.DisableBackgroundImage();
    }

	public void OnPointerClick (PointerEventData eventData){
	}

	public void OnPointerEnter (PointerEventData eventData){
		if (manager.isDragging && !manager.isRemoving) {
			//Debug.Log ("Item sobre caixa");
		}
	}

	public void OnPointerExit (PointerEventData eventData){
		if (manager.isDragging && !manager.isRemoving) {
			//ItemBau ();
		}
	}

	public void UpdateText(string _string){
		textComp.text = _string;
	}

	public void UpdateColorChest(Color _color){
		if(imageComp == null){
			imageComp = this.GetComponent<Image>();
		}
		if(rectComp == null){
			rectComp = this.GetComponent<RectTransform>();
		}
		imageComp.color = _color;
	}

	public void ToggleRaycastTarget(bool _enable){
		if(imageComp == null){
			imageComp = this.GetComponent<Image>();
		}
		if(rectComp == null){
			rectComp = this.GetComponent<RectTransform>();
		}
		imageComp.raycastTarget = _enable;	


		GetComponent<Animator>().SetBool("_checkPass",imageComp.raycastTarget);
		
		GetComponent<Animator>().SetBool("_enable",imageComp.raycastTarget);
	}

	public bool ToggleRaycastTarget(){

		return imageComp.raycastTarget;

	}

	public void ToggleBonusParticle(bool _enable){		
		particleBonusChest.SetActive(_enable);
	}
	public void CheckPassM(){
		if(GetComponent<Animator>().enabled==true)
		GetComponent<Animator>().SetBool("_checkPass",false);
	}

		
}
