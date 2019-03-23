using UnityEngine;

public class TutorDidAumDid : MonoBehaviour {

  
    public Animator animTutor;
    public GameObject panel;
    public GameObject tutor;



    void Start() {

        if (PlayerPrefs.HasKey("tutorAumentDid") == false) {
            PlayerPrefs.SetInt("tutorAumentDid", 1);
            animTutor.SetInteger("emCena", 1);
            panel.SetActive(false);
            tutor.SetActive(true);
        } else {
            tutor.SetActive(false);
            panel.SetActive(true);
        }
    }


}
