using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class changeLevel : MonoBehaviour {
	
	public Animator anim;
	public Text thisText;
	public Text parabensText;
	int tempNumParabens;
	int tempAcertoErro;
	public string[] parabensTexts = new string[2];
	public string textLevelChange;
	public bool hasCustomWaitTime;
	public float customWaitTime = 3f;
	public bool hasWaitTimeRightNWrong;
	public float waitTimeCustomized;
	public bool desativarLocal;
	public GameObject canvasON;
    public SoundManager soundManager2;
    public AudioClip audio1;
   // public AnimationClip animationClip;

    static readonly int numParabensHash = Animator.StringToHash("numParabens");
    static readonly int acertoErroHash = Animator.StringToHash("acertoErro");

    public bool chekcBT;
    public GameObject panelblok;

    // Use this for initialization
    private void Start() {
        soundManager2 = FindObjectOfType<SoundManager>();
    }


    public void startChangeLevelAnimation(int level){

        if (soundManager2 == null) {
            soundManager2 = FindObjectOfType<SoundManager>();
        }
        if (audio1 != null ) {
          
          soundManager2.startVoiceFX(audio1);
        }
          
        anim.enabled = true;
		thisText.text = textLevelChange.Replace ("<level>", level.ToString ());
		parabensText.text = parabensTexts[Random.Range(0, parabensTexts.Length)];
		tempNumParabens=1;
	//	tempAcertoErro=1;
		anim.SetInteger(numParabensHash, tempNumParabens);
        //	anim.SetInteger("acertoErro",tempAcertoErro);
     
            StartCoroutine(TempoAnim());
        
		//Debug.Log("Mudança de Nivel");

	}
	void McanvasON(){
		//if(canvasON != null)
		//canvasON.GetComponent<GraphicRaycaster> ().enabled = true;

	}

	public void startCerto(string _text){
		anim.enabled = true;
		parabensText.text = _text;
		//parabensText.text = parabensTexts[Random.Range(0, parabensTexts.Length)];
		//tempNumParabens=1;
		tempAcertoErro=1;
		//anim.SetInteger("numParabens",tempNumParabens);
		anim.SetInteger(acertoErroHash, tempAcertoErro);
		StartCoroutine(TempoAnim());
		//Debug.Log("Mudança de Nivel");

	}


	public void startErrado(string _text){
		anim.enabled = true;
	
		parabensText.text = _text;
		//parabensText.text = parabensTexts[Random.Range(0, parabensTexts.Length)];
		//tempNumParabens=1;
		tempAcertoErro=2;
		//anim.SetInteger("numParabens",tempNumParabens);
		anim.SetInteger(acertoErroHash, tempAcertoErro);
		StartCoroutine(TempoAnim());
		//Debug.Log("Mudança de Nivel");

	}
		IEnumerator TempoAnim(){
        if (chekcBT) {
            panelblok.SetActive(true);
        }
      //  panelblok.SetActive(true);

        if (tempNumParabens == 1) {
			if (hasCustomWaitTime) {
				yield return new WaitForSeconds (customWaitTime);
			} else {
				yield return new WaitForSeconds (2f);
			}
			tempNumParabens = 0;
			anim.SetInteger (numParabensHash, tempNumParabens);
				

		}
		if (tempAcertoErro == 1 || tempAcertoErro == 2) {
			if (hasWaitTimeRightNWrong) {
				yield return new WaitForSeconds (waitTimeCustomized);
			} else {
				yield return new WaitForSeconds (2f);
			}
			tempAcertoErro = 0;
			anim.SetInteger (acertoErroHash, tempAcertoErro);
				
		}
		anim.enabled = true;
		tempAcertoErro = 0;
		anim.SetInteger (acertoErroHash, tempAcertoErro);
		tempNumParabens = 0;
		anim.SetInteger (numParabensHash, tempNumParabens);
		yield return new WaitForSeconds (1f);
        if (canvasON != null) {
            canvasON.GetComponent<GraphicRaycaster>().enabled = true;
        }
		anim.enabled = false;
        if (chekcBT) {
            panelblok.SetActive(false);
        }

    }
}
