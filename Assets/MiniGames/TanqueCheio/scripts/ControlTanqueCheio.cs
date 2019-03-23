using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using MEC;
using DG.Tweening;
using UnityEngine.Rendering;
public class ControlTanqueCheio :MonoBehaviour {
    public Transform panelBolaDestroy2;
    public Image panelFund;
    public GameObject instObj;
    public Transform posInst;
    public float forcaObj;
    public GameObject[] paiObjInst;
    public Transform[] recipientes;
    public SpriteRenderer[] corCenario;
    public Transform panelBolaDestroy;
    public Transform[] bolasDestroy;

    public float timeDestroyTotal;
    int quantRec;
    int numbRecp;
    public Text[] nameRecp;
    int numberBola;
    bool checkPauseInst;
    public int numeroLetras;
    public string alfabetoLetras;
    int corNumb;
    public Color[] partCor;
    public int numeroPassarLevel;
    int numbCerto;
    int numbErro;
    public bool destroyTudo;
    public Text[] AcerErro;
    public Transform[] acertoErroTrans;
    public List<string> alfabetoLista = new List<string>();
    public List<GameObject> objSceneTotal = new List<GameObject>();
    public List<GameObject> listVermelho = new List<GameObject>();
    public List<GameObject> listVerde = new List<GameObject>();
    public List<GameObject> listAmarelo = new List<GameObject>();
    public List<GameObject> listAzul = new List<GameObject>();

    public Vector3 posCamIni;

    int passouDestroyTotal;
    int passouDestroyCor;
    public bool destroyCor;
    public GameObject pergCerErr;
    public changeLevel changeLevel2;
    public int numbLevel;
    bool checkPassarLevel;
    bool movePassY;
    public bool moveRecp;
    public SpriteRenderer[] corCano;
    bool checkMove;
    public RectTransform panelPont;
    public Transform moeda;
    public Transform fundMoeda;

    void Start() {
        posCamIni = transform.position;
        acertoErroTrans[0] = AcerErro[0].gameObject.transform;
        acertoErroTrans[1] = AcerErro[1].gameObject.transform;
        quantRec = recipientes.Length;
        numbRecp = -1;
        alfabetoLetras = alfabetoLetras.Replace(" ", "");
        var array = new string[alfabetoLetras.Length];
        for (int i = 0; i < array.Length; i++) {
            array[i] = alfabetoLetras[i].ToString();
            // quantLetra = i;
            alfabetoLista.Add(alfabetoLetras[i].ToString());
        }
        alfabetoLista.Suffle();
        Invoke("ChamarRecp", 0.5f);
        Invoke("InstObs", 1);
        AcerErro[0].text = "" + numbCerto;
        AcerErro[1].text = "Erros: " + numbErro;


    }


    void InstObs() {
        GameObject clone = Instantiate(instObj, posInst.position, transform.rotation) as GameObject;
        clone.GetComponent<Rigidbody2D>().AddForce(this.posInst.up * (forcaObj * 100));
        objSceneTotal.Add(clone);
        clone.GetComponent<ObjEstourar>().ControlTanqueCheio2 = GetComponent<ControlTanqueCheio>();
        clone.GetComponent<ObjEstourar>().numberBolaLocal = numberBola;
        //Debug.Log(numeroLetras);
        clone.GetComponent<ObjEstourar>().letraBolha.text = alfabetoLista[numeroLetras];
        clone.GetComponent<ObjEstourar>().recebetext = alfabetoLista[numeroLetras];

        clone.GetComponent<ObjEstourar>().objSprBolha.color = partCor[corNumb];
        clone.GetComponent<ObjEstourar>().partCorObj = partCor[corNumb];
        // clone.GetComponent<ObjEstourar>().Sublinhado.GetComponent<TextMesh>().color = partCor[corNumb];
        // clone.GetComponent<ObjEstourar>().letraBolha.color = partCor[corNumb];
        clone.GetComponent<ObjEstourar>().luzBola.color = partCor[corNumb];
        clone.GetComponent<ObjEstourar>().corNumLocal = corNumb;
        if (corNumb == 0) {
            listVermelho.Add(clone);
        } else if (corNumb == 1) {
            listVerde.Add(clone);
        } else if (corNumb == 2) {
            listAmarelo.Add(clone);
        } else if (corNumb == 3) {
            listAzul.Add(clone);
        }
        if (corNumb < partCor.Length - 1) {
            corNumb = corNumb + 1;
        } else {

            corNumb = 0;
        }


        if (numeroLetras < alfabetoLista.Count - 1) {
            numeroLetras = numeroLetras + 1;

        } else {

            alfabetoLista.Suffle();
            numeroLetras = 0;
        }


        if (!checkPauseInst) {

            Invoke("InstObs", 2f);
        }
      
    }
    void Update() {
        if (Input.GetKey("up") && !checkPauseInst) {
            checkPauseInst = true;
            pergCerErr.SetActive(true);
            panelFund.DOFade(0.8f, 1);
            panelBolaDestroy.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
            /*for (int i = 0; i < recipientes.Length; i++) {
                recipientes[i].DOLocalMove(new Vector3(recipientes[i].localPosition.x, 5f, recipientes[i].localPosition.z), 0.25f, false);
            }
            */

        }
      

    }
    void ChamarRecp() {
        numbRecp = numbRecp + 1;
        if (numbRecp < recipientes.Length) {
            Timing.RunCoroutine(ChamarRecpTime());

        }


    }

    public void DestrotBolasTotal() {

       // transform.position = new Vector3 (posCamIni.x, posCamIni.y,posCamIni.z);
        destroyTudo = true;
        checkPauseInst = true;
        if (objSceneTotal.Count != 0) {
            objSceneTotal[0].GetComponent<ObjEstourar>().DestroyOBJ();
            //objSceneTotal.Remove(objSceneTotal[0]);
            //  timeDestroyTotal = timeDestroyTotal + 0.3f;
            Invoke("DestrotBolasTotal", 0.05f);
            passouDestroyTotal = 0;
        } else if (objSceneTotal.Count == 0 && passouDestroyTotal==0) {
            passouDestroyTotal = 1;
            Invoke("Voltarpos", 1f);

        }

    }
    void Voltarpos() {
        transform.DOLocalMove(posCamIni, 0.25f, false);

    }
    public void DestrotBolasVermelhas() {
        destroyCor = true;
       // checkPauseInst = true;
        if (listVermelho.Count != 0) {
            listVermelho[0].GetComponent<ObjEstourar>().DestroyOBJ();
            //objSceneTotal.Remove(objSceneTotal[0]);
         //   listVermelho.Remove(listVermelho[0]);
            //  timeDestroyTotal = timeDestroyTotal + 0.3f;
            Invoke("DestrotBolasVermelhas", 0.1f);
        } else {
            Voltarpos();
            Invoke("InstObs", 0.5f);
        }
    }
    public void DestrotBolasVerdes() {
        destroyCor = true;
        // checkPauseInst = true;
        if (listVerde.Count != 0) {
            listVerde[0].GetComponent<ObjEstourar>().DestroyOBJ();
            //objSceneTotal.Remove(objSceneTotal[0]);
           // listVerde.Remove(listVerde[0]);
            //  timeDestroyTotal = timeDestroyTotal + 0.3f;
            Invoke("DestrotBolasVerdes", 0.1f);
        } else {
            Voltarpos();
            Invoke("InstObs", 0.5f);
        }
    }
    public void DestrotBolasAmarelas() {
        destroyCor = true;
        // checkPauseInst = true;
        if (listAmarelo.Count != 0) {
            listAmarelo[0].GetComponent<ObjEstourar>().DestroyOBJ();
            //objSceneTotal.Remove(objSceneTotal[0]);
           // listAmarelo.Remove(listAmarelo[0]);
            //  timeDestroyTotal = timeDestroyTotal + 0.3f;
            Invoke("DestrotBolasAmarelas", 0.1f);
        } else {
            Voltarpos();
            Invoke("InstObs", 0.5f);
        }
    }
    public void DestrotBolasAzul() {
        destroyCor = true;
        // checkPauseInst = true;
        if (listAzul.Count != 0) {
            listAzul[0].GetComponent<ObjEstourar>().DestroyOBJ();
            //objSceneTotal.Remove(objSceneTotal[0]);
         //   listAzul.Remove(listAzul[0]);
            //  timeDestroyTotal = timeDestroyTotal + 0.3f;
            Invoke("DestrotBolasAzul", 0.1f);
        } else {
            Voltarpos();
            Invoke("InstObs", 0.5f);
        }
    }
    IEnumerator<float> ChamarRecpTime() {

        yield return Timing.WaitForSeconds(0.2f);
        panelPont.DOAnchorPosX(-390f, 0.25f, false);
        recipientes[numbRecp].DOLocalMove(new Vector3(recipientes[numbRecp].localPosition.x, 3f, recipientes[numbRecp].localPosition.z), 0.25f, false);
        yield return Timing.WaitForSeconds(0.3f);
        panelPont.DOAnchorPosX(-403f, 0.25f, false);
        recipientes[numbRecp].DOLocalMove(new Vector3(recipientes[numbRecp].localPosition.x, 4.5f, recipientes[numbRecp].localPosition.z), 0.25f, false);
        nameRecp[numbRecp].DOFade(1, 1);
        yield return Timing.WaitForSeconds(0.5f);
      //  recipientes[numbRecp].gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(recipientes[numbRecp].gameObject.GetComponent<RecipineteControl>().velX, recipientes[numbRecp].gameObject.GetComponent<Rigidbody2D>().velocity.y);
      //  recipientes[numbRecp].gameObject.GetComponent<RecipineteControl>().MoverY();
        Invoke("ChamarRecp", 0.5f);


        //recpTocado.transform.DOLocalMove(new Vector3(recpTocado.position.x, 0.0002f, 0), 0.5f, false);

    }
    IEnumerator<float> AcertoAnim() {

        yield return Timing.WaitForSeconds(0.1f);
        moeda.DOScale(2f, 0.2f);
        yield return Timing.WaitForSeconds(0.1f);
        fundMoeda.DOScale(1.5f, 0.2f);
        yield return Timing.WaitForSeconds(0.1f);
        moeda.DOScale(1f, 0.2f);
        yield return Timing.WaitForSeconds(0.1f);
        fundMoeda.DOScale(1f, 0.2f);

    }
    public void AcertoAnimBt() {
        Timing.RunCoroutine(AcertoAnim());
    }
        public void ContagemErroAcerto(bool Acertou) {
        if (Acertou) {
            numbCerto = numbCerto + 1;
            Timing.RunCoroutine(AcertoAnim());
            AcerErro[0].text = "" + numbCerto;
            acertoErroTrans[0].DOScale(new Vector3(2, 2, 2), 0.25f);
            if (numbCerto == numeroPassarLevel && !checkPassarLevel) {
                moveRecp = true;
                if (!checkMove) {
                    checkMove = true;
                   // Debug.Log("passouLevel");
                   // ChamarMoverec();
                }
                numeroPassarLevel += numeroPassarLevel;
                checkPauseInst = true;
                checkPassarLevel = true;
                 numbLevel = numbLevel + 1;
                changeLevel2.startChangeLevelAnimation(numbLevel);
                Invoke("VoltarInstObs", 2.5f);
                Invoke("TamanoAcerto", 2f);
            } else {
                Invoke("TamanoAcerto", 0.25f);
            }
            // acertoErroTrans[0].DOScale(new Vector3(1, 1, 1), 1);
           
        } else {
            numbErro = numbErro + 1;
            AcerErro[1].text = "Erros: " + numbErro;
            acertoErroTrans[1].DOScale(new Vector3(2, 2, 2), 0.25f);
            //  acertoErroTrans[1].DOScale(new Vector3(1, 1, 1), 1);
            Invoke("TamanhoErro", 0.25f);
            //Timing.RunCoroutine(ContagemErroAcertoTime(false));
        }

    }
    void ChamarMoverec() {
      //  recipientes[0].gameObject.GetComponents<RecipineteControl>()[0].MudaDir();
        //recipientes[0].gameObject.GetComponents<RecipineteControl>()[0].MoverY();
      //  recipientes[1].gameObject.GetComponents<RecipineteControl>()[0].MudaDir();
       // recipientes[1].gameObject.GetComponents<RecipineteControl>()[0].MoverY();
    }
    void VoltarInstObs() {
        movePassY = false;
        checkPassarLevel = false;
        checkPauseInst = false;
        InstObs();

    }
    void TamanoAcerto() {
        acertoErroTrans[0].DOScale(new Vector3(1, 1, 1), 0.25f);
    }
    void TamanhoErro() {
        acertoErroTrans[1].DOScale(new Vector3(1, 1, 1), 0.25f);


    }

    public void AcertouQuest(bool certo) {
        if (certo) {
            Timing.RunCoroutine(PanelDestroyLisON());
        } else {
            PanelDestroyLisOff();
            Invoke("InstObs", 0.5f);
        }

    }

    IEnumerator<float> PanelDestroyLisON() {
        checkPauseInst = true;
        panelFund.DOFade(0.8f, 1);
        panelBolaDestroy.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
        yield return Timing.WaitForSeconds(0.2f);
        panelFund.DOFade(0.8f, 1);
        panelBolaDestroy.DOScale(new Vector3(1, 1, 1), 0.2f);
        yield return Timing.WaitForSeconds(0.1f);
        bolasDestroy[0].DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.25f);
        yield return Timing.WaitForSeconds(0.2f);
        bolasDestroy[0].DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f);
        yield return Timing.WaitForSeconds(0.1f);
        bolasDestroy[1].DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.25f);
        yield return Timing.WaitForSeconds(0.2f);
        bolasDestroy[1].DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f);
        yield return Timing.WaitForSeconds(0.1f);
        bolasDestroy[2].DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.25f);
        yield return Timing.WaitForSeconds(0.2f);
        bolasDestroy[2].DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f);
        yield return Timing.WaitForSeconds(0.1f);
        bolasDestroy[3].DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.25f);
        yield return Timing.WaitForSeconds(0.2f);
        bolasDestroy[3].DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f);

        //recpTocado.transform.DOLocalMove(new Vector3(recpTocado.position.x, 0.0002f, 0), 0.5f, false);

    }
    public void PanelDestroyLisOff() {
        panelBolaDestroy.DOScale(new Vector3(0f,0f, 0f), 0.1f);
        panelFund.DOFade(0, 1);
        for (int i = 0; i < bolasDestroy.Length; i++) {
            bolasDestroy[i].transform.localScale = new Vector3(0, 0, 0);

        }
        checkPauseInst = false;
        
    }

}
