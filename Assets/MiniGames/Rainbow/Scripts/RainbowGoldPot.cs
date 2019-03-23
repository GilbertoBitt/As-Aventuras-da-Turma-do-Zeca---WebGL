using UnityEngine;
using System.Collections.Generic;
using MEC;

public class RainbowGoldPot : MonoBehaviour {

	public RainbowController controller;
	public GameObject particule;
	public GameObject particule2;
	public GameObject particule3;
    public Collider2D personCol;
    public Collider2D goldCol;
    void Start () {
        goldCol = GetComponent<Collider2D>();
    controller = Camera.main.GetComponent<RainbowController>();
        Physics2D.IgnoreCollision(personCol, goldCol,true);
    }

	IEnumerator<float> TempoPart(){
		yield return Timing.WaitForSeconds(1f);
		if (controller.jogarBalde == false) {
			particule.SetActive (false);
		}

	} 

	void OnTriggerEnter2D(Collider2D col){
		
	if(particule.activeInHierarchy==false){
		particule.SetActive (true);
		Timing.RunCoroutine (TempoPart ());
	}
        if (controller != null && col != null) {
            if (controller.lunch.Contains(col.gameObject) && controller.checkFim == false) {
                //col.gameObject.GetComponent<Collision2D> ().contacts [0].point;
                StartCoroutine(controller.CatchCoin(this.transform.position, 0));
                /*controller.amountPoints += controller.currentLevel.coinValue;
                controller.coinsAmount += 1;*/
                Destroy(col.gameObject);
            } else if (controller.joys.Contains(col.gameObject) && controller.checkFim == false) {
                //controller.amountPoints += controller.currentLevel.goldBarValue;
                //controller.goldBarAmount += 1;
                StartCoroutine(controller.CatchCoin(this.transform.position, 1));
                Destroy(col.gameObject);
            } else if (controller.manners.Contains(col.gameObject) && controller.checkFim == false) {
                //controller.amountPoints += controller.currentLevel.diamontsValue;
                //controller.diamontAmount += 1;
                StartCoroutine(controller.CatchCoin(this.transform.position, 2));
                Destroy(col.gameObject);
            } else if (controller.knowledge.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 3));
                Destroy(col.gameObject);
            } else if (controller.study.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 4));
                Destroy(col.gameObject);
            } else if (controller.grades.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 5));
                Destroy(col.gameObject);
            } else if (controller.friendship.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 6));
                Destroy(col.gameObject);
            } else if (controller.bonusItems.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 7));
                Destroy(col.gameObject);
            } else if (controller.negativeItem.Contains(col.gameObject) && controller.checkFim == false) {
                StartCoroutine(controller.CatchCoin(this.transform.position, 8));
                Destroy(col.gameObject);
            }
        }
    }

	public void EndAllCoroutines(){
        Timing.KillCoroutines();
	}

}
