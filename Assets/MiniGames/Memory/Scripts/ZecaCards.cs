using System.Collections;
using UnityEngine;

public class ZecaCards : OverridableMonoBehaviour {

	public GameObject cards;
	public Manager_1_1B Manager_1_1BB;
	public MemoryGameManager memoryG;
	public GameObject[] parcards;
	public bool checkPart;
	bool checkjoacard;
	void Start(){
		checkjoacard=false;
	}

    public override void UpdateMe() {
		if(memoryG.isGameEnded && checkjoacard==false && memoryG.levelDificult>3 && memoryG.checkAvancar1==2){
			StartCoroutine(TemPartc2());
		}

	}
	void AtivarPartCards(){

		foreach (var item in parcards) {
			item.SetActive (true);
		}
	}
	void DesativarPartCards(){

		foreach (var item in parcards) {
			item.SetActive (false);
		}
	}


   void JogarCards(){
		
	Manager_1_1BB.buttonFadin();
      
	StartCoroutine(TemPartc());
   }
   void cardZerar(){
		
		cards.GetComponent<Animator>().SetInteger("NumbCheck", 0);
   }

   
   IEnumerator TemPartc()	{
        cards.GetComponent<Animator>().SetBool("nextCanbeStarted", false);
        yield return new WaitForSeconds(0.05f);
        cards.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        //  cards.GetComponent<Animator>().SetBool("nextCanbeStarted", false);

        // yield return new WaitForSeconds(0.1f);

        cards.GetComponent<Animator>().SetBool("nextCanbeStarted", true);
        yield return new WaitForSeconds(0.1f);

        Manager_1_1BB.partCards2.SetActive(false);
	foreach (var item in Manager_1_1BB.cardMaos) {
	item.enabled=false;
	} 
	foreach (var item in Manager_1_1BB.partsCards2) {
	item.SetActive(false);
	} 
	yield return new WaitForSeconds(1f);
	Manager_1_1BB.partCards.SetActive(false);
	//GetComponent<Animator>().SetInteger("posCorpoZeca",0);
		
	}
	IEnumerator TemPartc3()	{
		//	cards.GetComponent<Animator>().SetInteger("NumbCheck", 0);
	yield return new WaitForSeconds(1f);
	Manager_1_1BB.partCards.SetActive(false);	
	GetComponent<Animator>().SetInteger("posCorpoZeca",0);
		foreach (var item in parcards) {
			item.SetActive (false);
		}
		
	}
	IEnumerator TemPartc2()	{
	checkjoacard=true;
	GetComponent<Animator>().SetInteger("posCorpoZeca",5);
	yield return new WaitForSeconds(2f);
	//GetComponent<Animator>().SetInteger("posCorpoZeca",5);
	
		
	}


    void ResteScale() {
        //Manager_1_1BB.ResetCardScale();

    }

}
