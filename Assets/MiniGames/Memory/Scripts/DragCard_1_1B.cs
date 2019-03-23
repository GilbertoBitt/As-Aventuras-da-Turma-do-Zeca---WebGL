using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DragCard_1_1B : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {
	
	public Manager_1_1B manager;
	public Sprite characterSprite;
	public Text TextComponent;
	public Text RightOneTextComponent;
	public UnityEvent OnBeginDragE;
	public UnityEvent OnDragE;
	public UnityEvent OnEndDragE;

	public bool hasDroped = false;
	public DropCard_1_1B dropedCard;
	public Transform OriginalParent;
	public Vector3 pos;
	public GameObject ParticleItem;
	public CanvasGroup thisCanvasGroup;
	
	// Use this for initialization
	void Start () {
		if(manager == null){
			manager = FindObjectOfType<Manager_1_1B>();
		}

		thisCanvasGroup = GetComponent<CanvasGroup>();
		
	}
	
	// Update is called once per frame


	public void OnBeginDrag(PointerEventData eventData){
		if(manager.isPlayTime && Input.touchCount <= 1){
			ParticleItem.SetActive(true);
			OnBeginDragE.Invoke();
			thisCanvasGroup.blocksRaycasts = false;
			//manager.sound.startSoundFX(findAudio(characterSprite.name));
			if(hasDroped){
				hasDroped = false;
				dropedCard.cardDraged = null;
				this.transform.SetParent(OriginalParent);
				transform.localScale = new Vector3(1f,1f,1f);
			} else {
				pos = this.transform.position;
			}
		}
	}

	public void OnDrag(PointerEventData data){
		if(manager.isPlayTime && Input.touchCount <= 1){
			OnDragE.Invoke();
			float distance = this.transform.position.z - Camera.main.transform.position.z;
			Vector3 pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,distance);
			this.transform.position = Camera.main.ScreenToWorldPoint(pos);
		}
	}

    public void OnEndDrag(PointerEventData eventData){
			OnEndDragE.Invoke();
			thisCanvasGroup.blocksRaycasts = true;
			manager.groupLayout.enabled = false;
			manager.groupLayout.enabled = true;
			ParticleItem.SetActive(false);
    }

	public void UpdateSprite(Sprite sprite){
		characterSprite = sprite;
		TextComponent.text = sprite.name;
	}

	public void Clear(){
		dropedCard = null;
		hasDroped = false;
	}


	public AudioClip findAudio(string spriteName){
		switch (spriteName)
		{	
			case "Zeca":
			return manager.clips[10];
			case "João":
			return manager.clips[4];
			case "Paulo":
			return manager.clips[7];
			case "Ana":
			return manager.clips[0];
			case "Manu":
			return manager.clips[5];
			case "Tati":
			return manager.clips[8];
			case "Bia":
			return manager.clips[1];
			case "Tobias":
			return manager.clips[9];
			case "José":
			return manager.clips[3];
			case "Carla":
			return manager.clips[2];
			case "Juca":
			return manager.clips[6];
			default:
				return null;
		}
	}

}
