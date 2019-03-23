using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;

public class Manager_1_1B : MonoBehaviour {
    public GDLHandler gdlLog;
	public int level = 0;
	public SpriteRenderer[] cardMaos;

	public SpriteRenderer cardMao;
	public GameObject[] partsCards;
	public GameObject partCards;

	public GameObject[] partsCards2;
	public GameObject partCards2;
	public MemoryGameManager cardManager;
	public DragCard_1_1B[] dragCards = new DragCard_1_1B[5];
	public CanvasGroup[] dragCardsCanvasGroup = new CanvasGroup[5];
	public GridLayoutGroup groupLayout;
	public GridLayoutGroup groupLayoutDrop;
	public GameObject panelDitatica;
	public Transform parentDrag;
	public Transform parentDrop;
	public Animator parentDropAnimator;
	public Transform parentMiddle;
	GridLayoutGroup parentMiddleGridLayout;
	public GameObject prefabMiddle;
	public List<GameObject> middleCardsPositions = new List<GameObject>();
	public DropCard_1_1B[] dropCards = new DropCard_1_1B[5];
	public List<Sprite> spritesChoosen = new List<Sprite>();
	public GameObject[] deactiveThisPanels;
	public GameObject nextButton;
	public Text scoreTextAnimComponent;
	public AudioClip[] clips = new AudioClip[13];
    public AudioClip[] fxsClips = new AudioClip[3];
	public SoundManager sound;

	public bool isPlayTime = false;
	public int scoreMade = 0;
	public GameObject panelFinal;
//	public GameObject panelFinalC;
	public FinalScore finalScore;
	public Color RedColor;
	public Color GreenColor;
	public Color YellowColor;
	public Text textRight;
	public Text textWrong;

	[HeaderAttribute("Animation Settings")]
	public float repositionDuration;
	public AnimationCurve repositionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float WaitScoreToShow;
	public float showScoreDuration;
	public AnimationCurve showScoreCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Vector3 makeDeckScaleSize;
	public float makeDeckDuration = 1.0f;
	public AnimationCurve makeDeckCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float fadeInButtonDuration = 1.0f;
	public AnimationCurve fadeInButtonCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Vector3 posOffsetText;
	public float wrongTextDuration = 1.0f;
	public AnimationCurve wrongTextCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public GameObject particulasCartsDita;
 
	public ExpressaoCorporal zecaCard;


	public GameObject panelDesafio;
	Animator panelDesafioAnimator;
	public int contCart;

	bool checkpanelDesafio;
	private MemoryGameManager managerMemory;
	public PanelDesafioControl panelDesafioController;
	public GameObject interagindoG;

	[HeaderAttribute("LOG")]
	public int idHabilidade;
	public int idDificuldade;
	public LogSystem LOG;


	// Use this for initialization
	void Start () {
        Resources.UnloadUnusedAssets();
        parentMiddleGridLayout = parentMiddle.GetComponent<GridLayoutGroup>();
		parentDropAnimator = parentDrop.GetComponent<Animator>();
		panelDesafioAnimator = panelDesafio.GetComponent<Animator>();
		managerMemory = GetComponent<MemoryGameManager>();
		zecaCard = managerMemory.personReation;
//particulasCartsDita.SetActive(false);
		isPlayTime = false;
		level = 0;
		//cardManager.spriteCards.Suffle();
		for (int i = 0; i < 5; i++){
			dragCards[i].gameObject.SetActive(false);
		}
		//cardMao.enabled=false;
		partCards.SetActive(false);
		partCards2.SetActive(false);
		cardMao.enabled=false;
		//panelDesafio.SetActive(checkpanelDesafio);
	}
	
	// Update is called once per frame
	
 	 IEnumerator TemPartc()	{

	yield return Yielders.Get(2f);
	partCards.SetActive(false);
	partCards2.SetActive(false);
		cardMao.enabled=false;
		
	}
	public void verifyCards(){

		int dropCardsLength = dropCards.Length;
		int temp = 0;
		for (int i = 0; i < dropCardsLength; i++){
			if(dropCards[i].cardDraged != null){
				temp++;
			}
		}

		if(temp >= 5){
			//verify Answers;
				scoreMade = 0;
			for (int i = 0; i < dropCardsLength; i++){
					dropCards[i].cardDraged.ParticleItem.SetActive(false);
					if(dropCards[i].characterSprite == dropCards[i].cardDraged.characterSprite){
					scoreMade++;
					}
				}
				isPlayTime = false;
				//Debug.Log("Check if its Correct");
				StartCoroutine(correction());
		}

	}

	IEnumerator correction(){
        //parentDropAnimator.SetBool("nextCanbeStarted", false);
        parentDropAnimator.enabled = false;
        yield return Yielders.Get(0.2f);
		//parentDropAnimator.SetBool("nextCanbeStarted", false);
        parentDropAnimator.enabled=false;
		int score = scoreMade * 10;
		int dropCardsLength = dropCards.Length;
		scoreTextAnimComponent.text = "Parabéns, você acertou: +" + score.ToString();
        LOG.AddPontosPedagogica(score);
        gdlLog.AddScore(score);
        for (int i = 0; i < dropCardsLength; i++){
			if(dropCards[i].characterSprite != dropCards[i].cardDraged.characterSprite){
				dropCards[i].thisoutline.effectColor = RedColor;
				if (LOG != null) {
					LOG.SaveEstatistica (58, false);
				}
			} else {
				dropCards[i].thisoutline.effectColor = GreenColor;

				if (LOG != null) {
					
					LOG.SaveEstatistica (58, true);
				}
			}
			dropCards[i].thisoutline.enabled = true;
		}

		

		List<Transform> WrongCards = new List<Transform>();
		List<DropCard_1_1B> WrongCardComponent = new List<DropCard_1_1B>();

		int middleCardsPositionsCount = middleCardsPositions.Count;
		for (int i = 0; i < middleCardsPositionsCount; i++){			
			Destroy(middleCardsPositions[i]);
		}

		int wrongs = 0;
		for (int i = 0; i < dropCardsLength; i++){			
			if(dropCards[i] != null && dropCards[i].characterSprite != dropCards[i].cardDraged.characterSprite){
				wrongs++;
				WrongCards.Add(dropCards[i].transform);
				WrongCardComponent.Add(dropCards[i]);

			}
			//zecaCard.ControlAnimCorpo.SetInteger ("posCorpoZeca",5);

		}



		parentMiddleGridLayout.enabled = false;
		
		groupLayout.enabled = false;
		middleCardsPositions.Clear();
		for (int i = 0; i < wrongs; i++){
			GameObject temp = Instantiate(prefabMiddle,parentMiddle) as GameObject;
			temp.transform.localScale = new Vector3(1f,1f,1f);
			middleCardsPositions.Add(temp);
		}
		
	//	particulasCartsDita.SetActive(true);
	 parentDropAnimator.SetBool("nextCanbeStarted", false);

		parentDropAnimator.enabled=false;
		parentMiddleGridLayout.enabled = true;
		

		StartCoroutine(StartTotalPointsInGame(score));

		yield return Yielders.Get(WaitScoreToShow);

		parentMiddleGridLayout.enabled = false;
		
		//Debug.Log("desativa");

		List<Vector3> posFrom = new List<Vector3>();
		List<Vector3> posTo = new List<Vector3>();
		for (int i = 0; i < wrongs; i++){
			WrongCards[i].SetParent(parentMiddle);
			posFrom.Add(WrongCards[i].transform.position);
		}
		for (int i = 0; i < wrongs; i++){
			posTo.Add(middleCardsPositions[i].transform.position);
		}

		groupLayoutDrop.enabled = true;
		//parentDrop.GetComponent<Animator>().enabled=false;

		float times = 0.0f;
		while (times < repositionDuration)
		{
			times += Time.deltaTime;
			float s = times / repositionDuration;

			for (int i = 0; i < wrongs; i++){
				WrongCards[i].position = Vector3.Lerp(posFrom[i],posTo[i], repositionCurve.Evaluate (s));
			}

			yield return Yielders.EndOfFrame;
		}

		for (int i = 0; i < wrongs; i++){			
			Destroy(middleCardsPositions[i]);
		}

		/*times = 0.0f;
		while (times < showScoreDuration)
		{
			times += Time.deltaTime;
			float s = times / showScoreDuration;

			scoreTextAnimComponent.transform.localScale = Vector3.Lerp(new Vector3(0f,0f,0f), new Vector3(1f,1f,1f), showScoreCurve.Evaluate(s));
			scoreTextAnimComponent.color = Color.Lerp (new Color(1f,1f,1f,0f), Color.yellow, showScoreCurve.Evaluate (s));

			yield return Yielders.EndOfFrame;
		}*/

		//textHere
		if(wrongs > 0){
			textWrong.gameObject.SetActive(true);
            //sound.startSoundFX(fxsClips[1]);
        } else {
			textWrong.gameObject.SetActive(false);
            //sound.startSoundFX(fxsClips[0]);
        }

		if(wrongs < 5){
			textRight.gameObject.SetActive(true);
		} else {
			textRight.gameObject.SetActive(false);
            //sound.startSoundFX(fxsClips[2]);
        }

        //Debug.Log("Wrongs Amount" + wrongs.ToString(), this);
		
        if(scoreMade <= 0) {
            //acertar todos os nomes.
            sound.startSoundFX(fxsClips[2]);
        } else if (scoreMade > 0 && scoreMade < 5) {
            //acertar e errar ao mesmo tempo.
            sound.startSoundFX(fxsClips[1]);
        } else if (scoreMade >= 5) {
            //errar todos os nomes.
            sound.startSoundFX(fxsClips[0]);
        } 

		parentMiddleGridLayout.enabled = true;

		for (int i = 0; i < wrongs; i++){
			WrongCardComponent[i].updateRightText();
		}

		times = 0.0f;
		while (times < wrongTextDuration)
		{
			times += Time.deltaTime;
			float s = times / wrongTextDuration;

			for (int i = 0; i < wrongs; i++){
				//WrongCards[i].position = Vector3.Lerp(posFrom[i],posTo[i], repositionCurve.Evaluate (s));
				WrongCardComponent[i].cardDraged.RightOneTextComponent.transform.localPosition = Vector3.Lerp(new Vector3(0f,0f,0f),posOffsetText,wrongTextCurve.Evaluate(s));
				WrongCardComponent[i].cardDraged.RightOneTextComponent.color = Color.Lerp(new Color(1f,1f,1f,0f),YellowColor, wrongTextCurve.Evaluate(s));
				WrongCardComponent[i].cardDraged.TextComponent.color = Color.Lerp(YellowColor,RedColor, wrongTextCurve.Evaluate(s));
			}

			yield return Yielders.EndOfFrame;
		}

		groupLayoutDrop.enabled = false;
		level++;
		nextButton.SetActive(true);


	}

	public void NextButton(){

		
		textRight.gameObject.SetActive(false);
		textWrong.gameObject.SetActive(false);
		if(level < 3){
            //RunStartB();
            Timing.RunCoroutine(makeDeck());
        } else {

			if(checkpanelDesafio==false){
				interagindoG.SetActive (true);
				checkpanelDesafio=true;
				interagindoG.GetComponent<GraphicRaycaster> ().enabled = true;
				panelDesafioController.starAmount = cardManager.starsAmount;
				panelDesafioController.scoreAmount = cardManager.pointsAmount;
				panelDesafio.SetActive(checkpanelDesafio);
				panelDesafioAnimator.SetInteger("panelDesafioNumber",1);


			}
	
		}
			
		
	}

    public void ResetCardScale() {
        for (int i = 0; i < dropCards.Length; i++) {
            dropCards[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
       
    }
	
	public IEnumerator Begin11B(){

		int dragCardsLength = dragCards.Length;
		
		for (int i = 0; i < dragCardsLength; i++){
			dragCards[i].RightOneTextComponent.color = new Color(1f,1f,1f,0f);
			dragCards[i].TextComponent.color = YellowColor;
			dragCards[i].RightOneTextComponent.transform.localPosition = new Vector3(0f,0f,0f);
		}

		// remover com animator.
		for (int i = 0; i < dragCardsLength; i++){
            //if(dropCards[i] != null)
            //dropCards[i].transform.localScale = new Vector3(1f,1f,1f);
           
            if (dropCards[i] != null && dropCards[i].thisoutline != null) {
                dropCards[i].thisoutline.enabled = false;
            }
		}

        

        spritesChoosen.Clear();
		switch (level){
			case 0:
				for (int i = 0; i < 5; i++){
					spritesChoosen.Add(cardManager.spriteCards[i]);
				}
			break;	
			case 1:
				for (int i = 5; i < 10; i++){
					spritesChoosen.Add(cardManager.spriteCards[i]);
				}
			break;
			case 2:
				for (int i = 0; i < 11; i++){
					if(i%2==1){
						spritesChoosen.Add(cardManager.spriteCards[i]);
					}
				}
			break;
			default:
			break;
		}

		spritesChoosen.Suffle();
		
		for (int i = 0; i < 5; i++){			
			dragCards[i].transform.SetParent(parentDrag);
			dragCards[i].UpdateSprite(spritesChoosen[i]);
			dragCards[i].Clear();
			dragCards[i].thisCanvasGroup.blocksRaycasts = true;

		}

		spritesChoosen.Suffle();

		for (int i = 0; i < 5; i++){
			dropCards[i].transform.SetParent(parentDrop);
			dropCards[i].UpdateSprite(spritesChoosen[i]);
			dropCards[i].Clear();
		}
        //ResetCardScale();
        yield return Yielders.Get(0.6f);
        zecaCard.posCorpoZeca = 5;
        zecaCard.ControlAnimCorpo.SetInteger("posCorpoZeca", 5);

        yield return Yielders.Get(0.5f);
      //  ResetCardScale();

        //JogarCartas();

        /* for (int i = 0; i < dragCardsLength; i++) {
             //if (dropCards[i] != null)
                 dropCards[i].transform.localScale = new Vector3(1f, 1f, 1f);

         }*/


    }

	public void buttonFadin(){
		cardManager.sound.startSoundFX(cardManager.audio[4]);
		StartCoroutine(beginButtons());
	}

	public IEnumerator beginButtons(){
		for (int i = 0; i < 5; i++){
			dragCards[i].gameObject.SetActive(true);
		}

		groupLayout.enabled = true;

		float times = 0.0f;
		while (times < fadeInButtonDuration)
		{
			times += Time.deltaTime;
			float s = times / fadeInButtonDuration;

			//int scory = (int)Mathf.Lerp (startPoints, scoreT, cardManager.textPointsEffectCurve.Evaluate (s));

			float xScale = Mathf.Lerp(0f,1f, fadeInButtonCurve.Evaluate(s));
			float yScale = Mathf.Lerp(0f,1f, fadeInButtonCurve.Evaluate(s));

			for (int i = 0; i < 5; i++){
				dragCards[i].transform.localScale = new Vector3(xScale,yScale,1f);
			}

			yield return Yielders.EndOfFrame;
		}

		groupLayout.enabled = false;

       


        yield return Yielders.Get(0.1f);

        
       
        //animação pra voltar pra tela.
        //ao final de animação desativar e ativar novamento groupLayout.
        isPlayTime = true;

        yield return Yielders.Get(1.3f);
        for (int i = 0; i < dropCards.Length; i++) {
            dropCards[i].iconSoundCanvasGroup.DOFade(1f,.3f);
        }

    }


    public void RunStartB() {
        Timing.RunCoroutine(StartBGame());
    }

	public IEnumerator<float> StartBGame(){

        LOG.StartTimerDidatica(true);
        gdlLog.StartTimer();


        cardManager.spriteCards.Suffle();
		int deactiveThisPanelsLength = deactiveThisPanels.Length;
		for (int i = 0; i < deactiveThisPanelsLength; i++){
			deactiveThisPanels[i].SetActive(false);
		}
		
			contCart=contCart+1;
			StartCoroutine(Begin11B());
		
		panelDitatica.SetActive(true);
      
        yield return Timing.WaitForOneFrame;
	//	groupLayoutDrop.gameObject.GetComponent<Animator>().enabled=true;
		//cardMao.enabled=true;
		partCards.SetActive(true);
		cardMao.enabled=true;
		partCards2.SetActive(true);

        zecaCard.posCorpoZeca = 5;
        zecaCard.ControlAnimCorpo.SetInteger("posCorpoZeca", 5);
        //particulasCartsDita.SetActive(true);

    }

	public IEnumerator StartTotalPointsInGame(int increase){
		float times = 0.0f;
		int startPoints = cardManager.pointsAmount;
		int scoreT = cardManager.pointsAmount + increase;
		cardManager.pointsAmount += increase;	
		while (times < cardManager.increasePointsEffect)
		{
			times += Time.deltaTime;
			float s = times / cardManager.increasePointsEffect;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, cardManager.textPointsEffectCurve.Evaluate (s));
			cardManager.textPoints.text = scory.ToString ();
			yield return Yielders.EndOfFrame;
		}
	}


	public IEnumerator<float> makeDeck(){
parentDropAnimator.SetBool("nextCanbeStarted", false);
		groupLayout.enabled = false;

        managerMemory.checkdidatica = true;
        List<Vector3> cardsPos = new List<Vector3>();

		for (int i = 0; i < 5; i++){
			cardsPos.Add(dropCards[i].transform.position);
		}
		
		float times = 0.0f;
		while (times < makeDeckDuration)
		{
			times += Time.deltaTime;
			float s = times / makeDeckDuration;

			//int scory = (int)Mathf.Lerp (startPoints, scoreT, cardManager.textPointsEffectCurve.Evaluate (s));

			float xScale = Mathf.Lerp(1f,0f, cardManager.makeDeckCurve.Evaluate(s));
			float yScale = Mathf.Lerp(1f, 0f, cardManager.makeDeckCurve.Evaluate(s));

			for (int i = 0; i < 5; i++){
				dropCards[i].transform.localScale = new Vector3(xScale,yScale,1f);
				dropCards[i].transform.position = Vector3.Lerp(cardsPos[i],cardManager.characterDeckLocation.position,cardManager.makeDeckCurve.Evaluate(s));
              
			}

			yield return Timing.WaitForOneFrame;
				
		}
	
		//parentDropAnimator.enabled=true;
        partCards.SetActive(true);
        cardMao.enabled = true;
        partCards2.SetActive(true);

        for (int i = 0; i < dropCards.Length; i++) {
            dropCards[i].iconSoundCanvasGroup.DOFade(0f,0.3f);
        }

        for (int i = 0; i < 5; i++){
			if(dragCards[i] != null)
			dragCards[i].gameObject.SetActive(false);
		}

        //ResetCardScale();
        //cardMao.enabled = true;
        //  yield return Timing.WaitForSeconds(1f);


        //retirar e chamar pelo animator.
        //	cardManager.cardsAnimation.SetBool("nextCanbeStarted", true);
        yield return Timing.WaitForSeconds(0.1f);
        StartCoroutine(Begin11B());	

        

        //cardManager.cardsAnimation.SetBool("nextCanbeStarted", true);	

    }

    public void JogarCartas() {
		//parentDropAnimator.SetInteger("NumbCheck", 1);
       // parentDropAnimator.enabled = true;
     
        partCards.SetActive(false);
        cardMao.enabled = false;
        partCards2.SetActive(false);      

    }
}
