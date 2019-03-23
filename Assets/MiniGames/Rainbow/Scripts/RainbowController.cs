using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using MEC;

public class RainbowController : OverridableMonoBehaviour {

	public SelPersons SelPersons;
	public GameObject panelDesafio;

	public ControlCenario ControlCenario2;

	public List<GameObject> spawnPositions = new List<GameObject> ();
	/*[HideInInspector]
	public List<GameObject> firstBonus = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> secondBonus = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> thirdBonus = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> fourthBonus = new List<GameObject>();*/
	[HideInInspector]
	public List<GameObject> bonusItems = new List<GameObject>();

	[HideInInspector]
	public List<GameObject> lunch = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> joys = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> manners = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> knowledge = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> study = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> grades = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> friendship = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> negativeItem = new List<GameObject>();

    public Transform instanceParent;

	public List<RainbowLevelConfig> levels = new List<RainbowLevelConfig> ();
	public RainbowLevelConfig currentLevel;

	public Slider energyBar;

	public int dificult = 0;
	public int characterSelected = 0;

	/*[Range(1,10)]
	public float minSpeedInstance = 1f;
	[Range(1,20)]
	public float maxSpeedInstance = 1f;

	[Range(0,1)]
	public float[] coinSpeedVariation;*/

	[Header("Player Points/Amounts")]
	/*public int coinsAmount = 0;
	public int goldBarAmount = 0;
	public int diamontAmount = 0;*/
	public int lunchAmount = 0;
	public int joysAmount = 0;
	public int mannersAmount = 0;
	public int knowledgeAmount = 0;
	public int studyAmount = 0;
	public int gradesAmount = 0;
	public int friendshipAmount = 0;
	public int bonusItemAmount = 0;
	public int amountPoints = 0;

	private float speedInstance;
	private bool waitToInstance = false;

	private int oldPointsAmount = 0;
    public int pontosNegativo = 10;

	[Header("UI Configs")]
	public float timer = 0f;
	public Text timerText;
	private bool waitToStart = true;

	public float waitStillTime = 0f;
	public Transform UIParents;
	public GameObject[] UIPrefabs;
	private int answerAmount = 0;
	public InputField userInput;
	//public GameObject panelEndGame;
	public Text pointsText;
	public Text levelText;
	public Text parabensText;
	int levelTextNum;

	[Range(0,100)]
	public int bonusPointsValue = 0;
	private int typeItem;


	[Header("Level Increase Text")]
	[TextArea()]
	public string textToShow;
	public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Transform start;
	public Transform end;
	public Transform textTransform;
	public float duration = 1.0f;
	float t;
	bool isTextMoving = false;

	[Header("Bucket Text")]
	public string bucketText;
	private Text bucketTextComponent;
	public AnimationCurve bucketTextFadeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public Color startColor;
	public Color targetColor;
	public float bucketColorLerpDuration = 1.0f;
	public Transform bucketTransform;
	public Transform bucketTextTransform;


	public Sprite estrelaVazia;
	public Sprite estrelaCheia;
	public Transform estrelas;
	public int amountStars = 0;
	float ElapseTime=0f;
	public float movementTime;
	public Transform posCam;
	public Vector2 posCamIni;
	public int introAnim;	
	public GameObject iniciarBT;
//	public GameObject arcoris;
	public float GoMoveCam;
	public bool checkFim;
	UnityStandardAssets._2D.PlatformerCharacter2D plataformCharacter2D;
	UnityStandardAssets.CrossPlatformInput.ButtonHandler ButtonHandler2;
	public GameObject person;
	public GameObject inputBTD;

	


	[Header("- Tela de Contagem")]
	public Transform[] sidePanels;
	public AnimationCurve changeItemTCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float changeItemTDuration = 1.0f;
	public int selectedItem = 0;
	public int actualTargetCount = 0;
	private bool isChanging = false;
	public ItemIndexed[] itensToCount;
	public Transform panelItens;

	public AnimationCurve fadeInCurveItem = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float fadeInDuration = 1.0f;
	public int currentIndexedItem = 0;
	private int oldIndexItem = 0;

	public int userAnswers;

	public Sprite[] iconsImage = new Sprite[7];
	public GameObject trigLimt;
	public GameObject panelEnd;
	bool panelEndShow;
	public bool jogarBalde;
	public RainbowGoldPot[] controlBaldes;
	public RainbowGoldPot controlBalde;

	public Text[] texts;
	public List<int> alternatives = new List<int>();
	public Color defaultColor;
	public Color selectColor;
	public Color rightColor;
	public Color wrongColor;

	[Header("Sound Manager")]
	public SoundManager soundManager;
	public AudioClip[] soundFX;

	bool virarPerson;
	bool stopTimeShow;
	bool stopRainItem = false;
	public int checkStartGame;
	bool checkStartIstan = false;
	bool checkSomArco = false;
	public GameObject arcoIris;
	bool isCountUpdated = false;
	bool isPlaying = false;

	public GameObject pauseMenuPanel;
	public GameConfig GameConfig;
	public Toggle[] Toggles;
	public LoadManager loadManager;
	float toqueTela;
	public GameObject[] HUD;

	public Button[] buttons;

	public Sprite buttonDefault;
	public Sprite buttonSelect;
	public GameObject barraEner;
	//public GameObject confetesG;
	public Animator parabensAnim;
	public string[] texParabens; 
	public string[] texErrAcerto; 
	public GameObject aguaMang;
	public GameObject countEnd;
	public FinalScore finalScore;
	public GameObject PanelProfessora;
	int professoraAjuda=-1;
	float timeM;
	bool checkprofessoraAjuda;

	[Header("Text Increase")]
	public float transferDurationTime;
	public float transferDurationInGame;
	public AnimationCurve transferCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

	[Header("Text Richies")]
	public Text textOfRichies;
	public float durationEffect;
	public AnimationCurve effectCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public string textEffect;
	public Text ajudaBt;
	public GameObject aprendaJogar;
	public GameObject personagensB;

	public GameObject[] controlBaldepartZeca;
	public GameObject[] controlBaldepartTatiBia;
	public GameObject[] controlBaldepartPaulo;
	public GameObject[] controlBaldepartJoaoManu;
	public bool checkDesafio;

	bool checkPlay;
	bool checkStartGame2;
	public bool pararcheckCam;
	Vector3 posViewPort;
	bool checkPersonMove;

	 bool moveCam;
	 public bool moveCaPassou;

	 public GameObject cenario1;
	public GameObject cenario2;
	 public GameObject panelAcerto;

	public GameObject prof;
	public GameObject sairGameObj;
	public GameObject[] panels;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Adição e Subtração Painel")]
	#endif
	public managerAddRainbow managerNext;

	//public  GameObject personsSS;
	// Use this for initialization

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("LOG")]
	#endif
	public bool isTimerLogStart = false;
	public bool isTimerLogDidaticaStart = false;
	public float timerLudica = 0f;
	public float timerDidatica = 0f;
	public int scoreLudica = 0;
	public int scorePegagogica = 0;
	public int scoreInteragindo = 0;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Configuração Didatica")]
	#endif
	public int idHabilidade;
	public int idDificuldade;
	public int idMinigame;
	public LogSystem log;
	public Button perg;
	bool checkPassou;
    tutorPanelArcoirirs tutorPanelArcoirirs2;
    int numbCont;
    int tutor_D;

    public GameConfig config;
    public Minigames minigame;

    public GameObject[] notPc;
    public GameObject panelTeclado;

	 

	public bool checkfim2;
    private bool isFirstTime;

    public void MoveCamM(){
		moveCam=true;
		cenario2.SetActive(true);
		//panelAcerto.SetActive(true);
	}

	IEnumerator<float> scrptProf(){
	
			yield return Timing.WaitForSeconds (2f);
			prof.GetComponent<espressFacialProf>().enabled=true;
		
	
	}

	public IEnumerator<float> StartTotalPoints(int increase){
		float times = 0.0f;
		int scoreT = amountPoints + increase;
        amountPoints = scoreT;
        while (times < transferDurationTime){
			times += Time.deltaTime;
			float s = times / transferDurationTime;

			int scory = (int)Mathf.Lerp (amountPoints, scoreT, transferCurve.Evaluate (s));
			pointsText.text = scory.ToString ();

			yield return 0f;
		}
		//amountPoints = scoreT;
	}
	


	public IEnumerator<float> StartTotalPointsInGame(int increase){
		float times = 0.0f;

		int scoreT = amountPoints + increase;
        amountPoints = scoreT;

        while (times < transferDurationInGame)
		{
            times += Time.deltaTime;
			float s = times / transferDurationInGame;

			int scory = (int)Mathf.Lerp (amountPoints, scoreT, transferCurve.Evaluate (s));
			pointsText.text = scory.ToString ();

			//bucketTextComponent.color = Color.Lerp (startColor, colors, transferCurve.Evaluate (s));
			yield return 0f;
		}

        //UpdateStars(scoreT);

	}
 	

	void Start () {
        Input.multiTouchEnabled = true;
        if (Application.platform == RuntimePlatform.WindowsPlayer) 
		// if (Application.platform == RuntimePlatform.WindowsEditor) 
		 {
            for (int i = 0; i < notPc.Length; i++) {
                notPc[i].SetActive(false);				
            }
			panelTeclado.SetActive(true);
        } else {
            for (int i = 0; i < notPc.Length; i++) {
                notPc[i].SetActive(true);				
            }
			panelTeclado.SetActive(false);
        }

        minigame = config.allMinigames[3];


        if (PlayerPrefs.HasKey("TutorArcoIris_D") == false) {
            PlayerPrefs.SetInt("TutorArcoIris_D", 0);

        } else {
            PlayerPrefs.SetInt("TutorArcoIris_D", 1);
            tutor_D = PlayerPrefs.GetInt("TutorArcoIris_D", 1);
          
        }
        PanelProfessora.SetActive(true);
        Resources.UnloadUnusedAssets();

        Timing.RunCoroutine(scrptProf());
	//	cenario1.SetActive(true);

		//characterSelected = PlayerPrefs.GetInt("characterSelected", 0);
		CrossPlatformInputManager.SetAxisZero("Horizontal");
	//	 personsSS = Instantiate(personagensSel,posInicial.transform.position, Quaternion.identity) as GameObject;
//		 SelPersons = person.GetComponent<SelPersons>();
		 SelPersons.PersonSel = PlayerPrefs.GetInt("characterSelected", 0);


		if (SelPersons.PersonSel == 0) {
			person = SelPersons.personZeca;		
			controlBalde = controlBaldes [0];
			bucketTransform = controlBaldes[0].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "zeca";
			controlBaldepartZeca [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "zeca";
			controlBaldepartZeca [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "zeca";
			controlBaldepartZeca [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "zeca";
			controlBaldepartZeca [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "zeca";

			
		}
		else if (SelPersons.PersonSel == 1) {
			person = SelPersons.AmigosZeca;
			controlBalde = controlBaldes [1];
			bucketTransform = controlBaldes[1].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "Tati";
			controlBaldepartTatiBia [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Tati";
			controlBaldepartTatiBia [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Tati";
			controlBaldepartTatiBia [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Tati";
			controlBaldepartTatiBia [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Tati";

		}
		else if (SelPersons.PersonSel == 2) {
			person = SelPersons.AmigosZeca;
			controlBalde = controlBaldes [2];
			bucketTransform = controlBaldes[2].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "Paulo";
			controlBaldepartPaulo [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Paulo";
			controlBaldepartPaulo [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Paulo";
			controlBaldepartPaulo [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Paulo";
			controlBaldepartPaulo [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Paulo";

		}
		else if (SelPersons.PersonSel == 3) {
			person = SelPersons.AmigosZeca;
			controlBalde = controlBaldes [3];
			bucketTransform = controlBaldes[3].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "manu";
			controlBaldepartJoaoManu [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "manu";
			controlBaldepartJoaoManu [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "manu";
			controlBaldepartJoaoManu [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "manu";
			controlBaldepartJoaoManu [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "manu";

		}
		else if (SelPersons.PersonSel == 4) {
			person = SelPersons.AmigosZeca;
			controlBalde = controlBaldes [3];
			bucketTransform = controlBaldes[3].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "Joao";
			controlBaldepartJoaoManu [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Joao";
			controlBaldepartJoaoManu [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Joao";
			controlBaldepartJoaoManu [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Joao";
			controlBaldepartJoaoManu [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Joao";
		}
		else if (SelPersons.PersonSel == 5) {
			person = SelPersons.AmigosZeca;
			controlBalde = controlBaldes [1];
			bucketTransform = controlBaldes[1].GetComponent<Transform>();
			controlBalde.GetComponent<SpriteRenderer>().sortingLayerName = "Bia";
			controlBaldepartTatiBia [0].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Bia";
			controlBaldepartTatiBia [1].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Bia";
			controlBaldepartTatiBia [2].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Bia";
			controlBaldepartTatiBia [3].GetComponent<ParticleSystemRenderer> ().sortingLayerName = "Bia";
		}

		
		aprendaJogar.SetActive (false);
//		aguaMang.GetComponent<DigitalRuby.RainMaker.RainScript2D> ().RainIntensity = 0f;
		aguaMang.SetActive (false);


		barraEner.SetActive(false);
		posCamIni = new Vector2 (GetComponent<Transform> ().transform.position.x,GetComponent<Transform> ().transform.position.y);
		//person = GameObject.FindGameObjectWithTag ("Player");
		plataformCharacter2D = person.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
		//ButtonHandler2 = inputBTD.GetComponent<UnityStandardAssets.CrossPlatformInput.ButtonHandler>();

		actualTargetCount = 0;
		trigLimt.SetActive(false);
        //int levelIndex = Random.Range (0, levels.Count);
        //Timing.RunCoroutine (playAgain ());
        //InvokeRepeating ("DecreaseEnergyOverTime", 1f, currentLevel.energyDecreateTimeRate);
        Physics2D.IgnoreLayerCollision (8, 0, true);
		bucketTextComponent = bucketTextTransform.GetComponent<Text> ();
		//Timing.RunCoroutine (TimeMoveCam ());
		introAnim = 1;
		waitToStart = true;
		person.GetComponent<Transform> ().transform.localScale = new Vector3 (person.GetComponent<Transform> ().transform.localScale.x * -1, person.GetComponent<Transform> ().transform.localScale.y, person.GetComponent<Transform> ().transform.localScale.z);

		//confetesG.SetActive (false);
		//this.panelDesafio.GetComponent<GameObject>().SetActive(false);
			panelDesafio.SetActive(false);
		posViewPort = Camera.main.WorldToViewportPoint(person.transform.position);

		GameConfig.LOG.personagem = GameConfig.GetCharName(PlayerPrefs.GetInt("characterSelected", 0));
        tutorPanelArcoirirs2 = PanelProfessora.GetComponent<tutorPanelArcoirirs>();
    }

    // Update is called once per frame

    public override void UpdateMe() {

			if (transform.position.x > 0 && (checkStartGame == 1 || checkStartGame == 2) && moveCam==true) {
			transform.position = Vector3.Lerp(transform.position, posCam.transform.position, (Time.deltaTime * movementTime));

		} 	
		
        if(checkFim==false){	
			//PanelProfessora.SetActive(true);
			if(aprendaJogar.GetComponent<aprendaJogarArcoIris>().tutorMoveFim==true && aprendaJogar.activeInHierarchy==true && checkFim==false && tutorPanelArcoirirs2.tutor_0==0){
			aprendaJogar.GetComponent<aprendaJogarArcoIris> ().tutorMoveFim = false;
			aprendaJogar.SetActive (false);
			pararcheckCam = true;
			Timing.RunCoroutine (PlayAgain ());
			
		}
			else if(aprendaJogar.GetComponent<aprendaJogarArcoIris>().tutorMoveFim==true && aprendaJogar.activeInHierarchy==true && checkFim==false && tutorPanelArcoirirs2.tutor_0==1){
				pararcheckCam = true;
				Timing.RunCoroutine (PlayAgain ());
			}
		//if (Time.realtimeSinceStartup-timeM > 5f && PanelProfessora.activeInHierarchy==true){
			//PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
			//timeM = Time.realtimeSinceStartup;
		//}
		
	
	



		if(checkStartGame == 2) {	
		 if(checkStartGame2==false){
			 checkStartGame2=true;	
		 	Timing.RunCoroutine (InicaBt ());
			checkStartGame2=true;
			//stopmoveCam=false;
		 }
		 
		}
		if (introAnim == 1 && transform.position.x < 11f && checkFim==false  ) {			
			introAnim = 2;
			checkStartGame = 2;
			personagensB.SetActive(false);
                Destroy(personagensB);


		}

		posViewPort = Camera.main.WorldToViewportPoint(person.transform.position);


#if UNITY_ANDROID || UNITY_IOS

            if (Input.touchCount == 1 && isPlaying == true) {
		
				if (Input.touches [0].position.x < Screen.width / 2 && posViewPort.x > 0.1f) {					
					CrossPlatformInputManager.SetAxisNegative ("Horizontal");
					//checkPersonMove=true;
				}
				if (Input.touches [0].position.x > Screen.width / 2 && posViewPort.x < 0.97f) {				
					CrossPlatformInputManager.SetAxisPositive ("Horizontal");
					//checkPersonMove=true;
				}
		} else if(Input.touchCount > 1 && isPlaying == true){
	
				if (Input.touches [0].position.x < Input.touches [1].position.x && posViewPort.x < 0.97f) {					
					CrossPlatformInputManager.SetAxisPositive ("Horizontal");
					//checkPersonMove=true;
		
				}
				if (Input.touches [0].position.x > Input.touches [1].position.x && posViewPort.x > 0.1f) {					
					CrossPlatformInputManager.SetAxisNegative ("Horizontal");
					//checkPersonMove=true;
			
				}
		} else if(isPlaying == true && Input.touchCount == 0){
			posViewPort = Camera.main.WorldToViewportPoint(person.transform.position);
			CrossPlatformInputManager.SetAxisZero ("Horizontal");
			//checkPersonMove=false;
	
		}

#endif

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN || UNITY_STANDALONE

            if (Input.GetKey(KeyCode.LeftArrow) && isPlaying == true){
			if(posViewPort.x > 0.1f){
				posViewPort = Camera.main.WorldToViewportPoint(person.transform.position);
				CrossPlatformInputManager.SetAxisNegative ("Horizontal");
				//checkPersonMove=true;
			
			}			
		} else if (Input.GetKey(KeyCode.RightArrow) && isPlaying == true){
			if(posViewPort.x < 0.97f){
				posViewPort = Camera.main.WorldToViewportPoint(person.transform.position);
				CrossPlatformInputManager.SetAxisPositive ("Horizontal");
				//checkPersonMove=true;
		
			}
		} else if(isPlaying == true && (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow)) ) {
		
			CrossPlatformInputManager.SetAxisZero ("Horizontal");
			//checkPersonMove=false;
			
		}

		#endif

		if (waitToStart == false) {

			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
			if (Input.GetKeyDown (KeyCode.Space)) {
				Timing.RunCoroutine (InstanceControll ());
			}

			if(Input.GetKeyDown(KeyCode.B)){
				Timing.RunCoroutine(CatchCoin(new Vector3(0f,0f,0f), Random.Range(0,3)));
			}
			#endif

			if (waitToInstance == false) {
				Timing.RunCoroutine (InstanceControll ());
			
			}

			/*if (amountPoints != oldPointsAmount) {
				oldPointsAmount = amountPoints;
			}*/



			TimerUpdate ();
			LevelVerifier ();

			if (energyBar.value <= 0.01f) {

				checkFim = true;
			PanelProfessora.SetActive(true);
			tutorPanelArcoirirs2.ChamarPanel2();
                    tutorPanelArcoirirs2.avancarTutor.SetActive(false);




            }
		} else {

			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
			if (Input.GetKeyDown (KeyCode.Space)) {
				StopAllCoroutines ();
				Timing.RunCoroutine(PlayAgain());


			}

			if (Input.GetKeyDown (KeyCode.U)) {
				if(amountStars < 3){					
					/*amountStars++;
					IncreaseStars();

                        //scoreIncrease*/
				} else {
					clearStars();
				}
			}

			/*if (Input.GetKeyDown (KeyCode.B)) {
				typeItem = 0;
				countThings(amountTypeItem());
			}*/

			
			


			#endif

		}
		
	

		FixBucketPosition ();

}
else if (checkFim==true && checkfim2==true){

			if (countEnd.activeInHierarchy == true && PanelProfessora.activeInHierarchy == false) {

                if (Time.realtimeSinceStartup - timeM > 10f && userAnswers == -1)
				{
					timeM = Time.realtimeSinceStartup;
					ajudaBt.text = "Fechar";
					//ajudaBt.fontSize = 70;
					//professoraAjuda = 1;
                    //PanelProfessora.SetActive (true);		
                    //PanelProfessora.GetComponent<tutorPanelArcoirirs>().BalaoScaleM2();
					perg.enabled = true;
					//if(checkPassou){
						//PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",10);
					//}
					checkPassou = !checkPassou;
				

					//Debug.Log("ponto 1");
					timeM = Time.realtimeSinceStartup;
				}
				if (Time.realtimeSinceStartup - timeM > 10f && userAnswers > -1)
				{
					timeM = Time.realtimeSinceStartup;
					ajudaBt.text = "Fechar";
					professoraAjuda = 2;
					//ajudaBt.fontSize = 70;
					//PanelProfessora.SetActive (true);
                    //PanelProfessora.GetComponent<tutorPanelArcoirirs>().BalaoScaleM3();

                   // PanelProfessora.GetComponent<Animator> ().enabled=true;
					perg.enabled = true;
					//Debug.Log("ponto 2");
					//PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",10);
					timeM = Time.realtimeSinceStartup;
				}
                
			}


	if(moveCaPassou==false){
		moveCam=true;
		cenario1.SetActive(true);
		moveCaPassou=true;
	}

			isPlaying = false;
			if (isCountUpdated == false) {
				itensToCount = ReturnTopThree ();
				UpdateAllSprites ();
				isCountUpdated = true;
			
			}
			if (person.activeInHierarchy == true) {
				plataformCharacter2D.m_Anim.SetBool ("jogarBalde", jogarBalde);
                
            }
			waitToInstance = true;
			CancelInvoke ();
			trigLimt.SetActive (true);			
			if(moveCam==true){
				transform.position = Vector3.Lerp (transform.position, new Vector3 (posCamIni.x, posCamIni.y, -10), (Time.deltaTime * movementTime));
                

			}
				
			
			if (plataformCharacter2D.checkClickMove == false && trigLimt.transform.position.x > person.transform.position.x) {
				if (virarPerson == false) {
					virarPerson = true;
					if (SelPersons.PersonSel == 0) {
						person.GetComponent<ExpressFacialZeca> ().SorrindoOn ();
					} else {
						person.GetComponent<ExpressFacialAmigos> ().SorrindoOn ();
					}
					person.GetComponent<Transform> ().transform.localScale = new Vector3 (person.GetComponent<Transform> ().transform.localScale.x * -1, person.GetComponent<Transform> ().transform.localScale.y, person.GetComponent<Transform> ().transform.localScale.z);
				}

						
						plataformCharacter2D.m_MaxSpeed = -4;
						plataformCharacter2D.m_Anim.SetFloat ("move", -1);
			
						CrossPlatformInputManager.SetAxisNegative("Horizontal");
						controlBalde.particule3.SetActive(true);
						plataformCharacter2D.m_Anim.SetFloat ("move", -1);
						
						
			
			} if (plataformCharacter2D.checkClickMove == false && trigLimt.transform.position.x < person.transform.position.x) {
				plataformCharacter2D.m_MaxSpeed = 4;
				CrossPlatformInputManager.SetAxisNegative("Horizontal");
				controlBalde.particule3.SetActive(true);
				

			}  if (plataformCharacter2D.checkClickMove == true) {
				if (virarPerson == false) {
					virarPerson = true;
					person.GetComponent<Transform> ().transform.localScale = new Vector3 (person.GetComponent<Transform> ().transform.localScale.x * -1, person.GetComponent<Transform> ().transform.localScale.y, person.GetComponent<Transform> ().transform.localScale.z);
				}

				CrossPlatformInputManager.SetAxisZero("Horizontal");
                if (tutor_D == 0) {
                   // PanelProfessora.GetComponent<tutorPanelArcoirirs>().profAnim.SetInteger("numText", 11);
                }
                //controlBalde.particule2.SetActive(false);

            }
			if (transform.position.x > posCamIni.x - 5.5f) {
				panelEnd.SetActive (true);
				//arcoIris.SetActive (false);
				ControlCenario2.GetComponent<Animator> ().SetInteger ("introCheck", 10);
				moveCam=false;
			//	cenario2.SetActive(false);
				if (panelEndShow == false) {
					panelEndShow = true;
					HideHUD ();
					Timing.RunCoroutine (jogarBaldeAnim ());
					//controlBalde.particule.SetActive (true);
					StopAllCoroutines ();
					CancelInvoke ();
					Timing.RunCoroutine (TimeShow ());
                    isFirstTime = true;
                    startCount();
					
				}
					
				//startCount();
			}



		}

        
    
	}



    public void ChangeToDidatica() {
        itensToCount = GenerateFakeValues();
        UpdateAllSprites();
        isCountUpdated = true;
        virarPerson = true;        
        person.GetComponent<Transform>().transform.localScale = new Vector3(person.GetComponent<Transform>().transform.localScale.x * -1, person.GetComponent<Transform>().transform.localScale.y, person.GetComponent<Transform>().transform.localScale.z);
        transform.position = new Vector3(posCamIni.x - 5.5f, transform.position.y, transform.position.z);
        CrossPlatformInputManager.SetAxisZero("Horizontal");
        ControlCenario2.GetComponent<Animator>().SetInteger("introCheck", 10);
        panelEndShow = true;
        panelEnd.SetActive(true);
        HideHUD();
        Timing.RunCoroutine(jogarBaldeAnim());
        //controlBalde.particule.SetActive (true);
        StopAllCoroutines();
        CancelInvoke();
        Timing.RunCoroutine(TimeShow());
        startCount();
    }


	public void Prof2Avancar(){
		checkfim2=true;
	}
	IEnumerator<float> jogarBaldeAnim(){
       
        if (jogarBalde == true) {
			jogarBalde = false;
			yield return Timing.WaitForSeconds (0.02f);

			timeM = Time.realtimeSinceStartup;
			jogarBalde = true;
			controlBalde.particule3.SetActive(false);
			controlBalde.particule.SetActive(false);
		} else {
			jogarBalde = true;
			controlBalde.particule3.SetActive(false);
			controlBalde.particule.SetActive(false);
		}
		Timing.RunCoroutine (showItems ());
	}
	 IEnumerator<float> TimeMoveCam(){
		yield return Timing.WaitForSeconds (GoMoveCam);
		introAnim = 1;
	}

	public void blockButtons(){
		foreach (var item in buttons) {
			item.interactable = false;
		}
	}

	public void enableButtons(){
		foreach (var item in buttons) {
			item.interactable = true;
		}
	}

	IEnumerator<float> TimeParabens(){
		blockButtons ();
		panelAcerto.SetActive(true);
		yield return Timing.WaitForSeconds (3f);
		if (panelAcerto.activeInHierarchy == true) {
            parabensAnim.enabled = true;
            parabensAnim.SetInteger ("numParabens",0);
			parabensAnim.SetInteger ("acertoErro",0);
		}
		yield return Timing.WaitForSeconds (0.5f);
		//panelAcerto.SetActive(true);
		yield return Timing.WaitForSeconds (0.5f);
        panelAcerto.SetActive(false);
        Timing.RunCoroutine(jogarBaldeAnim());
        // parabensAnim.enabled = false;
       // next();
        enableButtons ();

	}
	public void TimeBarraM(){
		Timing.RunCoroutine(TimeBarra());
	}

	IEnumerator<float> TimeBarra(){
		yield return Timing.WaitForSeconds (5f);
		barraEner.SetActive(true);

	}
	public IEnumerator<float> TimeShow(){	
		//startCount();
		yield return Timing.WaitForSeconds (1f);	
		if (checkSomArco == false) {
			checkSomArco = true;
			//soundManager.startSoundFX (soundFX [5]);
		}
	
		yield return Timing.WaitForSeconds (2f);
		if (stopTimeShow == false){
			stopTimeShow = true;


		}
			
	}
	public void somEstrela(){	
		//startCount();
		//yield return Timing.WaitForSeconds (0.1f);	
		soundManager.startSoundFX (soundFX [5]);

	}
	void IncreaseStars(int _amount){
        amountStars = _amount;
        for (int i = 0; i < _amount; i++) {
			estrelas.GetChild (i).GetComponent<Image> ().sprite = estrelaCheia;
		}
	}

	void clearStars(){
		amountStars = 0;
		for (int i = 0; i < 3; i++) {
			estrelas.GetChild (i).GetComponent<Image> ().sprite = estrelaVazia;
		}
	}


	void FixBucketPosition(){
		//corrigir
		//Vector3 BucketPosition = Camera.main.WorldToScreenPoint (bucketTransform.position);
		Vector3 BucketPosition = bucketTransform.position;
		bucketTextTransform.position = BucketPosition;

		//Vector3 pos = bucketTransform.position;
		//pos.z = 10f;
		//bucketTextTransform.position =  Camera.main.ScreenToWorldPoint(pos);

	}

	void TimerUpdate(){
        timer += Time.deltaTime;
	//	string minSec = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);
		//timerText.text = minSec;
	}



	void LevelVerifier(){
		if (amountPoints >= ((dificult + 1) * 100)) {

            if (dificult < 3) {
                dificult += 1;
                if (levels.Count > dificult) {

                    float temp = (float)currentLevel.energyIncreaseLevelUp;
                    Timing.RunCoroutine(showText());
                    //showText ();
                    energyBar.value += temp;
                    if (levels.Count > dificult) {
                        currentLevel = levels[dificult];
                    }
                    //int temp2 = dificult;
                    //				levelText.text = temp2.ToString();
                    if (amountStars < 3) {
                        amountStars++;
                    }
                    IncreaseStars(amountStars);
                }
            }			
		}
		//levelText.fontSize = levelTextNum;
	}

	public void StartGame(){
		Timing.RunCoroutine (TempoStartGame ());
		log.StartTimerLudica (true);
	}
	public IEnumerator<float> TempoStartGame(){
		aguaMang.SetActive (true);
//		aguaMang.GetComponent<DigitalRuby.RainMaker.RainScript2D> ().RainIntensity = 1f;
		yield return Timing.WaitForSeconds (2f);
		checkStartGame = 1;


	}
	public IEnumerator<float> InicaBt(){
		yield return Timing.WaitForSeconds (5f);
		if (tutorPanelArcoirirs2.tutor_0==0) {
			checkStartIstan = true;
			aprendaJogar.SetActive (true);

		}
		else if (checkPlay==false ){
		aprendaJogar.SetActive (false);	
		Timing.RunCoroutine (FecharAgua ());
		
		checkPlay=true;			
		Timing.RunCoroutine (PlayAgain ());
		}
		iniciarBT.SetActive(false);

		//ControlCenario2.GetComponent<Animator> ().SetInteger ("introCheck", 2);

	}
	public IEnumerator<float> FecharAgua(){
		yield return Timing.WaitForSeconds (2f);
		aguaMang.SetActive(false);
		//ControlCenario2.GetComponent<Animator> ().SetInteger ("introCheck", 2);

	}
	public IEnumerator<float> PlayAgain(){
		ControlCenario2.GetComponent<Animator> ().SetInteger ("introCheck", 2);
		moveCam=false;
		cenario1.SetActive(false);		
		yield return Timing.WaitForSeconds (1f);
		isPlaying = false;
		actualTargetCount = 0;
		waitToStart = true;
		isCountUpdated = false;
		userAnswers = -1;
		ClearList ();
		lunchAmount = 0;
		joysAmount = 0;
		mannersAmount = 0;
		knowledgeAmount = 0;
		studyAmount = 0;
		gradesAmount = 0;
		friendshipAmount = 0;
		bonusItemAmount = 0;
		amountPoints = 0;
		timer = 0f;
		waitToInstance = false;
		dificult = 0;
		currentLevel = levels [dificult];
		energyBar.value = energyBar.maxValue;
		clearStars ();
		CancelInvoke ();

		yield return Timing.WaitForSeconds (1f);
		//int temp = dificult + 1;
//		levelText.text = temp.ToString();

		InvokeRepeating ("DecreaseEnergyOverTime", 1f, currentLevel.energyDecreateTimeRate);
		waitToStart = false;
		isPlaying = true;


	
		GameConfig.LOG.personagem = GameConfig.GetCharName(PlayerPrefs.GetInt("characterSelected", 0));
		//ControlCenario2.GetComponent<Animator> ().SetInteger ("introCheck", 2);
		aguaMang.SetActive(false);

	}

	void ClearList(){
		/*firstBonus.Clear ();
		secondBonus.Clear ();
		thirdBonus.Clear ();
		fourthBonus.Clear ();
		bonusItems.Clear ();*/
		lunch.Clear ();
		joys.Clear ();
		manners.Clear ();
		knowledge.Clear ();
		grades.Clear ();
		friendship.Clear ();
		study.Clear ();
	}

	void EndGameCall(){
		checkFim = true;
		StopAllCoroutines ();
		CancelInvoke ();
		typeItem = 0;
		//panelEndGame.SetActive (true);
		waitToStart = true;
//		countThings(amountTypeItem());
	}

	IEnumerator<float> InstanceControll(){
		waitToInstance = true;

		int howMany = Random.Range (1, currentLevel.amountOfInstance+1);

		IList<GameObject> positionsSpawn = spawnPositions;	
		ShuffleListExtensions.Suffle<GameObject> (positionsSpawn);

		//int randomSpawn = Random.Range (0, spawnPositions.Count);
		//GameObject spawnPosition = spawnPositions [randomSpawn];

		int[] indexInst = new int[9];

		for (int i = 0; i < 9; i++) {
			indexInst [i] = i;
		}

		int resultIndex = 0;

		/*GameObject thisPrefabs = null;
		if (resultIndex < 3) {
			thisPrefabs = currentLevel.Prefabs [resultIndex];
		} else if (resultIndex == 3) {
			int indexRandom = Random.Range (0, currentLevel.goodFruitsPrefabs.Length);
			thisPrefabs = currentLevel.goodFruitsPrefabs [indexRandom];
		}else if (resultIndex == 4) {
			int indexRandom = Random.Range (0, currentLevel.badFruitsPrefabs.Length);
			thisPrefabs = currentLevel.badFruitsPrefabs [indexRandom];
		}
		//ExtRandom<GameObject>.WeightedChoice(indexInst, currentLevel.nWeights);*/

		speedInstance = Random.Range (currentLevel.minSpeedInstance, currentLevel.maxSpeedInstance);

		GameObject CoinInstances = null;
		//int randomSpeed = Random.Range (0,currentLevel.coinSpeedVariation.Length);

		for (int i = 0; i < howMany; i++) {

			resultIndex = ExtRandom<int>.WeightedChoice(indexInst, currentLevel.nWeights);

			GameObject thisPrefabs = null;

			//TODO remove bad foods.

			/*if (resultIndex < 3) {
				thisPrefabs = currentLevel.Prefabs [resultIndex];
			} else if (resultIndex == 3) {
				int indexRandom = Random.Range (0, currentLevel.goodFruitsPrefabs.Length);
				thisPrefabs = currentLevel.goodFruitsPrefabs [indexRandom];
			}else if (resultIndex == 4) {
				int indexRandom = Random.Range (0, currentLevel.badFruitsPrefabs.Length);
				thisPrefabs = currentLevel.badFruitsPrefabs [indexRandom];
			}*/

			thisPrefabs = currentLevel.Prefabs [resultIndex];

			CoinInstances = Instantiate (thisPrefabs, positionsSpawn[i].transform.position, Quaternion.identity, instanceParent) as GameObject;
			//CoinInstances.isStatic = true;

			//TODO change and add lists.

			/*if (resultIndex == 0 || resultIndex == 1) {
				firstBonus.Add (CoinInstances);
			} else if (resultIndex == 2){
				secondBonus.Add (CoinInstances);
			}else if (resultIndex == 3 || resultIndex == 4 || resultIndex == 5){
				thirdBonus.Add (CoinInstances);
			}else if (resultIndex == 6){
				fourthBonus.Add (CoinInstances);
			}else if (resultIndex == 7){
				bonusItems.Add (CoinInstances);
			}*/



			switch (resultIndex) {
			case 0:
				lunch.Add (CoinInstances);
				break;
			case 1:
				joys.Add (CoinInstances);
				break;
			case 2:
				manners.Add (CoinInstances);
				break;
			case 3:
				knowledge.Add (CoinInstances);
				break;
			case 4:
				study.Add (CoinInstances);
				break;
			case 5:
				grades.Add (CoinInstances);
				break;
			case 6:
				friendship.Add (CoinInstances);
				break;
			case 7:
				bonusItems.Add (CoinInstances);
				break;
            case 8:
                negativeItem.Add(CoinInstances);
                break;
            default:
				break;
			}
		}

		//GameObject CoinInstance = Instantiate (thisPrefabs, spawnPosition.transform.position, Quaternion.identity, instanceParent) as GameObject;
		//CoinInstance.GetComponent<Rigidbody2D> ().gravityScale = currentLevel.coinSpeedVariation;

		yield return Timing.WaitForSeconds(speedInstance);
		waitToInstance = false;
	}

	void DecreaseEnergyOverTime(){
		float temp = (float)currentLevel.energyDecreaseRate;
		energyBar.value -= temp;
	}


	public void backToMenu(){
		Time.timeScale = 1;
		StopAllCoroutines();
		loadManager.LoadAsync ("selectionMinigames");
	}

	/*public void badFruitsDecrease(){
		float temp = (float)currentLevel.badFruitsDecrease;
		energyBar.value -= temp;
	}

	public void goodFruitsIncrease(){
		float temp = (float)currentLevel.goodFruitsIncrease;
		energyBar.value += temp;
	}*/


	public IEnumerator<float> showText(){
		
		panelAcerto.SetActive(true);
		yield return Timing.WaitForSeconds(0.1f);
		soundManager.startSoundFX (soundFX [0]);
		isTextMoving = true;
		if (panelAcerto.activeInHierarchy == true) {
            parabensAnim.enabled = true;
            parabensAnim.SetInteger ("numParabens",1);
		}
		int temp = dificult + 1;
		Text thisText = textTransform.GetComponent<Text> ();
		thisText.text = textToShow.Replace ("<level>", temp.ToString ());
		int nParabens = Random.Range(0,texParabens.Length);
		parabensText.text = texParabens[nParabens];

		/*if (thisText.text.Contains ("<level>") == true) {

		}*/

		yield return Timing.WaitForSeconds(1f);
		isTextMoving = false;
		if (panelAcerto.activeInHierarchy == true) {
            parabensAnim.enabled = true;
            parabensAnim.SetInteger ("numParabens",0);
		}
		yield return Timing.WaitForSeconds(0.5f);
		panelAcerto.SetActive(false);
		//confetesG.SetActive (false);

	}



	public IEnumerator<float> showTextBucket(Color colors){
		float times = 0.0f;
		bucketTextComponent.text = bucketText;
		bucketTextComponent.color = colors;
		Transform bucketTransform = bucketTextComponent.gameObject.transform;
		/*if (thisText.text.Contains ("<level>") == true) {

		}*/
		while (times < bucketColorLerpDuration)
		{
            times += Time.deltaTime;
			float s = times / bucketColorLerpDuration;
			//coinInMove.transform.position = Vector3.Lerp(position, newPosition, (ElapseTime / movementTime));
			//textTransform.position = Vector3.Lerp(start.position, end.position, bucketTextFadeCurve.Evaluate(s));
			//bucketTextComponent.fontSize = Mathf.Lerp(12,12,12);
			float size = Mathf.Lerp(0.5f,1f,bucketTextFadeCurve.Evaluate (s));
			bucketTransform.localScale = new Vector3 (size, size, size);
			bucketTextComponent.color = Color.Lerp (startColor, colors, bucketTextFadeCurve.Evaluate (s));
			//ElapseTime += Time.deltaTime;
			yield return 0f;
		}
	}

	public IEnumerator<float> richies(bool isSpecial){
		float times = 0.0f;
		textOfRichies.text = textEffect;
		Transform richies = textOfRichies.gameObject.transform;
		Color coloStart = new Color (1f, 1f, 1f, 0f);
		while (times < durationEffect)
		{
            times += Time.deltaTime;
			float s = times / durationEffect;
			float size = Mathf.Lerp(0.5f,1f,effectCurve.Evaluate (s));
			if (isSpecial) {
				textOfRichies.color = Color.Lerp (coloStart, Color.cyan, bucketTextFadeCurve.Evaluate (s));
			} else {
				textOfRichies.color = Color.Lerp (coloStart, Color.white, bucketTextFadeCurve.Evaluate (s));
			}
			richies.localScale = new Vector3 (size, size, size);
			yield return 0f;
		}
	}

	public IEnumerator<float> CatchCoin(Vector3 colisionPosition,int type){

		yield return Timing.WaitForSeconds (0.01f);


		switch (type) {
			case 0:
				//amountPoints += 1;
				lunchAmount++;
				bucketText = "+1";
				textEffect = "Lanches";
				soundManager.startSoundFX(soundFX[2], .3f);
				Timing.KillCoroutines("BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (false));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(1));
			break;
		case 1:
			//amountPoints += 1;
			joysAmount++;
			bucketText = "+1";
			textEffect = "Brincadeiras";
			soundManager.startSoundFX (soundFX [2], .3f);
				Timing.KillCoroutines("BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (false));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(1));
			break;
		case 2:
			//amountPoints += 3;
			mannersAmount++;
			bucketText = "+3";
			textEffect = "Boas Maneiras";
			soundManager.startSoundFX (soundFX [2], .3f);
				Timing.KillCoroutines("BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");

				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (false));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(3));
			break;
		case 3:			
			//amountPoints += 5;
			knowledgeAmount++;
			bucketText = "+5";
			textEffect = "Conhecimento";
			soundManager.startSoundFX (soundFX [2], .3f);
				Timing.KillCoroutines("BucketText");
				Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
			Timing.RunCoroutine (richies (false));
			Timing.RunCoroutine (StartTotalPoints(5));
			break;
		case 4:
			//amountPoints += 5;
			studyAmount++;
			bucketText = "+5";
			textEffect = "Atividades Fisícas";
			soundManager.startSoundFX (soundFX [2], .3f);
				Timing.KillCoroutines("BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (false));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(5));
			break;
		case 5:
			//amountPoints += 5;
			gradesAmount++;
			bucketText = "+5";
			textEffect = "Boas Notas";
			soundManager.startSoundFX (soundFX [2], .3f);
				Timing.KillCoroutines("BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (false));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(5));
			break;
		case 6:
			//amountPoints += 10;
			friendshipAmount++;
			bucketText = "+10";
			textEffect = "Amizade";
			soundManager.startSoundFX (soundFX [6], .3f);
				Timing.KillCoroutines("BucketText");
			Timing.RunCoroutine (showTextBucket (Color.cyan), "BucketText");
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (richies (true));
				yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (StartTotalPoints(10));
			break;
		case 7:
			soundManager.startSoundFX (soundFX [1]);
			energyBar.value += currentLevel.energyIncrease;
			bonusItemAmount++;
			//bucketText = "Lanches";
			Timing.KillCoroutines("BucketText");
			yield return Timing.WaitForSeconds(0.1f);
			Timing.RunCoroutine (showTextBucket (Color.white), "BucketText");
            pointsText.text = amountPoints.ToString();
            break;
        case 8:
            if (amountPoints >= 10) {
                amountPoints -= pontosNegativo;
            }
            soundManager.startSoundFX(soundFX[3]);
            bucketText = "-" + pontosNegativo.ToString();
            Timing.KillCoroutines("BucketText");
            yield return Timing.WaitForSeconds(0.1f);
            Timing.RunCoroutine(showTextBucket(Color.red), "BucketText");
            pointsText.text = amountPoints.ToString();
            break;
            default:
			break;
		}
		yield return Timing.WaitForSeconds(0.1f);
		if (SelPersons.PersonSel == 0) {
			
			person.GetComponent<ExpressFacialZeca> ().SorrindoOn ();
		} else {
			
			person.GetComponent<ExpressFacialAmigos> ().SorrindoOn ();
		}

		

	}

	public void startCount(){
        buttons[4].interactable = false;
        config.Rank(log.idMinigame, amountPoints, amountStars);
        waitToStart = true;
		isPlaying = false;
		//itensToCount = returnTopThree();
		currentIndexedItem = 0;
		timeM = Time.realtimeSinceStartup;
        
        Timing.RunCoroutine(NextTargetCount(currentIndexedItem,oldIndexItem));
        
        randomAlternatives ();
		//Timing.RunCoroutine (showItems ());

		log.pontosLudica = amountPoints;
		log.StartTimerLudica (false);
		log.StartTimerDidatica (true);
		log.faseLudica = dificult+1;
        
    }

	public void confirmBTCount(){
        //soundManager.startSoundFX (soundFX [4], 0.52f);
        buttons[4].interactable = false;
        StopAllCoroutines ();
        timeM = Time.realtimeSinceStartup;
		if (isChanging == false) {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (userAnswers > -1 && userAnswers != null) {
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                panelAcerto.SetActive (true);
				if (userAnswers == itensToCount [currentIndexedItem].amount) {
					if (SelPersons.PersonSel == 0) {
						person.GetComponent<ExpressFacialZeca> ().SorrindoOn ();
					} else {
						person.GetComponent<ExpressFacialAmigos> ().SorrindoOn ();
					}

					soundManager.startSoundFX (soundFX [4]);
					sidePanels [currentIndexedItem].GetChild (2).GetChild (0).GetComponent<Text> ().color = Color.green;
					sidePanels [currentIndexedItem].GetComponent<Image> ().color = rightColor;
					Timing.RunCoroutine (StartTotalPoints (50));
					scorePegagogica += 50;
					parabensText.text = "Acertou! \b +50 Pontos";
					log.AddPontosPedagogica (50);
					log.SaveEstatistica (110, true);
					if (panelAcerto.activeInHierarchy == true) {
                        parabensAnim.enabled = true;
                        parabensAnim.SetInteger ("acertoErro",1);
                        //Debug.Log("1Acerto");
					}
					Timing.RunCoroutine (TimeParabens ());
					//PanelProfessora.SetActive (true);		
					if (PanelProfessora.activeInHierarchy == true) {
					//	PanelProfessora.SetActive (true);		
						PanelProfessora.GetComponent<Animator>().enabled = true;
						PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
					}
					next ();
				} else {
					soundManager.startSoundFX (soundFX [3]);
					if (SelPersons.PersonSel == 0) {
						person.GetComponent<ExpressFacialZeca> ().TristeOn ();
					} else {
						person.GetComponent<ExpressFacialAmigos> ().TristeOn ();
					}

					sidePanels [currentIndexedItem].GetComponent<Image> ().color = wrongColor;
					sidePanels [currentIndexedItem].GetChild (2).GetChild (0).GetComponent<Text> ().color = Color.red;
					//sidePanels [currentIndexedItem].GetChild (2).GetChild (1).GetComponent<Image> ().color = Color.white;
					next ();
					//parabensText.text = "Errou! \n O certo era " +  itensToCount [currentIndexedItem].amount.ToString();
					parabensText.text = "Errou! \n O certo era " +  itensToCount [oldIndexItem].amount;
					log.SaveEstatistica (110, false);
					//Handheld.Vibrate();
					if (panelAcerto.activeInHierarchy == true){
                        parabensAnim.enabled = true;
                        parabensAnim.SetInteger ("acertoErro",2);
                        //Debug.Log("2erro");
					}
					//parabensAnim.SetInteger ("acertoErro",2);
					Timing.RunCoroutine (TimeParabens ());
					//PanelProfessora.SetActive (true);		
					if(PanelProfessora.activeInHierarchy == true)
						PanelProfessora.SetActive (true);		
						//PanelProfessora.GetComponent<Animator>().enabled = true;
					  //  PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
				}
			} else {
				//TODO personagem falando para usuario digitar.
				soundManager.startSoundFX (soundFX [3]);
				//Handheld.Vibrate();
		
			}

		}
	}

	public void ColorSelectButton(int i){
		for (int j = 0; j < texts.Count(); j++) {
			if (j == i) {
				texts [j].gameObject.transform.parent.GetComponent<Image> ().sprite = buttonSelect;
			} else {
				texts [j].gameObject.transform.parent.GetComponent<Image> ().sprite = buttonDefault;
			}
		}
	}

	public void ColorClearButtons(){
		for (int i = 0; i < texts.Count(); i++) {
			texts [i].gameObject.transform.parent.GetComponent<Image> ().sprite = buttonDefault;
		}
	}


	public void btanwser01(){
		userAnswers = alternatives [0];
		PanelProfessora.SetActive (false);		
		if (PanelProfessora.activeInHierarchy == true) {
			PanelProfessora.GetComponent<Animator>().enabled = true;
			PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
		}
		//butto.SetInteger ("numText",0);
		timeM = Time.realtimeSinceStartup;
		ColorSelectButton (0);
        buttons[4].interactable = true;

    }

	public void btanwser02(){
		userAnswers = alternatives [1];
		PanelProfessora.SetActive (false);		
		if (PanelProfessora.activeInHierarchy == true) {
			PanelProfessora.GetComponent<Animator>().enabled = true;
			PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
		}
		timeM = Time.realtimeSinceStartup;
		ColorSelectButton (1);
        buttons[4].interactable = true;
    }

	public void btanwser03(){
		userAnswers = alternatives [2];
		PanelProfessora.SetActive (false);		
		if (PanelProfessora.activeInHierarchy == true) {
			PanelProfessora.GetComponent<Animator>().enabled = true;
			PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
		}
		timeM = Time.realtimeSinceStartup;
		ColorSelectButton (2);
        buttons[4].interactable = true;
    }

	public void btanwser04(){
		userAnswers = alternatives [3];
		PanelProfessora.SetActive (false);		
		if (PanelProfessora.activeInHierarchy == true) {
			PanelProfessora.GetComponent<Animator>().enabled = true;
			PanelProfessora.GetComponent<Animator> ().SetInteger ("numText",0);
		}
		timeM = Time.realtimeSinceStartup;
		ColorSelectButton (3);
        buttons[4].interactable = true;
    }

	public void activeButton(){

	}

	public void randomAlternatives(){
		int amount = itensToCount [currentIndexedItem].amount;

		alternatives.Clear ();

		List<int> alternativesTemp = new List<int>();
		alternativesTemp.Add(amount);

		int tempMax = 50;
		if(amount + 10 > 50){
			int increase = 50 - amount;
			tempMax = amount + increase;
		} else {
			tempMax = amount + 10;
		}

		int tempMin = 0;
		if(amount-10 <= 0){
			tempMin = 1;
		} else {
			tempMin = amount - 10;
		}
	
		for (int i = 0; i < 3; i++) {
			int temp = 0;

			temp = Random.Range(tempMin,tempMax);
			while (alternativesTemp.Contains (temp)) {
				temp = Random.Range(tempMin,tempMax);
			}
            		
			//bool isOnList = alternativesTemp.Contains (temp);
		
			alternativesTemp.Add (temp);
	
		}

		alternatives = alternativesTemp.OrderBy (x => x).ToList ();

		for (int i = 0; i < 4; i++) {
			texts [i].text = alternatives [i].ToString ();
		}

	}

	public void next(){

		sidePanels [currentIndexedItem].GetChild (2).GetChild (0).GetComponent<Text> ().text = userAnswers.ToString();

		if (currentIndexedItem < 2 && isChanging == false) {
			itensToCount = ReturnTopThree ();
			currentIndexedItem++;
		
			randomAlternatives ();
            buttons[4].interactable = false;
            Timing.RunCoroutine (NextTargetCount (currentIndexedItem, oldIndexItem));
			userAnswers = -1;

		} else if(isChanging == false) {
			this.person.GetComponent<Animator> ().enabled = false;
			if(checkDesafio==false){
			//	checkDesafio=true;
			//	panelDesafio.SetActive(checkDesafio);
				Timing.RunCoroutine (TimePanels ());

			}
			//panelDesafio.GetComponent<Animator>().SetInteger("panelDesafioNumber",1);
			managerNext.enabled = true;
			managerNext.StartGame();
			//TODO mostrar tela de pontuação aqui.
			
		}

		ColorClearButtons ();
	}


	public IEnumerator<float> TimePanels(){
		yield return Timing.WaitForSeconds (1.25f);
		panelAcerto.SetActive (false);
		yield return Timing.WaitForSeconds (.25f);	
		foreach (var item in panels) {
			item.SetActive (false);
		}
		yield return Timing.WaitForSeconds (.5f);
		sairGameObj.SetActive (false);

	}

	public void validateData(){
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
        if (userAnswers != -1 || userAnswers != null) {
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (userAnswers > 50) {
				userAnswers = 50;
			}
		}
	}

	public void hideCounts(){
		countEnd.SetActive (false);
	}

	public void CountThings(int amount){
        buttons[4].interactable = false;
        answerAmount = amount;

		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < UIParents.GetChild (i).transform.childCount; j++) {
				Destroy (UIParents.GetChild (i).GetChild(j).gameObject);
			}
		}

		int actualAmount = 0;

		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 10; j++) {
				if (actualAmount == amount || actualAmount > amount) {
					break;
				} else {
					actualAmount++;
					UIParents.GetChild (i).transform.GetComponent<GridLayoutGroup> ().enabled = false;
					GameObject item = Instantiate (UIPrefabs [typeItem], new Vector3 (0f, 0f, 0f), Quaternion.identity, UIParents.GetChild (i).transform) as GameObject;
					UIParents.GetChild (i).transform.GetComponent<GridLayoutGroup> ().enabled = true;
				}
			}

			if (actualAmount >= amount) {
				break;
			}

		}
	}

	public IEnumerator<float> NextTargetCount(int actual, int old){
		isChanging = true;
		float times = 0.0f;

		Vector2 startPos = sidePanels [actual].transform.position;
		Vector2 startPos2 = sidePanels [old].transform.position;
		Vector2 targetPos = new Vector2 (startPos2.x, startPos.y);
		Vector2 targetPos2 = new Vector2 (startPos.x, startPos2.y);

		Transform target = sidePanels [actual];
		Transform target2 = sidePanels [old];

		Outline targetItemOutline = null;
		Image targetImage = null;
		Text targetText = null;
		Outline targetCircleOutline = null;

		Outline targetItemOutline2 = null;
		Image targetImage2 = null;
		Text targetText2 = null;
		Outline targetCircleOutline2 = null;

		returnTargets (ref targetItemOutline,ref targetImage,ref targetText,ref targetCircleOutline, actual);
		returnTargets (ref targetItemOutline2,ref targetImage2,ref targetText2,ref targetCircleOutline2, old);

		Color startColors = targetText.color;
		Color endColor = targetText2.color;
		Color imageStartColor = targetImage.color;
		Color imageeEndColor = targetImage2.color;
		Color outlineColorStart = targetItemOutline.effectColor;
		Color outlineColorEnd = targetItemOutline2.effectColor;
		Color outlineCircleStart = targetCircleOutline.effectColor;
		Color outlineCircleEnd = targetCircleOutline2.effectColor;

		targetText.text = returnName(itensToCount [actual].type);

		while (times < changeItemTDuration)
		{
            times += Time.deltaTime;
			float s = times / changeItemTDuration;

			target.position = Vector2.Lerp (startPos, targetPos, changeItemTCurve.Evaluate (s));
			target2.position = Vector2.Lerp (startPos2, targetPos2, changeItemTCurve.Evaluate (s));

			yield return 0f;
		}

        if (isFirstTime == false) {
            isFirstTime = true;
            sidePanels[0].transform.position = new Vector3(sidePanels[2].transform.position.x + 1.3f, sidePanels[0].transform.position.y, sidePanels[0].transform.position.z);
        }


        oldIndexItem = currentIndexedItem;

		isChanging = false;
	}


	public void returnTargets(ref Outline itemOutline, ref Image panelImage, ref Text text, ref Outline circleOutline, int i){

		itemOutline = sidePanels [i].GetChild(0).GetComponent<Outline> ();
		panelImage = sidePanels [i].GetComponent<Image> ();
		text = sidePanels [i].GetChild (1).GetComponent<Text> ();
		circleOutline = sidePanels [i].GetChild(2).GetComponent<Outline> ();

	}

	public void UpdateAllSprites(){

		for (int i = 0; i < 3; i++) {
			sidePanels [i].GetChild(0).GetChild(0).GetComponent<Image> ().sprite = returnSprite(itensToCount[i].type);
		}
	}

	public enum rainbowItem
	{
		lunch = 0,
		joys,
		manners,
		knowledge,
		study,
		grades,
		friendship,
        negativeItem,
		none
	}

	public int returnAmount(rainbowItem item){
		switch (item) {
		case rainbowItem.lunch:
			return lunchAmount;
		case rainbowItem.joys:
			return joysAmount;
		case rainbowItem.manners:
			return mannersAmount;
		case rainbowItem.knowledge:
			return knowledgeAmount;
		case rainbowItem.study:
			return studyAmount;
		case rainbowItem.grades:
			return gradesAmount;
		default:
			return 0;
		}
	}

	public Sprite returnSprite(rainbowItem item){
		switch (item) {
		case rainbowItem.lunch:
			return iconsImage[0];
		case rainbowItem.joys:
			return iconsImage[1];
		case rainbowItem.manners:
			return iconsImage[2];
		case rainbowItem.knowledge:
			return iconsImage[3];
		case rainbowItem.study:
			return iconsImage[4];
		case rainbowItem.grades:
			return iconsImage[5];
		case rainbowItem.friendship:
			return iconsImage[6];
		default:
			return iconsImage[0];
		}
	}

	public string returnName(rainbowItem item){
		switch (item) {
		case rainbowItem.lunch:
			return "Lanches";
		case rainbowItem.joys:
			return "Brincadeiras";
		case rainbowItem.manners:
			return "Boas Maneiras";
		case rainbowItem.knowledge:
			return "Conhecimento";
		case rainbowItem.study:
			return "Atividades Fisícas";
		case rainbowItem.grades:
			return "Boas Notas";
		case rainbowItem.friendship:
			return "Amizade";
		default:
			return "none";
		}
	}

	public int returnID(rainbowItem item){
		switch (item) {
		case rainbowItem.lunch:
			return 0;
		case rainbowItem.joys:
			return 1;
		case rainbowItem.manners:
			return 2;
		case rainbowItem.knowledge:
			return 3;
		case rainbowItem.study:
			return 4;
		case rainbowItem.grades:
			return 5;
		case rainbowItem.friendship:
			return 6;
		default:
			return 0;
		}
	}

	public int returnAmountsItem(int id){
		switch (id) {
		case 0:
			return lunchAmount;
		case 1:
			return joysAmount;
		case 2:
			return mannersAmount;
		case 3:
			return knowledgeAmount;
		case 4:
			return studyAmount;
		case 5:
			return gradesAmount;
		case 6:
			return friendshipAmount;
		default:
			return 0;
		}
	}

	public rainbowItem returnItemType(int id){
		switch (id) {
		case 0:
			return rainbowItem.lunch;
		case 1:
			return rainbowItem.joys;
		case 2:
			return rainbowItem.manners;
		case 3:
			return rainbowItem.knowledge;
		case 4:
			return rainbowItem.study;
		case 5:
			return rainbowItem.grades;
		case 6:
			return rainbowItem.friendship;
		default:
			return rainbowItem.none;
		}
	}

	public ItemIndexed[] ReturnTopThree(){

		List<ItemIndexed> itensAmount = new List<ItemIndexed> ();
		ItemIndexed[] arrayResult = new ItemIndexed[7];
		for (int i = 0; i < 7; i++) {
			//int temp = returnAmountsItem (i);
			itensAmount.Add (new ItemIndexed(returnAmountsItem(i),i,returnItemType(i)));
		}
		var listOrder = itensAmount.OrderByDescending (x => x.amount).ToList ();
		arrayResult = listOrder.ToArray ();
		for (int i = 0; i < 7; i++) {
			
		}
		return arrayResult;
	}

    public ItemIndexed[] GenerateFakeValues() {

        List<ItemIndexed> itensAmount = new List<ItemIndexed>();
        ItemIndexed[] arrayResult = new ItemIndexed[7];
        for (int i = 0; i < 7; i++) {
            int temp = returnAmountsItem(i);
            itensAmount.Add(new ItemIndexed(Random.Range(1,40), i, returnItemType(i)));
        }
        var listOrder = itensAmount.OrderByDescending(x => x.amount).ToList();
        arrayResult = listOrder.ToArray();
        for (int i = 0; i < 7; i++) {

        }
        return arrayResult;
    }


    public IEnumerator<float> showItems(){

        buttons[4].interactable = false;


        List<Image> imageItens = new List<Image> ();
	
		for (int i = 0; i < panelItens.childCount; i++) {
			imageItens.Add (panelItens.GetChild (i).GetChild(0).GetComponentInChildren<Image> ());
		}

		foreach (var item in imageItens) {
			item.color = Color.clear;
		}

		yield return Timing.WaitForSeconds (1f);
		Timing.RunCoroutine (fadeOrder (50,imageItens));
        
        if (PlayerPrefs.HasKey("tutorIni2") == false) {
          //  PanelProfessora.SetActive(true);
        //    PlayerPrefs.SetInt("tutorIni2", 1);
        //    tutorPanelArcoirirs2.BalaoScaleM2();
        }
        else if (numbCont < 2) {
            numbCont = numbCont + 1;
            tutorPanelArcoirirs2.soundManager.startVoiceFXReturn(tutorPanelArcoirirs2.audiosTutorial[7]);

        }
      //  Debug.Log("passou");


    }

	IEnumerator<float> fadeOrder(int amount,List<Image> imageItens){
		for (int i = 0; i < itensToCount [currentIndexedItem].amount; i++) {
			if (imageItens[i] != null)
			Timing.RunCoroutine (fadeInItem (imageItens[i], itensToCount [currentIndexedItem]));
			yield return Timing.WaitForSeconds (0.05f);
		}
		if (checkprofessoraAjuda == false) {
			checkprofessoraAjuda = true;
			professoraAjuda = 0;
		}
	}



	//TODO contagem, limpar tela, mudar icone selecionado.
	IEnumerator<float> fadeInItem(Image image, ItemIndexed item){

		float times = 0.0f;
		/*if (thisText.text.Contains ("<level>") == true) {
		}*/

		image.sprite = returnSprite (item.type);

		while (times < fadeInDuration)
		{
            times += Time.deltaTime;
			float s = times / fadeInDuration;
			//coinInMove.transform.position = Vector3.Lerp(position, newPosition, (ElapseTime / movementTime));
			//textTransform.position = Vector3.Lerp(start.position, end.position, bucketTextFadeCurve.Evaluate(s));
			image.color = Color.Lerp (Color.clear, Color.white, fadeInCurveItem.Evaluate (s));
			//ElapseTime += Time.deltaTime;
			yield return 0f;
		}
	}

	public void PauseGame(){
		isPlaying = false;
		Time.timeScale = 0f;
		OpenConfig ();
		HideHUD ();
		pauseMenuPanel.SetActive (true);
	}

	public void UnpauseGame(){
		isPlaying = true;
		ShowHUD ();
		Time.timeScale = 1f;
		pauseMenuPanel.SetActive (false);
		GameConfig.SavePrefs ();
	}

	public void UnpauseAndBackToMenu(){
		Time.timeScale = 1f;
		StopAllCoroutines ();
		CancelInvoke ();
		GameConfig.SavePrefs ();
		GameConfig.SaveLOG();
		loadManager.LoadAsync ("selectionMinigames");
		pauseMenuPanel.SetActive (false);
	}

	public void backgroundMusicValidate(){

		if (Toggles [0].isOn) {
			GameConfig.isAudioOn = false;
		} else {
			GameConfig.isAudioOn = true;
		}

	}


	public void soundFXValidate(){

		if (Toggles [1].isOn) {
			GameConfig.isAudioFXOn = false;
			soundManager.stopCurrentFxs ();
		} else {
			GameConfig.isAudioFXOn = true;
			soundManager.playCurrentsFxs ();
		}

	}

	public void VoicesValidate(){

		if (Toggles [2].isOn) {
			GameConfig.isAudioVoiceOn = false;
		} else {
			GameConfig.isAudioVoiceOn = true;
		}

	}

	public void OpenConfig(){

		if (GameConfig.isAudioOn) {
			Toggles [0].isOn = false;
		} else {
			Toggles [0].isOn = true;
		}

		if (GameConfig.isAudioFXOn) {
			Toggles [1].isOn = false;
		} else {
			Toggles [1].isOn = true;
		}

		if (GameConfig.isAudioVoiceOn) {
			Toggles [2].isOn = false;
		} else {
			Toggles [2].isOn = true;
		}

	}

	
	public void HideHUD(){
		foreach (var item in HUD) {
			item.SetActive (false);
		}
	}

	public void ShowHUD(){
		foreach (var item in HUD) {
			item.SetActive (true);
		}
	}

	public void EndAllCoroutines(){
        Timing.KillCoroutines();
		for (int i = 0; i < controlBaldes.Length; i++) {
			controlBaldes[i].EndAllCoroutines();
		}
	}

    public void StopRunning() {
        CrossPlatformInputManager.SetAxisZero("Horizontal");
    }

    

}


[System.Serializable]
public class ItemIndexed{	
	public int amount;
	public int id;
	public RainbowController.rainbowItem type;


	public ItemIndexed ()
	{
		
	}

	public ItemIndexed (int _amount,int _id, RainbowController.rainbowItem _type)
	{
		amount = _amount;
		id = _id;
		type = _type;
	}

}
