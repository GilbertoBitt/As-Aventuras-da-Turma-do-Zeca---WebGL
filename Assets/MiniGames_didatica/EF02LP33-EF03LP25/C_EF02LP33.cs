using UnityEngine;

public class C_EF02LP33 : MonoBehaviour {

  
    public Animator animTutor;
    public GameObject panel;
    public GameObject tutor;



    void Start() {

        if (PlayerPrefs.HasKey("C_EF02LP33") == false) {
            PlayerPrefs.SetInt("C_EF02LP33", 1);
            animTutor.SetInteger("emCena", 1);
            panel.SetActive(false);
            tutor.SetActive(true);
        } else {
            tutor.SetActive(false);
            panel.SetActive(true);
        }
    }


}
