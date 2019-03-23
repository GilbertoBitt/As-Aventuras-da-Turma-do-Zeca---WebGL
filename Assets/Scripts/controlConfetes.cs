using UnityEngine;

public class controlConfetes : MonoBehaviour {

	public Material[] ConfetesMat;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int tpM = Random.Range (0,ConfetesMat.Length);
		GetComponent<ParticleSystemRenderer> ().material = ConfetesMat [tpM];
	}
}
