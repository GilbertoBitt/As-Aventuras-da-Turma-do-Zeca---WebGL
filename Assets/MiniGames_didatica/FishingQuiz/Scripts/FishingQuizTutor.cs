using UnityEngine;

public class FishingQuizTutor : MonoBehaviour {

    // Use this for initialization


    public Animator animTutor;
    public GameObject panel;
    public GameObject tutor;



    void Start () {

        if (PlayerPrefs.HasKey("FishingQuizTutor") == false) {
            PlayerPrefs.SetInt("FishingQuizTutor", 1);
            animTutor.SetInteger("emCena", 1);
            panel.SetActive(false);
            tutor.SetActive(true);
        } else {
            tutor.SetActive(false);
            panel.SetActive(true);
        }
    }
	
	
}
