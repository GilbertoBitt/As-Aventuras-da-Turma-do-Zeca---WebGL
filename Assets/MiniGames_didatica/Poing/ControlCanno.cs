using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControlCanno : MonoBehaviour {

    Rigidbody2D canRig;
    public Transform pos1;
    public Transform pos2;
    public bool checkBool;
    public bool checkBoo2;
    public float angleV;
    public ManagePoing ManagePoing2;
    void Start () {
        canRig = GetComponent<Rigidbody2D>();      
        canRig.DOJump(pos1.transform.position, 0.5f, 10,5f, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

  
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name== "coolD") {
            StartCoroutine(TimeIrD());
        }
        else if(collision.gameObject.name == "coolE") {
            StartCoroutine(TimeIrE());
        }

    }
    void MrotateM() {
        if (!checkBoo2) {
            checkBoo2 = true;
           // angleV = 30f;
            canRig.DORotate(30, 0.2f);
        } else {
            // angleV = -30f;
            checkBoo2 = false;
            canRig.DORotate(-30f, 0.2f);
        }
      
    }

    void DoJumpM() {
      //  checkBool = !checkBool;
        if (!checkBool) {
            checkBool = true;
            angleV = 30f;
            canRig.DOJump(pos2.transform.position, 0.5f, 10, 5f, false);
        } else {
            angleV = -30f;
            checkBool = false;
            canRig.DOJump(pos1.transform.position, 0.5f, 10, 5f, false);
        }
     
    }
    IEnumerator TimeIrD() {      
        canRig.DORotate(-30, 0.5f);      
        yield return new WaitForSeconds(1f);
        canRig.DOJump(pos2.transform.position, 0.5f, 10, 5f, false);
       
    }
    IEnumerator TimeIrE() {
        canRig.DORotate(30, 0.5f);
        yield return new WaitForSeconds(1f);
        canRig.DOJump(pos1.transform.position, 0.5f, 10, 5f, false);
    }

}
