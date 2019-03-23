using System.Collections.Generic;
using UnityEngine;
using MEC;

public class ControlPassaro : MonoBehaviour {

	public float movementTime;
	public ControlInstPassaro ControlInstPassaro;
	RectTransform posIni;
	RectTransform posPousar;
	Transform posFim;
	public bool parou;
	public bool dirpref;
	int movP;
	bool checkPousar;
	bool checkFim;
	int numPosFim;
	bool subPass;
	int numPosPouso;
	float tamPass;
    Transform _transform;
    Vector3 newPos;

    static readonly int ParadoPassaroHash = Animator.StringToHash("ParadoPassaro");


    void Start () {
		tamPass = ControlInstPassaro.tamPass;
		numPosPouso = Random.Range(0,ControlInstPassaro.posPousarM.Length);
		int numPosIni = Random.Range(0,ControlInstPassaro.posIniM.Length);
		posIni = ControlInstPassaro.GetComponent<RectTransform>();
		posPousar = ControlInstPassaro.posPousarM[numPosPouso];
        //posFim = ControlInstPassaro.posFimC[numPosFim];
        //posIni = ControlInstPassaro.posIniM[0];
        _transform = transform;
        _transform.position = ControlInstPassaro.posIniM[numPosIni].transform.position;
        _transform.localScale = new Vector2(tamPass, tamPass);
		if (numPosIni == 0) {
            _transform.localScale = new Vector2(tamPass, tamPass);
			
		} else {
            _transform.localScale = new Vector2(-tamPass, tamPass);
		}

		
	}

	// Update is called once per frame
	void Update () {
		if (ControlInstPassaro.Manager_1_2ALocal.destroyPref==true) {
			ControlInstPassaro.numbPass = 0;
			Destroy(gameObject);
		}

		else{
		if(movP==0){
            _transform.position = Vector3.Lerp (_transform.position, posPousar.transform.position, (Time.deltaTime * movementTime));			
		}

		if(movP==2){

                _transform.position = Vector3.Lerp (_transform.position, posFim.transform.position, (Time.deltaTime * movementTime));	
		
		}
			
		if (_transform.position.x > posPousar.transform.position.x-2f && dirpref) {
				parou = true;
		GetComponent<Animator>().SetBool(ParadoPassaroHash, parou);
		}
		else if (_transform.position.x < posPousar.transform.position.x+2f && dirpref==false && checkPousar == false) {
			checkPousar = true;
			Timing.RunCoroutine(TimeParou());
			}
		//GetComponent<Animator>().SetBool("ParadoPassaro", parou);
		if (checkFim == true && numPosFim==0) {
			if (_transform.position.x >= posFim.transform.position.x - 2f && dirpref == false) {
				if (subPass == false) {
					subPass = true;
					ControlInstPassaro.numbPass = ControlInstPassaro.numbPass - 1;
				}
					
			Destroy(gameObject, 2);


			}
		}
		else if (checkFim == true && numPosFim==1) {
			if (_transform.position.x <= posFim.transform.position.x + 2f && dirpref == false) {
				if (subPass == false) {
					subPass = true;
					ControlInstPassaro.numbPass = ControlInstPassaro.numbPass - 1;
				}

			Destroy(gameObject, 2);
			}
		}


		}

	}

	public IEnumerator<float> TimeParou(){
        float timeV2 = 0;

        yield return Timing.WaitForSeconds(7f);
        if (this != null) {
            GetComponent<Animator>().SetBool(ParadoPassaroHash, true);
            movP = 1;
            timeV2 = Random.Range(3f, 5f);
        }

		yield return Timing.WaitForSeconds(timeV2);
        if (this != null) {
            transform.localScale = new Vector2(tamPass * -1, tamPass);
            timeV2 = Random.Range(3f, 5f);
        }

		yield return Timing.WaitForSeconds(timeV2);
        if (this != null) {
            transform.localScale = new Vector2(tamPass * -1, tamPass);
        }

		float timeV = Random.Range(3f,5f);
		yield return Timing.WaitForSeconds(timeV);
        if (this != null) {
            GetComponent<Animator>().SetBool(ParadoPassaroHash, false);
        }

		yield return Timing.WaitForSeconds(0.8f);
        if (this != null) {
            numPosFim = Random.Range(0, ControlInstPassaro.posFimC.Length);
            if (numPosFim == 0) {
                _transform.localScale = new Vector2(-tamPass, tamPass);
            } else {
                _transform.localScale = new Vector2(tamPass, tamPass);
            }
        

		posFim = ControlInstPassaro.posFimC[numPosFim];
		checkFim = true;
        }
        yield return Timing.WaitForSeconds(0.1f);
		movP = 2;
			

	}
}

