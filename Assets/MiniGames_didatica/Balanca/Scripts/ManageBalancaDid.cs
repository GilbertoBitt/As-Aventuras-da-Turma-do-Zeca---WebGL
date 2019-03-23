using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;
public class ManageBalancaDid : MonoBehaviour {

    public List<ObjsBalanca> ObjBalanca = new List<ObjsBalanca>();
    public GameObject pranchaG;
    Rigidbody2D pranchaRig;

    Camera cameraObj;

    public Transform[] btAlterTrans;

    public Transform[] objPesoTras;

    public GameObject[] objPeso;
    Vector3 objPos1;
    Vector3 objPos2;
    Rigidbody2D rigPeso1;
    Rigidbody2D rigPeso2;

    SpriteRenderer obj1Sprite;
    SpriteRenderer obj2Sprite;
    int randR;
    public int[] massP;

    bool maiorV;
    int respCert;
    public int numQuestao;
    public int numPerg;
    //int respCertMenor;

    public Text perguntaText;
    public Text texMaiMen;
    public Text texFinal;
    public Text texDir;
    public Text texEsq;
    public Text textAcerErr;
    public Text finalTempo;
    public Button[] btAlter;

    Vector3 offsetFollow;
    Vector3 defaultOffset;

    public Slider barraV;
    public float contBarra;
    public float numbN;

    public float transitionDuration;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    bool checkiniPerg;



    void Start () {
        ObjBalanca.Suffle();
        cameraObj = Camera.main;
        objPos1 = new Vector3(objPeso[0].GetComponent<Transform>().transform.position.x, objPeso[0].GetComponent<Transform>().transform.position.y, objPeso[0].GetComponent<Transform>().transform.position.z);
        objPos2 = new Vector3(objPeso[1].GetComponent<Transform>().transform.position.x, objPeso[1].GetComponent<Transform>().transform.position.y, objPeso[1].GetComponent<Transform>().transform.position.z);
        pranchaRig = pranchaG.GetComponent<Rigidbody2D>();
        rigPeso1 = objPeso[0].GetComponent<Rigidbody2D>();
        rigPeso2 = objPeso[1].GetComponent<Rigidbody2D>();
        obj1Sprite = objPeso[0].GetComponent<SpriteRenderer>();
        obj2Sprite = objPeso[1].GetComponent<SpriteRenderer>();
        soltarPeso(true);
        for (int i = 0; i < btAlterTrans.Length; i++) {
            btAlterTrans[i].position = objPesoTras[i].position;
        }
        Timing.RunCoroutine(radomPergunta(), "stop");
        Timing.RunCoroutine(TamanhoTime(), "stop2");
    }

    void soltarPeso(bool kinematic) {

       rigPeso1.isKinematic = kinematic;
       rigPeso2.isKinematic = kinematic;
        /*
        if (kinematic) {
            rigPeso1.constraints = RigidbodyConstraints2D.FreezePositionY;
          //  rigPeso1.constraints = RigidbodyConstraints2D.None;
            rigPeso1.constraints = RigidbodyConstraints2D.FreezePositionX;

            rigPeso2.constraints = RigidbodyConstraints2D.FreezePositionY;
          //  rigPeso2.constraints = RigidbodyConstraints2D.None;
            rigPeso2.constraints = RigidbodyConstraints2D.FreezePositionX;

        } 
        else {
          //  rigPeso1.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigPeso1.constraints = RigidbodyConstraints2D.None;
            rigPeso1.constraints = RigidbodyConstraints2D.FreezePositionX;

          //  rigPeso2.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigPeso2.constraints = RigidbodyConstraints2D.None;
            rigPeso2.constraints = RigidbodyConstraints2D.FreezePositionX;

        }
       */
    }

    public IEnumerator<float> TamanhoTime() {

        // Debug.Log("dfds");

        Vector3 startOffsetB = offsetFollow;
        Vector3 endOffsetB = defaultOffset;
        float times = 0.0f;

        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;

            //displayContagem.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            barraV.value = Mathf.Lerp(contBarra, numbN, transitionCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }

        yield return Timing.WaitForSeconds(0.000000001f);
        Timing.RunCoroutine(TamanhoTime(), "stop");

        if (numbN > -0.1f) {
            for (int i = 0; i < btAlter.Length; i++) {
                btAlter[i].interactable = false;
            }
            barraV.value = 0;
           // CompleTx.DOText("", 1f, true, ScrambleMode.None);
           // xPrancha = pranchaG2.transform.localPosition.x;
           // pranchaG2.DOLocalMoveX(xPrancha + 50f, 1f, false);
          //  SoundManager2.startSoundFX(somsBt[2]);
            Timing.RunCoroutine(SemResp());
            //Timing.RunCoroutine(randomPergunta());
        } else {
            // displayContagem.text = "Acabou o Tempo";

        }

    }

    IEnumerator<float> SemResp() {

        Timing.KillCoroutines("stop");
        yield return Timing.WaitForSeconds(0.5f);
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = false;
        }
        //  btsAlter[numberPergL - 1].image.sprite = imgBtAlter[1];
        texMaiMen.DOText(" ", 1.25f, true, ScrambleMode.None);
        texFinal.DOText(" ", .25f, true, ScrambleMode.None);
        textAcerErr.DOText(" ", 1f, true, ScrambleMode.None);
        perguntaText.DOText(" ", 1f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(0.5f);
        // Timing.RunCoroutine(MovePranchaTime());
        // pranchaG2.DOPivotX(0.1f * 0.0001f, 1f);
        // xPrancha = pranchaG2.transform.localPosition.x;
        // pranchaG2.DOLocalMoveX(xPrancha + 50f, 1f, false);

        //texMaiMen.DOText(" ", 1f, true, ScrambleMode.None);
        finalTempo.DOText("Terminou o Tempo, tente de novo", 0.5f, true, ScrambleMode.None);


        // perguntaR.DOPivotX(1f, 1f);
        // respErrada = respErrada + 1;
        yield return Timing.WaitForSeconds(3f);
        //contPergunt();
        Timing.RunCoroutine(radomPergunta());

    }
    void PergMaior() {
      //  Debug.Log("Maior");
        maiorV = true;
        massP[0] = Random.Range(1, 10);
        massP[1] = Random.Range(1, 10);
        do {
            massP[0] = Random.Range(1, 10);
            //  Debug.Log("sda");
        } while (massP[0] == massP[1]);
        //  randR = Random.Range(0,3);
        //  if (randR >= 1) {
        pranchaRig.freezeRotation = false;
       
     

        if (massP[0] > massP[1]) {
          //  Debug.Log("1");
            massP[0] = 10;
            massP[1] = 5;
            obj1Sprite.sprite = ObjBalanca[numQuestao].objEMaior;
            obj2Sprite.sprite = ObjBalanca[numQuestao].objEMenor;
            respCert = 1;
        } else {
           // Debug.Log("2");
            massP[0] = 5;
            massP[1] = 10;
            obj1Sprite.sprite = ObjBalanca[numQuestao].objEMenor;
            obj2Sprite.sprite = ObjBalanca[numQuestao].objEMaior;
            respCert = 2;
        }

        rigPeso1.mass = massP[0];
        rigPeso2.mass = massP[1];
       // texDir.text = "" + rigPeso1.mass;
       // texEsq.text = "" + rigPeso2.mass;

    }
    void PergMenor() {
      //  Debug.Log("Menor");
        maiorV = true;
        massP[0] = Random.Range(1, 10);
        massP[1] = Random.Range(1, 10);
        do {
            massP[0] = Random.Range(1, 10);
            
        } while (massP[0] == massP[1]);
      
        pranchaRig.freezeRotation = false;      
     

        if (massP[0] > massP[1]) {
         //   Debug.Log("3");
            massP[0] = 5;
            massP[1] = 10;
            obj1Sprite.sprite = ObjBalanca[numQuestao].objEMenor;
            obj2Sprite.sprite = ObjBalanca[numQuestao].objEMaior;
            respCert = 1;
        } else {
        //    Debug.Log("4");
            massP[0] = 10;
            massP[1] = 5;
            obj1Sprite.sprite = ObjBalanca[numQuestao].objEMaior;
            obj2Sprite.sprite = ObjBalanca[numQuestao].objEMenor;
            respCert = 2;
        }
        rigPeso1.mass = massP[0];
        rigPeso2.mass = massP[1];
       // texDir.text = "" + rigPeso1.mass;
       // texEsq.text = "" + rigPeso2.mass;


    }
    IEnumerator<float> radomPergunta() {
        if (ObjBalanca.Count - 1 == numQuestao) {
            numQuestao = -1;
        }
       
        if (!checkiniPerg) {
            checkiniPerg = true;
        } else {
            numPerg = numPerg + 1;
            numQuestao = numQuestao + 1;
        }
       
        finalTempo.DOText(" ", 0.25f, true, ScrambleMode.None);
        soltarPeso(true);
        barraV.value = 10;
        contBarra = 10;
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = false;
        }
        // yield return Timing.WaitForSeconds(0.05f);

        objPeso[0].transform.position = new Vector3(objPos1.x, objPos1.y, objPos1.z);
        objPeso[1].transform.position = new Vector3(objPos2.x, objPos2.y, objPos2.z);
        objPeso[0].transform.localEulerAngles = new Vector3(0, 0, 0);
        objPeso[1].transform.localEulerAngles = new Vector3(0, 0, 0);
        pranchaG.transform.localEulerAngles = new Vector3(0,0,0);


        texMaiMen.DOText(" ", .25f, true, ScrambleMode.None);
        texFinal.DOText(" ", .25f, true, ScrambleMode.None);
      //  textAcerErr.DOText(" ", 25f, true, ScrambleMode.None);


       // } 
        /*
        else {
            pranchaRig.freezeRotation = true;  
            rigPeso1.mass = massP[0];
            massP[1]= massP[0];
            rigPeso2.mass = massP[1];
        }
        */

        int maior = Random.Range(0,2);
        //Debug.Log(maior);
        //maior = 2;
        // maiorV = false;
        texMaiMen.text = " ";
      //  perguntaText.DOText("Qual é o Maior ?", 0.5f, true, ScrambleMode.None);
        if (maior == 1) {
            perguntaText.DOText("Qual é o Maior ?", 0.5f, true, ScrambleMode.None);
            // yield return Timing.WaitForSeconds(0.25f);
           // texFinal.DOText("?", .25f, true, ScrambleMode.None);
            PergMaior();    

        } 
        else {
            perguntaText.DOText("Qual é o Menor ?", 0.5f, true, ScrambleMode.None);
            //texMaiMen.DOText("Menor", 1f, true, ScrambleMode.None);
            // yield return Timing.WaitForSeconds(0.25f);
            //texFinal.DOText("?", .25f, true, ScrambleMode.None);
            PergMenor();
        }
       
        texDir.text = "" + massP[1];
        texEsq.text = "" + massP[0];

        yield return Timing.WaitForSeconds(0.5f);
        
       

        for (int i = 0; i < btAlterTrans.Length; i++) {
            btAlterTrans[i].position = objPesoTras[i].position;
        }
       
       // yield return Timing.WaitForSeconds(0.25f);
        Timing.RunCoroutine(TamanhoTime(), "stop2");
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = true;
        }
    }
    public void CheckRespost(int resp) {
        soltarPeso(false);
        if (resp == respCert) {
            Timing.RunCoroutine(Corretresp());
        }
        else {
            Timing.RunCoroutine(Errtresp());
        } 
       
    }
    IEnumerator<float> Corretresp() {
        // Debug.Log("acerto");
        Timing.KillCoroutines("stop2");
        texMaiMen.DOText(" ", .25f, true, ScrambleMode.None);
        texFinal.DOText(" ", .25f, true, ScrambleMode.None);
       // textAcerErr.DOText(" ", .25f, true, ScrambleMode.None);
        perguntaText.DOText(" ", .25f, true, ScrambleMode.None);
        textAcerErr.DOText("acerto", .5f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(3f);
        textAcerErr.DOText(" ", .25f, true, ScrambleMode.None);
        Timing.RunCoroutine(radomPergunta());
        
    }
    IEnumerator<float> Errtresp() {
        //  Debug.Log("Erro");

        Timing.KillCoroutines("stop2");
        texMaiMen.DOText(" ", .25f, true, ScrambleMode.None);
        texFinal.DOText(" ", .25f, true, ScrambleMode.None);
        // textAcerErr.DOText(" ", .25f, true, ScrambleMode.None);
        perguntaText.DOText(" ", .25f, true, ScrambleMode.None);
        textAcerErr.DOText("Erro", .5f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(3f);
        textAcerErr.DOText(" ", .25f, true, ScrambleMode.None);
        Timing.RunCoroutine(radomPergunta());

    }
}
