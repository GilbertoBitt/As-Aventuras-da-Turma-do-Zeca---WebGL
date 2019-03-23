using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.SerializableAttribute]
public class GeometryPlaces_1_2B : MonoBehaviour, IPointerClickHandler{

	public Image imageComponent;
	public Manager_1_2B manager;
	public Manager_1_2B.geometryForm form;
	public bool canbeFound = false;

	public void OnPointerClick(PointerEventData eventData){
		if(canbeFound == true && manager.canBePlayed == true){
			manager.AddUserPick(this);		
			manager.hasFound++;
			imageComponent.color = Color.clear;
			canbeFound = false;
			manager.soundManager.startVoiceFX(manager.clipsAudio[0]);
		}
	}

	public void updateImage(Sprite spriteItem){
		imageComponent.sprite = spriteItem;
		imageComponent.color = Color.white;
		imageComponent.preserveAspect = true;
		canbeFound = true;
	}

	public void resetConfig(){
		form = Manager_1_2B.geometryForm.none;
		canbeFound = false;
		imageComponent.sprite = null;
		imageComponent.color = Color.clear;
	}
	
}
