using UnityEngine;

public class SlowDownItem1_3 : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Componentes")]
	#endif
	public BoxCollider2D thisCollider2D;
	public Manager1_3A manager;

	void Awake(){
		if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}
	}

	#region OnValidade

	/*void OnValidate(){
		/*if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}
	}*/
	#endregion


	#region Methods

	public void CatchItem(){
		manager.ReceiveSlowItem();
		this.gameObject.SetActive(false);
	}

	void OnBecameVisible(){
		thisCollider2D.enabled = true;
	}

	void OnBecomeInvisible(){
		if (this.isActiveAndEnabled) {
			this.gameObject.SetActive(false);
		}
	}

	#endregion

	#region Collision And Trigger

	void OnTriggerEnter2D(Collider2D other) {
		if (thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
			CatchItem();
		}
	}

	#endregion

}
