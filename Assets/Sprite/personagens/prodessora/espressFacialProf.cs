using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class espressFacialProf : MonoBehaviour {


	public GameObject olhosFechados;
    public Image olhosFechadosI;
    public Image bocaI;
    public GameObject bocaAberta;
	float timeOlhos;
	float timeBoca;
	public float[] myTimeOlhos;
	public float[] myTimeBoca;
	int  NumTimeOlhos;
	int  NumTimeBoca;
	bool checkOlhos;
	bool checkBoca;
    public AudioClip[] audios;

    public bool checkInteragindo;
    
    void Start () {
        olhosFechadosI = olhosFechados.GetComponent<Image>();
        bocaI = bocaAberta.GetComponent<Image>();
        if (checkInteragindo) {
            olhosFechados.SetActive(true);
           // olhosFechadosI = olhosFechados.GetComponent<Image>();
            olhosFechadosI.enabled = false;
        } 
        else {
            olhosFechados.SetActive(false);
        }

        if (checkInteragindo) {
            bocaAberta.SetActive(true);
         
            bocaI.enabled = false;
        }
        else {
            bocaAberta.SetActive(false);
        }
          
		timeOlhos = Time.realtimeSinceStartup;
		timeBoca = Time.realtimeSinceStartup;
		
	
	}
	
	// Update is called once per frame
	void Update () {

if(	checkOlhos==false){
	if (Time.realtimeSinceStartup - timeOlhos > myTimeOlhos[NumTimeOlhos]) {
                if (checkInteragindo) {
                    olhosFechadosI.enabled = true;
                } 
                else {
                    olhosFechados.SetActive(true);
                }
               
			StartCoroutine (FecharOlhos());
			timeOlhos = Time.realtimeSinceStartup;
			NumTimeOlhos = Random.Range(0,myTimeOlhos.Length);
		}

}
		
if(checkBoca==false && bocaAberta!= null)
        {
	if (Time.realtimeSinceStartup - timeBoca > myTimeBoca[NumTimeBoca]) {
                if (checkInteragindo) {
                   
                    bocaI.enabled = true;
                } else {
                    bocaAberta.SetActive(true);
                }
               
			StartCoroutine (AbrirBoca());
			timeBoca = Time.realtimeSinceStartup;
			NumTimeBoca = Random.Range(0,myTimeBoca.Length);
			
		}
}
		
	}
	IEnumerator FecharOlhos() {
		yield return Yielders.Get (1f);
        if (checkInteragindo) {
            olhosFechadosI.enabled = false;
        } 
        else {
            olhosFechados.SetActive(false);
        }
        checkOlhos =false;
		//StopCoroutine(FecharOlhos());


		

	}
	IEnumerator AbrirBoca() {

		yield return Yielders.Get (0.5f);
        if (checkInteragindo) {           
            bocaI.enabled = false;
        } else {
            bocaAberta.SetActive(false);
        }

		checkBoca=false;
	//	StopCoroutine(AbrirBoca());

	}
   

}
