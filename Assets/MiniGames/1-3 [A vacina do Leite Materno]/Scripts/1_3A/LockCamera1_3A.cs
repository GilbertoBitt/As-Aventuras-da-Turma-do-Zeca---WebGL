using UnityEngine;
using UnityEngine.Events;

public class LockCamera1_3A : MonoBehaviour {

	public UnityEvent OnPlayerCollisionEnter;
	public Manager1_3A manager2D;
	public Collider2D thisCollider2D;

	// Use this for initialization
	void Start () {
		thisCollider2D = this.GetComponent<Collider2D>();
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (thisCollider2D.IsTouching(manager2D.playerCollider)) {
			OnPlayerCollisionEnter.Invoke();
		}

	}


}
