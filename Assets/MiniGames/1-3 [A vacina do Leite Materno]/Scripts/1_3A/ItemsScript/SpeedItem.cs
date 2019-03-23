using UnityEngine;

public class SpeedItem : OverridableMonoBehaviour {

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Principal")]
	#endif
	public float increaseSpeedTo;
	public float timeOfEffect;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Componentes")]
	#endif
	public BoxCollider2D thisCollider2D;
	public Manager1_3A manager;
    public PresetPlataforms1_3 PresetPlataforms1_3s;

    public float tranfY;
    public float tranfRY;
    public GameObject part;
    GameObject obj;
    bool checkIni;
    public SpriteRenderer[] objsSpr;
    public ParticleSystem[] objsPart;
    public int numbPlat;

    protected override void Awake() {
        base.Awake();
        if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}
        PresetPlataforms1_3s = this.transform.parent.GetComponentInParent<PresetPlataforms1_3>();
        numbPlat = PresetPlataforms1_3s.numbPlataform;
        //if (manager.numbCont == PresetPlataforms1_3s.numbPlataform && PresetPlataforms1_3s.numbRandom == 2) {
        if (this.numbPlat == 4 || this.numbPlat == 10 || this.numbPlat == 18 || this.numbPlat > 26) {
            //Destroy(gameObject);
          

        } 
        else {
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.GetComponent<SpriteRenderer>());
            DestroyOBJ();
            this.gameObject.SetActive(false);

        }

       
        Invoke("TimeOBJ", 2f);
       // Invoke("DestroyOBJ", 2.1f);

    }

    void DestroyOBJ() {
        for (int i = 0; i < objsSpr.Length; i++) {
            Destroy(objsSpr[i]);
        }
        for (int i = 0; i < objsPart.Length; i++) {
            Destroy(objsPart[i]);
        }
    }

    void TimeOBJ() {
        tranfY = transform.position.y;
        checkIni = true;
     

    }
	#region OnValidade

	void OnValidate(){
		/*if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}*/
	}
    #endregion

    #region Update
    public override void UpdateMe() {
        if (checkIni) {
            transform.position = new Vector3(transform.position.x, tranfY + (Mathf.PingPong(Time.time / 20, 0.15f) - 0.15f / 2f), transform.position.z);


            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (Mathf.PingPong(Time.time, .5f) - .5f / 2f));
            transform.rotation = rotation;
        }
      
        //rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.PingPong(Time.time*30f, 10));
        // transform.rotation = rotation;
        
    }
    #endregion

    #region Methods

    public void CatchItem(){
		manager.SpeedItem(8, timeOfEffect);
        obj = Instantiate(part, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        obj.transform.SetParent(this.transform.parent);
        gameObject.SetActive(false);
        Invoke("DestroyMet",2f);
	}
    void DestroyMet() {
        Destroy(obj);
        Destroy(gameObject);
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
        if (other.CompareTag("Player") == true) {
            CatchItem();
        }

       /* if (thisCollider2D.IsTouching(manager.BoxCollider2DPlayer) || thisCollider2D.IsTouching(manager.playerCollider)) {
            Debug.Log("Item SPeed");
			CatchItem();
		}
        */
	}
   

    #endregion

}
