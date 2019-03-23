using UnityEngine;

public class SelPersons : MonoBehaviour {

	// 0 zeca
	// 1 Tati
	// 2 Paulo
	// 3 Manu
	// 4 Joao
	// 5 Bia

	public int PersonSel;
	public GameObject personZeca;
	public GameObject AmigosZeca;
	public GameObject personTatiBia;
	public GameObject spinerTatiBia;
	public GameObject personTati;
	public GameObject personBia;
	public GameObject personPaulo;
	public GameObject personJoaoManu;
	public GameObject spinerJoaoManu;
	public GameObject personManu;
	public GameObject personJoao;
	//public GameObject posBalde;
	public bool activeSel;
    public bool selDel;
    public Collider2D[] collVoandoM;
    public bool checkVacine;

    //	public Transform posInicial;

    public bool checkDanece;
    public GameObject[] limbZeca;
    public GameObject[] limbPaulo;
    public GameObject[] limbBiaTati;
    public GameObject[] limbJoaoManu;

   


    public void Start () {      

        Invoke("selPerson", 0.25f);
	}
    void selPerson() {

        if (activeSel) {
            PersonSel = PlayerPrefs.GetInt("characterSelected", 0);
        

        }
        if (personZeca != null) {
            personZeca.SetActive(false);
        }
        if (AmigosZeca != null) {
            AmigosZeca.SetActive(false);
        }
        if (personTatiBia != null) {
            personTatiBia.SetActive(false);
        }
        if (spinerTatiBia != null) {
            spinerTatiBia.SetActive(false);
        }
        if (personTati != null) {
            personTati.SetActive(false);
        }
        if (personBia != null) {
            personBia.SetActive(false);
        }
        if (personPaulo != null) {
            personPaulo.SetActive(false);
        }
        if (spinerJoaoManu != null) {
            spinerJoaoManu.SetActive(false);
        }
        if (personManu != null) {
            personManu.SetActive(false);
        }
        if (personJoao != null) {
            personJoao.SetActive(false);
        }
        if (PersonSel == 0) {
            personZeca.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        personZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }
            if (checkDanece) {
                for (int i = 0; i < limbZeca.Length; i++) {
                    limbZeca[i].SetActive(true);
                }

            }
            if (selDel) {
                Destroy(AmigosZeca,0.5f);
            }
        } else if (PersonSel == 1) {
            AmigosZeca.SetActive(true);
            personTatiBia.SetActive(true);
            personTati.SetActive(true);
            spinerTatiBia.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        AmigosZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }
            if (checkDanece) {
                for (int i = 0; i < limbBiaTati.Length; i++) {
                    limbBiaTati[i].SetActive(true);
                }

            }
            if (selDel) {
                Destroy(personZeca, 0.2f);
                Destroy(personPaulo, 0.3f);
                Destroy(personJoaoManu, 0.4f);
                Destroy(personBia, 0.5f);
            }
        } else if (PersonSel == 2) {
            AmigosZeca.SetActive(true);
            personPaulo.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        AmigosZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }

            if (checkDanece) {
                for (int i = 0; i < limbPaulo.Length; i++) {
                    limbPaulo[i].SetActive(true);
                }

            }
            if (selDel) {
                Destroy(personZeca, 0.2f);
                Destroy(personTatiBia, 0.3f);
                Destroy(personJoaoManu, 0.4f);
            }
        } else if (PersonSel == 3) {
            AmigosZeca.SetActive(true);
            personJoaoManu.SetActive(true);
            spinerJoaoManu.SetActive(true);
            personManu.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        AmigosZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }
            if (checkDanece) {
                for (int i = 0; i < limbJoaoManu.Length; i++) {
                    limbJoaoManu[i].SetActive(true);
                }

            }
            if (selDel) {
                Destroy(personZeca, 0.2f);
                Destroy(personPaulo, 0.3f);
                Destroy(personTatiBia, 0.4f);
                Destroy(personJoao, 0.5f);
            }
        } else if (PersonSel == 4) {
            AmigosZeca.SetActive(true);
            personJoaoManu.SetActive(true);
            spinerJoaoManu.SetActive(true);
            personJoao.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        AmigosZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }
            if (checkDanece) {
                for (int i = 0; i < limbJoaoManu.Length; i++) {
                    limbJoaoManu[i].SetActive(true);
                }

            }
            if (selDel) {
                Destroy(personZeca, 0.2f);
                Destroy(personPaulo, 0.3f);
                Destroy(personTatiBia, 0.4f);
                Destroy(personManu, 0.5f);
            }
            if (personJoaoManu.activeInHierarchy == false) {
                personJoaoManu.SetActive(true);
            }
        } else if (PersonSel == 5) {
            AmigosZeca.SetActive(true);
            personTatiBia.SetActive(true);
            spinerTatiBia.SetActive(true);
            personBia.SetActive(true);
            if (checkVacine) {
                for (int i = 0; i < collVoandoM.Length; i++) {
                    if (collVoandoM[i] != null) {
                        AmigosZeca.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().collVoando = collVoandoM[i];
                    }
                }
            }
            if (checkDanece) {
                for (int i = 0; i < limbBiaTati.Length; i++) {
                    limbBiaTati[i].SetActive(true);
                }
            }
            if (selDel) {
                Destroy(personZeca, 0.2f);
                Destroy(personPaulo, 0.3f);
                Destroy(personJoaoManu, 0.4f);
                Destroy(personTati, 54f);
            }
        }     
    }
}
