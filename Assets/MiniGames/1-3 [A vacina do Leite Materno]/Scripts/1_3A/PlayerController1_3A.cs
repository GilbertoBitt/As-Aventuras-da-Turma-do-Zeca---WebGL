using UnityEngine;

public class PlayerController1_3A : MonoBehaviour {

	public float forwardForce;
	public float upForce;
	public Rigidbody2D thisRigidBody;
	public CapsuleCollider2D capsuleCollider2D;
	public LayerMask layerMask;
	public Camera cameraFollow;
	public bool isOnGround;
	public bool isJumping;
	public int countJump = 0;
	public int maxJumpCount = 2;
	// Use this for initialization
	void Start () {
		cameraFollow = Camera.main;
		//thisRigidBody = this.GetComponent<Rigidbody2D>();
		//capsuleCollider2D = this.GetComponent<CapsuleCollider2D>();
		countJump = 0;
		isOnGround = true;
		isJumping = false;
	}
	
	// Update is called once per frame
	/*void Update () {
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
			cameraFollow.transform.position = new Vector3(cameraFollow.transform.position.x + speed, cameraFollow.transform.position.y, cameraFollow.transform.position.z);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
			cameraFollow.transform.position = new Vector3(cameraFollow.transform.position.x - speed, cameraFollow.transform.position.y, cameraFollow.transform.position.z);
		}

	}*/

	void FixedUpdate(){
		
		thisRigidBody.AddForce(Vector2.left * forwardForce, ForceMode2D.Impulse);
		if (Input.GetKeyDown(KeyCode.Space) && countJump < maxJumpCount) {
			countJump++;
			thisRigidBody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (capsuleCollider2D.IsTouchingLayers(layerMask)) {
			countJump = 0;
		}
	}
}
