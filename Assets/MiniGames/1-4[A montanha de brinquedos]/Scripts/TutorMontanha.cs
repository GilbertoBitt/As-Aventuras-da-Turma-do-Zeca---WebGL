using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorMontanha : MonoBehaviour {

	public int tutorNumber;
	public string[] TextTutor;
	public GameObject tutorialMontanha;
	public GameObject Manage;
	public GameObject montanha;
	public GameObject didatica;
	public Animator animTutor;
	public ChestHandler1_4A[] ChestHandler1_4A2;
	public int numbTutor;
	public int tutorIniM_0;
	//public int tutorIniMD_0;
	public GameObject btIniciar;
	public GameObject[] gTutor;
	public ZecaBauMonanha[] ZecaBauMonanhaV;
	public Text profBalao;
	public Text btPulartext;
	public GameObject Prof;
    public SoundManager soundManager;
    public AudioClip[] audiosTutorial;
    public AudioClip audiosTutorialIni;
    bool checkpassAudio;
    bool checkDidatica;
    public int checkDidatica2;
    bool chekcChamarAjuda=false;
    public TutorGM TutorGM;
    public int numtext;
    bool checkIni;

    void Start () {
		animTutor = tutorialMontanha.GetComponent<Animator> ();
        animTutor.enabled = true;
		animTutor.SetInteger ("numbTutor",2);
		if(PlayerPrefs.HasKey("TutorM_0P")==false){
		//	profBalao.text = TextTutor [0];
			PlayerPrefs.SetInt("TutorMP_0P",1);
           // Invoke("tutoSom1",1f);
            tutoSom1();
            StartCoroutine (StatTutorialMontanha());
		}
		else{
			//PlayerPrefs.SetInt("TutorM_0P",1);
			tutorIniM_0 = PlayerPrefs.GetInt("TutorMP_0P",1);
			foreach (var item in gTutor)
			{
				item.SetActive(false);
			}	
			btIniciar.SetActive (true);
		}
	//	Debug.Log (PlayerPrefs.HasKey("TutorM_0P"));
		
	}
    public void tutoSom1() {
       
        if (!chekcChamarAjuda) {
            chekcChamarAjuda = true;
            soundManager.startVoiceFXReturn(audiosTutorialIni);
           
        } 
    }
     
	public void Bt_pular () {
        profBalao.enabled = false;
        if (!TutorGM.checkClickB) {
            TutorGM.checkClickB = true;
        }
     

        if (tutorNumber == 0) {
			if (PlayerPrefs.HasKey ("TutorM_0P") == false) {
				PlayerPrefs.SetInt ("TutorM_0P", 1);
				tutorIniM_0 = PlayerPrefs.GetInt ("TutorM_0P", 1);
			}
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			foreach (var item in ChestHandler1_4A2) {
				item.enabled = true;
			}	
			foreach (var item in ZecaBauMonanhaV) {
				item.enabled = true;
			}	
			Manage.SetActive (true);
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 1);
			montanha.GetComponent<GraphicRaycaster> ().enabled = true;
			didatica.GetComponent<GraphicRaycaster> ().enabled = true;
			Prof.GetComponent<Animator> ().enabled = true;
			Prof.GetComponent<espressFacialProf> ().enabled = false;
			//btIniciar.SetActive (true);
		} else if (tutorNumber == 1) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.enabled = false;
            ///GetComponent<GameObject>().SetActive (false);
            /// montanha.GetComponent<GraphicRaycaster> ().enabled = true;
            didatica.GetComponent<GraphicRaycaster> ().enabled = true;
			Prof.GetComponent<Animator> ().enabled = true;
			Prof.GetComponent<espressFacialProf> ().enabled = false;
			
		}
		else if (tutorNumber == 2) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
		else if (tutorNumber == 3) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
		else if (tutorNumber == 4) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
		else if (tutorNumber == 5) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
		else if (tutorNumber == 6) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
		else if (tutorNumber == 7) {
			Time.timeScale = 1f;
			foreach (var item in gTutor) {
				item.SetActive (false);
			}	
			GetComponent<GraphicRaycaster> ().enabled = false;
			animTutor.SetInteger ("numbTutor", 0);
            //	animTutor.enabled = false;
            StartCoroutine(TimeVoltarIniAnimator());
            //animTutor.SetInteger ("numbTutor", 1);
            //GetComponent<GameObject>().SetActive (false);

        }
//		GetComponent<GameObject>().SetActive (false);
		montanha.GetComponent<GraphicRaycaster> ().enabled = true;
		didatica.GetComponent<GraphicRaycaster> ().enabled = true;
       // StartCoroutine(TimeVoltarIniAnimator());
	}
	void VoltarIniAnimator(){
		animTutor.SetInteger ("numbTutor", 0);
		animTutor.enabled = false;
		
	}
   public IEnumerator TimeVoltarIniAnimator() {
        yield return Yielders.Get(.5f);
       // animTutor.SetInteger("numbTutor", 0);
        animTutor.enabled = false;

    }
    
    public void RepetirAudio() {
        if (!checkIni) 
            soundManager.startVoiceFXReturn(audiosTutorialIni);
        else
            soundManager.startVoiceFXReturn(audiosTutorial[numtext]);

    }

    // Update is called once per frame

   IEnumerator StatTutorialMontanha () {
        
         yield return Yielders.Get(.1f);
       
        animTutor.enabled = true;
        //	animTutor.SetInteger ("numbTutor", 0);

    }

   
    public void StartGameT(){
		foreach (var item in ChestHandler1_4A2)
		{
			item.enabled = true;
		}	
		Manage.SetActive (true);
		GetComponent<GraphicRaycaster> ().enabled = false;
		//GetComponent<GameObject>().SetActive (false);
	}

    public void ChamarTutor() {
        animTutor.enabled = true;
        animTutor.SetInteger("numbTutor", -2);
        StartCoroutine(TimnebtActiv());

    }
    IEnumerator TimnebtActiv() {
        // soundManager.startVoiceFXReturn(audiosTutorial[numtext]);
        RepetirAudio();
        soundManager.startVoiceFXReturn(audiosTutorialIni);
        // Debug.Log("Passou");
       
        yield return Yielders.Get(10f);
        checkIni = true;
        btIniciar.SetActive(true);
        profBalao.text = TextTutor[0];
      //  yield return Yielders.Get(0.5f);
        //soundManager.startVoiceFXReturn(audiosTutorial[numtext]);
       // checkIni = false;
        //RepetirAudio();


    }


}
