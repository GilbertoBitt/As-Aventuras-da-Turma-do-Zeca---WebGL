using UnityEngine;

public class rederControlCam : MonoBehaviour {

	void OnBecameInvisible() {
		enabled = false;
		//Debug.Log (enabled);
		GetComponent<SpriteRenderer>().enabled = enabled;
	}
	void OnBecameVisible() {
		enabled = true;
		GetComponent<SpriteRenderer>().enabled = enabled;
	}
	void OnWillRenderObject() {
		enabled = true;
		GetComponent<SpriteRenderer>().enabled = enabled;
	}
}
