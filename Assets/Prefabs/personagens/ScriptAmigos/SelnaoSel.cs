using UnityEngine;

public class SelnaoSel : MonoBehaviour {

	public GameObject[] personagens;
	void Start () {
		personagens [PlayerPrefs.GetInt ("characterSelected", 0)].SetActive (false);
		
	}
	

}
