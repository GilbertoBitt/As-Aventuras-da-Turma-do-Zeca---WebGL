using UnityEngine;
using UnityEngine.UI;


public class TutorVacine : MonoBehaviour {

    
    public int tutorIniV_0;
    public GameObject iniciarG1;
    public GameObject iniciarG2;
    public GameObject chamarAjudaBT;
    public Animator animTurtor;
    public GameObject Professora;
    public GameObject turtorVac2;
    public GameObject pularTex;
    Button iniciarBt1;
    Button iniciarBt2;
    public GameObject sound;
    public GraphicRaycaster rayTargeg;
    public Image imageG;
    public Text textTutor;
    public GameObject btTelao;
   public ControlSomTutor ControlSomTutor2;

    void Start () {
        ControlSomTutor2 = Camera.main.GetComponent<ControlSomTutor>();
        imageG = GetComponent<Image>();
        System.GC.Collect();
        iniciarBt1 = iniciarG1.GetComponent<Button>();
        iniciarBt2 = iniciarG2.GetComponent<Button>();
        if (PlayerPrefs.HasKey("TutorV_Ini") == false)
        {
            Debug.Log("dgsd");
           
            if (animTurtor.isActiveAndEnabled == true) {
                animTurtor.SetInteger("iniTutor", 1);
            } 
            PlayerPrefs.SetInt("TutorV_Ini", 1);
            iniciarG1.SetActive(true);
            chamarAjudaBT.SetActive(false);
            iniciarG2.SetActive(false);
            Invoke("Onprof", 1f);
            Invoke("Som1", 1f);

        }
        else
        {           
            tutorIniV_0 = PlayerPrefs.GetInt("TutorV_Ini", 1);
            iniciarG1.SetActive(false);
            iniciarG2.SetActive(true);
            chamarAjudaBT.SetActive(true);
            if (animTurtor.isActiveAndEnabled == true)
            {
                animTurtor.SetInteger("iniTutor", 2);
            }
            Invoke("Som2", 1f);
        }
      
    }
    public void BtchamarAjuda()
    {
        animTurtor.SetInteger("iniTutor", 1);
        iniciarG1.SetActive(true);
        chamarAjudaBT.SetActive(false);
        iniciarG2.SetActive(false);
        Invoke("Onprof", 1f);
        Invoke("Som1", 1f);
    }

    public void ZerarAnim() {
        if (animTurtor.isActiveAndEnabled == true)
        {
            animTurtor.SetInteger("iniTutor", 0);
            Invoke("AnimOff", 2f);
        }
    }
    void AnimOff() {
        animTurtor.enabled = false;
    }

    
    public void Onprof() {
        Professora.GetComponent<espressFacialProf>().enabled = true;
    }
    public void Som1() {
        animTurtor.enabled = true;
        sound.SetActive(true);
        rayTargeg.enabled=true;
        imageG.enabled = false;
        turtorVac2.SetActive(true);
        Invoke("Som2Prof", 1f);
        if (animTurtor.isActiveAndEnabled == true)
        {
            animTurtor.SetInteger("iniTutor", 1);
        }

    }
    public void Som3() {
        ControlSomTutor2.numTutor = 1;
        animTurtor.enabled = true;
        sound.SetActive(true);
        rayTargeg.enabled = true;
        imageG.enabled = false;
        turtorVac2.SetActive(true);
        Invoke("Som2Prof", 1f);
        if (animTurtor.isActiveAndEnabled == true) {
            animTurtor.SetInteger("iniTutor", 1);
        }

    }
    void Som2() {
        animTurtor.enabled = true;
        sound.SetActive(true);        
        rayTargeg.enabled = true;
        imageG.enabled = false;
        turtorVac2.SetActive(true);
        if (animTurtor.isActiveAndEnabled == true)
        {
            animTurtor.SetInteger("iniTutor", 2);
        }

    }
    void Som2Prof() {
        ControlSomTutor2.SomTutor();
    }
}
