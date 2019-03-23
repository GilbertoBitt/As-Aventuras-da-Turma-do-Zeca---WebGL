using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class tutorPanelArcoirirs : MonoBehaviour {

    public string[] textprofessorTutor;
    public Text textprofessoraG;
    public int numtext;

    public GameObject PanelBtInicar;
    public GameObject sairGame;
    public SoundManager soundManager;
    public AudioClip[] audiosTutorial;
    public int numbAudio;
    #region AnimatorHashKeys

    static readonly int numTextHash = Animator.StringToHash("numText");

    #endregion
    public Vector3 offsetFollowB;
    public Vector3 defaultOffsetB;
    public float transitionDurationB;
    public GameObject balaoG;
    public GameObject TextGB;
    public GameObject btPular;
    public GameObject avancarTutor;
    Text balaoT;
    Button btPularB;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public int numbIni1;

    public int tutor_0;
    public GameObject[] tutos;
    public Image fundo;
    public GameObject panelItens;
    public GameObject panelItens2;
    public GameObject checkfim2G;
    public bool checkVoz;


    public void Start() {
        fundo = GetComponent<Image>();
        numtext = 1;
        btPularB = btPular.GetComponent<Button>();
        balaoT = TextGB.GetComponent<Text>();
        if (PlayerPrefs.HasKey("tutorIni1") == false) {
            tutor_0 = 0;
            PlayerPrefs.SetInt("tutorIni1", 1);
            for (int i = 0; i < tutos.Length; i++) {
                tutos[i].SetActive(true);
                ChamarTutor(1);
            }
            fundo.enabled=true;
            panelItens.SetActive(true);
            panelItens2.SetActive(true);
        }
        else  {
            fundo.enabled = false;
            tutor_0 = 1;
            for (int i = 0; i < tutos.Length; i++) {
                tutos[i].SetActive(false);
                ChamarPanelIni();
            }
           

        }     
        

    }

    public void BalaoScaleM() {     
        Timing.RunCoroutine(BalaoScale());
    }
    public void BalaoScaleM2() {
        fundo.enabled = false;
        Timing.RunCoroutine(BalaoScale2(7));
    }
    public void BalaoScaleM3() {
        fundo.enabled = false;

        Timing.RunCoroutine(BalaoScale2(6));
    }
    public IEnumerator<float> BalaoScaleStart() {
        Vector3 startOffsetB = offsetFollowB;
        Vector3 endOffsetB = defaultOffsetB;
        float timesB = 0.0f;
        balaoT.text = textprofessorTutor[numtext].ToUpper();
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            balaoG.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            TextGB.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        btPularB.interactable = true;
        soundManager.startVoiceFX(audiosTutorial[numtext]);

    }
    public IEnumerator<float> BalaoScale2(int numb) {
        Vector3 startOffsetB = offsetFollowB;
        Vector3 endOffsetB = defaultOffsetB;
        float timesB = 0.0f;
        if (numb != 10) {
            avancarTutor.SetActive(true);
        }
        btPular.SetActive(false);
        balaoT.text = textprofessorTutor[numb].ToUpper();
        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            balaoG.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            TextGB.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return 0f;
        }
        btPularB.interactable = true;
        if(checkVoz==true)
        soundManager.startVoiceFX(audiosTutorial[numb]);

    }

    public IEnumerator<float> BalaoScale() {
        Vector3 startOffsetB = offsetFollowB;
        Vector3 endOffsetB = defaultOffsetB;
        float timesB = 0.0f;

        while (timesB < transitionDurationB) {
            timesB += Time.deltaTime;
            float s = timesB / transitionDurationB;
            TextGB.transform.localScale = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        numtext = numtext + 1;
        balaoT.text = textprofessorTutor[numtext].ToUpper();

        if (numtext < 5) {
            timesB = 0.0f;

            while (timesB < transitionDurationB) {
                timesB += Time.deltaTime;
                float s = timesB / transitionDurationB;
                TextGB.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
                yield return Timing.WaitForOneFrame;
            }
            soundManager.startVoiceFX(audiosTutorial[numtext]);
            btPularB.interactable = true;


        } else {

            ChamarPanelIni();


        }




    }

    public void RepetirAudio() {
       
        soundManager.startVoiceFX(audiosTutorial[numtext]);

    }
    public void ChamarPanelIni() {
        checkVoz=true;
        soundManager.VoiceOverHandler();
        PanelBtInicar.SetActive(true);
        sairGame.SetActive(true);
        gameObject.SetActive(false);
        panelItens.SetActive(false);
        panelItens2.SetActive(false);
    }
    public void ChamarPanel2() {
        //soundManager.VoiceOverHandler();
        checkVoz=false;
        PanelBtInicar.SetActive(true);
        sairGame.SetActive(true);
        checkfim2G.SetActive(true);
       for (int i = 0; i < tutos.Length; i++)
       {
           tutos[i].SetActive(true);
       }
        Timing.RunCoroutine(BalaoScale2(10));
       // panelItens.SetActive(false);
       // panelItens2.SetActive(false);
    }
    void DesObj() {
        gameObject.SetActive(false);
    }
    public void ChamarTutor(int numbIni) {

        if (numbIni < 2) {
            numtext = 1;
            Timing.RunCoroutine(BalaoScaleStart());
        } else if (numbIni == 2) {
            
        }

    }

    public void EndAllCoroutines(){
        Timing.KillCoroutines();
		StopAllCoroutines ();
	}
}
