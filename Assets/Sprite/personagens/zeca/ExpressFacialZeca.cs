using UnityEngine;

public class ExpressFacialZeca : MonoBehaviour {

	public GameObject OlhosFechados;

	public GameObject Sobrancelha;

	public GameObject bocaAberta;
	public GameObject bocafechadaT;
	float timeM;
    float timeM2;
    public float[] myTimeM;
	int  NumTime;

	public float[] myTimeBoca;
	int  NumTimeBoca;
	float timeBoca;
    public bool checkFalando;
    public bool falar;
    public bool falarTriste;

    void Start () {
        this.OlhosFechados.SetActive (false);
        if (Sobrancelha != null) {
            this.Sobrancelha.SetActive(false);
        }
        if (bocaAberta != null) {
            this.bocaAberta.SetActive(false);
        }
        if (bocafechadaT != null) {
            this.bocafechadaT.SetActive(false);
        }
		timeM = Time.time;
        timeM2 = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timeM > myTimeM[NumTime] && OlhosFechados.activeInHierarchy==false) {
		facePiscar ();

			timeM = Time.time;
			NumTime = Random.Range(0,myTimeM.Length);
		}
       
            if (falar && !falarTriste && Time.time - timeM2 > myTimeBoca[NumTimeBoca] && bocaAberta.activeInHierarchy == false && checkFalando)
            {

            SorrindoOn();

                timeM2 = Time.time;
                NumTimeBoca = Random.Range(0, myTimeBoca.Length);
            }
        if (falar && falarTriste && Time.time - timeM2 > myTimeBoca[NumTimeBoca] && bocaAberta.activeInHierarchy == false && checkFalando)
        {

            TristeOn();

            timeM2 = Time.time;
            NumTimeBoca = Random.Range(0, myTimeBoca.Length);
        }



    }

    public void falandoOn()
    {
        checkFalando = true;
    }
    public void falandoOff()
    {
        checkFalando = false;
    }
    void AbrirBoca() {

        Invoke("AbrirBocaIv", 0.5f);

    }
	void AbrirBocaIv() {
        this.bocaAberta.SetActive(false);
    }
    void facePiscar() {
        if (this.OlhosFechados != null)
            this.OlhosFechados.SetActive(true);
        Invoke("facePiscarIv", 0.5f);
    }
    void facePiscarIv() {       
        if (this.OlhosFechados != null)
            this.OlhosFechados.SetActive(false);
    }
    public void SorrindoOn () {
		int sobrancelha = Random.Range (0,1);
       
        if (sobrancelha == 0 && Sobrancelha != null) {
			//Sobrancelha.SetActive (true);
		}
        this.bocaAberta.SetActive (true);
        Invoke("SorrindoOnIV", 0.5f);

    }
    void SorrindoOnIV() {
        this.bocaAberta.SetActive(false);
        if (Sobrancelha != null)
        {
            this.Sobrancelha.SetActive(false);
        }
      

    }
    public void SorrindoOff () {
		int sobrancelha = Random.Range (0,1);
		if (sobrancelha == 0 && Sobrancelha != null) {
            this.Sobrancelha.SetActive (false);
		}
		bocaAberta.SetActive (false);
        this.OlhosFechados.SetActive (true);
        Invoke("SorrindoOffIV", 0.5f);
		

	}
    void SorrindoOffIV() {
        this.bocaAberta.SetActive(true);
        if (this.Sobrancelha != null)
        {
            this.Sobrancelha.SetActive(true);
        }
       
        this.OlhosFechados.SetActive(false);
        this.bocaAberta.SetActive(false);

    }
	public void TristeOn () {
		int sobrancelha = Random.Range (0,1);
		if (sobrancelha == 0 && Sobrancelha != null) {
            //this.Sobrancelha.SetActive (true);
		}
        this.bocafechadaT.SetActive (true);
        this.OlhosFechados.SetActive (true);
        this.bocaAberta.SetActive (false);
        Invoke("TristeOnIV", 1.5f);

    }
    public void TristeOnDebte() {
        int sobrancelha = Random.Range(0, 1);
        if (sobrancelha == 0 && Sobrancelha != null) {
            //this.Sobrancelha.SetActive (true);
        }
        this.bocafechadaT.SetActive(true);
     //   Debug.Log("triste");
      //  this.OlhosFechados.SetActive(true);
      //  this.bocaAberta.SetActive(false);
//Invoke("TristeOnIV", 1.5f);

    }

    public void TristeOffDebte() {
        int sobrancelha = Random.Range(0, 1);
        if (sobrancelha == 0 && Sobrancelha != null) {
            //this.Sobrancelha.SetActive (true);
        }
        this.bocafechadaT.SetActive(false);
      //  Debug.Log("alegre");
        //  this.OlhosFechados.SetActive(true);
        //  this.bocaAberta.SetActive(false);
        // Invoke("TristeOnIV", 1.5f);

    }
    void TristeOnIV() {
        this.bocafechadaT.SetActive(false);
        if (this.Sobrancelha != null)
        {
            this.Sobrancelha.SetActive(false);
        }
        this.OlhosFechados.SetActive(false);
        this.bocaAberta.SetActive(false);

    }
    public void TristeOff () {
		int sobrancelha = Random.Range (0,1);
		if (sobrancelha == 0 && Sobrancelha != null) {
            this.Sobrancelha.SetActive (false);
		}
        this.bocafechadaT.SetActive (false);
        this.OlhosFechados.SetActive (false);
        this.bocaAberta.SetActive (true);
        Invoke("TristeOffIV", 0.5f);

    }
    void TristeOffIV() {
        this.bocafechadaT.SetActive(false);
        if (this.Sobrancelha != null)
        {
            this.Sobrancelha.SetActive(false);
        }
        this.OlhosFechados.SetActive(false);
        this.bocaAberta.SetActive(false);

    }

}
