using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class managerAddRainbow : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Configurações")]
	#endif
	public int timesToAsk = 4;
	public int timesAsk = 0;
	public int scoreAmount = 0;
	public int starAmount;
	public int scorePerRight = 30;
	public int rightResult;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Adição e Subtração Painel")]
	#endif
	public GameObject mainPanel;
	public GameObject oldPanel;
	public RainbowController rainbowController;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	//[SeparatorAttribute("Painéis Laterais ")]
	#endif
	//public Text[] sidePanelTextsName = new Text[3];
	//public Text[] sidePanelTextsCount = new Text[3];
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Botões de Opções!")]
	#endif
	public int[] alternatives = new int[4];
	public int alternativeSelected = -1;
	public Text[] alternativeTexts = new Text[4];
	public Button confirmButton;
	public Button[] buttonsAlternative;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Textos do Painel Principal")]
	#endif
	public Text firstNumberText;
	public Text secondNumberText;
	public Text sinalOfOperationText;
	public Text resultOfOperation;
	//public Image[] imagePanels;
	public Color selectionColor;
	public Color defaultColor;
	public ItemIndexed[] itensToCount;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Tela de Parabéns")]
	#endif
	public Animator congratsAnimator;
	public GameObject congratsPanel;
	public Text congratsText;
	private int acertoErrohash;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Desafio")]
	#endif
	public Animator chalengePanelAnimator;
	public GameObject chalengePanel;
	public PanelDesafioControl challengeController;
	public int panelDesafioNumberHash;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Painel Tutorial")]
	#endif
	public GameObject painelTutorial;
	public GameObject pointT;
	public int scoreDidatica = 0;
	public float timePedagogica = 0f;
	public bool timerStart = false;
    public Image characterImage;
    public Sprite[] characterImages;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Configuração Didatica")]
	#endif
	public int idHabilidade;
	public int idDificuldade;
	public LogSystem log;
	public Text facaConta;
	public string[] facaContaT;
	public Image[] buttons2;
	public Sprite[] buttonsSp;
    tutorPanelArcoirirs tutorPanelArcoirirs2;
    int contNunbAudioPos;
    int contNunbAudioNeg;
    public GameObject pauseBt;
    public SoundManager soundManager;
    public AudioClip acertoAudioClip;
    public AudioClip erroAudioClip;


    // Use this for initialization
    public void StartGame () {
		Timing.RunCoroutine(StartGameLate(), Segment.SlowUpdate);

	}

	IEnumerator<float> StartGameLate(){
        yield return Timing.WaitForSeconds(0.6f);
        pauseBt.SetActive(false);
        // Debug.Log("Soma e Subtração ativada! açlskdçlaksdçalksdçls");
        tutorPanelArcoirirs2 = painelTutorial.GetComponent<tutorPanelArcoirirs>();
          
        int person = PlayerPrefs.GetInt("characterSelected", 0);

        characterImage.sprite = characterImages[person];

        painelTutorial.SetActive(false);
		yield return Timing.WaitForSeconds(2f);
		acertoErrohash = Animator.StringToHash("acertoErro");
		panelDesafioNumberHash = Animator.StringToHash("panelDesafioNumber");
		chalengePanel.SetActive(false);
		mainPanel.SetActive(true);
		oldPanel.SetActive(false);

		itensToCount = rainbowController.ReturnTopThree();

		/*for (int i = 0; i < 3; i++) {
			sidePanelTextsName[i].text = rainbowController.returnName(itensToCount[i].type);
			sidePanelTextsCount[i].text = rainbowController.returnAmountsItem(itensToCount[i].id).ToString();
		}*/

		scoreAmount = rainbowController.amountPoints;
		starAmount = rainbowController.amountStars;
		//rainbowController.enabled = false;
		scoreDidatica = rainbowController.scorePegagogica;
		timePedagogica = rainbowController.timerDidatica;
		timerStart = rainbowController.isTimerLogDidaticaStart;
        
        NextQuestion();
        Timing.RunCoroutine(timingTODisable());
        confirmButton.interactable = false;
        yield return Timing.WaitForSeconds(2f);
        congratsPanel.SetActive(true);
        congratsAnimator.enabled = true;
    }

    IEnumerator<float> timingTODisable() {
        yield return Timing.WaitForSeconds(5f);
        
        rainbowController.enabled = false;
    }

	public void buttonAnswer01(){
		alternativeSelected = alternatives[0];
		buttons2[0].sprite = buttonsSp[1];
		buttons2[1].sprite = buttonsSp[0];
		buttons2[2].sprite = buttonsSp[0];
		buttons2[3].sprite = buttonsSp[0];
		resultOfOperation.text = alternativeSelected.ToString();
        confirmButton.interactable = true;

    }

	public void buttonAnswer02(){
		alternativeSelected = alternatives[1];
		buttons2[0].sprite = buttonsSp[0];
		buttons2[1].sprite = buttonsSp[1];
		buttons2[2].sprite = buttonsSp[0];
		buttons2[3].sprite = buttonsSp[0];
		resultOfOperation.text = alternativeSelected.ToString();
        confirmButton.interactable = true;
    }

	public void buttonAnswer03(){
		alternativeSelected = alternatives[2];
		buttons2[0].sprite = buttonsSp[0];
		buttons2[1].sprite = buttonsSp[0];
		buttons2[2].sprite = buttonsSp[1];
		buttons2[3].sprite = buttonsSp[0];
		resultOfOperation.text = alternativeSelected.ToString();
        confirmButton.interactable = true;
    }

	public void buttonAnswer04(){
		alternativeSelected = alternatives[3];
		buttons2[0].sprite = buttonsSp[0];
		buttons2[1].sprite = buttonsSp[0];
		buttons2[2].sprite = buttonsSp[0];
		buttons2[3].sprite = buttonsSp[1];
		resultOfOperation.text = alternativeSelected.ToString();
        confirmButton.interactable = true;
    }

	public void buttonConfirm(){
		confirmButton.interactable = false;
		buttons2[0].sprite = buttonsSp[0];
		buttons2[1].sprite = buttonsSp[0];
		buttons2[2].sprite = buttonsSp[0];
		buttons2[3].sprite = buttonsSp[0];
		QuestionCorrection();
	}

	public void QuestionCorrection(){
		timesAsk++;
		bool isRight = false;
		if (alternativeSelected == rightResult) {
			isRight = true;
		}
		ButtonsEnable(false);
		Timing.RunCoroutine(CoroutineCorrection(isRight));
	}

	IEnumerator<float> CoroutineCorrection(bool isRight){
		//TODO Mostrar Parabéns aqui.
		//TODO adicionar pontos.
		congratsPanel.SetActive(true);
        congratsAnimator.enabled = true;
        if (isRight) {
			congratsText.text = "Acertou! \b +30 Pontos";
			//rainbowController.GameConfig.didaticaStatic(true, idHabilidade, idDificuldade, idMinigame);
			log.SaveEstatistica (114, true);
			log.AddPontosPedagogica (30);
            scoreAmount += 30;
            scoreDidatica += 30;           
            congratsAnimator.SetInteger (acertoErrohash,1);
            soundManager.startSoundFX(acertoAudioClip);
           //Debug.Log("1ssr");
		} else {
			congratsText.text = "Errou! \n O certo era " +  rightResult.ToString();
			//rainbowController.GameConfig.didaticaStatic(false, idHabilidade, idDificuldade, idMinigame);
			log.SaveEstatistica (114, false);
            congratsAnimator.SetInteger (acertoErrohash,2);
            soundManager.startSoundFX(erroAudioClip);
            //Debug.Log("2dt");
        }
		yield return Timing.WaitForSeconds(1.5f);
		congratsAnimator.SetInteger (acertoErrohash,0);
		congratsAnimator.SetInteger (acertoErrohash,0);
        confirmButton.interactable = true;
        //congratsAnimator.enabled = false;
        // congratsPanel.SetActive(false);
        NextQuestion();
	}

	public void NextQuestion(){
		if (timesAsk < timesToAsk) {
			if (timesAsk == 0) {

                int firstTemp = Random.Range(6, 9);
                int secondTemp = Random.Range(1, 5);
                idHabilidade = 5;

                ConfigureQuestion(firstTemp, secondTemp, true);
				ColorHighlight(0, 1);
			} else if (timesAsk == 1) {

                int firstTemp = Random.Range(6, 9);
                int secondTemp = Random.Range(1, 5);
                idHabilidade = 6;
                ConfigureQuestion(firstTemp, secondTemp, false);
				ColorHighlight(0,2);
			}else if (timesAsk == 2) {

                int firstTemp = Random.Range(10, 15);
                int secondTemp = Random.Range(1, 5);
                idHabilidade = 7;
                ConfigureQuestion(firstTemp, secondTemp, true);
				ColorHighlight(1, 2);
			}else if (timesAsk == 3) {

                int firstTemp = Random.Range(10, 15);
                int secondTemp = Random.Range(1, 5);
                idHabilidade = 8;
                ConfigureQuestion(firstTemp, secondTemp, false);
				ColorHighlight(0, 1);
			}
            confirmButton.interactable = false;

        } else {
			//TODO chamar painel de desafio aqui.
			Timing.RunCoroutine(CallChalenge());
		}
	}

	IEnumerator<float> CallChalenge(){
		challengeController.scoreAmount = this.scoreAmount;
		challengeController.starAmount = this.starAmount;
		chalengePanel.SetActive(true);
		//rainbowController.HUD [1].SetActive (true);
		pointT.SetActive (false);
		chalengePanelAnimator.SetInteger(panelDesafioNumberHash, 1);
		yield return Timing.WaitForSeconds(2f);
		mainPanel.SetActive(false);
		this.enabled = false;

		log.StartTimerDidatica (false);

	}


	public void ColorHighlight(int _first, int _second){
		/*for (int i = 0; i < 3; i++) {
			if (i == _first || i == _second) {
				imagePanels[i].color = selectionColor;
			} else {
				imagePanels[i].color = defaultColor;
			}
		}*/
	}

	public void ConfigureQuestion(int _first,int _second,bool isAddition){
		resultOfOperation.text = "?";
		firstNumberText.text = _first.ToString();
		secondNumberText.text = _second.ToString();

		int tempMin;
		int tempMax;
		int correctResult;

		if (isAddition) {
			correctResult = _first + _second;
			sinalOfOperationText.text = "+";
            contNunbAudioPos = 1;
            if (contNunbAudioPos<2) {
                contNunbAudioPos = contNunbAudioPos+1;
                tutorPanelArcoirirs2.soundManager.startVoiceFXReturn(tutorPanelArcoirirs2.audiosTutorial[8]);
            }
           
            facaConta.text = facaContaT[0];
		} else {
			correctResult = _first - _second;
			sinalOfOperationText.text = "-";
            if (contNunbAudioNeg < 2) {
                contNunbAudioNeg = contNunbAudioNeg+1;
                tutorPanelArcoirirs2.soundManager.startVoiceFXReturn(tutorPanelArcoirirs2.audiosTutorial[9]);
            }           
            facaConta.text = facaContaT[1];
		}

		if (correctResult - 10 <= 0) {
			tempMin = correctResult - (correctResult - 1);
		} else {
			tempMin = correctResult - 10;
		}

		tempMax = correctResult + 10;

		List<int> alternativesTemp = new List<int>();

		alternativesTemp.Add(correctResult);

		for (int i = 0; i < 3; i++) {
			int temp = 0;

			temp = Random.Range(tempMin,tempMax);
			while (alternativesTemp.Contains (temp)) {
				temp = Random.Range(tempMin,tempMax);
			}

			bool isOnList = alternativesTemp.Contains (temp);

			alternativesTemp.Add (temp);

		}

		alternatives = alternativesTemp.OrderBy (x => x).ToArray();

		for (int i = 0; i < 4; i++) {
			alternativeTexts[i].text = alternatives [i].ToString ();
		}

		confirmButton.interactable = true;

		rightResult = correctResult;

		ButtonsEnable(true);
	}

	public void ButtonsEnable(bool _enable){
		for (int i = 0; i < 4; i++) {
			buttonsAlternative[i].interactable = _enable;
		}
	}
		


}
