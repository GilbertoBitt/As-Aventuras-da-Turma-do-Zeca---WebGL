using UnityEngine;
using System.Collections.Generic;
using MEC;

public class RainbowCoin : OverridableMonoBehaviour {

	public RainbowController controller;
	private Vector2 curveFall;
	private Vector2 actualCurve;

	public GameObject part;

	bool stopCair;
	 Color[] corLuz = new Color[7];

	// Use this for initialization
	void Start () {
		corLuz[0] = new Vector4(0, 0, 1, 1);
		corLuz[1] = new Vector4(0, 1, 1, 1);
		corLuz[2] = new Vector4(0, 1, 0, 1);
		corLuz[3] = new Vector4(1, 0, 1, 1);
		corLuz[4] = new Vector4(0, 1, 1, 1);
		corLuz[5] = new Vector4(1, 0, 0, 1);
		corLuz[6] = new Vector4(1, 0.9f, 0.016f, 1);	


		controller = Camera.main.GetComponent<RainbowController> ();
		curveFall = controller.currentLevel.curveOfFall;
	}

    // Update is called once per frame
    public override void UpdateMe() {
      if(stopCair==false && controller != null && controller.currentLevel != null) {

		float xDelta = Mathf.Lerp (curveFall.x, 0f, controller.currentLevel.curveXDeltaSpeed * Time.deltaTime);
		curveFall.x = xDelta;
		Vector3 newPos = new Vector3 (transform.position.x + curveFall.x, transform.position.y + curveFall.y, transform.position.z);
		Vector3 newPosition = Vector3.Lerp (transform.position, newPos, controller.currentLevel.curveOfFallTime * Time.deltaTime);
		transform.position = newPosition;
	  }
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.CompareTag("Ground")) {
			Timing.RunCoroutine(TempCair());
			GetComponent<SpriteRenderer>().enabled=false;
			GetComponent<CircleCollider2D>().enabled=false;
			Destroy (gameObject,1);
		//	GameObject obj = Instantiate(part, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		//	obj.transform.SetParent(this.transform);
		//	obj.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		//	int corN = Random.Range(0,corLuz.Length);
		//	obj.GetComponent<ParticleSystem>().startColor = corLuz[corN];
		//	obj.transform.localScale = new Vector3(1,1,1);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag("Ground")) {			
			Timing.RunCoroutine(TempCair());
			//GetComponent<SpriteRenderer>().enabled=false;
			//GetComponent<CircleCollider2D>().enabled=false;
			Destroy (this.gameObject);
			//	GameObject obj = Instantiate(part, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			//	obj.transform.SetParent(this.transform);
			//	obj.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			//	int corN = Random.Range(0,corLuz.Length);
			//	obj.GetComponent<ParticleSystem>().startColor = corLuz[corN];
			//	obj.transform.localScale = new Vector3(1,1,1);
		}
	}

	void OnBecomeInvisible(){
		Timing.RunCoroutine(TempCair());
		GetComponent<SpriteRenderer>().enabled=false;
		GetComponent<CircleCollider2D>().enabled=false;
		Destroy (gameObject,1);
	}

	IEnumerator<float> TempCair(){

		yield return Timing.WaitForSeconds(0.1f);
		stopCair=true;
	}

	public void EndAllCoroutines(){
        Timing.KillCoroutines();
	}
	
}
