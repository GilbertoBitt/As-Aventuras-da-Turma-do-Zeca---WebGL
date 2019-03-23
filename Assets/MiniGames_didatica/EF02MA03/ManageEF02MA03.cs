using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class ManageEF02MA03 : MonoBehaviour {

    public GameObject pranchaG;

    public Vector3 offsetFollowB;
    public Vector3 offsetFollowB2;
    public Vector3 defaultOffsetB;
    public Vector3 defaultOffsetB2;
    public float transitionDuration;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public Text displayContagem;
    public Text pergText;
    public Text respText;
    public Text number1;
    public Text number2;
    public Text textAcerErr;
    public float contagem;

    Vector3 offsetFollow;
    Vector3 defaultOffset;
    public float numbN;
    public int numbN2;

    int nubC1;
    int nubC2;
    int respCerta;

    public Slider barraV;

    public float contBarra;

    public int level;

    public int contPerg;

    int barraNumber;

    bool checkPassLevel;

    public Color colorTrans;
    Color colorAtualPerg;
    Color colorAtualResp;
    Color colorAtualNumber;
    public float durationOfFade = 1.0f;
    public AnimationCurve fadeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    bool checkIni;
    int numberPergL;

    public Button[] altern;
    public Image[] imgAltern;

    public Image imgBt;
    public Sprite[] sprBt;

    public float xPrancha;
    public BoxCollider2D[] coll;

    bool puxaRampa;
    bool puxaRampa2;
    public GameConfig gameConfigN;
    public GDLHandler logHandler;
    public SDLHandler sdlHandler;
    int levelG;
    public int anoLetivo;
    public GameObject anim;
    changeLevel changeLevel2;
    int checkNn;

    public int pointsP;
    public int pointsN;
    SoundManager SoundManager2;
    public AudioClip[] somsBt;

    public BoxCollider2D[] colbox;


    public GameObject chaoG;

    public bool checkChao;

    public GameObject bolaP;
    public GameObject[] bolas;

    public GameObject posBola;
    public GameObject interagindo;

    public GameObject[] desfim;
    void Start () {
        // Ativarcanvas(false);
        numbN = 10;
        changeLevel2 = anim.GetComponent<changeLevel>();
        SoundManager2 = FindObjectOfType<SoundManager>();
        levelG = 1;
        gameConfigN.UpdateAll();
        offsetFollowB = new Vector3(pranchaG.transform.position.x, pranchaG.transform.position.y, pranchaG.transform.position.z);
        colorAtualPerg = pergText.color;
        colorAtualResp = respText.color;
        colorAtualNumber = number1.color;
        logHandler.StartTimer();

        PergRandom();

        for (int i = 0; i < altern.Length; i++) {
            altern[i].enabled = false;

        }
      
        Timing.RunCoroutine(TamanhoTime(), "stop");
        Timing.RunCoroutine(FadeText1a0start());

        ///

        // pergText.text = pergText.text.Replace("<random1>", int);
    }

    void checkZeroNumber(int checkN) {
        //------------------1º Ano--------------------//
        if (nubC1 < 10 && checkN==1) {
            number1.text = "0" + nubC1;
        } else if(nubC1 > 9 && checkN == 1) {
            number1.text = "" + nubC1;
        }
        
        if (nubC2 < 10 && checkN == 1) {
            number2.text = "0" + nubC2;
        } else if  (nubC2 > 9 && checkN == 1){
            number2.text = "" + nubC2;
        }

        //------------------2º Ano--------------------//
        if (nubC1 < 10 && checkN == 2) {
            number1.text = "00" + nubC1;
        } 
        else if (nubC1 > 99 && nubC1 < 100 && checkN == 1) {
            number1.text = "0" + nubC1;
        }
        else if (nubC1 > 100 && checkN == 1) {
            number1.text = "" + nubC1;
        }
        if (nubC2 < 10 && checkN == 2) {
            number2.text = "00" + nubC2;
        } 
        else if (nubC1 > 99 && nubC1 < 100 && checkN == 1) {
            number2.text = "0" + nubC2;
        }
        else if (nubC2 > 100 && checkN == 1) {
            number2.text = "" + nubC2;
        }
        //------------------3º Ano--------------------//

        if (nubC1 < 10 && checkN == 3) {
            number1.text = "000" + nubC1;
        } else if (nubC1 < 100 && checkN == 3) {
            number1.text = "00" + nubC1;
        } else if (nubC1 < 999 && checkN == 3) {
            number1.text = "0" + nubC1;
        } else if (nubC1 > 999 && checkN == 3) {
            number1.text = "" + nubC1;
        }

        if (nubC2 < 10 && checkN == 3) {
            number2.text = "000" + number2;
        } else if (nubC2 < 100 && checkN == 3) {
            number2.text = "00" + number2;
        } else if (nubC2 < 999 && checkN == 3) {
            number2.text = "0" + number2;
        } else if (nubC2 > 999 && checkN == 3) {
            number2.text = "" + number2;
        }
  
    }
    void PerguntaMDir(){
        // Debug.Log("ano letivo: " + anoLetivo + " levelG: " + levelG);
        // pergText.text = "O Numero <random1> é _______  que <random2> ";
        //gameConfigN.currentClass.idAnoLetivo
        anoLetivo = gameConfigN.currentClass.idAnoLetivo;
        if (anoLetivo == 1 && levelG==1) {
            sdlHandler.idDificuldade = 1;           
            nubC1 = Random.Range(1, 30);
            nubC2 = Random.Range(1, 30);
            checkZeroNumber(1);
        }
        else if(anoLetivo == 1 && levelG == 2) {
            nubC1 = Random.Range(1, 60);
            nubC2 = Random.Range(1, 60);
            checkZeroNumber(1);
        }
        else if  (anoLetivo == 1 && levelG == 3) {
            nubC1 = Random.Range(1, 99);
            nubC2 = Random.Range(1, 99);
            checkZeroNumber(1);
        } 
        else if (anoLetivo == 2 && levelG == 1) {
            sdlHandler.idDificuldade = 2;
            nubC1 = Random.Range(1, 99);
            nubC2 = Random.Range(1, 99);
            checkZeroNumber(2);
        } 
        else if (anoLetivo == 2 && levelG == 2) {
            nubC1 = Random.Range(100, 500);
            nubC2 = Random.Range(100, 500);
            checkZeroNumber(2);
        } 
        else if (anoLetivo == 2 && levelG == 3) {
            nubC1 = Random.Range(1, 999);
            nubC2 = Random.Range(1, 999);
            checkZeroNumber(2);
        } 
        else if (anoLetivo == 3 && levelG == 1) {
            sdlHandler.idDificuldade = 3;
            nubC1 = Random.Range(1, 99);
            nubC2 = Random.Range(1, 99);
            checkZeroNumber(3);
        } else if (anoLetivo == 3 && levelG == 2) {
            nubC1 = Random.Range(1, 999);
            nubC2 = Random.Range(1, 999);
            checkZeroNumber(3);

        } else if (anoLetivo == 3 && levelG == 3) {
            nubC1 = Random.Range(1, 9999);
            nubC2 = Random.Range(1, 9999);
            checkZeroNumber(3);
        }
        //Debug.Log("ano letivo: " + anoLetivo + " levelG: " + levelG);


    }

    void PerguntaIgual() {
       // pergText.text = "O Numero <random1> é _______  que <random2> ";
        nubC1 = Random.Range(1, 999);
        nubC2 = nubC1;
        //  number1.text = "" + nubC1;
        //  number2.text = "" + nubC2;

        // Debug.Log("ano letivo: " + anoLetivo + " levelG: " + levelG);
        anoLetivo = gameConfigN.currentClass.idAnoLetivo;

        if (anoLetivo == 1 && levelG == 1) {
            nubC1 = Random.Range(1, 30);
            nubC2 = nubC1;
            checkZeroNumber(1);
        } else if (anoLetivo == 1 && levelG == 2) {
            nubC1 = Random.Range(1, 60);
            nubC2 = nubC1;
            checkZeroNumber(1);
        } else if (anoLetivo == 1 && levelG == 3) {
            nubC1 = Random.Range(1, 99);
            nubC2 = nubC1;
            checkZeroNumber(1);
        } else if (anoLetivo == 2 && levelG == 1) {
            nubC1 = Random.Range(1, 99);
            nubC2 = nubC1;
            checkZeroNumber(2);
        } else if (anoLetivo == 2 && levelG == 2) {
            nubC1 = Random.Range(100, 500);
            nubC2 = nubC1;
            checkZeroNumber(2);
        } else if (anoLetivo == 2 && levelG == 3) {
            nubC1 = Random.Range(1, 999);
            nubC2 = nubC1;
            checkZeroNumber(2);
        } else if (anoLetivo == 3 && levelG == 1) {
            nubC1 = Random.Range(1, 99);
            nubC2 = nubC1;
            checkZeroNumber(3);
        } else if (anoLetivo == 3 && levelG == 2) {
            nubC1 = Random.Range(1, 999);
            nubC2 = nubC1;
            checkZeroNumber(3);
            
        } else if (anoLetivo == 3 && levelG == 3) {
            nubC1 = Random.Range(1,9999);
            nubC2 = nubC1;
            checkZeroNumber(3);

        }
        //Debug.Log("ano letivo: " + anoLetivo + " levelG: " + levelG);
        //  pergText.text = pergText.text.Replace("<random1>", nubC1.ToString());
        //  pergText.text = pergText.text.Replace("<random2>", nubC2.ToString());

    }

    public void ChangeText(string text) {
        displayContagem.text = text;
    }


    public IEnumerator<float> MovePranchaTime() {
        
        defaultOffsetB = new Vector3(offsetFollowB.x +  1, offsetFollowB.y, offsetFollowB.z);
        Vector3 startOffsetB = offsetFollowB;
        Vector3 endOffsetB = defaultOffsetB;
        float times = 0.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;
           //pranchaG.transform.position = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
           pranchaG.transform.position = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        xPrancha = endOffsetB.x + xPrancha;
        offsetFollowB = new Vector3(pranchaG.transform.position.x, pranchaG.transform.position.y, pranchaG.transform.position.z);
        if (xPrancha > 42f && !puxaRampa) {
            coll[0].enabled = false;
            puxaRampa = true;
        }
  

    }
    private void Update() {
        if (checkChao) {
            checkChao = false;
            Timing.RunCoroutine(MoverChao());

        }
    }
    public void moverchaoM() {
        Timing.RunCoroutine(MoverChao());
    }
    public IEnumerator<float> MoverChao() {
      
        yield return Timing.WaitForSeconds(0.2f);
        offsetFollowB2 = new Vector3(chaoG.transform.position.x, chaoG.transform.position.y, chaoG.transform.position.z);

        defaultOffsetB2 = new Vector3(offsetFollowB2.x-4, offsetFollowB2.y, offsetFollowB2.z);
        for (int i = 0; i < bolas.Length; i++) {
            GameObject clone = Instantiate(bolas[i], new Vector3(posBola.transform.position.x+4.2f, posBola.transform.position.y, posBola.transform.position.z), Quaternion.identity, bolaP.transform);
            clone.transform.localPosition = new Vector3(-13.4f, 0, 0);
        }

        //Vector3 startOffsetB = offsetFollowB;

        Vector3 startOffsetB = offsetFollowB2;
        Vector3 endOffsetB = defaultOffsetB2;
        float times = 0.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;
            //pranchaG.transform.position = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            chaoG.transform.position = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        // xPrancha = endOffsetB.x + xPrancha;
        offsetFollowB2 = new Vector3(chaoG.transform.position.x, chaoG.transform.position.y, chaoG.transform.position.z);

        yield return Timing.WaitForSeconds(1f);

        for (int i = 0; i < bolas.Length; i++) {
           GameObject clone =  Instantiate(bolas[i], new Vector3(posBola.transform.position.x+1, posBola.transform.position.y, posBola.transform.position.z), Quaternion.identity, bolaP.transform);
            clone.transform.localPosition = new Vector3(0, 0, 0);
        }

        defaultOffsetB2 = new Vector3(offsetFollowB2.x, offsetFollowB2.y+1.5f, offsetFollowB2.z);
        startOffsetB = offsetFollowB2;
        endOffsetB = defaultOffsetB2;
        times = 0.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;
            //pranchaG.transform.position = Vector3.Lerp(endOffsetB, startOffsetB, transitionCurve.Evaluate(s));
            chaoG.transform.position = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        //xPrancha = endOffsetB.x + xPrancha;
        offsetFollowB2 = new Vector3(chaoG.transform.position.x, chaoG.transform.position.y, chaoG.transform.position.z);
        Ativarcanvas(false);
        // pranchaG.transform.position = new Vector3();
        // setaBalao.anchoredPosition = new Vector2(posSeta[numbPerson], setaBalao.anchoredPosition.y);

        yield return Timing.WaitForSeconds(2f);
        Timing.KillCoroutines("stop");
        interagindo.SetActive(true);
        logHandler.currentLevel = levelG;
        logHandler.StopTimer();
        logHandler.scoreAmount = pointsP;
        sdlHandler.Send();
    }

    public void Ativarcanvas(bool ativarB) {
        for (int i = 0; i < desfim.Length; i++) {
            if (desfim[i] != null) {
                desfim[i].SetActive(ativarB);
            }
        }
    }
    public IEnumerator<float> ContagemRegressiva() {
        float times = 1.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;
            while (times < transitionDuration) {   
               // barraT[0].value = numbN;
                yield return Timing.WaitForOneFrame;
            }
            yield return Timing.WaitForOneFrame;
        }
        Timing.RunCoroutine(ContagemRegressiva(), "stop");
    }

    public IEnumerator<float> TamanhoTime() {
            Vector3 startOffsetB = offsetFollow;
            Vector3 endOffsetB = defaultOffset;
            float times = 0.0f;

            while (times < transitionDuration) {
                times += Time.deltaTime;
                float s = times / transitionDuration;
                
                displayContagem.transform.localScale = Vector3.Lerp(startOffsetB, endOffsetB, transitionCurve.Evaluate(s));
                barraV.value = Mathf.Lerp(contBarra, numbN, transitionCurve.Evaluate(s));
                yield return Timing.WaitForOneFrame;
            }
            contBarra = numbN;
            yield return Timing.WaitForSeconds(0.000000001f);
           
            numbN = numbN - 0.25f;
        if (numbN <= 0f && !puxaRampa2) {
            puxaRampa2 = true;
            SoundManager2.startSoundFX(somsBt[2]);
            Timing.KillCoroutines("stop");
            Timing.RunCoroutine(PuxaRampaZeroTempo());
        }
           
            displayContagem.text = "" + numbN;
            if (numbN > -0.5f) {
                Timing.RunCoroutine(TamanhoTime(), "stop");
            } else {
                displayContagem.text = "Acabou o Tempo";          
            }      
    }

    public IEnumerator<float> PuxaRampaZeroTempo() {
        yield return Timing.WaitForSeconds(0.3f);
        MovePrancha();
        Timing.RunCoroutine(FadeText0a1());
        PergRandom();
        yield return Timing.WaitForSeconds(0.3f);
        Timing.RunCoroutine(TamanhoTime(), "stop");
        if (contPerg <= 3) {
            numbN = 10;
        } else if (contPerg >= 4 && contPerg <= 7) {
            numbN = 7;
        } else if (contPerg >= 10) {
            numbN = 3.2f;
        }
        puxaRampa2 = false;

    }

    public IEnumerator<float> PuxaRampaContinuo() {
      
            yield return Timing.WaitForSeconds(0.01f);
            Timing.RunCoroutine(MovePranchaTime());
            Timing.RunCoroutine(PuxaRampaContinuo());
  
    }
    public IEnumerator<float> boxcool() {
        yield return Timing.WaitForSeconds(4f);
        for (int i = 0; i < colbox.Length; i++) {
            colbox[i].enabled = false;

        }

            }
    public void MovePrancha() {
       
        Timing.RunCoroutine(MovePranchaTime());
    }

    public void PergRandom() {
        int pergT = Random.Range(0,5);
        if (pergT == 1) {
            PerguntaIgual();
           
        } else {
            PerguntaMDir();           
        }

        if (nubC1 > nubC2) {
            respCerta = 1;
        }
        else if (nubC1 == nubC2) {
            respCerta = 2;
        } 
        else if (nubC1 < nubC2) {
            respCerta = 3;
        }
    
    }

    public void CheckResp(int NunbPerg) {
        Timing.KillCoroutines("stop");
        numberPergL = NunbPerg;
        if (numberPergL == 1) {
            respText.text = "Maior que";
        } 
        else if(numberPergL == 2) {
            respText.text = "Igual a";
        } 
        else if (numberPergL == 3) {
            respText.text = "Menor que";
        }
        SoundManager2.startSoundFX(somsBt[0]);
        Timing.RunCoroutine(FadeText1a0());
    }
    public IEnumerator<float> TamanhoBarraVoltar(int barraN) {

        float times = 0.0f;
        while (times < transitionDuration) {
            times += Time.deltaTime;
            float s = times / transitionDuration;
            if (barraN == 1) {
                barraV.value = Mathf.Lerp(contBarra, 10, transitionCurve.Evaluate(s));
                contBarra = 10;
            } else if (barraN == 2) {
                barraV.value = Mathf.Lerp(contBarra, 7, transitionCurve.Evaluate(s));
                contBarra = 7;
            } else if (barraN == 3) {
                barraV.value = Mathf.Lerp(contBarra, 4, transitionCurve.Evaluate(s));
                contBarra = 4;
            }
            yield return Timing.WaitForOneFrame;
        }
        barraV.value = 0;
        yield return Timing.WaitForSeconds(1f);
    }
    IEnumerator<float> FadeText1a0start() {
        float times = 0.0f;
        if (!checkIni) {
            checkIni = true;
            times = 0.0f;
            while (times < durationOfFade) {
                times += Time.deltaTime;
                float s = times / durationOfFade;
                pergText.color = Color.Lerp(colorTrans, colorAtualPerg, fadeCurve.Evaluate(s));               
                number1.color = Color.Lerp(colorTrans, colorAtualNumber, fadeCurve.Evaluate(s));
                number2.color = Color.Lerp(colorTrans, colorAtualNumber, fadeCurve.Evaluate(s));
                yield return Timing.WaitForOneFrame;
            }
            for (int i = 0; i < altern.Length; i++) {
                altern[i].enabled = true;
            }
        }
    }
    IEnumerator<float> TextResposta() {
        float times = 0.0f;
        while (times < durationOfFade) {
            times += Time.deltaTime;
            float s = times / durationOfFade;
           
            respText.color = Color.Lerp(colorTrans, colorAtualResp, fadeCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
    }
        IEnumerator<float> FadeText1a0() {
        Timing.KillCoroutines("stop");
        for (int i = 0; i < altern.Length; i++) {
            altern[i].enabled = false;
        }
        float times = 0.0f;
        while (times < durationOfFade) {
            times += Time.deltaTime;
            float s = times / durationOfFade;
           
            respText.color = Color.Lerp(colorTrans, colorAtualResp, fadeCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        yield return Timing.WaitForSeconds(0.3f);

        if (contPerg <= 4) {            
            numbN = 10;
        } else if (contPerg >= 5 && contPerg <= 10) {           
            numbN = 7;
        } else if (contPerg >= 11) {        
            numbN = 3.2f;
        }
        contPerg = contPerg + 1;

        yield return Timing.WaitForSeconds(0.3f);

        if (numberPergL == respCerta) {
            pointsP = pointsP + 1;

            if (pointsP == 4 || pointsP == 8) {
                levelG = levelG+1;
                changeLevel2.startChangeLevelAnimation(levelG);
            } 

            imgBt.sprite = sprBt[1];
            SoundManager2.startSoundFX(somsBt[1]);

            sdlHandler.SaveEstatistica(true);

            yield return Timing.WaitForSeconds(0.25f);

            times = 0.0f;
            while (times < durationOfFade) {
                times += Time.deltaTime;
                float s = times / durationOfFade;
               
                pergText.color = Color.Lerp(colorAtualPerg, colorTrans, fadeCurve.Evaluate(s));
                number1.color = Color.Lerp(colorAtualNumber, colorTrans, fadeCurve.Evaluate(s));
                number2.color = Color.Lerp(colorAtualNumber, colorTrans, fadeCurve.Evaluate(s));
                respText.color = Color.Lerp(colorAtualResp, colorTrans, fadeCurve.Evaluate(s));
                yield return Timing.WaitForOneFrame;
            }

        } else {
            pointsN = pointsN + 1;
            imgBt.sprite = sprBt[2];
            SoundManager2.startSoundFX(somsBt[2]);
            yield return Timing.WaitForSeconds(0.25f);
            for (int i = 0; i < imgAltern.Length; i++) {
                imgAltern[i].sprite = sprBt[2];
                if (i == respCerta - 1) {
                    imgAltern[i].sprite = sprBt[1];
                }
            }
            sdlHandler.SaveEstatistica(false);
            MovePrancha();
                yield return Timing.WaitForSeconds(1f);
                times = 0.0f;
                while (times < durationOfFade) {
                    times += Time.deltaTime;
                    float s = times / durationOfFade;
                  
                    pergText.color = Color.Lerp(colorAtualPerg, colorTrans, fadeCurve.Evaluate(s));
                    number1.color = Color.Lerp(colorAtualNumber, colorTrans, fadeCurve.Evaluate(s));
                    number2.color = Color.Lerp(colorAtualNumber, colorTrans, fadeCurve.Evaluate(s));
                    respText.color = Color.Lerp(colorAtualResp, colorTrans, fadeCurve.Evaluate(s));

                yield return Timing.WaitForOneFrame;
                }
            if (pointsN > 7) {
                Timing.RunCoroutine(boxcool());
            }
      
        }
        respCerta = 0;       
        PergRandom();
        imgBt.sprite = sprBt[0];
        yield return Timing.WaitForSeconds(1f);
        Timing.RunCoroutine(TamanhoTime(), "stop");
        Timing.RunCoroutine(FadeText0a1());
    }

    IEnumerator<float> FadeText0a1() {

        imgBt.sprite = sprBt[0];
        float times = 0.0f;
        times = 0.0f;
        SoundManager2.startSoundFX(somsBt[3]);
        while (times < durationOfFade) {
            times += Time.deltaTime;
            float s = times / durationOfFade;
            pergText.color = Color.Lerp(colorTrans, colorAtualPerg, fadeCurve.Evaluate(s));
            number1.color = Color.Lerp(colorTrans, colorAtualNumber, fadeCurve.Evaluate(s));
            number2.color = Color.Lerp(colorTrans, colorAtualNumber, fadeCurve.Evaluate(s));
            yield return Timing.WaitForOneFrame;
        }
        for (int i = 0; i < altern.Length; i++) {
            imgAltern[i].sprite = sprBt[0];
        }
        yield return Timing.WaitForSeconds(0.2f);
        for (int i = 0; i < altern.Length; i++) {
            altern[i].enabled = true;
        }

    }
    
}
