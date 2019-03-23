using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ItemHandler1_4A : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

//	[SeparatorAttribute("Referencias")]
	public Manager1_4A gameManager;
	public List<GameObject> itemsOnSlot = new List<GameObject> ();
	//[SeparatorAttribute("Variaveis de Controle")]
	public int slotCount;
	private bool isBeenDrag = false;
	public bool hasEnded = false;
	public int maxBadItems = 0;
	public int amountBadItems = 0;
	public List<ItemHandler1_4A> handlersOfTheFloor = new List<ItemHandler1_4A> ();
	public bool isRed = false;
	[HideInInspector]
	public GameObject itemToDrag;
	private Vector3 startPosition;
	private Vector3 startScale;
	[HideInInspector] 
	public List<ItemGroup1_4A> itensGroup = new List<ItemGroup1_4A>();
//	[SeparatorAttribute("Configuração para Reconstrução")]
	public List<ItemHistory1_4A> originalItems = new List<ItemHistory1_4A>();
	public List<ItemHandler1_4A> handlersOfTheFloorHistory = new List<ItemHandler1_4A> ();
	public int startMaxBadItems = 0;
	public Floor1_4A floorParent;
	public int sortingOrder;	
	private int sortingOrderStart;
    public bool blockDrag = false;

	// Use this for initialization
	public void Start () {
		//itemsOnSlot.Reverse ();
		slotCount = itemsOnSlot.Count;
	}
	
	// Update is called once per frame
	/*void Update () {
		//BugFixRed a Nonred
		if (itemToDrag.GetComponent<Image> ().color == Color.red && isRed == false) {
			removeRedItem ();
		}
	
	}*/

	public void removeRedItem(){
		itemToDrag.GetComponent<ItemGroup1_4A> ().UpdateImage (Color.white);
		itemToDrag.GetComponent<ItemGroup1_4A> ().toggleLock(false);
	}

	public void removeAddItem(bool _enable){
		itemToDrag.GetComponent<ItemGroup1_4A> ().toggleLock(_enable);
	}

	#region DragAndDrop

	public void OnBeginDrag (PointerEventData eventData){
		int temp = slotCount - 1;
		if (blockDrag == false && gameManager.isDragging == false && isBeenDrag == false && !gameManager.isRemoving && isRed == false) {
			UpdateDragItem ();
			gameManager.checkItensPos();
			itemToDrag.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			startPosition = itemToDrag.transform.position;
			startScale = itemToDrag.transform.localScale;

			gameManager.isDragging = true;
			isBeenDrag = true;			

			itemToDrag.transform.SetParent (gameManager.dragParent);
			itemToDrag.transform.localScale = startScale;
			itemToDrag.GetComponent<ItemGroup1_4A> ().isBeenDrag = true;
			itemToDrag.GetComponent<ItemGroup1_4A> ().DisableBackgroundImage();
			sortingOrderStart = itemToDrag.GetComponent<Canvas>().sortingOrder;
			itemToDrag.GetComponent<Canvas>().sortingOrder = 7;
		}
	}
		
	public void OnDrag (PointerEventData eventData){
		if (gameManager.isDragging == true && isBeenDrag == true && !gameManager.isRemoving && isRed == false) {
			/*float distance = this.transform.position.z - Camera.main.transform.position.z;
			Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,distance);*/
			//itemToDrag.transform.position = Camera.main.ScreenToWorldPoint(pos);
			Vector3 pos = Input.mousePosition;
			pos.z = 10f;
			itemToDrag.transform.position =  Camera.main.ScreenToWorldPoint(pos);
			itemToDrag.GetComponent<ItemGroup1_4A> ().isBeenDrag = true;
		}
	}

	public void OnEndDrag (PointerEventData eventData){
		if (itemToDrag != null) {
			itemToDrag.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			itemToDrag.transform.SetParent (this.transform);
			itemToDrag.transform.position = startPosition;
			itemToDrag.transform.localScale = startScale;
			isBeenDrag = false;
			gameManager.isDragging = false;
			itemToDrag.GetComponent<ItemGroup1_4A> ().isBeenDrag = false;
			itemToDrag.GetComponent<ItemGroup1_4A> ().EnableBackgroundImage();
			itemToDrag.GetComponent<Canvas>().sortingOrder = sortingOrderStart;
		}
	}


	#endregion

	public void AddHandler(ItemHandler1_4A _Handler){
		handlersOfTheFloor.Add (_Handler);
	}

	public void RemoveHandler(ItemHandler1_4A _Handler){
		handlersOfTheFloor.Remove (_Handler);
	}

	/// <summary>
	/// Updates the item information and Update his sprite icon/Image.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="spriteImage">Sprite image.</param>
	public void UpdateItemInfo(int id,Item1_4A itemInfo){
		if (id <= itemsOnSlot.Count) {
			itemsOnSlot [id].GetComponent<ItemGroup1_4A> ().itemInfo = itemInfo;
			itemsOnSlot [id].GetComponent<ItemGroup1_4A> ().UpdateImage ();
		}
	}

	public void UpdateItemInfo(int id,Item1_4A itemInfo, float alphaImage){
		if (id <= itemsOnSlot.Count) {
			itemsOnSlot [id].GetComponent<ItemGroup1_4A> ().itemInfo = itemInfo;
			itemsOnSlot [id].GetComponent<ItemGroup1_4A> ().UpdateImage (alphaImage);
		}
	}

	public void ActiveCollider2D(){
		if (itemToDrag != null) {
			itemToDrag.GetComponent<ItemGroup1_4A> ().toggleCollider (true);
		}
	}

	public void deactiveCollider2D(){
		if (itemToDrag != null) {
			itemToDrag.GetComponent<ItemGroup1_4A> ().toggleCollider (false);
		}
	}

	public void removeItemInFront(bool _isRight){
		int temp = slotCount - 1;
        if (itemsOnSlot[temp] != null) {
            GameObject item = itemsOnSlot[temp];
            itemsOnSlot[temp].GetComponent<ItemGroup1_4A>().DisableBackgroundImage();
            itemsOnSlot.RemoveAt(temp);
            if (_isRight) {
                item.SetActive(false);
            } else {
                gameManager.ThrowItemAway(item.transform);
            }
            slotCount--;
        }
	}

	public void UpdateDragItem(){
		if (itemsOnSlot.Count > 0) {
			slotCount = itemsOnSlot.Count;
			itemToDrag = itemsOnSlot [slotCount - 1];
            //blockDrag = false;
        }
	}

	public void UpdateStartList(){
		for (int i = 0; i < itemsOnSlot.Count; i++) {
			ItemHistory1_4A item = new ItemHistory1_4A ();
			item.itemComp = itemsOnSlot [i];
			item.sibblingIndex = itemsOnSlot [i].transform.GetSiblingIndex ();
			originalItems.Add (item);
		}
	}

	public void disableAllColliders2D(){
		for (int i = 0; i < itemsOnSlot.Count; i++) {
			itemsOnSlot [i].GetComponent<ItemGroup1_4A> ().toggleCollider (false);
		}
	}

	public void disableAllLocks(){
		for (int i = 0; i < itemsOnSlot.Count; i++) {
			itemsOnSlot[i].GetComponent<ItemGroup1_4A>().toggleLock(false);
		}
	}

	public void ResetItemsPos(){
		itemsOnSlot.Clear ();
		for (int i = 0; i < originalItems.Count; i++) {
			itemsOnSlot.Add (originalItems [i].itemComp);
			originalItems [i].itemComp.transform.SetParent (this.transform);
			originalItems [i].itemComp.transform.SetSiblingIndex (originalItems [i].sibblingIndex);
			originalItems [i].itemComp.SetActive (true);
        }
        blockDrag = false;
		slotCount = itemsOnSlot.Count;
		handlersOfTheFloor = handlersOfTheFloorHistory;
		isRed = false;
		amountBadItems = 0;
		maxBadItems = startMaxBadItems;
		hasEnded = false;
		UpdateDragItem ();
		disableAllLocks();
		disableAllColliders2D ();
		ActiveCollider2D ();
		removeRedItem ();
	}

	public void StartMaxBadItemsUpdate(){
		startMaxBadItems = maxBadItems;
	}

	public void UpdateItemHandlerOnChilds(){
		for (int i = 0; i < itemsOnSlot.Count; i++) {
			itemsOnSlot [i].GetComponent<ItemGroup1_4A> ().UpdateItemHandler (this);
		}
	}

	public void ResetBonusItems(){
		for (int i = 0; i < itemsOnSlot.Count; i++){
			itemsOnSlot[i].GetComponent<ItemGroup1_4A>().toggleBonusItem(false);
		}
	}

	public void UpdateItemHandlersHistory(){
		//handlersOfTheFloorHistory = handlersOfTheFloor;
		handlersOfTheFloorHistory = handlersOfTheFloor.ToList();
		/*for (int i = 0; i < handlersOfTheFloor.Count; i++) {
			ItemHandler1_4A _item = new ItemHandler1_4A ();
			_item = handlersOfTheFloor [i];
			handlersOfTheFloor.Add (_item);
		}*/
	}

	public void UpdateBadItemsLimit(){
		maxBadItems = returnMaxBadItems ();
	}

	public int returnMaxBadItems(){
		return handlersOfTheFloor.Count;	
	}

	public void updateOtherHandlers(){
		for (int i = 0; i < handlersOfTheFloorHistory.Count; i++) {
			handlersOfTheFloorHistory [i].amountBadItems = amountBadItems;
			handlersOfTheFloorHistory [i].maxBadItems = maxBadItems;
		}
	}

	public void RemoveThisHandlerFromAll(ItemHandler1_4A _handler){
		for (int i = 0; i < this.handlersOfTheFloor.Count; i++) {
			this.handlersOfTheFloor[i].removeIfExist (_handler);
		}
	}

	public void removeIfExist(ItemHandler1_4A _handler){
		if (this.handlersOfTheFloor.Contains (_handler)) {
			this.handlersOfTheFloor.Remove (_handler);
		}
	}

	public ItemGroup1_4A ItemBonusHere(){
		if(itemToDrag != null){
			itemToDrag.GetComponent<ItemGroup1_4A>().isBonusItem = true;
			//itemToDrag.GetComponent<ItemGroup1_4A>().UpdateColor(Color.blue);
			return itemToDrag.GetComponent<ItemGroup1_4A>();
		} else {
			//Debug.Log("ERRO");
			return null;
		}
	}

	public void checkDragItem(){
		itemToDrag.GetComponent<ItemGroup1_4A>().startDragging();
	}

	public void redItem(){
		StartCoroutine(CheckIfRedsGone());
	}

	public IEnumerator CheckIfRedsGone(){
		yield return new WaitUntil (() => handlersOfTheFloor.All (x => x.hasEnded == true));
		isRed = false;
		removeRedItem ();
		yield return Yielders.Get(1f);
		removeRedItem ();
	}

	
}

[System.Serializable]
public class ItemHistory1_4A {

	public GameObject itemComp;
	public int sibblingIndex;
	
}
