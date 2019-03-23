using System.Collections;
using UnityEngine;

public class ZecaBauMonanha : MonoBehaviour {

	public Manager1_4A Manager1_Mont;
	public ChestHandler1_4A ChestHandler1_4AZ;
	public Animator bau;
	bool chekcPass;
	void Start (){

			StartCoroutine (ZecaBaufalseStar ());



	}

	public void AbrirBauZ () {
	bau.enabled = true;
	//GetComponent<Animator>().SetBool("AbrirBau",true);
	//Manager1_Mont.OpenChest();
	ChestHandler1_4AZ.ToggleRaycastTarget (true);
		
	}
	
	// Update is called once per frame
	void Zecafalse () {
		Manager1_Mont.checkZecaBau=false;
		//GetComponent<Animator>().SetBool("AbrirBau",false);
		
	}
	void ZecaBaufalse () {
	//	bau.enabled = false;
		GetComponent<Animator>().SetBool("AbrirBau",false);

	}
	void ZecaBautrue () {
		bau.enabled = true;

	}
	IEnumerator ZecaBaufalse2 () {
		yield return Yielders.Get(.5f);
		//bau.enabled = false;
		yield return Yielders.Get(.25f);
		GetComponent<Animator> ().enabled = false;
	}
	IEnumerator ZecaBaufalseStar () {
		yield return Yielders.Get(2.5f);
		bau.enabled = false;
	}
}
