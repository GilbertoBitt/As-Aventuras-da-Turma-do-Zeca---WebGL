using UnityEngine;
using System.Collections.Generic;
using MEC;

public class TutorItem : MonoBehaviour {

    Manager1_3A manager;
    public bool tutorVoar;
    public bool tutorVel;
    public bool leite;
    public bool pular;
    public bool baixar;
    public BoxCollider2D thisCollider2D;
    public Animator tutorAnim;
    public TutorVacine TutorVacine1;
    public string textoItem;
    public ControlSomTutor ControlSomTutor2;
    



    void Start () {


        if (manager == null)
        {
            manager = Camera.main.GetComponent<Manager1_3A>();

            // tutorAnim = manager.tutorVacine.GetComponent<Animator>();
            TutorVacine1 = manager.tutorVacine.GetComponent<TutorVacine>();
            tutorAnim = TutorVacine1.animTurtor;
            thisCollider2D = this.GetComponent<BoxCollider2D>();
        }

        ControlSomTutor2 = Camera.main.GetComponent<ControlSomTutor>();
        if (PlayerPrefs.HasKey("tutorVoarN1") == false && tutorVoar)
        {
           // PlayerPrefs.SetInt("tutorVoarN1", 1);
            thisCollider2D.enabled = true;
        }
        else if (PlayerPrefs.HasKey("tutorVelN1") == false && tutorVel)
        {
           // PlayerPrefs.SetInt("tutorVelN1", 1);
            thisCollider2D.enabled = true;
        }
        else if ( PlayerPrefs.HasKey("tutorleiteN1") == false && leite)
        {
           // PlayerPrefs.SetInt("tutorleiteN1", 1);
            thisCollider2D.enabled = true;
        }
        else if ( PlayerPrefs.HasKey("tutorpularN1") == false && pular)
        {
            //PlayerPrefs.SetInt("tutorpularN1", 1);
            thisCollider2D.enabled = true;
        }
        else if ( PlayerPrefs.HasKey("tutorbaixarN1") == false && baixar)
        {
           // PlayerPrefs.SetInt("tutorbaixarN1", 1);
            thisCollider2D.enabled = true;
        }
        else {
          

        }

    }

    IEnumerator<float> SomTutor(int recNum) {
        ControlSomTutor2.numTutor = recNum;
        ControlSomTutor2.SomTutor();
        yield return Timing.WaitForSeconds(0.2f);
      

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerPrefs.HasKey("tutorVelN1") == false && tutorVel && thisCollider2D.IsTouching(manager.BoxCollider2DPlayer))
        {
            manager.textointemCheck = true;
           TutorVacine1.btTelao.SetActive(true);
             TutorVacine1.pularTex.SetActive(false);
            Timing.RunCoroutine(SomTutor(3));
          
            PlayerPrefs.SetInt("tutorVelN1", 1);
            //manager.checkTutorcorrer = PlayerPrefs.GetInt("tutorVelN1", 1) == 1 ? true : false;
            TutorVacine1.textTutor.text = textoItem;
            manager.tutorVacineGame = true;
            manager.tutorVacine.SetActive(true);
           // TutorVacine1.iniciarG1.SetActive(true);
            TutorVacine1.turtorVac2.SetActive(true);
            TutorVacine1.pularTex.SetActive(true);
            TutorVacine1.Professora.SetActive(true);
            TutorVacine1.Professora.GetComponent<Animator>().enabled=true;
            tutorAnim.enabled=true;
            if (tutorAnim.isActiveAndEnabled == true)
            {
                tutorAnim.SetInteger("iniTutor", 1);
            }
            Time.timeScale = 0;
            thisCollider2D.enabled = false;
            
        }
        else if (PlayerPrefs.HasKey("tutorVoarN1") == false && tutorVoar && thisCollider2D.IsTouching(manager.BoxCollider2DPlayer))
        {
            manager.textointemCheck = true;
            // manager.checkTutorVoar = PlayerPrefs.GetInt("tutorVoarN1", 1) == 1 ? true : false;
            TutorVacine1.btTelao.SetActive(true);
            TutorVacine1.pularTex.SetActive(false);
            Timing.RunCoroutine(SomTutor(5));
            PlayerPrefs.SetInt("tutorVoarN1", 1);
            TutorVacine1.textTutor.text = textoItem;
            manager.tutorVacineGame = true;
            manager.tutorVacine.SetActive(true);
            // TutorVacine1.iniciarG1.SetActive(true);
            TutorVacine1.turtorVac2.SetActive(true);
            TutorVacine1.Professora.SetActive(true);
            TutorVacine1.pularTex.SetActive(true);
            TutorVacine1.Professora.GetComponent<Animator>().enabled = true;
            tutorAnim.enabled = true;
            if (tutorAnim.isActiveAndEnabled == true)
            {
                tutorAnim.SetInteger("iniTutor", 1);
            }
            Time.timeScale = 0;
            thisCollider2D.enabled = false;
         
        } else if (PlayerPrefs.HasKey("tutorleiteN1") == false && leite  && thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
            manager.textointemCheck = true;
            TutorVacine1.btTelao.SetActive(true);
            TutorVacine1.pularTex.SetActive(false);
            PlayerPrefs.SetInt("tutorleiteN1", 1);
            Timing.RunCoroutine(SomTutor(1));
            // manager.checkTutorLeite = PlayerPrefs.GetInt("tutorleiteN1", 1) == 1 ? true : false;
            TutorVacine1.textTutor.text = textoItem;
            manager.tutorVacineGame = true;
            manager.tutorVacine.SetActive(true);
            // TutorVacine1.iniciarG1.SetActive(true);
            TutorVacine1.turtorVac2.SetActive(true);
            TutorVacine1.Professora.SetActive(true);
            TutorVacine1.pularTex.SetActive(true);
            TutorVacine1.Professora.GetComponent<Animator>().enabled = true;
            tutorAnim.enabled = true;
            if (tutorAnim.isActiveAndEnabled == true) {
                tutorAnim.SetInteger("iniTutor", 1);
            }
            Time.timeScale = 0;
            thisCollider2D.enabled = false;

        } else if (PlayerPrefs.HasKey("tutorbaixarN1") == false && baixar && thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
            manager.textointemCheck = true;
            TutorVacine1.btTelao.SetActive(true);
               TutorVacine1.pularTex.SetActive(false);
            PlayerPrefs.SetInt("tutorbaixarN1", 1);
            Timing.RunCoroutine(SomTutor(2));
            // manager.checkTutorAbaixar = PlayerPrefs.GetInt("tutorbaixarrN1", 1) == 1 ? true : false;
            TutorVacine1.textTutor.text = textoItem;
            manager.tutorVacineGame = true;
            manager.tutorVacine.SetActive(true);
            // TutorVacine1.iniciarG1.SetActive(true);
            TutorVacine1.turtorVac2.SetActive(true);
            TutorVacine1.Professora.SetActive(true);
            TutorVacine1.pularTex.SetActive(true);
            TutorVacine1.Professora.GetComponent<Animator>().enabled = true;
            tutorAnim.enabled = true;
            if (tutorAnim.isActiveAndEnabled == true) {
                tutorAnim.SetInteger("iniTutor", 1);
            }
            Time.timeScale = 0;
            thisCollider2D.enabled = false;

        } /*else if (PlayerPrefs.HasKey("tutorpularN1") == false && pular && thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
              TutorVacine1.btTelao.SetActive(true);
                TutorVacine1.pularTex.SetActive(false);
            Timing.RunCoroutine(SomTutor(4));
            PlayerPrefs.SetInt("tutorpularN1", 1);
           // manager.checkTutorPular = PlayerPrefs.GetInt("tutorpularN1", 1) == 1 ? true : false;
            TutorVacine1.textTutor.text = textoItem;
            manager.tutorVacineGame = true;
            manager.tutorVacine.SetActive(true);
            // TutorVacine1.iniciarG1.SetActive(true);
            TutorVacine1.turtorVac2.SetActive(true);
            TutorVacine1.Professora.SetActive(true);
            TutorVacine1.pularTex.SetActive(true);
            TutorVacine1.Professora.GetComponent<Animator>().enabled = true;
            tutorAnim.enabled = true;
            if (tutorAnim.isActiveAndEnabled == true) {
                tutorAnim.SetInteger("iniTutor", 1);
            }
            Time.timeScale = 0;
            thisCollider2D.enabled = false;

        }
        */
    }
}
