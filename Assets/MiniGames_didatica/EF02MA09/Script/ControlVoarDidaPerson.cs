using UnityEngine;

public class ControlVoarDidaPerson : MonoBehaviour {

    public Animator personVoando;
    public float targetYAnim;
    public ManagerEF02MA09 ManagerEF02MA09s;


    void Start () {
        personVoando = GetComponent<Animator>();
        targetYAnim = ManagerEF02MA09s.targetY;
    }
	
	// Update is called once per frame
	void Update () {
        targetYAnim = ManagerEF02MA09s.VerticalVirtualAxis;
        personVoando.SetBool("checkMove", ManagerEF02MA09s.checkMove);
        personVoando.SetFloat("targetYAnim", targetYAnim);



    }
}
