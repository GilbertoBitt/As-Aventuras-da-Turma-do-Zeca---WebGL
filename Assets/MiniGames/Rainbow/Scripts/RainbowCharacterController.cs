using UnityEngine;

public class RainbowCharacterController : MonoBehaviour {

	[Range(0f,10f)]
	public float speed = 5f;
	//public bool isRight = true;
	public float xScale = 0.5f;


	private Rigidbody2D playerRigidBody;
	UnityStandardAssets._2D.PlatformerCharacter2D plataformCharacter2D;

	[Header("References")]
	public RainbowController controller;

	
	// Use this for initialization
	void Start () {
		controller = Camera.main.GetComponent<RainbowController>();
		playerRigidBody = this.GetComponent<Rigidbody2D> ();
		//xScale = playerTransform.localScale.x;
		//Invoke ("activeKinematic", 1f);
		plataformCharacter2D = this.GetComponent <UnityStandardAssets._2D.PlatformerCharacter2D>();
	}
	



void activeCorrotine(){
	StartCoroutine (controller.TimeShow ());
		
}
void activeSomEstrela(){
controller.somEstrela();

}
	void activeKinematic(){
		playerRigidBody.isKinematic = true;
	}

	/*void OnCollisionEnter2D(Collider2D col){
		if (controller.lunch.Contains (col.gameObject)) {
			
		} else if (controller.joys.Contains (col.gameObject)) {
			
		}else if (controller.manners.Contains (col.gameObject)) {
			
		}else if (controller.knowledge.Contains (col.gameObject)) {
		
		}else if (controller.study.Contains (col.gameObject)) {
		
		}else if (controller.grades.Contains (col.gameObject)) {
			
		}else if (controller.friendship.Contains (col.gameObject)) {
			
		}else if (controller.bonusItems.Contains (col.gameObject)) {
			
		}
	}
	
	IEnumerator kinematic(){
		yield return WaitForSeconds (0.5f);
	}*/

	void moveSpeedVariation(){
		/*if (controller.energyBar.value <= 25) {
			speed = controller.currentLevel.speedVariations [0];
		} else if(controller.energyBar.value > 25 && controller.energyBar.value <= 50) {
			speed = controller.currentLevel.speedVariations [1];
		}else if(controller.energyBar.value > 50 && controller.energyBar.value <= 75) {
			speed = controller.currentLevel.speedVariations [2];			
		}else if(controller.energyBar.value > 75 && controller.energyBar.value <= 100) {
			speed = controller.currentLevel.speedVariations [3];			
		}*/

		int maxValue = (int)controller.energyBar.maxValue;
		int deltaX = maxValue / controller.currentLevel.speedVariations.Length;

		for (int i = 0; i < controller.currentLevel.speedVariations.Length; i++) {
			if ((controller.energyBar.value > deltaX*i) && (controller.energyBar.value <= deltaX*(i+1))) {
				speed = controller.currentLevel.speedVariations [i];
				/*float first = deltaX * i;
				float seconds = deltaX * (i + 1);
				//Debug.Log ("if ((" + controller.energyBar.value + ">" + first + ") && (" + controller.energyBar.value + "<=" + seconds + ")))");*/
			}
		}


	//playerRigidBody.velocity = new Vector2 (speed, playerRigidBody.velocity.y);
	}


	



}
