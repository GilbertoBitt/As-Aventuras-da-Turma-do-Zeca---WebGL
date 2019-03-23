using UnityEngine;
using UnityEngine.UI;

public class ImageOnRender : MonoBehaviour {

	public Image thisImage;

	void OnBecameInvisible() {

		checkIfNull();

		thisImage.enabled = false;
	}

	void OnBecameVisible() {
		
		checkIfNull();

		thisImage.enabled = true;
	}

	void checkIfNull(){

		if (thisImage == null) {
			this.GetComponent<Image>();
		}

	}

}
