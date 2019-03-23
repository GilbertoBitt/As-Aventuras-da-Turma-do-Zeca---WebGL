using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.UI;

public class Manager_1_2A : OverridableMonoBehaviour {

	public Image parentImage;
	public RectTransform rectImage;
	public int dificult = 0;
	public float speedMovingImage;
	public bool isRight;
	public bool isLeft;
	public List<Level1_2A> Levels = new List<Level1_2A> (); 	
	public List<PlacesToHide_1_2A> placesToHide = new List<PlacesToHide_1_2A>();
	public List<PlacesToHide_1_2A> placesChoosen = new List<PlacesToHide_1_2A>();
	public List<Sprite> itemsToHide = new List<Sprite> ();
	public List<Sprite> itemsToChoosen = new List<Sprite> ();
	public List<Image> ImagensPanel = new List<Image> ();
	public GameObject[] ButtonsPanelG;
	public GameObject pauloLock;
	public ButtonExtension1_2A pauloLockButton;
	public Image pauloLockFade;
	public Material Grayscale;
	public Sprite pauloSprite;
	public int amountsFinded = 0;
	public int amountsToFind = 0;
	public int scoreAmount;
	public int starsAmount;
	public Slider timerSlider;
	public int decreasePerWrongs;
	public int pointsRight;
	public int actualCombo = 1;
	public Sprite fullStarSprite;
	public Sprite emptyStarSprite;
	public Transform starParent;
	public float fadeInDuration = 1.0f;
	public AnimationCurve fadeInCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float fadeOutDuration = 1.0f;
	public AnimationCurve fadeOutCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float increaseScoreDuration = 1.0f;
	public AnimationCurve increaseScoreCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public AnimationCurve timeToPointsCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float timeToPointsDuration = 1.0f;
	public Image fadeImage;
	public bool foundPaulo = false;
	public Text scoreText;
	public bool canBeStarted = false;
	public Manager_1_2B nextManager;
	public Button rightBT;
	public Button leftBT;
	public SoundManager soundManager;
	public List<AudioClip> audios = new List<AudioClip>();
	private List<Quaternion> originRotation = new List<Quaternion>();
	// Use this for initialization
	public bool isGameRunning;
	public SelPersons personagems;
	public Vector3 startpos;
	public Text foundPauloText;
	public changeLevel nextLevel;
	public GameObject particlePrefab;
	public Transform particlesParent;
	public Transform particleTransform;
	public float particleMovementDuration;
	public float[] parallaxLayersSpeed;
	public Vector3[] parallaxMovementOffset;
	public Transform[] parallaxLayers;
	public GameObject selPersonPrefab;
	public float findParticleDuration = 1.0f;
	public AnimationCurve findParticleCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public GameObject findParticlePrefab;
	public Sprite defoqSprite;
	public AudioClip[] sfx;
	public Image fillImageSlider;
	public Color sliderColorError;
	public float sliderErrorDuration = 1.0f;
	public AnimationCurve sliderErrorCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);
	private bool isSliderWrong = false;
	public Color slideColorStart;
	public Text textError;
	private IEnumerator textErrorRoutine;
	public Color startTextErrorColor;
	public float errorTextEffectDuration = 1.0f;
	public AnimationCurve errorTextEffectCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);
	public GameObject touchPrefabIcon;
	public Transform touchParent;
	public Vector3 offsetTextErrorPos;
	public Transform touchParents;
	private bool hasEndByTime = false;
	public GameObject exclamation;
	public RectTransform exclamationRect;
    public Text exclamationText;
    public IEnumerator routineToEnd;
	public bool isChoosingFound = false;
	public List<ButtonExtension1_2A> buttonsOnPanel = new List<ButtonExtension1_2A>();
	public List<ButtonExtension1_2A> buttonsOnPanelFinded = new List<ButtonExtension1_2A>();
	public ButtonExtension1_2A buttonsPedro;
	public float blinkExclamationDuration = 1.0f;
	public AnimationCurve blinkExclamationCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);
	private Sprite foundedSprite;
	public IEnumerator releasePauloRoutine;
	public float scoreDecreaseDuration = 1.0f;
	public AnimationCurve ScoreDecreaseCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Text textErrou;
	public GameObject tutorPanel; 
	public Animator tutorPanelAnimator;
	public GameObject btAvancar;
	public GameObject btIniciar;
	public Text tutorTex;
	public string tutorTexC;
	public int tutorIni_0;
	public int tutorIni_1;
	Color _color;
	int startAmountPoins = 0;
	public PauseManager pauseManager;
	public GameObject panelItems;
	public GameObject HighLightsComp;
	public bool destroyPref;
	public LogSystem log;
	public bool emCenaCheck;
    private Transform parentImageTransform;
    static readonly int emCenaHash = Animator.StringToHash("emCena");
    private StringFast stringFast = new StringFast();

    public ControlSomTutor ControlSomTutor2;

    public Minigames minigame;
    public GameConfig config;

    public ScrollRect scrollRectMain;

    public float intervalTime;
    public float speedToShowMessage;
    public float intervalBackgroundMessage;
    public float speedToShowBackground;
    public Image FadeOutBlurImage;
    public Text textFinalMessage;
    private IEnumerator timeToPointsRoutine;
    public CanvasGroup canvasGroup;
    public ShakeCamera shakeCamera;
    public KeyCode buttonRightKey;
    public KeyCode buttonLeftKey;
    public Image RedImageDamage;
    public AnimationCurve curveRedAnimation;
    public float RedAlpha;
    public float RedTimerForFade;
    public float offsetScreenEdge = 30f;


    [TextArea()]
    public string[] textos;
    void Start () {
		isGameRunning = false;
        Input.multiTouchEnabled = false;

        Resources.UnloadUnusedAssets();

        minigame = config.allMinigames[1];

        tutorPanelAnimator = tutorPanel.GetComponent<Animator>();
		_color = buttonsOnPanel[0].exclamationPoint.color;
		if(PlayerPrefs.HasKey("TutorSP_0")==false){
			PlayerPrefs.SetInt("TutorSP_0",0);
			tutorPanelAnimator.SetInteger(emCenaHash, 1);
			btIniciar.SetActive(false);
		

		
		}
		else{
			PlayerPrefs.SetInt("TutorSP_0",1);
			tutorIni_0 = PlayerPrefs.GetInt("TutorSP_0",1);	
			tutorPanelAnimator.SetInteger(emCenaHash, 0);
			btAvancar.SetActive(false);
			btIniciar.SetActive(true);
		
			
		}
		if(PlayerPrefs.HasKey("TutorSP_1")==false){
			PlayerPrefs.SetInt("TutorSP_1",0);
			tutorPanelAnimator.SetInteger(emCenaHash, 1);
			btAvancar.SetActive(true);
		}
		else{
			PlayerPrefs.SetInt("TutorSP_1",1);	
		//	tutorIni_1 = PlayerPrefs.GetInt("TutorSP_1",1);		
			tutorPanelAnimator.SetInteger(emCenaHash, 0);	
			btAvancar.SetActive(false);
			btIniciar.SetActive(true);
		
		}

			for (int i = 0; i < Levels.Count; i++){
				placesToHide = Levels [i].hidenPlaces;

				for (int j = 0; j <  placesToHide.Count; j++){
						placesToHide[j].imageComponent.color = Color.clear;
				}
			}

		}

    

    public void BT_Inicar  () {
		Timing.RunCoroutine (startGame ());
		//nextManager.StartCoroutine(nextManager.lateStart());
		slideColorStart = fillImageSlider.color;			
		log.ClearAll ();
		log.StartTimerLudica (true);
		}
	
	
		public void StartGameOn  () {

		if (tutorIni_0 == 1 && tutorIni_0 == 0) {
		Timing.RunCoroutine (startGame ());
		slideColorStart = fillImageSlider.color;			
		//Debug.Log("1");
		}
		else if(tutorIni_0 == 1 && tutorIni_1 ==0){
          //  tutorIni_1 = PlayerPrefs.GetInt("TutorSP_1", 1);
            Timing.RunCoroutine(startGame());
            //slideColorStart = fillImageSlider.color;
            tutorPanelAnimator.SetInteger(emCenaHash, 0);
		//	nextManager.StartCoroutine(nextManager.lateStart());
	   // Debug.Log("2");
		}
		else{
		//Debug.Log("3");
			//nextManager.StartCoroutine(nextManager.lateStart());
			Timing.RunCoroutine (startGame ());
		}
		log.StartTimerLudica (true);
		exclamationRect = exclamation.GetComponent<RectTransform>();
       
//		startpos = personagems.transform.position;


		/*for (int i = 0; i < buttonsOnPanel.Count; i++){
			buttonsOnPanel[i].buttonText = buttonsOnPanel[i].transform.GetChild(0).GetComponent<Text>();
			buttonsOnPanel[i].exclamationPoint = buttonsOnPanel[i].transform.GetChild(1).GetComponent<Text>();
		}*/
	}

	private bool gettingPointsRunning = false;
	private bool timeEnded = false;

    public void ChamarAjuda () {	
	//	canBeStarted = false;
			tutorPanelAnimator.SetInteger(emCenaHash, 1);
		}

	public void OnSliderChange(){
		if (isGameRunning) {
			if (timerSlider.value <= 0.1f) {
				hasEndByTime = true;
				isChoosingFound = false;
				stopBlink();
				CancelInvoke ();
				timeToPointsRoutine = TimeToPoints ();
				StartCoroutine(timeToPointsRoutine);
			}
		}
	}

    public override void UpdateMe() {

		#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN

		if(Input.GetKeyDown(KeyCode.J) && canBeStarted == false ){
            log.faseLudica = dificult + 1;
			timerSlider.value = 0;
			//GoForEducation();
            //Debug.Log("UpdateMe", this);

        }

       
			
		#endif

		if (isGameRunning == true) {
			if (timerSlider.value <= 0.1f) {
				hasEndByTime = true;
				isChoosingFound = false;
				stopBlink();
				CancelInvoke ();
				timeToPointsRoutine = TimeToPoints ();
				//Debug.Log ("Update is still Running");
				StartCoroutine(timeToPointsRoutine);
			}
			if (amountsFinded >= amountsToFind && gettingPointsRunning == false) {
				gettingPointsRunning = true;
				isGameRunning = false;
				isChoosingFound = false;
				stopBlink();

				Timing.RunCoroutine(GettingPoints());
			}
            //Debug.Log("need2");

            if (Input.GetKeyDown(buttonRightKey)) {
                bt_rightDown();
            } else if (Input.GetKeyUp(buttonRightKey)) {
                bt_rightUp();
            } else if (Input.GetKeyDown(buttonLeftKey)) {
                bt_leftDown();
            } else if (Input.GetKeyUp(buttonLeftKey)) {
                bt_leftUp();
            }
		}

		if(isGameRunning){
            Vector3 newPos;
            if (rectImage !=null && isChoosingFound == false){

                Vector2 limit = new Vector2(0f, 2100f);

                int tempCount = parallaxLayers.Length;
                float speedMoving = (speedMovingImage * Time.deltaTime);
                if ((isRight && (rectImage.offsetMin.x > -limit.y)) || (IsOnRightScreenEdge() && rightBT.interactable)){
					isLeft = false;
                    
                    newPos.x = Vector3.right.x * speedMoving;
                    newPos.y = Vector3.right.y * speedMoving;
                    newPos.z = Vector3.right.z * speedMoving;
                    parentImage.transform.position -= newPos;

                    for (int i = 0; i < tempCount; i++){
						Vector3 temp = Vector3.right + parallaxMovementOffset[i];
                        float paralaxSpeed = (parallaxLayersSpeed[i] * Time.deltaTime);
                        newPos.x = temp.x * paralaxSpeed;
                        newPos.y = temp.y * paralaxSpeed;
                        newPos.z = temp.z * paralaxSpeed;
                        parallaxLayers[i].position -= newPos;
					}
				}

                if ((rectImage.offsetMin.x <= -limit.y) && rightBT.interactable == true){
					rightBT.interactable = false;
				} else if (rightBT.interactable == false && (rectImage.offsetMin.x > -limit.y)){
					rightBT.interactable = true;
				}

                if ((isLeft && (rectImage.offsetMin.x < -limit.x)) || (IsOnLeftScreenEdge() && leftBT.interactable)) {
					isRight = false;
                    //float speedMoving = (speedMovingImage * Time.deltaTime);
                    newPos.x = Vector3.right.x * speedMoving;
                    newPos.y = Vector3.right.y * speedMoving;
                    newPos.z = Vector3.right.z * speedMoving;
                    parentImage.transform.position += newPos;                    
                    for (int i = 0; i < tempCount; i++){
						Vector3 temp = Vector3.right + parallaxMovementOffset[i];
                        if (parallaxLayers[i] != null) {
                            float paralaxSpeed = (parallaxLayersSpeed[i] * Time.deltaTime);
                            newPos.x = temp.x * paralaxSpeed;
                            newPos.y = temp.y * paralaxSpeed;
                            newPos.z = temp.z * paralaxSpeed;
                            parallaxLayers[i].position += newPos;
                        }
					}
				}

				if(rectImage.offsetMin.x >= limit.x && leftBT.interactable == true){
					leftBT.interactable = false;
				} else if (leftBT.interactable == false && rectImage.offsetMin.x < limit.x) {
					leftBT.interactable = true;
				}



			}
		}
	}

    public bool IsOnRightScreenEdge() {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
            if (Input.mousePosition.x >= (Screen.width - offsetScreenEdge)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    public bool IsOnLeftScreenEdge() {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
            if (Input.mousePosition.x <= (0 + offsetScreenEdge)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

	public IEnumerator<float> startGame(){


      //  tutorPanelAnimator.SetInteger(emCenaHash, 1);
        int countTemp;

        isGameRunning = false;
		
		textErrou.gameObject.SetActive(false);
		
		tutorPanelAnimator.SetInteger (emCenaHash, 0);

		//tutorIni_0 = 1;
		buttonsOnPanelFinded.Clear();
		yield return Timing.WaitForSeconds(.5f);
		startAmountPoins = scoreAmount;
        Level1_2A level = null;
        if (Levels[dificult] != null) {
            level = Levels[dificult];
        }
		particlesParent = level.particlesParent;
        scrollRectMain.content = level.ImageBackground.rectTransform;
        countTemp = level.parallaxLayers.Length;

        for (int i = 0; i < countTemp; i++){
			parallaxLayers[i] = level.parallaxLayers[i];
		}

		timerSlider.maxValue = level.timer;
        timerSlider.value = level.timer;

		if(parentImage !=null){
			parentImage.gameObject.SetActive(false);
		}

		parentImage = level.ImageBackground;
		rectImage = Levels [dificult].ImageBackground.GetComponent<RectTransform>();
		placesToHide = level.hidenPlaces;

		parentImage.gameObject.SetActive(true);

		if(dificult >= 1){
            countTemp = placesToHide.Count;
            for (int i = 0; i < countTemp; i++){
				placesToHide[i].transform.rotation = originRotation[i];	
		}
		}

		itemsToHide.Suffle ();
		placesToHide.Suffle ();		


		for (int i = 0; i < 5; i++) {
			itemsToChoosen.Add (itemsToHide [i]);
		}
			
		itemsToChoosen.Add (pauloSprite);
		originRotation.Clear();
        countTemp = placesToHide.Count;
        for (int i = 0; i < countTemp; i++) {			
			placesToHide [i].isDone = false;
			placesToHide [i].Image = null;
			placesToHide [i].imageComponent.sprite = null;
			placesToHide [i].imageComponent.enabled = false;
			placesToHide [i].imagePanel = null;
			placesToHide [i].imageComponent.color = Color.white;
			placesToHide [i].touchParent = level.touchParent;
			originRotation.Add(placesToHide [i].transform.localRotation);
			placesToHide[i].enabled = false;
		}

        countTemp = placesChoosen.Count;
        for (int i = 0; i < countTemp; i++){
			placesChoosen[i].imagePanel = null;
		}

		placesChoosen.Clear();

		for (int i = 0; i < 5; i++) {
			placesToHide[i].enabled = true;
			placesToHide [i].UpdateImage(itemsToChoosen [i]);
			placesToHide [i].imageComponent.enabled = true;	
			placesChoosen.Add (placesToHide [i]);
		}

        placesToHide.Suffle ();

        countTemp = placesToHide.Count;
        for (int i = 0; i < countTemp; i++) {
			if (placesToHide[i].Image == null) {
				placesToHide[i].enabled = true;
				placesToHide[i].UpdateImage(itemsToChoosen [5]);
				placesToHide[i].imageComponent.enabled = true;
				placesChoosen.Add (placesToHide[i]);
				break;
			}
		}
			
		placesChoosen[5].imageComponent.color = Color.clear;

		for (int i = 0; i < 6; i++) {
			if(ImagensPanel [i] != null && placesChoosen [i] != null){
				ImagensPanel [i].sprite = itemsToChoosen [i];
				buttonsOnPanel[i].spriteOfButton = itemsToChoosen [i];
				buttonsOnPanel[i].UpdateText();
				ImagensPanel [i].color = Color.black;
				placesChoosen [i].imagePanel = ImagensPanel [i];
			}
		}
        
		for (int i = 0; i < ButtonsPanelG.Length; i++){
			ButtonsPanelG[i].SetActive(true);
		}

		pauloLock.SetActive(false);

		amountsToFind = 6;
		amountsFinded = 0;

		float times = 0.0f;
		while (times < fadeOutDuration)
		{
			times += Time.deltaTime;
			float s = times / fadeOutDuration;

			fadeImage.color = Color.Lerp (Color.white,new Color (1f, 1f, 1f, 0f), fadeOutCurve.Evaluate (s));

			yield return 0f;
		}

		fadeImage.gameObject.SetActive(false);
		isGameRunning = true;
		actualCombo = 1;
        parentImageTransform = parentImage.transform;
        foundPaulo = false;
		isChoosingFound = false;
		fadeImage.raycastTarget = false;
		textErrou.gameObject.SetActive(false);
		releasePauloRoutine = releasePaulo ();
		StartCoroutine (releasePauloRoutine);
		timerSlider.value = timerSlider.maxValue;
		if (!canBeStarted) {
			InvokeRepeating ("DecreaseTimeOverSeconds", 1f, 1f);
		}
		isGameRunning = true;
		gettingPointsRunning = false;
		timeEnded = false;
		destroyPref = false;
		System.GC.Collect();
	}

	public IEnumerator<float> ResetGame(){
		destroyPref = true;
		isGameRunning = false;
		CancelInvoke();
		StopCoroutine(releasePauloRoutine);
		buttonsOnPanelFinded.Clear();
		yield return Timing.WaitForSeconds(.5f);

		fadeImage.gameObject.SetActive(true);
		textErrou.gameObject.SetActive(true);

		Color startColor = foundPauloText.color;
		Color colorNoAlpha = new Vector4(startColor.r,startColor.g,startColor.b,0f);

		float duration = .5f;
		float times = 0.0f;
		while (times < duration)
		{
			times += Time.deltaTime;
			float s = times / duration;

			fadeImage.color = Color.Lerp (new Color (1f, 1f, 1f, 0f), Color.white, fadeInCurve.Evaluate (s));
			textErrou.color = Color.Lerp(colorNoAlpha, startColor, fadeInCurve.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}		

		Level1_2A level = Levels [dificult];

		particlesParent = level.particlesParent;

		for (int i = 0; i < level.parallaxLayers.Length; i++){
			parallaxLayers[i] = level.parallaxLayers[i];
		}

		if(parentImage !=null){
			parentImage.gameObject.SetActive(false);
		}

		parentImage = level.ImageBackground;
		rectImage = Levels [dificult].ImageBackground.GetComponent<RectTransform>();
		placesToHide = level.hidenPlaces;

		parentImage.gameObject.SetActive(true);

		if(dificult >= 1){
		for (int i = 0; i <  placesToHide.Count; i++){
				placesToHide[i].transform.rotation = originRotation[i];	
				placesToHide[i].enabled = false;
		}
		}

		itemsToHide.Suffle ();
		placesToHide.Suffle ();		


		for (int i = 0; i < 5; i++) {
			itemsToChoosen.Add (itemsToHide [i]);
		}
			
		itemsToChoosen.Add (pauloSprite);
		originRotation.Clear();

		for (int i = 0; i < placesToHide.Count; i++) {
			placesToHide[i].enabled = true;
			placesToHide [i].isDone = false;
			placesToHide [i].Image = null;
			placesToHide [i].imageComponent.sprite = null;
			placesToHide [i].imageComponent.enabled = false;
			placesToHide [i].imagePanel = null;
			placesToHide [i].imageComponent.color = Color.white;
			placesToHide [i].touchParent = level.touchParent;
			originRotation.Add(placesToHide [i].transform.localRotation);
		}

		for (int i = 0; i < placesChoosen.Count; i++){
			placesChoosen[i].imagePanel = null;
		}

		placesChoosen.Clear();

		for (int i = 0; i < 5; i++) {
			placesToHide [i].UpdateImage(itemsToChoosen [i]);
			placesToHide [i].imageComponent.enabled = true;	
			placesChoosen.Add (placesToHide [i]);
		}

        placesToHide.Suffle ();

		for (int i = 0; i < placesToHide.Count; i++) {
			if (placesToHide[i].Image == null) {
                placesToHide[i].enabled = true;
                placesToHide[i].UpdateImage(itemsToChoosen [5]);
                placesToHide[i].imageComponent.enabled = true;
				placesChoosen.Add (placesToHide[i]);
				break;
			}
		}

		placesChoosen[5].imageComponent.color = Color.clear;

		for (int i = 0; i < 6; i++) {
			if(ImagensPanel [i] != null && placesChoosen [i] != null){
				ImagensPanel [i].sprite = itemsToChoosen [i];
				buttonsOnPanel[i].spriteOfButton = itemsToChoosen [i];
				buttonsOnPanel[i].UpdateText();
				ImagensPanel [i].color = Color.black;
				placesChoosen [i].imagePanel = ImagensPanel [i];
			}
		}

		for (int i = 0; i < ButtonsPanelG.Length; i++){
			ButtonsPanelG[i].SetActive(true);
		}

		pauloLock.SetActive(false);

		amountsToFind = 6;
		amountsFinded = 0;

		yield return Timing.WaitForSeconds(3f);

		times = 0.0f;
		while (times < duration)
		{
			times += Time.deltaTime;
			float s = times / duration;

			fadeImage.color = Color.Lerp (Color.white,new Color (1f, 1f, 1f, 0f), fadeOutCurve.Evaluate (s));
			textErrou.color = Color.Lerp(startColor, colorNoAlpha, fadeInCurve.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}

		textError.gameObject.SetActive(false);

		fadeImage.gameObject.SetActive(false);
		isGameRunning = true;
		actualCombo = 1;
		foundPaulo = false;
		isChoosingFound = false;
		fadeImage.raycastTarget = false;
		foundPaulo = false;
		isChoosingFound = false;
		
		releasePauloRoutine = releasePaulo ();
		StartCoroutine (releasePauloRoutine);
		if (!canBeStarted) {
			InvokeRepeating ("DecreaseTimeOverSeconds", 1f, 1f);
		}
		isGameRunning = true;
		fadeImage.gameObject.SetActive(false);
		isChoosingFound = false;
		isGameRunning = true;
		destroyPref = false;
	}

	IEnumerator TimeToPoints(){

		starsAmount++;
        IncreaseStars(starsAmount);
		

		isGameRunning = false;
		CancelInvoke();
		int timeToPointsAmount = (int)timerSlider.value;
		int scoreTemp = scoreAmount + timeToPointsAmount;
		int scoreTempStart = scoreAmount;
		float times = 0.0f;
		while (times < timeToPointsDuration)
		{
			times += Time.deltaTime;
			float s = times / timeToPointsDuration;
            
			Timing.RunCoroutine(timeToScoreParticles());
			timerSlider.value = Mathf.Lerp(timeToPointsAmount,0f,timeToPointsCurve.Evaluate(s));
			int scory = (int)Mathf.Lerp (scoreTempStart, scoreTemp, timeToPointsCurve.Evaluate (s));
			scoreText.text = scory.ToString ();
			
			yield return Timing.WaitForOneFrame;
		}

		scoreAmount = scoreTemp;
        log.pontosLudica = scoreAmount;
        dificult++;
		if(dificult > 2 || hasEndByTime){
			isChoosingFound = false;
			stopBlink();
			yield return Yielders.Get(1f);
            if (hasEndByTime) {
                log.faseLudica = dificult + 1;
            } else {
                log.faseLudica = 4;               
            }
            isGameRunning = false;
			CancelInvoke ();
			timerSlider.value = 0;
		    GoForEducation();
            Debug.Log("TimeToPoints", this);
        } else {
			Timing.RunCoroutine (startGame ());
		}
	}

	public void bt_rightDown(){
		isRight = true;
	}

	public void bt_rightUp(){
		isRight = false;
	}

	public void bt_leftDown(){
		isLeft = true;
	}

	public void bt_leftUp(){
		isLeft = false;
	}

	public IEnumerator releasePaulo(){		
		yield return new WaitUntil(() => amountsFinded >= 5);
		destroyPref = true;
		CancelInvoke();
		stopBlink();
		yield return Yielders.Get(1.5f);
		
		fadeImage.gameObject.SetActive(true);

		Color startColor = foundPauloText.color;
		Color colorNoAlpha = new Vector4(startColor.r,startColor.g,startColor.b,0f);

		foundPauloText.gameObject.SetActive(true);
		foundPauloText.color = colorNoAlpha;

		float duration = .5f;
		float times = 0.0f;
		while (times < duration)
		{
			times += Time.deltaTime;
			float s = times / duration;

			foundPauloText.color = Color.Lerp(colorNoAlpha, startColor, fadeInCurve.Evaluate (s));
			fadeImage.color = Color.Lerp (new Color (1f, 1f, 1f, 0f), Color.white, fadeInCurve.Evaluate (s));

			yield return Yielders.EndOfFrame;
		}

        soundManager.startVoiceFX(sfx[2]);
		yield return Yielders.Get(3f);
		placesChoosen[5].imageComponent.color = Color.white;
		foundPaulo = true;

		for (int i = 0; i < ButtonsPanelG.Length; i++){
			ButtonsPanelG[i].SetActive(false);
		}

		pauloLock.SetActive(true);

		times = 0.0f;
		while (times < duration)
		{
			times += Time.deltaTime;
			float s = times / duration;

			foundPauloText.color = Color.Lerp(startColor, colorNoAlpha, fadeInCurve.Evaluate (s));
			fadeImage.color = Color.Lerp (Color.white,new Color (1f, 1f, 1f, 0f), fadeOutCurve.Evaluate (s));

			yield return Yielders.EndOfFrame;
		}

		fadeImage.gameObject.SetActive(false);
		foundPauloText.gameObject.SetActive(false);
		foundPauloText.color = startColor;		
		destroyPref = false;
		if (!canBeStarted) {
			InvokeRepeating ("DecreaseTimeOverSeconds", 1f, 1f);
		}
		System.GC.Collect();
	}

		public IEnumerator<float> GettingPoints(){

		yield return Timing.WaitForSeconds(1f);

		int temp = dificult+1;	

		fadeImage.raycastTarget = true;

		fadeImage.gameObject.SetActive(true);

		HighLightsComp.SetActive(true);
        stringFast.Clear();
        stringFast.Append("Nível ").Append(temp);

        nextLevel.textLevelChange = stringFast.ToString();
		nextLevel.startChangeLevelAnimation(2);


		yield return Timing.WaitForSeconds(1f);

		float times = 0.0f;
		while (times < fadeInDuration)
		{
			times += Time.deltaTime;
			float s = times / fadeInDuration;

			fadeImage.color = Color.Lerp (new Color (1f, 1f, 1f, 0f), Color.white, fadeInCurve.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}

		
		CancelInvoke ();
		timeToPointsRoutine = TimeToPoints ();
		StartCoroutine(timeToPointsRoutine);
	}

	public void GoForEducation(){
		log.StartTimerLudica (false);
		isChoosingFound = false;
		stopBlink();
        destroyPref = true;
        isGameRunning = false;
        log.pontosLudica = scoreAmount;
        canBeStarted = true;
        Debug.Log("Starting Didatica.");
        tutorPanel.GetComponent<Image>().enabled = false;
        nextManager.StartCoroutine(nextManager.lateStart());
        //TutorialCheking();
        //Timing.RunCoroutine(FinalMessageEffectTransition());		
        //Debug.Log("Inicio da Didatica 2", this);

    }

    IEnumerator<float> FinalMessageEffectTransition() {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0f, 0.001f);

        if (log.faseLudica == 4) {
            //terminou tudo. todas as 3 fases e encontrou todos os personagens.
            textFinalMessage.text = textos[2];
            //yield return Timing.WaitForOneFrame;
        } else if (log.faseLudica < 4 && amountsFinded > 0) {
            //ainda não terminou tudo mas encontrou algum dos personagens.
            textFinalMessage.text = textos[1];
            //yield return Timing.WaitForOneFrame;
        } else {
            //não encontrou nada nem terminou nada. zerado burro cego.
            textFinalMessage.text = textos[0];
            //yield return Timing.WaitForOneFrame;
        }

        textFinalMessage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, .3f);
        textFinalMessage.DOFade(1f, .3f);

        yield return Timing.WaitForSeconds(14.3f);



        //TutorialCheking();


    }

    public void TutorialCheking() {
        if (emCenaCheck == false) {
            tutorPanelAnimator.SetInteger(emCenaHash, 0);
           // Debug.Log("1");
        }

        if (tutorIni_1 == 0) {
          //  tutorIni_1 = PlayerPrefs.GetInt("TutorSP_1", 1);
            Timing.KillCoroutines();
            tutorTex.text = tutorTexC;
			CancelInvoke ();
            StartCoroutine(GoForEducationTime());
            Debug.Log("tutorIni_1", this);

        } else {
           // Debug.Log("3");
            Timing.KillCoroutines();
            CancelInvoke();
        }

        config.Rank(log.idMinigame, scoreAmount, starsAmount);
        //this.enabled = false;
    }
	
	IEnumerator GoForEducationTime(){
        ControlSomTutor2.numTutor = 1;
        emCenaCheck = true;
		yield return Yielders.Get (1f);	
		tutorPanelAnimator.SetInteger(emCenaHash, 2);
		//nextManager.StartCoroutine(nextManager.lateStart());
		btAvancar.SetActive (true);


	}

	void DecreaseTimeOverSeconds(){
		float temp = (float)1f;
		timerSlider.value -= temp;
	}

	public void NothingFinded(){
		timerSlider.value -= (float)decreasePerWrongs;
		soundManager.startSoundFX(sfx[0]);
        //Fade Sequence.
        Sequence RedFlag = DOTween.Sequence();
        RedFlag.Append(RedImageDamage.DOFade(RedAlpha,RedTimerForFade)).SetEase(curveRedAnimation);
        RedFlag.AppendInterval(0.1f);
        RedFlag.Append(RedImageDamage.DOFade(0f,RedTimerForFade)).SetEase(curveRedAnimation);
        RedFlag.Play();
        shakeCamera.shakecamera();
		Timing.KillCoroutines("LostTimeBar");
		Timing.RunCoroutine(LostTimeBarEffect(),Segment.Update, "LostTimeBar");
		
	}

	IEnumerator<float> LostTimeBarEffect(){

		float times = 0.0f;
		while (times < sliderErrorDuration)
		{
			times += Time.deltaTime;
			float s = times / sliderErrorDuration;

			fillImageSlider.color = Color.Lerp (Color.white, sliderColorError, sliderErrorCurve.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}
	}

	IEnumerator<float> TextErrorEffect(){

        stringFast.Clear();
        stringFast.Append("- ").Append(decreasePerWrongs).Append(" Segundos");

        textError.text = stringFast.ToString();
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = 10.0f; //distance of the plane from the camera
		Vector3 pos = offsetTextErrorPos + Camera.main.ScreenToWorldPoint(screenPoint);
		textError.transform.position = pos;
		Color colorNoAlpha = new Vector4(startTextErrorColor.r,startTextErrorColor.g,startTextErrorColor.b,0f);

		float times = 0.0f;
		while (times < errorTextEffectDuration)
		{
			times += Time.deltaTime;
			float s = times / errorTextEffectDuration;

			textError.color = Color.Lerp (colorNoAlpha, startTextErrorColor, errorTextEffectCurve.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}
	}

	public IEnumerator<float> StartTotalPointsInGame(int increase){

		//Debug.Log ("Start Increase Points");
		float times = 0.0f;
		int startPoints = scoreAmount;
		int scoreT = scoreAmount + increase;
		scoreAmount += increase;
		while (times < increaseScoreDuration)
		{
			times += Time.deltaTime;
			float s = times / increaseScoreDuration;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, increaseScoreCurve.Evaluate (s));
			scoreText.text = scory.ToString ();
			yield return Timing.WaitForOneFrame;
		}

        //UpdateStars(scoreT);

    }

	public void Founded(Sprite spriteFinded, Vector3 posTo){
        //Debug.Log ("Founded");
        StartInteractable();
        isChoosingFound = true;
		soundManager.startSoundFX(sfx[1]);
		exclamation.transform.position = posTo;
		actualCombo++;
        //Timing.KillCoroutines("BLINKROUTINE");
        //Timing.RunCoroutine(PanelExclamationBlink(),Segment.LateUpdate,"BLINKROUTINE");
        DOTween.Kill(001, false);
        BlinkTween();


    }

	public void Founded(Sprite spriteFinded, Vector3 posTo, RectTransform rectT){
        //Debug.Log ("Founded");
        StartInteractable();
        isChoosingFound = true;
		
		foundedSprite = spriteFinded;

		exclamation.transform.position = posTo;
		exclamation.GetComponent<RectTransform>().sizeDelta = rectT.sizeDelta;

        //Timing.KillCoroutines("BLINKROUTINE");
        //Timing.RunCoroutine(PanelExclamationBlink(),Segment.LateUpdate,"BLINKROUTINE");
        DOTween.Kill(001, false);
        BlinkTween();

        actualCombo++;

	}

	public void FoundedPaulo(Sprite spriteFinded, Vector3 posTo, RectTransform rectT){
		//Debug.Log ("Founded");
		isChoosingFound = true;
		
		foundedSprite = spriteFinded;

		exclamation.transform.position = posTo;
		exclamation.GetComponent<RectTransform>().sizeDelta = rectT.sizeDelta;

        buttonClick(pauloLockButton);
		actualCombo++;
        StartInteractable();
    }

	public int fixCombo(){
		if (actualCombo == 0 || actualCombo == 1) {
			return 1;
		} else if (actualCombo == 2) {
			return 2;
		} else if (actualCombo >= 3) {
			return 3;
		} else {
			return 1;
		}
	}

	public int ReturnIDChar(string spriteName){
		switch (spriteName)
		{	
			case "Zeca":
			return 0;
			case "João":
			return 4;
			case "Paulo":
			return 2;
			case "Manu":
			return 3;
			case "Tati":
			return 1;
			case "Bia":
			return 5;
			default:
			return 0;
		}
	}

	IEnumerator<float> timeToScoreParticles(){

		GameObject thisParticle = Instantiate(particlePrefab,particlesParent) as GameObject;
		thisParticle.transform.localScale = new Vector3(1f,1f,1f);
		thisParticle.transform.position = particleTransform.position;
		Vector3 pos = particleTransform.position;
		Vector3 posTo = scoreText.transform.position;

		float times = 0.0f;
		while (times < particleMovementDuration)
		{
			times += Time.deltaTime;
			float s = times / particleMovementDuration;


			thisParticle.transform.position = Vector3.Lerp(pos,posTo, timeToPointsCurve.Evaluate(s));

			yield return Timing.WaitForOneFrame;
		}	

		Destroy(thisParticle);
	}

	IEnumerator<float> findParticle(Vector3 posParticles){
        GameObject particleInstance = Instantiate(findParticlePrefab,particlesParent) as GameObject;
		particleInstance.transform.localScale = new Vector3(10f,15f,0f);
		
		Vector3 pos = new Vector3(posParticles.x,posParticles.y,-5f);
		particleInstance.transform.position = pos;

		float times = 0.0f;
		while (times < findParticleDuration)
		{
			times += Time.deltaTime;
			float s = times / findParticleDuration;

			particleInstance.transform.localScale = Vector3.Lerp(new Vector3(10f,15f,50f),new Vector3(30f,60f,50f), findParticleCurve.Evaluate(s));

			yield return Timing.WaitForOneFrame;
		}

		particleInstance.transform.position = pos;
		Destroy(particleInstance);
	}

    public void BlinkTween() {
        isChoosingFound = true;
        for (int i = 0; i < buttonsOnPanel.Count - 1; i++) {
            if (!buttonsOnPanelFinded.Contains(buttonsOnPanel[i])) {
                Text mainExcText = exclamation.GetComponent<Text>();
                //buttonsOnPanel[i].UpdateTextColorExclamation(Color.Lerp(_colorNoAlpha, _color, blinkExclamationCurve.Evaluate(s)));
                buttonsOnPanel[i].ExclamationTextEnable(true);
                buttonsOnPanel[i].exclamationPoint.DOFade(1f, blinkExclamationDuration).SetLoops(-1, LoopType.Yoyo).SetId(001);
                Color temp = mainExcText.color;
                temp.a = 0f;
                mainExcText.color = temp;
                mainExcText.DOFade(1f, blinkExclamationDuration).SetLoops(-1, LoopType.Yoyo).SetId(001);
            }
        }
    }
	
	public IEnumerator<float> PanelExclamationBlink(){
       

        isChoosingFound = true;
        /*Color _colorNoAlpha = _color;
		_colorNoAlpha.a = 0f;
		while(isChoosingFound){
			
			float times = 0.0f;
			while (times < blinkExclamationDuration)
			{
				times += Time.deltaTime;
				float s = times / blinkExclamationDuration;
				for (int i = 0; i < buttonsOnPanel.Count-1; i++){
					if(!buttonsOnPanelFinded.Contains(buttonsOnPanel[i])){
						buttonsOnPanel[i].UpdateTextColorExclamation(Color.Lerp(_colorNoAlpha, _color, blinkExclamationCurve.Evaluate(s)));
					}
				}

				exclamation.GetComponent<Text>().color = Color.Lerp(_colorNoAlpha, _color, blinkExclamationCurve.Evaluate(s));
				
				yield return Timing.WaitForOneFrame;
			}
		}*/



        yield return Timing.WaitForOneFrame;

        //Debug.Log("Blinking STOP");
    }



	public void buttonClick(ButtonExtension1_2A _button){
		if(isChoosingFound == true){
			isChoosingFound = false;
			if(_button.spriteOfButton == foundedSprite){
				//Debug.Log("Correct");
				soundManager.startSoundFX(sfx[1]);
				actualCombo++;
				//destroyPref = true;
				if (foundedSprite == pauloSprite) {
					int temp = 50 * fixCombo ();

					Timing.RunCoroutine (StartTotalPointsInGame (temp));

				} else {					
					int temp = pointsRight * fixCombo ();
					Timing.RunCoroutine (StartTotalPointsInGame (temp));

				}
				_button.imageComp.color = Color.white;
				buttonsOnPanelFinded.Add(_button);
				stopBlink();
                StopInteractable();
                amountsFinded++;
			} else {
                //Debug.Log("Errado");
                //soundManager.startSoundFX(sfx[0]);
                StopInteractable();
                stopBlink();
				actualCombo = 0;
				Timing.RunCoroutine(ResetGame());
                shakeCamera.shakecamera();
            }
		}
	}

	public void stopBlink(){



		Timing.KillCoroutines("BLINKROUTINE");
        DOTween.Kill(001, false);

		for (int i = 0; i < buttonsOnPanel.Count; i++){					
			buttonsOnPanel[i].stopBlink();
		}

        Text mainExcText = exclamation.GetComponent<Text>();
        Color temp = mainExcText.color;
        temp.a = 0f;
        mainExcText.color = temp;
        exclamation.transform.position = new Vector3(9999f,9999f,10f);

		isChoosingFound = false;
	}

	public IEnumerator<float> scoreReset(){
		//Debug.Log ("Start Increase Points");
		float times = 0.0f;
		int startPoints = scoreAmount;
		int scoreT = startAmountPoins;
		scoreAmount = startAmountPoins;
		while (times < scoreDecreaseDuration)
		{
			times += Time.deltaTime;
			float s = times / scoreDecreaseDuration;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, ScoreDecreaseCurve.Evaluate (s));
			scoreText.text = scory.ToString ();
			yield return Timing.WaitForOneFrame;
		}	
	}

	public void StopInteractable(){
        /*for (int i = 0; i < buttonsOnPanel.Count; i++){
			buttonsOnPanel[i].stopInteractable();
		}*/
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0.8f;

    }

	public void StartInteractable(){
        /*for (int i = 0; i < buttonsOnPanel.Count; i++){
			buttonsOnPanel[i].startInteractable();
		}*/
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }


	public void EndRoutines(){
		destroyPref = true;
		StopAllCoroutines();
		CancelInvoke();
		Timing.KillCoroutines();
		nextManager.EndAllCoroutines();

	}

 

    public void IncreaseStars(int amount) {
        starsAmount = amount;
        for (int i = 0; i < 3; i++) {
            starParent.GetChild(i).GetComponent<Image>().sprite = emptyStarSprite;
        }

        for (int i = 0; i < amount; i++) {
            starParent.GetChild(i).GetComponent<Image>().sprite = fullStarSprite;
        }
    }

    public void MouseMovementOnEdge() {

    }

}
