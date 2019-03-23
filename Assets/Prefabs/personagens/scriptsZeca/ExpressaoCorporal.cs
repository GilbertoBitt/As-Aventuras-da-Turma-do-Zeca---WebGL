using System.Collections;
using UnityEngine;

public class ExpressaoCorporal : MonoBehaviour {

	public Animator ControlAnimCorpo;
	public int posCorpoZeca;
	void Start () {
		ControlAnimCorpo = GetComponent<Animator> ();
		ControlAnimCorpo.SetFloat ("velMove",1);
	}
	
	// Update is called once per frame
	void FixedUpdate2 () {
		ControlAnimCorpo.SetInteger ("posCorpoZeca",posCorpoZeca );
		if (posCorpoZeca > 0) {
			StartCoroutine (VoltarPosIni ());
		} else {
			ControlAnimCorpo.SetFloat ("velMove",1);
		}
		
	}
	void PosCorpoZecaIni (){
		ControlAnimCorpo.SetFloat ("velMove",-1);

	}
	IEnumerator VoltarPosIni() {
		yield return new WaitForSeconds (2.5f);
		posCorpoZeca = 0;
		PosCorpoZecaIni ();

	}



}
