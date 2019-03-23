using UnityEngine;

public class FlyItem1_3 : OverridableMonoBehaviour {

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Principal")]
#endif
    public float timeOfEffect;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Componentes")]
#endif
    public BoxCollider2D thisCollider2D;
    public Manager1_3A manager;
    public PresetPlataforms1_3 PresetPlataforms1_3s;
    public int numbFly;

    float tranfY;
    float tranfRY;
    public GameObject part;
    GameObject obj;
    bool checkIni;
    public SpriteRenderer[] objsSpr;
    // Quaternion rotation;



    protected override void Awake(){
        base.Awake();
		if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}
        PresetPlataforms1_3s = this.transform.parent.GetComponentInParent<PresetPlataforms1_3>();
        numbFly = PresetPlataforms1_3s.numbPlataform;
        
        if (this.numbFly == 7 || this.numbFly == 14 || this.numbFly == 21 || this.numbFly > 27)
        {
              
        }
        
        else {
          
           Destroy(gameObject);
           Destroy(this.GetComponent<Collider2D>());
          Destroy(this.GetComponent<SpriteRenderer>());

            for (int i = 0; i < objsSpr.Length; i++) {
             Destroy(objsSpr[i]);
            }

        }
        
        Invoke("TimeOBJ", 2f);


    }
    void TimeOBJ()
    {
        tranfY = transform.position.y;
        checkIni = true;

    }

    public override void UpdateMe() {
        if (checkIni) {
            transform.position = new Vector3(transform.position.x, tranfY + (Mathf.PingPong(Time.time / 6, 0.5f) - 0.5f / 2f), transform.position.z);
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (Mathf.PingPong(Time.time * 2, .5f) - .5f / 2f));
            transform.rotation = rotation;
        }

    }

    #region OnValidade

    #endregion


    #region Methods

    public void CatchItem(){
		manager.StartFly(timeOfEffect);
        obj = Instantiate(part, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        obj.transform.SetParent(this.transform.parent);
        this.gameObject.SetActive(false);
	}
    void DestroyMet()
    {
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
            if (thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
			CatchItem();
		}
	}

	#endregion
}
