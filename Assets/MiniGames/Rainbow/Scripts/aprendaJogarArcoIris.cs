using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class aprendaJogarArcoIris : MonoBehaviour {

	public bool checkDir;
	public bool checkEsq;
	bool inicioIntro;
	public bool tutorMoveFim;
    public SoundManager soundManager;
    public tutorPanelArcoirirs tutorPanelArcoirirs2;

    public GameObject[] dirEsq;


	void movDir(){
		checkDir = true;
	}
	void movEsq(){
		checkEsq = true;
	}
	void movStop(){
		checkDir = false;
		checkEsq = false;
		GetComponent<Animator> ().SetBool ("checkDir",false);
		GetComponent<Animator> ().SetBool ("checkEsq",false);
		CrossPlatformInputManager.SetAxisZero("Horizontal");
		//CrossPlatformInputManager.SetAxisZero("Horizontal");
	}

	void MovZero(){
		CrossPlatformInputManager.SetAxisZero("Horizontal");
		tutorMoveFim = true;
	}
	IEnumerator TimeMovDir(){
		GetComponent<Animator> ().SetBool ("checkDir", true);
        soundManager.startVoiceFXReturn(tutorPanelArcoirirs2.audiosTutorial[5]);
        yield return new WaitForSeconds(1.5f);
		CrossPlatformInputManager.SetAxisPositive ("Horizontal");	
		yield return new WaitForSeconds(0.5f);
        movStop();
	}
	IEnumerator TimeMovEsq(){
		GetComponent<Animator> ().SetBool ("checkEsq", true);
        soundManager.startVoiceFXReturn(tutorPanelArcoirirs2.audiosTutorial[6]);
        yield return new WaitForSeconds(1.5f);
		CrossPlatformInputManager.SetAxisNegative ("Horizontal");	
		yield return new WaitForSeconds(0.5f);
        movStop();
	}



}
