using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;

public class MemoryGameManager : OverridableMonoBehaviour {

	public Animator cardsAnimation;
 
	[SpaceAttribute(10)]
	[HeaderAttribute("Settings")]
	[RangeAttribute(0,100)]
	public int pointsPerRight = 10;
	[RangeAttribute(30,120)]
	public float timePerGame = 60f;
	public 	bool puxarCardsCheck;
	public SelPersons SelPersonsR;

	public GameObject SelPersonsG;

	public GameObject gTutorMemory;

	public GameObject TutorBTavancar;
	public Button TutorBTavancarBT;

	public GameObject TutorBTajuda;
	
	public ExpressaoCorporal[] personReations;
	public ExpressaoCorporal personReation;

	[HeaderAttribute("Prefabs")]
	public GameObject prefabCard;
	[SpaceAttribute(10)]
	[HeaderAttribute("References")]
	public Transform cardsParent;
	public Text animatedText;
	public Transform animatedTextTransform;
	public Transform starsParent;
	public Image[] starsImage = new Image[3];
	public Text textPoints;
	public Slider timeSlider;
	public GridLayoutGroup gridLayout;

	public SoundManager sound;
	public GameConfig config;
	public Transform characterDeckLocation;
	public Manager_1_1B managerNext;
	public Button pauseButton;

	[SpaceAttribute(10)]
	[HeaderAttribute("Variables")]
	public bool nextCanbeStarted = false;
	public bool canFlipped = true;
	public bool hasCardFlipped = false;
	public int levelDificult = 0;
	public int combo = 0;
	public int starsAmount;
	public int pointsAmount;
	public int correctsAmount = 0;
	public List<Sprite> cardList = new List<Sprite>();
	public List<GameObject> cardInstance = new List<GameObject>();
	public List<MemoryCard> cardInstanceMemoryComp = new List<MemoryCard>();
	private MemoryCard firstCard;
	private Image firstCardImage;
	private bool startTimerIsRunning = false;
	public changeLevel animationChangeLevel;
	public GameObject panelHighLightsR;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AudioClip[] audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public int characterSelected = 0;

	public float offTime = 1f;
	public bool isGameEnded = false;
	


	[SpaceAttribute(10)]
	[HeaderAttribute("Database")]
	public Sprite CardBackground;
	public List<Sprite> spriteCards = new List<Sprite>();
	public Sprite StarFullIcon;
	public Sprite StarEmptyIcon;
	public string levelTextChange;
	

	[SpaceAttribute(10)]
	[HeaderAttribute("Animation Settings")]
	public AnimationCurve flipCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float flipDuration = 1.0f;
	public AnimationCurve startFlip = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float startFlipDuration = 1.0f;
	public AnimationCurve endCardCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float endCardDuration = 1.0f;

	public AnimationCurve startCardCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float startCardDuration = 1.0f;
	public AnimationCurve flipYCardCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float yScaleDelta = 1.05f;
	public float waitTime = 2f;
	public float textEffectDuration = 1.0f;
	public AnimationCurve textEffectCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float increasePointsEffect = 1.0f;
	public AnimationCurve textPointsEffectCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Vector3 makeDeckScaleSize;
	public float makeDeckDuration = 1.0f;
	public AnimationCurve makeDeckCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

	[SpaceAttribute(10)]
	[HeaderAttribute("Shake Settings")]
	public bool debugMode = false;//Test-run/Call ShakeCamera() on start
 
	public float shakeAmount;//The amount to shake this frame.
	public float shakeDuration;//The duration this frame.
 
	//Readonly values...
	float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
	float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
	float startDuration;//The initial shake duration, set when ShakeCamera is called.
 
	bool isRunning = false;	//Is the coroutine running right now?
 
	public bool smooth;//Smooth rotation?
	public float smoothAmount = 5f;//Amount to smooth
	bool personReationB;
	int tutorMemor_0;
	public int tutorMemor_1;
	 public int checkAvancar1;

    public GameObject btTextProfPular;
    public string[] textProfS;
	 public Text textProf;
	 public Text textAvancar;

	 public GameObject btInicar;
	public GameObject _EventSystem;

 	float timeM;
    public bool checkdidatica;
	static readonly int posCorpoZeca = Animator.StringToHash("posCorpoZeca");
	static readonly int NumB = Animator.StringToHash("NumB");


	[SpaceAttribute(10)]
	[HeaderAttribute("Animators")]

	Animator gTutorMemoryAnimator;

	[SpaceAttribute(10)]
	[HeaderAttribute("Estatisticas")]
	public LogSystem log;

	public ParticleSystemRenderer[] part;


	public DragCard_1_1B[] DragCard_1_1B1;
	public bool checkTrue;
    private int fase;
    private StringFast stringFast = new StringFast();


    public AudioClip[] audiosTutorial;

    public Minigames minigame;
    public bool checkAudio;
    public int numTutor;

    [TextArea()]
    public string[] textos;

    void Start () {
        checkAudio = true;
        timeM = Time.realtimeSinceStartup;
		btInicar.SetActive(false);
		pauseButton.interactable = false;

        minigame = config.allMinigames[0];

        gTutorMemoryAnimator = gTutorMemory.GetComponent<Animator>();

		if(PlayerPrefs.HasKey("TutorMemory_0")==false){
			
				PlayerPrefs.SetInt("TutorMemory_0",0);
				gTutorMemoryAnimator.SetInteger(NumB,1);
          
            // Invoke("SomTutorMemoria", 2f);

            TutorBTavancar.SetActive(true);
			pauseButton.interactable = false;
		}
		else{

				PlayerPrefs.SetInt ("TutorMemory_0", 1);
				tutorMemor_0 = PlayerPrefs.GetInt ("TutorMemory_0", 1);
				gTutorMemory.SetActive (true);

		//	StartCoroutine(TimeStartlate());
			btInicar.SetActive(true);
			//managerNext.contCart=1;
			
		}
		if(PlayerPrefs.HasKey("TutorMemory_1")==false){
			//if (gTutorMemory.activeInHierarchy == true) 
			PlayerPrefs.SetInt("TutorMemory_1",0);
         
           // Invoke("SomTutorMemoria", 2f);
        }
		else{
		//	if (gTutorMemory.activeInHierarchy == true) 
			PlayerPrefs.SetInt("TutorMemory_1",1);
			tutorMemor_1 = PlayerPrefs.GetInt("TutorMemory_1",1);		
			
		}
			
		for (int i = 0; i < starsParent.childCount; i++){
			starsImage[i] = starsParent.GetChild(i).GetComponent<Image>();
		}
		
		
		}

    public void SomTutorMemoria() {
        sound.startVoiceFX(audiosTutorial[numTutor]);
    }

    private bool started = false;
	// Use this for initialization
		public void Startlate () {
        if (started == false) {
            started = true;
            Debug.Log("test2");
            if (checkAvancar1 != 3) {
                Timing.RunCoroutine(TimeStartlate());
            }

        }
		}
	public IEnumerator<float> TimeStartlate () {
       
	 SelPersonsR.PersonSel = PlayerPrefs.GetInt("characterSelected", 0);
	 TutorBTajuda.SetActive(false);

		if (log != null) {
			log.StartTimerLudica (true);
		}
		if(SelPersonsR.PersonSel==0){
			personReation = personReations[0];
			foreach (var item in part) {
			item.sortingLayerName = "zeca";
			} 
			managerNext.cardMao = managerNext.cardMaos[0];
			managerNext.partCards = managerNext.partsCards[0];
			managerNext.partCards2 = managerNext.partsCards2[0];
		}
		else if(SelPersonsR.PersonSel==1){
			personReation = personReations[1];
			foreach (var item in part) {
			item.sortingLayerName = "Tati";
			}
			managerNext.cardMao = managerNext.cardMaos[1];
			managerNext.partCards = managerNext.partsCards[1];
			managerNext.partCards2 = managerNext.partsCards2[1];
		}
		else if(SelPersonsR.PersonSel==2){
			personReation = personReations[2];
			foreach (var item in part) {
			item.sortingLayerName = "Paulo";
			}
			managerNext.cardMao = managerNext.cardMaos[2];
			managerNext.partCards = managerNext.partsCards[2];
			managerNext.partCards2 = managerNext.partsCards2[2];
		}
		else if(SelPersonsR.PersonSel==3){
			personReation = personReations[3];
			foreach (var item in part) {
			item.sortingLayerName = "manu";
			}
			managerNext.cardMao = managerNext.cardMaos[3];
			managerNext.partCards = managerNext.partsCards[3];
			managerNext.partCards2 = managerNext.partsCards2[3];
		}
		else if(SelPersonsR.PersonSel==4){
			personReation = personReations[4];
			foreach (var item in part) {
			item.sortingLayerName = "Joao";
			}
			managerNext.cardMao = managerNext.cardMaos[4];
			managerNext.partCards = managerNext.partsCards[4];
			managerNext.partCards2 = managerNext.partsCards2[4];
		}
		else if(SelPersonsR.PersonSel==5){
			personReation = personReations[5];
			foreach (var item in part) {
			item.sortingLayerName = "Bia";
			}
			managerNext.cardMao = managerNext.cardMaos[5];
			managerNext.partCards = managerNext.partsCards[5];
			managerNext.partCards2 = managerNext.partsCards2[5];
		}
        managerNext.zecaCard = personReation;
        if (tutorMemor_0==1){
		yield return Timing.WaitForSeconds(0.2f);
		SelPersonsG.SetActive(true);
		isGameEnded = false;
            Timing.RunCoroutine(randomCardsList());
           // Debug.Log("test");
		timeSlider.maxValue = timePerGame;
		ClearStars();
		nextCanbeStarted = false;	
		if(managerNext.contCart==0){
		//managerNext.StartCoroutine(managerNext.StartBGame());
		}	
		
		characterSelected = PlayerPrefs.GetInt("characterSelected", 0);
			//personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);

		}
		else{
//		yield return Yielders.Get(3f);
		
			yield return Timing.WaitForSeconds(0.2f);
		
		SelPersonsG.SetActive(true);
		isGameEnded = false;
		Timing.RunCoroutine(randomCardsList());
		timeSlider.maxValue = timePerGame;
		ClearStars();
		nextCanbeStarted = false;
	//	contCart=contCart +1;
		if(managerNext.contCart==0){
		//managerNext.StartCoroutine(managerNext.StartBGame());
		}
		characterSelected = PlayerPrefs.GetInt("characterSelected", 0);
		//personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);


		}

		TutorBTavancarBT = TutorBTavancar.GetComponent<Button>();
       
    }

    // Update is called once per frame

    public override void UpdateMe() {

		if(timeSlider.value <= timeSlider.minValue && isGameEnded == false){
			isGameEnded = true;
			canFlipped = false;
			StopAllCoroutines();
			endMemoryGame();
		}

		#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN 

			if(Input.GetKeyDown(KeyCode.J)){
				timeSlider.value = timeSlider.minValue;
			}

		#endif

		if (DragCard_1_1B1[0].hasDroped == true || DragCard_1_1B1[1].hasDroped == true || DragCard_1_1B1[2].hasDroped == true || DragCard_1_1B1[3].hasDroped == true || DragCard_1_1B1[4].hasDroped == true) {
			checkTrue = true;
			timeM = Time.realtimeSinceStartup;
		} else {
			checkTrue = false;
			//timeM = Time.realtimeSinceStartup;
		}
		if (Time.realtimeSinceStartup-timeM > 15f && managerNext.dragCards[0].gameObject.activeInHierarchy==true && gTutorMemory.activeInHierarchy == false && checkTrue==false){
		timeM = Time.realtimeSinceStartup;
		gTutorMemory.GetComponent<Image> ().color= gTutorMemory.GetComponent<ColImageFund> ().fundCor;
		gTutorMemory.SetActive(true);
		TutorBTajuda.SetActive(true);
		TutorBTavancar.SetActive(false);
		gTutorMemoryAnimator.SetInteger(NumB,2);
		//pauseButton.interactable = false;
		

	}
		if (  gTutorMemory.activeInHierarchy == true && (managerNext.dragCardsCanvasGroup[0].blocksRaycasts==false ||  managerNext.dragCardsCanvasGroup[1].blocksRaycasts==false || managerNext.dragCardsCanvasGroup[2].blocksRaycasts==false ||  managerNext.dragCardsCanvasGroup[3].blocksRaycasts==false || managerNext.dragCardsCanvasGroup[4].blocksRaycasts==false)){
		gTutorMemoryAnimator.SetInteger(NumB,0);
		timeM = Time.realtimeSinceStartup;
		gTutorMemory.SetActive(false);
		TutorBTajuda.SetActive(false);
		TutorBTavancar.SetActive(false);
		gTutorMemoryAnimator.SetInteger(NumB,0);
		//	pauseButton.interactable = true;
		

	}

	}

	void DecreaseTime(){
		float temp = (float)1f;
		timeSlider.value -= temp;
	}

	void ClearStars(){
	//	personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);
		for (int i = 0; i < starsParent.childCount; i++){
			starsImage[i].sprite = StarEmptyIcon;
		}
	}

	/// <summary>
    /// Increase the amount of player stars.
    /// </summary>
    /// <param name="amount">Max of 3</param>
	void IncreaseStars(int amount){
        starsAmount = amount;
        for (int i = 0; i < amount; i++){
			starsImage[i].sprite = StarFullIcon;            
		}
	}

    public void Test(Transform cardTransform) {

        cardTransform.DOScaleY(yScaleDelta, flipDuration).SetEase(Ease.InOutElastic);

    }

	public IEnumerator<float> FlipAThisCard(MemoryCard card, Image imageComponent){
		//Start Flipping;
		//imageComponent.sprite = card.cardSprite;
		canFlipped = false;
		sound.startSoundFX(audio[3]);
		Transform cardTransform = imageComponent.gameObject.transform;
        float times = 0.0f;
		while (times < flipDuration)
		{
			times += Time.deltaTime;
			float s = times / flipDuration;

			float xScale = Mathf.Lerp (1f, -1f, flipCurve.Evaluate (s));
			float yScale = Mathf.Lerp (1f, yScaleDelta, flipYCardCurve.Evaluate (s));

			if(xScale <= 0f){
				card.updateSprite(true);
			}

			cardTransform.localScale = new Vector3(xScale, yScale,1f);
			//yield return Yielders.EndOfFrame;
	        yield return Timing.WaitForOneFrame;
        }

        /*Sequence cardFlipping = DOTween.Sequence();
        Vector3 finalScale = new Vector3(-1f, yScaleDelta, 1f);
        cardFlipping.Append(cardTransform.DOScaleX(-1f, flipDuration)).InsertCallback(0.3f/2f, () => card.updateSprite(true)).OnStart(() => Test(cardTransform));
        cardFlipping.Play();*/

        //yield return Timing.WaitForSeconds(flipDuration + 0.2f);

		imageComponent.sprite = card.cardSprite;
		if(hasCardFlipped == true){
			_EventSystem.SetActive (false);
			canFlipped = false;
			if(card.cardSprite == firstCard.cardSprite){
				card.partic.SetActive(true);
				firstCard.partic.SetActive(true);

				//yield return Yielders.Get(.1f);
				yield return Timing.WaitForSeconds(0.1f);
				hasCardFlipped = false;
				if(combo < 4){
					combo++;
				} else {
					combo = 4;
				}
				int points = pointsPerRight * comboMultiplier();
				if (log != null) {
					log.AddPontosLudica(points);
					//log.SaveEstatistica(
				}
				Timing.KillCoroutines("PointsIncrease");
				Timing.RunCoroutine(StartTotalPointsInGame(points), "PointsIncrease");


				/*if (SystemInfo.systemMemorySize > 1100) {
					//StartCoroutine(StartTotalPointsInGame(points));
				} else {
					lowDeviceStartPoins(points);
				}*/
				correctsAmount++;

				sound.startSoundFX(audio[0]);
				//textPoints.text = pointsAmount.ToString();
				Timing.RunCoroutine(showTextCombo());			
				if(correctsAmount == dificultChecker()){
					levelDificult++;
                    dificult();
					if(levelDificult < 6){						
						correctsAmount = 0;
						Timing.RunCoroutine(randomCardsList());
						personReationB = true;
					} else {
                        //starsAmount = 3;
                        //IncreaseStars(starsAmount);
                        fase = 4;
						endMemoryGame();			
					}
				}
				canFlipped = true;
				_EventSystem.SetActive (true);
			} else {
				card.isFlipped = false;
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,2);
				firstCard.isFlipped = false;
				sound.startSoundFX(audio[2]);
				Timing.RunCoroutine(FlipThisCardBack(card, firstCard));
				if(combo >= 1){
					combo = 0;
				}
			}
		} else{
			hasCardFlipped = true;
			firstCard = card;
			firstCardImage = imageComponent;
			canFlipped = true;
		}
		Timing.KillCoroutines ("timeVolta");
		Timing.RunCoroutine (TimeVolta0 (), "timeVolta");

	}
	IEnumerator<float> TimeVolta0 (){
		//yield return Yielders.Get(2f);
		yield return Timing.WaitForSeconds(2f);
		personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);
		
	}
	 public void AnimVolta0 (){
	//	yield return Yielders.Get(2f);
		//personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);
		Timing.RunCoroutine(TimeVolta0());
		
	}

	public void lowDeviceStartPoins(int increase){
		//int startPoints = pointsAmount;
		int scoreT = pointsAmount + increase;
		pointsAmount += increase;
		textPoints.text = scoreT.ToString ();
	}

	public IEnumerator<float> StartTotalPointsInGame(int increase){
		float times = 0.0f;
		//int startPoints = pointsAmount;
		int scoreT = pointsAmount + increase;
		pointsAmount += increase;
		while (times < increasePointsEffect)
		{
			times += Time.deltaTime;
			float s = times / increasePointsEffect;

			int scory = (int)Mathf.Lerp (pointsAmount, scoreT, textPointsEffectCurve.Evaluate (s));
			textPoints.text = scory.ToString ();
			//yield return Yielders.EndOfFrame;
			yield return Timing.WaitForOneFrame;
        }

       // UpdateStars(scoreT);


    }

	IEnumerator<float> FlipThisCardBack (MemoryCard FirstCard, MemoryCard SecondCard){

		float elapsed = 0.0f;
        Quaternion originalRotation = firstCard.transform.rotation;
		Quaternion originalRotation2 = SecondCard.transform.rotation;

		//gridLayout.enabled = false;

		while (elapsed < shakeDuration){ 
             elapsed += Time.deltaTime;
             float zShake = Random.value * shakeAmount - (shakeAmount /2);
			 Vector3 euleranglesNew = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + zShake);
			 SecondCard.transform.eulerAngles = euleranglesNew;			 
             FirstCard.transform.eulerAngles =  euleranglesNew;			 
             yield return Timing.WaitForOneFrame;
        }

		FirstCard.transform.rotation = originalRotation;
		SecondCard.transform.rotation = originalRotation;

		//gridLayout.enabled = true;
		
		//yield return Yielders.Get(.2f);
		yield return Timing.WaitForSeconds(0.2f);
		//	personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);
		Transform cardTransform = FirstCard.gameObject.transform;
		Transform cardTransformFirst = firstCardImage.gameObject.transform;
		float times = 0.0f;
		while (times < flipDuration)
		{
			times += Time.deltaTime;
			float s = times / flipDuration;

			float xScale = Mathf.Lerp (-1f, 1f, flipCurve.Evaluate (s));
			float yScale = Mathf.Lerp (1f, yScaleDelta, flipYCardCurve.Evaluate (s));

			if(xScale >= 0f){
				FirstCard.updateSprite(false);
				SecondCard.updateSprite(false);
			}

			cardTransform.localScale = new Vector3(xScale, yScale,1f);
			cardTransformFirst.localScale = new Vector3(xScale, yScale,1f);

			yield return Timing.WaitForOneFrame;
		}	

		FirstCard.updateSprite(false);
		SecondCard.updateSprite(false);
		firstCard = null;
		firstCardImage = null;
		hasCardFlipped = false;
		canFlipped = true;
		_EventSystem.SetActive (true);
	}

	IEnumerator<float> randomCardsList(){
		CancelInvoke();
		canFlipped = false;

		if(cardInstance.Count >= 1){			

			float times = 0.0f;
			while (times < endCardDuration){
				times += Time.deltaTime;
				float s = times / endCardDuration;

				float scale = Mathf.Lerp (1f, 0f, endCardCurve.Evaluate (s));
				Color colorLerp = Color.Lerp(Color.white, new Color(1f,1f,1f,0f), endCardCurve.Evaluate (s));

				for (int i = 0; i < cardInstance.Count; i++){
					cardInstance[i].transform.localScale = new Vector3(-scale, scale,scale);
					cardInstanceMemoryComp[i].imageComponent.color = colorLerp;
				}

				yield return Timing.WaitForOneFrame;
			}

			for (int i = 0; i < cardInstance.Count; i++){
				Destroy(cardInstance[i]);
			}
			cardInstance.Clear();
			cardInstanceMemoryComp.Clear();
		}

		//Limpa lista.
		cardList.Clear();
		int cardsAmount = 0;
        dificult();
        IncreaseStars(starsAmount);
        if (levelDificult < 6){
			cardsAmount = dificult();
		} else {
            waitTime = 6f;
            starsAmount = 3;
            offTime = 2f;
            IncreaseStars(starsAmount);
            gridLayout.constraintCount = 6;
            IncreaseStars(starsAmount);
			//Debug.Log("Game Ended 2");
            canFlipped = false;
            endMemoryGame();
            StopAllCoroutines();
        }
       // Debug.Log("cardsAmount: " + cardsAmount.ToString());
		spriteCards.Suffle();
		for (int i = 0; i < cardsAmount; i++){
			//MemoryCard card = new MemoryCard(spriteCards[i], this);
			cardList.Add(spriteCards[i]);
			cardList.Add(spriteCards[i]);
		}


		//yield return Yielders.Get(offTime);
		yield return Timing.WaitForSeconds(offTime);
		if (personReationB == true && checkdidatica==false) {
			//personReation.posCorpoZeca = 4;
			personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,4);
		}
	
		cardList.Suffle();

		int countCard = cardList.Count;
        Debug.Log("cardList: " + cardList.Count);


        for (int i = 0; i < countCard; i++){
			GameObject cardGO = Instantiate(prefabCard, new Vector3(0f, 0f, 0f),Quaternion.identity, cardsParent) as GameObject;
			cardInstance.Add(cardGO);
			cardGO.transform.localScale = new Vector3(0f,0f,0f);
			//cardGO.transform.localPosition = new Vector3(0,0,0);

			MemoryCard cardComponent = cardGO.GetComponent<MemoryCard>();

			cardInstanceMemoryComp.Add(cardComponent);

			cardComponent.cardSprite = cardList[i];
			cardComponent.manager = this;
			cardComponent.cardName = cardName(cardList[i]);
			cardComponent.updateSprite(false);
		}

        Debug.Log("countCard: " + countCard.ToString());
        float timer = 0.0f;
		while (timer < startCardDuration)
		{
			timer += Time.deltaTime;
			float s = timer / startCardDuration;

			float scale = Mathf.Lerp (0f, 1f, endCardCurve.Evaluate (s));
			Color colorLerp = Color.Lerp(new Color(1f,1f,1f,0f), Color.white, endCardCurve.Evaluate (s));

			for (int i = 0; i < cardInstance.Count; i++){
				if(cardInstance[i] != null)
				cardInstance[i].transform.localScale = new Vector3(scale, scale,scale);
				if(cardInstance[i] != null)
				cardInstanceMemoryComp[i].imageComponent.color = colorLerp;
			}

			yield return Timing.WaitForOneFrame;
		}

		//yield return Yielders.Get(.3f);
		Timing.WaitForSeconds(0.3f);
		
		if(startTimerIsRunning == false)
		Timing.RunCoroutine(timeToStart());
	}

	int comboMultiplier(){
        stringFast.Clear();
        int tempResult = 0;
        switch (combo)
		{
			case 0:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
				animatedText.text = "+10";
				return 1;

			case 1:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,1);
				animatedText.text = "+10";
				return 1;
			case 2:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 2;
                stringFast.Append("Combo x2 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
				return 2;
			case 3:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 3;
                stringFast.Append("Combo x3 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
				return 3;
			case 4:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 4;
                stringFast.Append("Combo x4 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 4;
			case 5:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 5;
                stringFast.Append("Combo x5 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 5;
			case 6:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 6;
                stringFast.Append("Combo x6 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 6;
			case 7:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 7;
                stringFast.Append("Combo x7 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 7;
			case 8:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 8;
                stringFast.Append("Combo x8 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 8;
			case 9:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 9;
                stringFast.Append("Combo x9 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 9;
			case 10:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 10;
                stringFast.Append("Combo x10 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 10;
			default:
				personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,3);
                tempResult = pointsPerRight * 1;
                stringFast.Append("Combo x1 \n +").Append(tempResult);
                animatedText.text = stringFast.ToString();
                return 1;
		}
	}

	IEnumerator<float> timeToStart(){
		canFlipped = false;
		startTimerIsRunning = true;

		pauseButton.interactable = false;
		int cardInstanceCount = cardInstanceMemoryComp.Count;
		int cardInstanceCount2 = cardInstance.Count;
		float times = 0.0f;
		while (times < startFlipDuration)
		{
			times += Time.deltaTime;
			float s = times / startFlipDuration;

			float xScale = Mathf.Lerp (1f, -1f, startFlip.Evaluate (s));
			float yScale = Mathf.Lerp (1f, yScaleDelta, flipYCardCurve.Evaluate (s));

			if(xScale <= 0f){
				
				for (int i = 0; i < cardInstanceCount; i++){
					cardInstanceMemoryComp[i].updateSprite(true);
				}
			}


			for (int i = 0; i < cardInstanceCount2; i++)
			{
				if(cardInstance[i] != null)
				cardInstance[i].transform.localScale = new Vector3(xScale, yScale,1f);
			}

            yield return Timing.WaitForOneFrame;
		}

		yield return Timing.WaitForSeconds(waitTime);

		times = 0.0f;
		while (times < startFlipDuration)
		{
			times += Time.deltaTime;
			float s = times / startFlipDuration;

			float xScale = Mathf.Lerp (-1f, 1f, startFlip.Evaluate (s));
			float yScale = Mathf.Lerp (1f, yScaleDelta, flipYCardCurve.Evaluate (s));

			if(xScale >= 0f){
				for (int i = 0; i < cardInstanceCount; i++){
					cardInstanceMemoryComp[i].updateSprite(false);
				}
			}

			for (int i = 0; i < cardInstanceCount2; i++)
			{
				cardInstance[i].transform.localScale = new Vector3(xScale, yScale,1f);
			}

			yield return Timing.WaitForOneFrame;
        }

		combo = 0;
		canFlipped = true;
		InvokeRepeating("DecreaseTime", 1f, 1f);
		startTimerIsRunning = false;
		pauseButton.interactable = true;
	}

	public int dificult(){
		switch (levelDificult){
			case 0:
                fase = 1;
				offTime = 0f;
				starsAmount = 0;
				IncreaseStars(starsAmount);	
				waitTime = 2f;
				gridLayout.constraintCount = 3;
				return 3;
			case 1:
				offTime = 0f;
				starsAmount = 0;
				IncreaseStars(starsAmount);	
				waitTime = 2f;
				gridLayout.constraintCount = 3;
				return 3;
			case 2:
                fase = 2;
                offTime = 2f;
                timeSlider.maxValue = timePerGame + 10;
                if (timeSlider.value != timeSlider.minValue) {
					animationChangeLevel.startChangeLevelAnimation(2);
				}
				timeSlider.value = timeSlider.maxValue;
				starsAmount = 1;
				IncreaseStars(starsAmount);	
				waitTime = 6f;
				gridLayout.constraintCount = 4;
				return 6;
			case 3:	
				offTime = 0f;
				starsAmount = 1;
				IncreaseStars(starsAmount);
				waitTime = 6f;
				gridLayout.constraintCount = 4;
				return 6;
			case 4:
                fase = 3;
                offTime = 2f;
                timeSlider.maxValue = timePerGame + 20;
                if (timeSlider.value != timeSlider.minValue) {
					animationChangeLevel.startChangeLevelAnimation(3);
				}
				timeSlider.value = timeSlider.maxValue;
				starsAmount = 2;
				waitTime = 9f;
				IncreaseStars(starsAmount);
				gridLayout.constraintCount = 6;
				return 9;
			case 5:
				offTime = 2f;
				starsAmount = 2;
				waitTime = 9f;
				IncreaseStars(starsAmount);	
				gridLayout.constraintCount = 6;
				return 9;
			case 6:				
				waitTime = 6f;
				starsAmount = 3;
				offTime = 2f;	
				IncreaseStars(starsAmount);	
				gridLayout.constraintCount = 6;
               // Debug.Log("Lastest Level.");
				//endMemoryGame();
				return 0;
			case 7:				
				offTime = 3f;				
				starsAmount = 3;
				waitTime = 6f;
				IncreaseStars(starsAmount);	
				gridLayout.constraintCount = 6;
				//endMemoryGame();
				return 0;
				
			default:
				return 6;
		}
		//	personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,0);
	}

	public int dificultChecker(){
		switch (levelDificult){
			case 0:
				return 3;
			case 1:
				return 3;
			case 2:
				return 6;
			case 3:
				return 6;
			case 4:
				return 9;
			case 5:
				return 9;
			case 6:
				return 0;
			default:
				return 6;
		}
	}


	public string cardName(Sprite image){
		return image.name;
	}

	IEnumerator<float> showTextCombo(){
		float times = 0.0f;
		while (times < textEffectDuration)
		{
			times += Time.deltaTime;
			float s = times / textEffectDuration;
			float size = Mathf.Lerp(0.5f,1f,textEffectCurve.Evaluate (s));
			animatedTextTransform.localScale = new Vector3 (size, size, size);
			animatedText.color = Color.Lerp (new Color(1f,1f,1f,0f), Color.yellow, textEffectCurve.Evaluate (s));
			yield return Timing.WaitForOneFrame; ;
		}
	}


	public void endMemoryGame(){
		//endMemory code here.. pass to educational minigame.
		//Debug.Log("Game Ended");

		if (log != null) {
            //log.faseLudica = levelDificult;
            log.faseLudica = fase;
			log.StartTimerLudica (false);
		}

		canFlipped = false;
		StopAllCoroutines();
		timeSlider.value = timeSlider.minValue;
		CancelInvoke();		
		isGameEnded = true;
		Timing.RunCoroutine(makeADeck());	
		//personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,5);	
		//nextCanbeStarted = true;
	}
		public void BtAvancar () {
		if (checkAvancar1 == 0) {
			checkAvancar1 = 1;
			if (gTutorMemory.activeInHierarchy == true) {
				gTutorMemoryAnimator.SetInteger (NumB, 0);	
			}
			pauseButton.interactable = true;
			timeM = Time.realtimeSinceStartup;
		} else if (checkAvancar1 == 1) {			
			tutorMemor_1 = 1;
			checkAvancar1 = 2;
			if (gTutorMemory.activeInHierarchy == true) {
				gTutorMemoryAnimator.SetInteger (NumB, 0);	
			}	
			pauseButton.interactable = true;
			TutorBTavancar.SetActive (false);
			textAvancar.text = "fechar";
			timeM = Time.realtimeSinceStartup;
            managerNext.RunStartB();
           // managerNext.JogarCartas();
            //Timing.RunCoroutine (makeADeck ());	
            //error aqui erro

        } else {
			if (gTutorMemory.activeInHierarchy == true) {
				gTutorMemoryAnimator.SetInteger (NumB, 0);	
			}
			pauseButton.interactable = true;
			TutorBTavancar.SetActive (false);
			textAvancar.text = "fechar";
			timeM = Time.realtimeSinceStartup;
		}


		}

		public void BtAjuda () {
			checkAvancar1=3;
			TutorBTavancar.SetActive(true);
			TutorBTavancarBT.interactable=true;
		if (gTutorMemory.activeInHierarchy == true) {
			gTutorMemoryAnimator.SetInteger(NumB,1);	
		}
			TutorBTavancar.SetActive(true);
			pauseButton.interactable = false;
			timeM = Time.realtimeSinceStartup;



		}
	public void BtAjuda2 () {
		gTutorMemoryAnimator.SetInteger(NumB,1);



	}
	IEnumerator<float> makeADeck(){

        checkAudio = false;
        int cardInstanceCount = cardInstance.Count;
		int cardInstanceMemoryCompCount = cardInstanceMemoryComp.Count;
			if(tutorMemor_1==0 || tutorMemor_1==1){
         
            numTutor = 1;
            canFlipped = false;
				puxarCardsCheck=true;
			
				List<Vector3> cardsPos = new List<Vector3>();

				
				for (int i = 0; i < cardInstanceCount; i++)
				{
					cardsPos.Add(cardInstance[i].transform.position);
				}

				float times = 0.0f;
				while (times < makeDeckDuration)
				{
				times += Time.deltaTime;
				float s = times / makeDeckDuration;

				float xScale = Mathf.Lerp (1f, makeDeckScaleSize.x, makeDeckCurve.Evaluate (s));
				float yScale = Mathf.Lerp (1f, makeDeckScaleSize.y, makeDeckCurve.Evaluate (s));
			

				if(xScale >= 0f){
					for (int i = 0; i < cardInstanceMemoryCompCount; i++){
						if(i >= cardInstanceMemoryComp.Count && cardInstanceMemoryComp[i] != null)
						cardInstanceMemoryComp[i].updateSprite(false);
					}
				}

				/*for (int i = 0; i < cardInstanceCount; i++)
				{
				cardInstance[i].transform.localScale = new Vector3(xScale, yScale,1f);
				}*/

				for (int i = 0; i < cardInstance.Count; i++)
				{
				cardInstance[i].transform.localScale = new Vector3(xScale, yScale,1f);
				cardInstance[i].transform.position = Vector3.Lerp(cardsPos[i], characterDeckLocation.position, makeDeckCurve.Evaluate (s));

				}	
				
				

				yield return Timing.WaitForOneFrame;
			//	managerNext.cardMao.enabled=true;	
			
				}
				managerNext.cardMao.enabled=true;	
				managerNext.partCards2.SetActive(true);
            btTextProfPular.SetActive(true);

            if (levelDificult == 0 && correctsAmount == 0) {
                textProf.text = textos[0];
            } else if (levelDificult >= 0 && correctsAmount >= 0 && correctsAmount <= 8 && levelDificult <= 6) {
                textProf.text = textos[1];
            } else if (levelDificult == 6 && correctsAmount == 9) {
                textProf.text = textos[2];
            }

            gTutorMemory.SetActive(true);
				TutorBTavancarBT.interactable=true;

			if (gTutorMemory.activeInHierarchy == true) {
				gTutorMemoryAnimator.SetInteger(NumB,1);
			}
				
				TutorBTavancar.SetActive(true);
					pauseButton.interactable = false;
				}

			else if(tutorMemor_1==1){
         
				if(puxarCardsCheck==false){
				canFlipped = false;
				//puxarCardsCheck=true;
			
				List<Vector3> cardsPos = new List<Vector3>();

				for (int i = 0; i < cardInstanceCount; i++)
				{
					cardsPos.Add(cardInstance[i].transform.position);
				}

				float times = 0.0f;
				while (times < makeDeckDuration)
				{
				times += Time.deltaTime;
				float s = times / makeDeckDuration;

				float xScale = Mathf.Lerp (1f, makeDeckScaleSize.x, makeDeckCurve.Evaluate (s));
				float yScale = Mathf.Lerp (1f, makeDeckScaleSize.y, makeDeckCurve.Evaluate (s));
			

				if(xScale >= 0f){
					for (int i = 0; i < cardInstanceMemoryCompCount; i++){
							if (cardInstanceMemoryComp [i] != null) {
								cardInstanceMemoryComp [i].updateSprite (false);
							}
					}
				}

				for (int i = 0; i < cardInstanceCount; i++)
					{
						if (cardInstance [i] != null) {
							cardInstance [i].transform.localScale = new Vector3 (xScale, yScale, 1f);
							cardInstance [i].transform.position = Vector3.Lerp (cardsPos [i], characterDeckLocation.position, makeDeckCurve.Evaluate (s));
						}
				}
				

				yield return Timing.WaitForOneFrame;

                }
                //	managerNext.cardMao.enabled=true;
                btTextProfPular.SetActive(true);
                if (levelDificult == 0 && correctsAmount == 0) {
                    textProf.text = "Que pena! Vamos tente melhor na proxima vez.";
                } else if (levelDificult >= 0 && correctsAmount >= 0 && correctsAmount <= 8 && levelDificult <= 6) {
                    textProf.text = "Muito bem! Você tem uma boa memória.";
                } else if (levelDificult == 6 && correctsAmount == 9) {
                    textProf.text = "Excelente! Você acertou todas as cartas.";
                }
            }
            btTextProfPular.SetActive(true);

            if(levelDificult == 0 && correctsAmount == 0) {
                textProf.text = "Que pena! Vamos tente melhor na proxima vez.";
            } else if (levelDificult >= 0 && correctsAmount >= 0 && correctsAmount <= 8 && levelDificult <= 6) {
                textProf.text = "Muito bem! Você tem uma boa memória.";
            } else if (levelDificult == 6 && correctsAmount == 9) {
                textProf.text = "Excelente! Você acertou todas as cartas.";
            }

            // = textProfS[0];
            config.Rank(log.idMinigame, pointsAmount, starsAmount);
            //nextCanbeStarted = true;
                //  numTutor = 1;
                //managerNext.cardMao.enabled=true;	
                canFlipped = true;
				//personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,5);
				//yield return Yielders.Get(1f);
				///personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,5);
				//managerNext.cardMao.enabled=false;
				//yield return Yielders.Get(0.1f);
				yield return Timing.WaitForSeconds(0.1f);
            //	managerNext.cardMao.enabled=false;

            managerNext.RunStartB();
        }


       // checkAudio = true;

        //personReation.ControlAnimCorpo.SetInteger (posCorpoZeca,5);
        //cardsAnimation.SetBool("nextCanbeStarted", true);
        Invoke("TutorTex2", 6f);
	}
    public void TutorTex2() {
        btTextProfPular.SetActive(false);
        textProf.text = textProfS[1];
    }

    public void timeCheat(){
		timeSlider.value = timeSlider.maxValue;
	}

	public string GetCharName(){
		switch (characterSelected) {
			case 0:
				return "Zeca";
			case 1:
				return "Tati";
			case 2:
				return "Paulo";
			case 3:
				return "Manu";
			case 4:
				return "Joao";
			case 5:
				return "Bia";
			default:
				return "Zeca";
		}
	}

    

}
