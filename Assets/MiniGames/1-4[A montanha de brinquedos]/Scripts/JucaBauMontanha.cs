using UnityEngine;
using UnityEngine.UI;

public class JucaBauMontanha : MonoBehaviour {

	public Manager1_4A Manager1_Mont;
	public Animator bau;
	public Animator zeca;
	public GameObject tutorial;
	public int tutorJuca;
    bool checkpassAudio;
    public TutorMontanha TutorMontanha2;
    void Start() {
        TutorMontanha2 = Manager1_Mont.TutorMontanha2;

    }
    void JucaFecha () {
		bau.enabled = true;
		Manager1_Mont.closeChest();
		
	}
	void Jucafalse () {
		GetComponent<Animator> ().enabled = true;
		Manager1_Mont.checkJucaBau=false;
		GetComponent<Animator>().SetBool("fecharBau",false);
		
	}


	void JucaBautrue () {
		bau.enabled = true;
		zeca.enabled = true;
		zeca.GetComponent<Animator>().SetBool("AbrirBau",false);
	}

	public void JucaTutorial () {
		if(PlayerPrefs.HasKey("TutorJuca_0")==false){
			Manager1_Mont.montanha.GetComponent<GraphicRaycaster> ().enabled = false;
			Manager1_Mont.didatica.GetComponent<GraphicRaycaster> ().enabled = false;
			PlayerPrefs.SetInt("TutorJuca_0",1);
			this.tutorJuca = PlayerPrefs.GetInt("TutorJuca_0",1);
            this.tutorial.GetComponent<TutorMontanha>().tutorialMontanha.GetComponent<Animator>().enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
            TutorMontanha2.numtext = 2;
            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 2;
			this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor [2];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[2]);
            TutorMontanha2.profBalao.enabled = true;

            foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
				item.SetActive (true);
			}	
			this.tutorial.GetComponent<TutorMontanha> ().gTutor [6].SetActive (false);
			this.tutorial.GetComponent<GraphicRaycaster> ().enabled = true;
			this.tutorial.SetActive (true);
			Time.timeScale = 0f;
		
		}
		else{
			//PlayerPrefs.SetInt("TutorJuca_0",1);
			tutorJuca = PlayerPrefs.GetInt("TutorJuca_0",1);

		}
       // TutorMontanha2.profBalao.enabled = false;
    }


	// Update is called once per frame

}
