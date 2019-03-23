using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

public class PanelDesafioControl : OverridableMonoBehaviour {

	public int bookID;
	public int idMinigame;
	public int numberGame;
    public int limitPergunt;
	 public int numberPergunt;
	public GameObject[] btAlternatives;
	Sprite alterimgIni;
	public GameObject confirmBt;
	public Button confirmBt2;
	public Text questionText;
	public Text[] alternativesText;
	public GameObject control;
	public GameConfig config;
	public List<question> questionsToAsk = new List<question>();
	public int userchoise = -1;

	public bool checkFimAnim;
	public UnityEvent OnFinal = new UnityEvent();

	public Color SelAlter;

	public Color SelNotAlter;
	public GameObject CanvasFim;

	[HeaderAttribute("Pontuação")]
	public int scoreAmount;
	public int starAmount;
	public int correctAmount;
	public FinalScore scoreFinal;
	public GameObject desCen;
	public GameObject selperson;
	public GameObject BalaoProfInteragindo;
	public GameObject prof;
	public int contN;
	public Text texBalao;
	public Color[] texBalaoColor;

	public string[] texCertoR;
	public string[] texErradoR;

	public SpriteState btAltSpr;
	public Sprite btAltCerta;
	public Sprite btAltErrada;
	public Sprite[] certoErradoSpr;
	public Image certoErradoG;
	public GameObject PauseButton;
	public bool timerStart = false;
	public float timerHere = 0f;
	public int scoreInteragindo = 0;
	public Text textPoints;
	public Text textPoints2;
    public espressFacialProf espressFacialProf2;
    [SpaceAttribute(10)]
	[HeaderAttribute("Animation Settings")]
	public AnimationCurve textPointsEffectCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

	public Text confirmProx;
	public string[] confirmProxTex;

	[HeaderAttribute("LOG SYSTEM")]
	public LogSystem log;
	public bool checkBtProxima;

	[SpaceAttribute(10)]
	[HeaderAttribute("Animation Settings")]
    public float IncreaseScoreEffectDuration = 0.3f;
    public SoundManager soundManager;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AudioClip[] audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    bool ExpressFacialProfCheck=false;


    #region HashAnimations

    static readonly int contNHash = Animator.StringToHash("contN");
    static readonly int panelDesafioNumberHash = Animator.StringToHash("panelDesafioNumber");
    static readonly int CertoErradoHash = Animator.StringToHash("CertoErrado");
    static readonly int PanelFinalHash = Animator.StringToHash("PanelFinal");

    #endregion

    // RainbowController controller1;
    void Start () {
        espressFacialProf2.olhosFechadosI = espressFacialProf2.olhosFechados.GetComponent<Image>();
        espressFacialProf2.bocaI = espressFacialProf2.bocaAberta.GetComponent<Image>();
        if (textPoints2 != null) {
            textPoints.text = textPoints2.text;
        }
		alterimgIni = btAlternatives[0].GetComponent<Image>().sprite;
		btAltSpr = btAlternatives[0].GetComponent<Button>().spriteState;
		
		if (PauseButton.activeInHierarchy == false) {
			//PauseButton.SetActive (true);
		}
		confirmBt2.interactable=false;
		LoadQuestion();
		
		checkFimAnim=false;
		if (desCen != null) {
			StartCoroutine(DesCenM());
		}
		numberPergunt = 0;
        textPoints.text = scoreAmount.ToString();
        Timing.RunCoroutine(SomProf());
        StartCoroutine(ExpressFacialProf());

    }
    

    IEnumerator<float> SomProf() {
        yield return Timing.WaitForSeconds(1.2f);

            soundManager.startVoiceFXReturn(audio[0]);

        



    }
    IEnumerator ExpressFacialProf() {
        yield return Yielders.Get(1f);
        espressFacialProf2.enabled = true;
    }

    public override void UpdateMe() {
		if (timerStart) {
			timerHere += Time.deltaTime;
		}
	}

    void ExpressFacialProf202() {

        StopCoroutine(ExpressFacialProf());
        espressFacialProf2.olhosFechadosI.enabled = false;
        espressFacialProf2.bocaI.enabled = false;
        espressFacialProf2.enabled = false;
        espressFacialProf2.olhosFechadosI.enabled = false;
        espressFacialProf2.bocaI.enabled = false;

    }


    IEnumerator DesCenM(){
		yield return Yielders.Get (3f);	
		if (desCen != null) {
			desCen.SetActive (false);
		}
		if (selperson != null) {
			selperson.SetActive (false);
		}
	}
	

	public void IntercativeBt () {
        if (!ExpressFacialProfCheck) {
            ExpressFacialProfCheck = true;
            StartCoroutine(ExpressFacialProf());
        }
       // StartCoroutine(ExpressFacialProf());

        if (contN == 0) {
			GetComponent<Animator>().SetInteger(contNHash, contN);
			
		} else {
			prof.SetActive (true);
			GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 1);
			foreach (var item in btAlternatives) {
				item.GetComponent<Image>().raycastTarget=true;

			}

			
		}
		
	}



	public void IntercativeBtConfirm () {
		confirmBt.GetComponent<Button>().enabled=true;
		confirmBt.GetComponent<Image>().raycastTarget=true;
		confirmBt.GetComponent<Button>().interactable=true;

	}

	IEnumerator TempoVIntercativeBt(){
		GetComponent<Animator>().SetInteger(contNHash, contN);
		yield return Yielders.Get (1f);
		if (BalaoProfInteragindo.activeInHierarchy == false) {
			foreach (var item in btAlternatives) {
				item.GetComponent<Image>().raycastTarget=true;

			}
		

		}

	}

		
	
	void CheckFimAnimM () {
		//GetComponent<Animator>().SetBool("checkFimAnim",false);
		
	}



	public void BtAlternative_A () {
		confirmBt.GetComponent<Button>().interactable=true;
		confirmBt.GetComponent<Button>().enabled=true;
		userchoise = 0;
        /*if (questionsToAsk[numberPergunt].rightAlternative == questionsToAsk[numberPergunt].alternatives[userchoise]) {
		}*/
        soundManager.startVoiceFXReturn(audio[1]);
    }
	public void BtAlternative_B () {
		confirmBt.GetComponent<Button>().interactable=true;
		confirmBt.GetComponent<Button>().enabled=true;
		userchoise = 1;
        soundManager.startVoiceFXReturn(audio[1]);

    }
	public void BtAlternative_C () {
		confirmBt.GetComponent<Button>().interactable=true;
		confirmBt.GetComponent<Button>().enabled=true;
		userchoise = 2;
        soundManager.startVoiceFXReturn(audio[1]);
    }
	public void BtAlternative_D () {
		confirmBt.GetComponent<Button>().interactable=true;
		confirmBt.GetComponent<Button>().enabled=true;
		userchoise = 3;
        soundManager.startVoiceFXReturn(audio[1]);
    }
	IEnumerator ChamarCanvas(){
			if(CanvasFim != null){
			yield return Yielders.Get (0.1f);
				CanvasFim.SetActive(true);
			}
		
	}
	IEnumerator ChamarPerg(){
		yield return Yielders.Get (2f);
		if(contN != 0)
			GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 1);


	}
	public void MudarTexConfirm(){
		
		if (checkBtProxima)
			confirmProx.text = confirmProxTex[1];
		else 
			confirmProx.text = confirmProxTex[0];

		if (numberPergunt >= limitPergunt) {
			confirmProx.text= "Final!";			
		}
	}

	void BtCertoErrado(){
		GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 0);
	}
	public void ChamarPergunt(){
		UpdateTexts(numberPergunt);
		MudarTexConfirm();
		GetComponent<Animator>().SetBool(CertoErradoHash, false);
		foreach (var item in btAlternatives) {
			
			item.GetComponent<Button>().interactable=true;
			item.GetComponent<Image>().sprite = alterimgIni;
		}

	}
	public void PularIntro (){
        soundManager.startVoiceFXReturn(audio[1]);
        if (contN==0){
			contN = 1;
            ExpressFacialProf202();
            GetComponent<Animator>().SetInteger(contNHash, contN);			
		}
		else	
			GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 2);
	}


	public void Bt_Sim () {
		if (!checkBtProxima && userchoise >= 0) {
			GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 10);
			checkBtProxima = true;
			confirmBt.GetComponent<Button>().interactable=false;
			confirmBt.GetComponent<Image>().raycastTarget=false;
			confirmBt.GetComponent<Button>().enabled=false;
            correction();
            numberPergunt++;
            

			foreach (var item in btAlternatives) {
				item.GetComponent<Image>().raycastTarget=false;
				item.GetComponent<Button>().interactable=false;

			}
			userchoise = -1;
          

        } else if(checkBtProxima){
			if (numberPergunt >= limitPergunt) {
				this.confirmBt.SetActive(false);
				this.confirmBt.GetComponent<Button>().interactable=false;	
				this.confirmBt.GetComponent<Image>().raycastTarget=false;
				this.confirmBt.GetComponent<Button>().enabled=false;
				contN = 0;
				OnFinal.Invoke();

				if (CanvasFim != null) {

				}			
			} else {
				this.confirmBt.SetActive(false);
				this.confirmBt.GetComponent<Button>().interactable=false;	
				this.confirmBt.GetComponent<Image>().raycastTarget=false;
				this.confirmBt.GetComponent<Button>().enabled=false;
				checkBtProxima = false;		
				GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 2);

			}

		}
        soundManager.startVoiceFXReturn(audio[2]);
    }
	IEnumerator TempoVoltar0(){
		yield return Yielders.Get(5f);
		//foreach (var item in btAlternatives) {
			//item.GetComponent<Button>().colors =  btAlt;
	//	}
		GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 2);

	}



	void pergT(){
		GetComponent<Animator>().SetInteger(panelDesafioNumberHash, 1);
	}

    public IEnumerator<float> scoreIncrease(int increase) {

       
        float times = 0.0f;
        int startPoints = scoreAmount;
        int scoreT = scoreAmount + increase;
        scoreAmount += increase;
        //Debug.Log(IncreaseScoreEffectDuration.ToString());
        while (times < IncreaseScoreEffectDuration) {
            times += Time.deltaTime;
            float s = times / IncreaseScoreEffectDuration;

            int scory = (int)Mathf.Lerp(startPoints, scoreT, textPointsEffectCurve.Evaluate(s));
            textPoints.text = scory.ToString();
            yield return Timing.WaitForOneFrame;
        }

    }
    IEnumerator TempoSomAcerErro(int mt) {
        yield return Yielders.Get(0.5f);
        soundManager.startVoiceFXReturn(audio[mt]);
    }
    public void correction(){
		if (questionsToAsk[numberPergunt].alternatives[userchoise].isCorrect) {
			certoErradoG.sprite = certoErradoSpr[0];
           
            btAlternatives[userchoise].GetComponent<Image>().sprite = btAltCerta;
			int rn = Random.Range (0, texCertoR.Length);
			texBalao.text = texCertoR[rn];
			texBalao.color = texBalaoColor [0];
			SaveEstatistica(questionsToAsk[numberPergunt], true);
            //scoreAmount += correctAmount;
            Timing.RunCoroutine(scoreIncrease(correctAmount));
			//textPoints.text =  "" + (int)Mathf.Lerp (scoreAmount, correctAmount, textPointsEffectCurve.Evaluate (1));
			if (log != null) {
				log.AddPontosInteragindo (correctAmount);
			}
            StartCoroutine(TempoSomAcerErro(3));
        } else {
			certoErradoG.sprite = certoErradoSpr[1];
           
            btAlternatives[userchoise].GetComponent<Image>().sprite = btAltErrada;
			int rn = Random.Range (0, texCertoR.Length);
			texBalao.text = texErradoR[rn];
			texBalao.color = texBalaoColor [1];
			SaveEstatistica(questionsToAsk[numberPergunt], false);
			for (int i = 0; i < 4; i++) {
				if (questionsToAsk[numberPergunt].alternatives[i].isCorrect){
				//	yield return Yielders.Get(0.2f);
					btAlternatives[i].GetComponent<Image>().sprite = btAltCerta;
				} 


			}
            //StartCoroutine(QuestionsCorret());
            StartCoroutine(TempoSomAcerErro(4));
        }
		GetComponent<Animator>().SetBool(CertoErradoHash, true);
	
	}

	public void SaveEstatistica(question thisQuestion, bool isRight){
        DBOESTATISTICA_DIDATICA statisticTemp = new DBOESTATISTICA_DIDATICA
        {
            acertou = (isRight) ? 1 : 0,
            idDificuldade = thisQuestion.idDificuldade,
            idHabilidade = thisQuestion.idHabilidade,
            idLivro = thisQuestion.idLivro,
            idGameDidatico = -1
        };
        if (log != null) {
            log.SaveEstatistica(statisticTemp);
        }

        //config.SaveStatistic(statisticTemp);
	}

	public void LoadQuestion(){

		DataService data = config.openDB();

        List<DBOPERGUNTAS> questions = new List<DBOPERGUNTAS>();

        if (config.sincModePerguntas == 3 && config.clientID != 1) {
            questions = data.GetQuestionList(bookID, config.clientID);
        } else if (config.sincModePerguntas == 2) {
            questions = data.GetQuestionListE(bookID, config.clientID);
        } else {
            questions = data.GetQuestionListE(bookID, 1);
        }		

		questions.Suffle();

		List<DBOPERGUNTAS> choosenQuestion = new List<DBOPERGUNTAS>();

		for (int i = 0; i < limitPergunt; i++) {
			choosenQuestion.Add(questions[i]);
		}

		questionsToAsk.Clear();
		for (int i = 0; i < choosenQuestion.Count; i++) {
            question questionTemp = new question() {
                questionString = choosenQuestion[i].textoPergunta,
                idHabilidade = choosenQuestion[i].idHabilidade,
                idDificuldade = choosenQuestion[i].idDificuldade,
                idPergunta = choosenQuestion[i].idPergunta,
                idLivro = choosenQuestion[i].idLivro,
            };
            DBOPERGUNTAS pTemp = choosenQuestion[i];
			List<DBORESPOSTAS> rTempList = data.GetAnswersList(pTemp.idPergunta);
            rTempList.Suffle();
            for (int j = 0; j < rTempList.Count; j++) {
				questionTemp.alternatives.Add(DBORespostaToAlternatives(rTempList[j]));
				if (rTempList[j].correta == 1) {
					questionTemp.rightAlternative = rTempList[j].textoResposta;
					questionTemp.rightAlternativeInt = j;
				}
			}
			questionTemp.alternatives.Suffle();
			questionsToAsk.Add(questionTemp);
		}
        Timing.RunCoroutine(LoadAudioData());
        UpdateTexts(numberPergunt);
	}

	public void UpdateTexts(int numberPergunt){
		questionText.text = questionsToAsk[numberPergunt].questionString;
		for (int i = 0; i < alternativesText.Length; i++) {
			alternativesText[i].text = questionsToAsk[numberPergunt].alternatives[i].textoPergunta;
		}
	}

	public void endQuestions(){
		timerStart = false;
		if (log != null) {
			log.StartTimerDidatica (false);
			log.SendGameLog ();
		}
		GetComponent<Animator>().SetBool(PanelFinalHash,true);
		scoreFinal.scoreAmount = this.scoreAmount;
		scoreFinal.starsAmount = this.starAmount;
		scoreFinal.OnFinalStart.Invoke ();
	}
	public void PauseOff(){
		if (PauseButton.activeInHierarchy == true) {
			PauseButton.SetActive (false);
		}   
	}


	public void EndAllCoroutines(){
        Timing.KillCoroutines();
	}

    public IEnumerator<float> LoadAudioData(){
        int tempCount = questionsToAsk.Count;
        for (int i = 0; i < tempCount; i++){
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + config.fullAudioClipDestinationQuestions + questionsToAsk[i].idPergunta +".ogg", AudioType.OGGVORBIS)) {
                yield return Timing.WaitUntilDone(www.SendWebRequest());
                if (www.isNetworkError) {
                } else {
                    questionsToAsk[i].audioClip = DownloadHandlerAudioClip.GetContent(www);
                    int count = questionsToAsk[i].alternatives.Count;
                    for (int j = 0; j < count; j++) {
                        using(UnityWebRequest answerWWW = UnityWebRequestMultimedia.GetAudioClip("file://" + config.fullAudioClipDestinationAnswers + questionsToAsk[i].alternatives[j].idPergunta + ".ogg", AudioType.OGGVORBIS)) {
							yield return Timing.WaitUntilDone(answerWWW.SendWebRequest());

                            if (www.isNetworkError) {

                            } else {
                                questionsToAsk[i].alternatives[j].audioClip = DownloadHandlerAudioClip.GetContent(answerWWW);
                            }
                        }
                    }
                }
            }
        }     
    }

    public Alternatives DBORespostaToAlternatives(DBORESPOSTAS dboResposta) {
        return new Alternatives() {
            idPergunta = dboResposta.idPergunta,
            idResposta = dboResposta.idResposta,
            isCorrect = dboResposta.correta == 1 ? true : false,
            textoPergunta = dboResposta.textoResposta,
        };
    }



}
