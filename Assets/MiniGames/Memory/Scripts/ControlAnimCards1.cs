using UnityEngine;

public class ControlAnimCards1 : MonoBehaviour {

    public Animator Cards; 

    void Start () {
        Cards = GetComponent<Animator>();

    }
	
	
    void endCards() {
        Cards.SetBool("nextCanbeStarted", false);
    }
}
