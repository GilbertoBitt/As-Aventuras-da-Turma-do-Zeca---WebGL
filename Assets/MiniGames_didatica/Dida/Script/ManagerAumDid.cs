using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;
public class ManagerAumDid : MonoBehaviour {

    public List<Item_AumDimi> palavras = new List<Item_AumDimi>();
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public int respCerta;
    public int respErrada;

    // public Text  
    public RectTransform perguntaR;
    public Text CompleTx;
    public Text palavraT;
    public Text palavraNO;
    public Text[] Alter;
    public Sprite[] imgBtAlter;
    // public string[] texPerg;

    public int numberPergL;
    public Vector3 offsetFollow;
    public Vector3 defaultOffset;
    public float transitionDuration;
    public Slider barraV;
    public float contBarra;
    public float numbN;

    public Vector2 Moveperg;
    public Color colorTrans;
    public Button[] btsAlter;
 
    Color colorAtualPerg;
    Color colorAtualAlter;
    public float durationOfFade = 1.0f;
    public AnimationCurve fadeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public int numPerg;
    bool chekperin;

    public int contPerg;

    SoundManager SoundManager2;

    public Vector3 offsetFollowB;
    public Vector3 defaultOffsetB;
    public RectTransform pranchaG;
    public RectTransform pranchaG2;
    public float xPrancha;
    bool puxaRampa;

    public BoxCollider2D[] coll;
    public Button[] btAlter;

    public GameObject chaoG;

    public bool checkChao;

    public GameObject bolaP;
    public GameObject[] bolas;

    public GameObject posBola;
    public GameObject interagindo;

    public GameObject[] desfim;


    public GameObject anim;
    changeLevel changeLevel2;
    int levelG;
    bool clickAlter;

    public AudioClip[] somsBt;

    public GameConfig gameConfigN;
    public GDLHandler logHandler;
    public SDLHandler sdlHandler;

    void Start () {
      
        gameConfigN.UpdateAll();
        SoundManager2 = FindObjectOfType<SoundManager>();
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = false;
        }
        changeLevel2 = anim.GetComponent<changeLevel>();
        levelG = 1;
        palavras.Suffle();
        xPrancha = pranchaG2.transform.localPosition.x;
        //Debug.Log("cvgsd");
        // colorAtualPerg = pergText.color;
        //colorAtualAlter = Alter[0].color;
        // pergText.color = colorTrans;
        logHandler.StartTimer();
        Timing.RunCoroutine(randomPergunta());
        Timing.RunCoroutine(TamanhoTime(),"stop");
        // randomPergunta();



        //Timing.RunCoroutine(FadeText0a1start());


    }

    // Update is called once per frame


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
            CompleTx.DOText("", 1f, true, ScrambleMode.None);
            xPrancha = pranchaG2.transform.localPosition.x;
            pranchaG2.DOLocalMoveX(xPrancha + 50f, 1f, false);
            SoundManager2.startSoundFX(somsBt[2]);
            Timing.RunCoroutine(SemResp());
            //Timing.RunCoroutine(randomPergunta());
        } else {
           // displayContagem.text = "Acabou o Tempo";

        }
        
    }
 
    IEnumerator<float> randomPergunta() {
        clickAlter = false;
        CompleTx.DOText("Complete", 1f, true, ScrambleMode.None);
      
        int pergT = Random.Range(0, 3);
        if (!chekperin) {
            chekperin = true;
        } else {
            numPerg = numPerg + 1;
            perguntaR.DOPivotX(0.5f, 1f);
            palavraT.DOText("", 1f, true, ScrambleMode.None);
            palavraNO.DOText("", 1f, true, ScrambleMode.None);
            for (int i = 0; i < btsAlter.Length; i++) {
                btsAlter[i].image.DOFade(0f, 1f);
            }

            for (int i = 0; i < Alter.Length; i++) {
                Alter[i].DOFade(0f, 1f);
            }
        }
        yield return Timing.WaitForSeconds(0.5f);
        for (int i = 0; i < btsAlter.Length; i++) {
            btsAlter[i].image.sprite = imgBtAlter[0];
        }

        if (pergT == 1) {
            if (palavras[numPerg].Ini != null){
                palavraT.DOText(palavras[numPerg].Ini, 1f, true, ScrambleMode.None);
                palavraNO.DOText("no diminutivo!", 1f, true, ScrambleMode.None);
            }
            // pergText.text = ;
            numberPergL = 1;
        } else if (pergT == 2) {
            if (palavras[numPerg].Ini != null) {
                palavraT.DOText(palavras[numPerg].Ini, 1f, true, ScrambleMode.None);
                palavraNO.DOText("normalmente!", 1f, true, ScrambleMode.None);
            }
            //pergText.text = "Complete a Palavra '" + palavras[numPerg].Ini + "', normalmente";
            numberPergL = 2;
        } else  {
            if (palavras[numPerg].Ini != null) {
                palavraT.DOText(palavras[numPerg].Ini, 1f, true, ScrambleMode.None);
                palavraNO.DOText("no aumentativo!", 1f, true, ScrambleMode.None);
                // pergText.text = "Complete a Palavra '" + palavras[numPerg].Ini + "', no aumentativo";
            }
            numberPergL = 3;
        }
        Alter[0].text = palavras[numPerg].dimitutivo;
        Alter[1].text = palavras[numPerg].normal;
        Alter[2].text = palavras[numPerg].aumentativo;


        for (int i = 0; i < btsAlter.Length; i++) {
            btsAlter[i].image.DOFade(1f, 1f);
        }

        for (int i = 0; i < Alter.Length; i++) {
            Alter[i].DOFade(1f, 1f);
        }
        // yield return Timing.WaitForSeconds(1f);
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = true;
        }
        Timing.RunCoroutine(TamanhoTime(), "stop");


    }

    public void CheckRespost(int resp) {
        for (int i = 0; i < btAlter.Length; i++) {
            btAlter[i].interactable = false;
        }

        Timing.KillCoroutines("stop");
        SoundManager2.startSoundFX(somsBt[0]);
        if (resp == 1) {
            palavraT.DOText(palavras[numPerg].dimiResposta, 0.3f, true, ScrambleMode.None);
        } else if (resp == 2) {
            palavraT.DOText(palavras[numPerg].normalResposta, 0.3f, true, ScrambleMode.None);
        } else if (resp == 3) {
            palavraT.DOText(palavras[numPerg].aumeResposta, 0.3f, true, ScrambleMode.None);
        }

        if (numberPergL == resp) {
            Timing.RunCoroutine(Corretresp());

        }
        else {
            Timing.RunCoroutine(Errtresp());

        }
      //  clickAlter = true;
        CompleTx.DOText("", 1f, true, ScrambleMode.None);
    }

    IEnumerator<float> Corretresp() {
        sdlHandler.SaveEstatistica(true);
        yield return Timing.WaitForSeconds(0.5f);
        btsAlter[numberPergL - 1].image.sprite = imgBtAlter[1];
        palavraNO.DOText(" ", 1f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(0.5f);
        palavraNO.DOText("Parabéns!", 1f, true, ScrambleMode.None);
        SoundManager2.startSoundFX(somsBt[1]);
        perguntaR.DOPivotX(0.8f, 1f);
        respCerta = respCerta + 1;
        if (respCerta == 4 || respCerta == 8) {
            levelG = levelG + 1;
            changeLevel2.startChangeLevelAnimation(levelG);
        }
        yield return Timing.WaitForSeconds(3f);
        contPergunt();
        Timing.RunCoroutine(randomPergunta());
        


    }
    IEnumerator<float> Errtresp() {
        sdlHandler.SaveEstatistica(false);
        yield return Timing.WaitForSeconds(0.5f);
        for (int i = 0; i < btsAlter.Length; i++) {
            btsAlter[i].image.sprite = imgBtAlter[2];
        }
        btsAlter[numberPergL - 1].image.sprite = imgBtAlter[1];
        palavraNO.DOText(" ", 1f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(0.5f);
        SoundManager2.startSoundFX(somsBt[2]);
        // Timing.RunCoroutine(MovePranchaTime());
        // pranchaG2.DOPivotX(0.1f * 0.0001f, 1f);
        xPrancha = pranchaG2.transform.localPosition.x;
        pranchaG2.DOLocalMoveX(xPrancha+50f, 1f, false);
        SoundManager2.startSoundFX(somsBt[2]);
        if (numberPergL == 1) {
            palavraNO.DOText("O Correto é " + palavras[numPerg].dimiResposta + "!", 1f, true, ScrambleMode.None);
        } 
        else if (numberPergL == 2) {
            palavraNO.DOText("O Correto é " + palavras[numPerg].normalResposta + "!", 1f, true, ScrambleMode.None);
        } 
        else if (numberPergL == 3) {
            palavraNO.DOText("O Correto é " + palavras[numPerg].aumeResposta + "!", 1f, true, ScrambleMode.None);
        }
        perguntaR.DOPivotX(1f, 1f);
        respErrada = respErrada + 1;
        yield return Timing.WaitForSeconds(3f);
        contPergunt();
        Timing.RunCoroutine(randomPergunta());
       
    }
    IEnumerator<float> SemResp() {
       
        Timing.KillCoroutines("stop");
        yield return Timing.WaitForSeconds(0.5f);
        for (int i = 0; i < btsAlter.Length; i++) {
            btsAlter[i].image.sprite = imgBtAlter[2];
        }
        btsAlter[numberPergL - 1].image.sprite = imgBtAlter[1];
        palavraNO.DOText(" ", 1f, true, ScrambleMode.None);
        yield return Timing.WaitForSeconds(0.5f);
        // Timing.RunCoroutine(MovePranchaTime());
        // pranchaG2.DOPivotX(0.1f * 0.0001f, 1f);
        // xPrancha = pranchaG2.transform.localPosition.x;
        // pranchaG2.DOLocalMoveX(xPrancha + 50f, 1f, false);
        if (numberPergL == 1) {
            palavraNO.DOText("Terminou o Tempo, o Correto é " + palavras[numPerg].dimiResposta + "!", 1f, true, ScrambleMode.None);
        } else if (numberPergL == 2) {
            palavraNO.DOText("Terminou o Tempo, o Correto é " + palavras[numPerg].normalResposta + "!", 1f, true, ScrambleMode.None);
        } else if (numberPergL == 3) {
            palavraNO.DOText("Terminou o Tempo, o Correto é " + palavras[numPerg].aumeResposta + "!", 1f, true, ScrambleMode.None);
        }
        perguntaR.DOPivotX(1f, 1f);
        // respErrada = respErrada + 1;
        yield return Timing.WaitForSeconds(3f);
        contPergunt();
        Timing.RunCoroutine(randomPergunta());

    }

    void contPergunt() {
        if (contPerg <= 4) {
            contBarra = 10;
        } else if (contPerg >= 5 && contPerg <= 10) {
            contBarra = 7;
        } else if (contPerg >= 11) {
            contBarra = 3.2f;
        }
       // numberPergL = 0;
        contPerg = contPerg + 1;
        if (contPerg == 5) {
            coll[0].enabled = false;
        }
    }

    public void Ativarcanvas(bool ativarB) {
        for (int i = 0; i < desfim.Length; i++) {
            if (desfim[i] != null) {
                desfim[i].SetActive(ativarB);
            }
        }
    }

}