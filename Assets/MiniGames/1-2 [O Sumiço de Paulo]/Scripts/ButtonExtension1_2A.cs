using UnityEngine;
using UnityEngine.UI;
using System;
 
 [Serializable]
public class ButtonExtension1_2A : Button{
	public Text exclamationPoint;
	private GameObject exclamationGameObject;	
	public Text buttonText;
	public Sprite spriteOfButton;
	public Image imageComp;

    // Use this for initialization
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    public void Start () {
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        if (exclamationGameObject == null){
			exclamationGameObject = exclamationPoint.gameObject;
		}
	}
	
	// Update is called once per frame


	public void UpdateTextColorExclamation(Color _color){
		if(exclamationGameObject.activeInHierarchy == false){
			exclamationGameObject.SetActive(true);
		}

		exclamationPoint.color = _color;
	}

    public void ExclamationTextEnable(bool _enable) {
        exclamationGameObject.SetActive(_enable);
        Color tempColor = exclamationPoint.color;
        tempColor.a = 0f;
        exclamationPoint.color = tempColor;
    }

    public Text GetTextComp() {
        if (exclamationGameObject.activeInHierarchy == false) {
            exclamationGameObject.SetActive(true);
        }

        return exclamationPoint;
    }

	public void UpdateText(string _text){
		buttonText.text = _text;
	}

	public void UpdateText(){
		buttonText.text = spriteOfButton.name;
	}

	public void stopBlink(){
		if(exclamationGameObject == null){
			exclamationGameObject = exclamationPoint.gameObject;
		}
		exclamationGameObject.SetActive(false);
	}

	public void stopInteractable(){
		interactable = false;
	}

	public void startInteractable(){
		interactable = true;
	}

}
