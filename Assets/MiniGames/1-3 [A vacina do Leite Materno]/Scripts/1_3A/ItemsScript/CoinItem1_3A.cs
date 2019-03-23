using UnityEngine;

public class CoinItem1_3A : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Principal")]
	#endif
	public int coinValue;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Componentes")]
	#endif
	public CircleCollider2D thisCollider2D;
	public Manager1_3A manager;
    public GameObject partivulaMoeda;
    GameObject partivulaMoedaR;
    SpriteRenderer spriteR;
    float tranfY;
    public float valorF;
    public float valorDivide;
    bool checkIni;

    #region OnValidade

    void Awake(){
        spriteR = GetComponent<SpriteRenderer>();
        tranfY = transform.position.y;

        if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<CircleCollider2D>();
		}
        Invoke("TimeOBJ", 2f);

    }

    /*void OnValidate(){
		if (manager == null) {
			manager = Camera.main.GetComponent<Manager1_3A>();
		}
		if (thisCollider2D == null) {
			thisCollider2D = this.GetComponent<BoxCollider2D>();
		}
	}*/
    #endregion
    void TimeOBJ()
    {
        tranfY = transform.position.y;
        checkIni = true;

    }
    /*
    private void Update()
    {
        if (checkIni)
        {
            transform.position = new Vector3(transform.position.x, tranfY + (Mathf.PingPong(Time.time / valorDivide, valorF) - (valorF) / 2f), transform.position.z);
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (Mathf.PingPong(Time.time, .5f) - .5f / 2f));
            transform.rotation = rotation;
        }

    }
    */
    #region Methods

    public void CatchItem(){
		manager.CoinCatch(coinValue);
        spriteR.enabled = false;
        thisCollider2D.enabled = false;
        //this.gameObject.SetActive(false);
    }

	void OnBecameVisible(){
		thisCollider2D.enabled = true;
	}

	void OnBecomeInvisible(){
		this.gameObject.SetActive(false);
	}

	#endregion

	#region Collision And Trigger

	void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") == true)
        {
            CatchItem();
            //partivulaMoedaR = Instantiate(partivulaMoeda, transform.position, Quaternion.identity) as GameObject;
            //partivulaMoedaR.transform.SetParent(this.transform);
            //partivulaMoedaR.transform.localScale = new Vector3(partivulaMoeda.transform.localScale.x, partivulaMoeda.transform.localScale.y , partivulaMoeda.transform.localScale.z);
            //Destroy(partivulaMoedaR, 1f);
            //Destroy(gameObject, 1.5f);

            ParticleSystem particleCoin = manager.GetParticulaFromPool(this.transform, partivulaMoeda.transform.localScale, transform.position);
            particleCoin.Play();


        }

        /*
        if (thisCollider2D.IsTouching(manager.BoxCollider2DPlayer)) {
			CatchItem();
		}
        */
	}

	#endregion

}
