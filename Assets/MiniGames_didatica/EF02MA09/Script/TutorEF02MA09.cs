using UnityEngine;

public class TutorEF02MA09 : MonoBehaviour {

    // Use this for initialization


    public Animator animTutor;
    public GameObject panel;
    public GameObject tutor;



    void Start () {

        if (PlayerPrefs.HasKey("tutorEF02MA09") == false) {
            PlayerPrefs.SetInt("tutorEF02MA09", 1);
            animTutor.SetInteger("emCena", 1);
            panel.SetActive(false);
            tutor.SetActive(true);
        } else {
            tutor.SetActive(false);
            panel.SetActive(true);
        }
    }
	
	
}
