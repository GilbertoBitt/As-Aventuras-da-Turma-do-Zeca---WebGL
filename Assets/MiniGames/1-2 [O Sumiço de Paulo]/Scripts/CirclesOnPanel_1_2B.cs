using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CirclesOnPanel_1_2B : MonoBehaviour, IPointerClickHandler{
	public Image imageComp;
	public Image buttonRemove;
	public Image correctCompImage;
	public GeometryPlaces_1_2B placeOfItem;
	public Outline outlineImage;
	public Manager_1_2B manager;



	public void OnPointerClick(PointerEventData eventData){
		if(placeOfItem != null && manager.canBePlayed == true){			
			onClick();
		}
	}

	public void onClick(){
        //placeOfItem.updateImage(imageComp.sprite);
        //placeOfItem.canbeFound = true;
        //manager.RemoveUserPick(placeOfItem);
        //manager.hasFound--;
        //manager.soundManager.startSoundFX(manager.clipsAudio[1]);
        manager.ShowObjectFormAnimation(placeOfItem.form);
	}

	public void updateImage(GeometryPlaces_1_2B place){
		manager = place.manager;
		imageComp.sprite = place.imageComponent.sprite;
		imageComp.preserveAspect = true;
		placeOfItem = place;
	}

	public void updateImage(){
		imageComp.sprite = null;
		placeOfItem = null; 
	}

	public void hideCloseButtton(){
		buttonRemove.gameObject.SetActive(false);
	}

	public void showCloseButton(){
		buttonRemove.gameObject.SetActive(true);
	}


}
