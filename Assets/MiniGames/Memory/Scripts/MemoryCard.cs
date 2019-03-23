using UnityEngine;
using UnityEngine.UI;
using MEC;

/// <summary>
/// Memory Card Class.
/// </summary>

[System.SerializableAttribute]
public class MemoryCard : MonoBehaviour {

	public Sprite cardSprite;
	public string cardName;
	public MemoryGameManager manager;	
	public Text TextName;
	public Image imageComponent;
	public bool isFlipped = false;
	public GameObject partic;

	public void flipCard(){
		//Flip Card to face screen.
		int touchCount = Input.touchCount;
		if(touchCount <= 1 && manager.isGameEnded == false){
			if(manager.canFlipped == true && isFlipped == false){
				isFlipped = true;
				//StartCoroutine(manager.FlipAThisCard(this, this.GetComponent<Image>()));
				Timing.RunCoroutine(manager.FlipAThisCard(this, this.GetComponent<Image>()));
			}
		} else {
			//Debug.Log("No Multitouch Allowed or game ended");
		}
	}

	public MemoryCard(){	
	}

	public MemoryCard(Sprite spriteCard,MemoryGameManager gameManager){		
			cardSprite = spriteCard;
			manager = gameManager;					
	}

	public void updateSprite(bool IsFaceUP){
		if(IsFaceUP){
			if(imageComponent != null)
			this.imageComponent.sprite = this.cardSprite;
			SetName(this.cardSprite.name);
			if(TextName != null)			
			this.TextName.gameObject.SetActive(true);
		} else {
			if(imageComponent != null)
			this.imageComponent.sprite = manager.CardBackground;
			SetName(this.cardSprite.name);
			if(TextName != null)			
			this.TextName.gameObject.SetActive(false);
		}		
	}

	public Transform GetCardTransform(){
		return this.transform;
	}

	public void SetName(string name){
		cardName = name;
		TextName.text = name;
	}



}
