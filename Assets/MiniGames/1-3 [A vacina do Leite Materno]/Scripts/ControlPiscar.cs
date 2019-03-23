using UnityEngine;

public class ControlPiscar : MonoBehaviour {

    public Animator imgAnim;
    public Manager1_3A Manager1_3AR;
    void Start () {
        this.Manager1_3AR = Camera.main.GetComponent<Manager1_3A>();
        imgAnim =GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void StopPiscar () {
        if(Manager1_3AR.checkPiscar == false){
            imgAnim.SetBool("StopPiscar", true);
        } 
    }
    void EnableFalse() {
      if(!Manager1_3AR.plataformController2d.EndedByLife){
        imgAnim.SetBool("StopPiscar", false);
        imgAnim.enabled = false;
      }
    }
}
