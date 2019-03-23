using System.Collections;
using UnityEngine;

public class TutorGM : MonoBehaviour {
	public GameObject TutorMontanhaOb;
	public bool checkClickB;
	public GameObject pularBt;
    bool checkpassAudio;
    public TutorMontanha TutorMontanha2;
   


    void Start(){
		StartCoroutine (TimeMudarTextNinical ());
       

    }
	public void VoltarAnim0 () {
		GetComponent<Animator>().enabled = true;
		GetComponent<Animator>().SetInteger ("numbTutor", 0);
		GetComponent<Animator>().enabled = false;
		StartCoroutine (TimeFalserTutor());
        StartCoroutine(TutorMontanhaOb.GetComponent<TutorMontanha>().TimeVoltarIniAnimator());


    }
	public void BackAnim0 () {
		GetComponent<Animator>().enabled = true;
		GetComponent<Animator>().SetInteger ("numbTutor", 0);

	}
	IEnumerator TimeFalserTutor () {
		yield return Yielders.Get(.1f);
		this.TutorMontanhaOb.SetActive (false);
	}
	void MudarTextNinical(){
        if (!checkClickB) {
            GetComponent<Animator>().enabled = true;
            checkClickB = true;
            TutorMontanha2.btPulartext.text = "Iniciar";
            TutorMontanha2.profBalao.enabled = true;
            TutorMontanha2.profBalao.text = TutorMontanha2.TextTutor[0];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[0]);
        }
		
            
        
    }
	IEnumerator TimeMudarTextNinical () {
		yield return Yielders.Get(10f);
        if (checkClickB == false) {
            pularBt.SetActive(false);
            checkClickB = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetInteger("numbTutor", -1);
        } else {

        }

	}
	public void animText(){


        GetComponent<Animator>().enabled = true;
		GetComponent<Animator>().SetInteger ("numbTutor", -1);
	}

}
