using System.Collections;
using UnityEngine;

public class ExpressFacialAmigos : MonoBehaviour {
	// 0 zeca
	// 1 Tati
	// 2 Paulo
	// 3 Manu
	// 4 Joao
	// 5 Bia

	public GameObject[] OlhosFechados;

	public GameObject[] Sobrancelha;

	public GameObject[] bocaFeliz;
	public GameObject[]bocaTriste;
	float timeM;
	public float[] myTimeM;
	int  NumTime;

	public float[] myTimeBoca;
	int  NumTimeBoca;
	float timeBoca;
    bool checkOlhos;
	void Start () {
        for (int i = 0; i < OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(false);
            }
        }
        for (int i = 0; i < Sobrancelha.Length; i++) {
            if (this.Sobrancelha[i] != null) {
                this.Sobrancelha[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaTriste.Length; i++) {
            if (this.bocaTriste[i] != null) {
                this.bocaTriste[i].SetActive(false);
            }
        }
       		
		timeM = Time.realtimeSinceStartup;

	}

	// Update is called once per frame
	void Update () {
		//if (Time.realtimeSinceStartup - timeM > myTimeM[NumTime] && OlhosFechados.activeInHierarchy==false) {
			if (Time.realtimeSinceStartup - timeM > myTimeM[NumTime] && checkOlhos==false) {
            checkOlhos = true;
            facePiscar ();
			timeM = Time.realtimeSinceStartup;
			NumTime = Random.Range(0,myTimeM.Length);
		}
	
	}
	void AbrirBoca() {

        Invoke("abrirBocaIV", 1f);

	}

    void abrirBocaIV() {
        for (int i = 0; i < this.bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(false);
            }
        }
    }
	void facePiscar() {
      
        for (int i = 0; i < this.OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(true);
            }
        }
        Invoke("facePiscarIv", 0.5f);
    }
    void facePiscarIv() {
        for (int i = 0; i < this.OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(false);
            }
        }
        checkOlhos = false;
    }

    public void SorrindoOn () {
		int sobrancelha = Random.Range (0,1);
		
        if (sobrancelha == 0) {
            for (int i = 0; i < this.Sobrancelha.Length; i++) {
                if (this.Sobrancelha[i] != null) {
                    this.Sobrancelha[i].SetActive(false);
                }
              
            }
        }
        for (int i = 0; i < this.bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) { 
            this.bocaFeliz[i].SetActive(true);
            }
        }

        Invoke("SorrindoOnIV", 1.5f);

    }
    void SorrindoOnIV() {
        for (int i = 0; i < Sobrancelha.Length; i++) {
            if (this.Sobrancelha[i] != null) {
                this.Sobrancelha[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(false);
            }
        }

    }
    public IEnumerator SorrindoOff () {
		int sobrancelha = Random.Range (0,1);
		if (sobrancelha == 0) {
			foreach (var item in Sobrancelha) {
				item.SetActive (false);
			}
		}
		foreach (var item in bocaFeliz) {
			item.SetActive (false);
		}
		foreach (var item in OlhosFechados) {
			item.SetActive (true);
		}
		yield return Yielders.Get (0.5f);
		foreach (var item in bocaFeliz) {
			item.SetActive (true);
		}
		foreach (var item in Sobrancelha) {
			item.SetActive (false);
		}
		foreach (var item in OlhosFechados) {
			item.SetActive (false);
		}
		foreach (var item in bocaFeliz) {
			item.SetActive (false);
		}

	}
	public void TristeOn () {
		int sobrancelha = Random.Range (0,1);
		if (sobrancelha == 0) {
            for (int i = 0; i < Sobrancelha.Length; i++) {
                if (this.Sobrancelha[i] != null) {
                    this.Sobrancelha[i].SetActive(false);
                }
            }
        }
		
        for (int i = 0; i < OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(true);
            }
        }
       
        for (int i = 0; i < bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaTriste.Length; i++) {
            if (this.bocaTriste[i] != null) {
                this.bocaTriste[i].SetActive(true);
            }
        }

        Invoke("TristeOnIf", 1.5f);

    }
    void TristeOnIf () {
        for (int i = 0; i < OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(false);
            }
        }
        for (int i = 0; i < Sobrancelha.Length; i++) {
            if (this.Sobrancelha[i] != null) {
                this.Sobrancelha[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(false);
            }
        }
        for (int i = 0; i < bocaTriste.Length; i++) {
            if (this.bocaTriste[i] != null) {
                this.bocaTriste[i].SetActive(false);
            }
        }
    }

    public void TristeOff () {
		int sobrancelha = Random.Range (0,1);
		
        if (sobrancelha == 0) {
            for (int i = 0; i < Sobrancelha.Length; i++) {
                if (this.Sobrancelha[i] != null) {
                    this.Sobrancelha[i].SetActive(false);
                }
            }
        }

        for (int i = 0; i < OlhosFechados.Length; i++) {
            if (this.OlhosFechados[i] != null) {
                this.OlhosFechados[i].SetActive(false);
            }
        }

        for (int i = 0; i < bocaFeliz.Length; i++) {
            if (this.bocaFeliz[i] != null) {
                this.bocaFeliz[i].SetActive(true);
            }
        }
        for (int i = 0; i < bocaTriste.Length; i++) {
            if (this.bocaTriste[i] != null) {
                this.bocaTriste[i].SetActive(false);
            }
        }

        Invoke("TristeOnIf", 0.5f);

    }



}

