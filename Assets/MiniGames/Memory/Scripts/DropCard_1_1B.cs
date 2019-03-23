using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MEC;

public class DropCard_1_1B : MonoBehaviour, IDropHandler, IPointerClickHandler {

	public Sprite characterSprite;
	public Manager_1_1B manager;
	public DragCard_1_1B cardDraged;
	public Vector3 positionOffset;

	public SoundManager sound;
	public List<AudioClip> clips = new List<AudioClip>();
	private Image thisImageComp;
	public Outline thisoutline;
    public CanvasGroup iconSoundCanvasGroup;

    // Use this for initialization
    void Start () {

		if(manager == null){
			manager = FindObjectOfType<Manager_1_1B>();
		}

		sound = manager.cardManager.sound;
		thisoutline = this.GetComponent<Outline>();

		thisImageComp = this.GetComponent<Image>();
//		clips = manager.cardManager.clips;
				
	}
	
	// Update is called once per frame
	
	
    public void Rescale1() {
     //   this.transform.localScale = Vector3.one;
        Debug.Log("Reset scale " + this.name, this);
    }

	public void OnDrop(PointerEventData data)
	{
		if(manager.isPlayTime){
			DragCard_1_1B cardDrag = data.pointerDrag.GetComponent<DragCard_1_1B>();
			if (cardDrag != null){
				if(cardDraged == null){
				//Debug.Log ("Dropped object was: "  + data.pointerDrag);
				cardDraged = cardDrag;
				cardDrag.hasDroped = true;
				cardDrag.dropedCard = this;
				cardDrag.gameObject.transform.SetParent(this.transform);
				cardDrag.gameObject.transform.localScale = new Vector3(1f,1f,1f);
				cardDrag.gameObject.transform.localPosition = positionOffset;
				sound.startSoundFX(manager.clips[12]);
				manager.verifyCards();				
				} else {
					manager.groupLayout.enabled = false;
					cardDrag.Clear();
					cardDrag.transform.SetParent(cardDrag.OriginalParent);
					cardDrag.transform.localScale = new Vector3(1f,1f,1f);
					manager.groupLayout.enabled = true;
				}
			}
		}
	}

	public void UpdateSprite(Sprite sprite){
		characterSprite = sprite;
		if (thisImageComp == null) {
			thisImageComp = this.GetComponent<Image>();
		}
		thisImageComp.sprite = sprite;
		Timing.RunCoroutine(TimeCards());
	}
	IEnumerator<float> TimeCards(){

		manager.zecaCard.ControlAnimCorpo.SetInteger ("posCorpoZeca",5);
		yield return Timing.WaitForSeconds(3f);
		manager.zecaCard.ControlAnimCorpo.SetInteger ("posCorpoZeca",5);
	}

	public void Clear(){
		cardDraged = null;
	}

	public void updateRightText(){
		cardDraged.RightOneTextComponent.text = this.characterSprite.name;
	}

	public void startsound(){
		if(characterSprite == manager.cardManager.spriteCards[0]){
			
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		sound.startVoiceFX(findAudio(characterSprite.name));
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
			return manager.clips[11];
			default:
				return null;
		}
	}

}

