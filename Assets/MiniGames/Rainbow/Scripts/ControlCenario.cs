using UnityEngine;

public class ControlCenario : MonoBehaviour {

	// Use this for initialization
	public RainbowController RainbowController2;
	public SoundManager audioArco;
	public AudioClip arcoiris;
	public AudioClip somCria;
	private AudioSource audioSource;
	public GameObject arcoirisG;

	void Start () {
		
			audioSource = audioArco.startSoundFXWithReturn (somCria);
			GetComponent<Animator> ().SetInteger ("introCheck", RainbowController2.introAnim);

	}
	

	public void SumirAcoiris(){
		arcoirisG.SetActive(false);
	}

	public void StarSom(){
		if (audioSource != null) {
			audioSource.Stop ();
		}
		audioArco.startSoundFX (arcoiris);
	//	GetComponent<Animator> ().SetInteger ("introCheck", 2);
	}

}
