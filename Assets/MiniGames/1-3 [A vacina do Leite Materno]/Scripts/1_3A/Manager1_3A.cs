using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using MEC;
using DG.Tweening;
using UnityEngine.Rendering;

public class Manager1_3A : OverridableMonoBehaviour {

    #region variables


#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configurações")]
#endif
    public LogSystem _log;
    public List<PresetPlataforms1_3> plataforms = new List<PresetPlataforms1_3>();
    public BoxCollider2D camColl;
    public Transform plataformParent;
    public Transform seringaT;
    public bool isGameRunning = false;
    public int playerLifes;
    public GameObject tutorVacine;
    public bool tutorVacineGame;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Referencia do Player")]
#endif
    public SelPersons SelPersons2;
    public UnityEvent SelPersonsEv;
    public GameObject personSel;
    public Transform playerTransform;
    public UnityStandardAssets._2D.Platformer2DUserControl plataformControlerUser;
    public UnityStandardAssets._2D.PlatformerCharacter2D plataformController2d;
    public Collider2D playerCollider;
    public BoxCollider2D BoxCollider2DPlayer;
    public Animator animPerson;
    //public AnimationState dsfsd;
    public int jumpCount;
    public int maxJump;
    public float defaultVelocity;
    public bool isSpeedIncreased = false;
    public bool isFlying = false;
    public float defaultGravity;
    public float gravityScaleFly;
    public bool hasFlyEffect = false;
    public Animator animEnfermeira;

    public Rigidbody2D playerRigidbody;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Camera Config")]
#endif
    public Transform mainCameraTransform;
    public Vector3 offsetFollow;
    public float transitionDuration;
    public Vector3 defaultOffset;
    public AnimationCurve transitionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public bool playerisOnDefault = false;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Barra de Progresso")]
#endif
    public Slider playerSlider;
    public Slider nurseSlider;
    public float offsetIncreaseOnWalk;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Contador")]
#endif
    public Text textContador;
    bool checkstartRunning;


#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Referencia Enfermeira")]
#endif
    public Rigidbody2D nurseRigidbody;
    public NurseFollow1_3A nurseManager;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Buttons")]
#endif
    public Button jumpButton;
    public Button slideButton;
    private bool hasSlideButtom;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Pontos")]
#endif
    public Text textScoreComp;
    public int ScoreAmount;
    public float increaseScoreDuration = 1.0f;
    public AnimationCurve increaseScoreCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    int numEnfer;

    public Sprite[] Spritefund;
    public Sprite[] SpriteGround1;
    public Sprite[] SpriteGround2;
    public Sprite[] SpriteBlock;
    public int numbCont;
    public int numCaindo;
    public Animator[] imgPersonAnimG;
    public Animator imgPersonAnimP;

    public GameObject particulePe;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("SoundManager")]
#endif
    public SoundManager soundManager;
    public AudioClip coinCatchSound;
    public AudioClip itemEffectSound;
    public AudioClip hitSoundFX;
    public AudioClip nurseSoundHit;
    public AudioClip speedEffectSound;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("ETCS")]
#endif

    public int[] numberV;
    public int number;
    public float smoothTime = 1f;
    public float smoothTime2;
    private float startTime;
    private float journeyLength;
    public Vector3 velocity;
    public bool cameraUP = false;
    public Text lifeTextAmount;
    public Manager1_3B managerNext;
    public int contadorLevel;
    public Sprite[] lifePerson;
    public Image lifePersonIm;
    public bool checkPiscar;
    public bool batendoCaixa;
    public bool deslizandoLeite;
    public Button BtEnter;
    public GameObject enfermeiraG;
    public GameObject jucaZeca;
    public GameObject jucaAmigos;
    public GameObject enfermeiraZeca;
    public GameObject enfermeiraAmigos;
    public RectTransform topLevel;
    public bool nurseHit;
    public GameObject[] capaVacine;
    [HideInInspector]
    public Queue<ParticleSystem> ParticulaMoedaPool = new Queue<ParticleSystem>();
    [HideInInspector]
    public Queue<ParticleSystem> ParticulaMoedaPoolOnScene = new Queue<ParticleSystem>();
    public GameObject partivulaMoedaPrefab;
    
   


    #endregion


#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Pints Slider Bars")]
#endif
    public Sprite[] spritesPin;
    public Image playerSliderPin;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Events")]
#endif

	#region EventsManager 

	//public UnityEvent OnUpdate;
	//public UnityEvent OnLateUpdate;
	//public UnityEvent OnFixedUpdate;
	//public UnityEvent OnStartWak;
    public UnityEvent OnPe;
    public UnityEvent OffPe;

    #endregion

    float posPlayerX;
    float posNurseX;
    // Use this for initialization

#if UNITY_STANDALONE
    public float horizontalStandAlone;
    public float verticalStandAlone;
#endif


    public GameConfig config;
    public Minigames minigame;
    public GameObject panelTeclado;
    public int playpreft1; 
 
    public GameObject jumpObj;
	public GameObject slideObj;

    public ScreenTransitionImageEffect circleFader;
    public float speedCircleFader;
    public float timeToWaitOnFader;
    public float speedToFadeTotal;
    public Image faderTotal;

    public AudioClip somErro;
    public SortingGroup[] SortingGroups;
    public bool textointemCheck;
    public bool checkCamZomm;
    Animator m_Anim;
    void Start () {
            ParticulaMoedaPool = new Queue<ParticleSystem>();
            ParticulaMoedaPoolOnScene = new Queue<ParticleSystem>();
    Input.multiTouchEnabled = true;

        circleFader.radiusReset();

        minigame = config.allMinigames[2];


        for (int i = 0; i < capaVacine.Length; i++)
        {
            if (capaVacine[i] != null)
            {
                capaVacine[i].SetActive(false);
            }
        }
        topLevel.offsetMax = new Vector2(topLevel.offsetMax.x, 80f);

        startTime = Time.time;
        personSel.SetActive (true);
        SelPersonsEv.Invoke();

        SelPersons2.PersonSel = PlayerPrefs.GetInt("characterSelected", 0);
        if (SelPersons2.PersonSel == 0) {
            // 0 zeca
            personSel = SelPersons2.personZeca;
            Destroy(SelPersons2.AmigosZeca);

        } else if (SelPersons2.PersonSel == 1) {
            // 1 Tati
            personSel = SelPersons2.AmigosZeca;
            Destroy(SelPersons2.personZeca);
            Destroy(SelPersons2.personPaulo);
            Destroy(SelPersons2.personJoaoManu);
            Destroy(SelPersons2.personBia);


        } else if (SelPersons2.PersonSel == 2) {
            // 2 Paulo
            personSel = SelPersons2.AmigosZeca;
            Destroy(SelPersons2.personZeca);
            Destroy(SelPersons2.personTatiBia);
            Destroy(SelPersons2.personJoaoManu);

        } else if (SelPersons2.PersonSel == 3) {
            // 3 Manu
            personSel = SelPersons2.AmigosZeca;
            Destroy(SelPersons2.personZeca);
            Destroy(SelPersons2.personPaulo);
            Destroy(SelPersons2.personJoao);
            Destroy(SelPersons2.personTatiBia);
        } else if (SelPersons2.PersonSel == 4) {
            // 4 Joao
            personSel = SelPersons2.AmigosZeca;
            Destroy(SelPersons2.personZeca);
            Destroy(SelPersons2.personPaulo);
            Destroy(SelPersons2.personManu);
            Destroy(SelPersons2.personTatiBia);

        } else if (SelPersons2.PersonSel == 5) {
            // 5 Bia
            personSel = SelPersons2.AmigosZeca;
            Destroy(SelPersons2.personZeca);
            Destroy(SelPersons2.personPaulo);
            Destroy(SelPersons2.personJoaoManu);
            Destroy(SelPersons2.personTati);
        }

        playerSliderPin.sprite = spritesPin[SelPersons2.PersonSel];
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        playerRigidbody = personSel.GetComponent<Rigidbody2D>();
		playerTransform = personSel.GetComponent<Transform>();
		plataformControlerUser = personSel.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();
		plataformController2d = personSel.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
		playerCollider = personSel.GetComponent<Collider2D>();
		BoxCollider2DPlayer = personSel.GetComponent<BoxCollider2D>();
		animPerson = personSel.GetComponent<Animator> ();
        imgPersonAnimP = imgPersonAnimG[SelPersons2.PersonSel];
        imgPersonAnimP.enabled = false;
        lifePersonIm.sprite = lifePerson[SelPersons2.PersonSel];
        plataforms.Suffle();
        LoadQueueParticle();
         if (Application.platform == RuntimePlatform.WindowsPlayer) {
            jumpObj.SetActive(false);
            slideObj.SetActive(false);
        }
        else{
            jumpObj.SetActive(true);
            slideObj.SetActive(true);

        }



        if (Application.platform == RuntimePlatform.WindowsPlayer && PlayerPrefs.HasKey("TutorV_IniTeclado") == false) {

            panelTeclado.SetActive(true);
            PlayerPrefs.SetInt("TutorV_IniTeclado", 1);
            playpreft1 = PlayerPrefs.GetInt("TutorV_IniTeclado", 1);
           
        }
            else if(Application.platform == RuntimePlatform.WindowsPlayer && PlayerPrefs.HasKey("TutorV_IniTeclado") == true) {
                panelTeclado.SetActive(false);
                
                IniCont();
            }
         else {
            panelTeclado.SetActive(false);
            Timing.RunCoroutine(Timer());
            UpdateLifeText();

            int temp = plataforms.Count;
            numberV = new int[temp];
            for (int i = 0; i < temp; i++) {
                number = Random.Range(1, 3);
                numberV[i] = i;
                plataforms[i].numbPlataform = i;
                plataforms[i].numbRandom = number;

            }

            nurseSlider.value = 0;
            playerSlider.value = 0;
        }
        // Timing.RunCoroutine(Timer());

    }

   


    public void IniCont() {
        Timing.RunCoroutine(Timer());
        UpdateLifeText();

        int temp = plataforms.Count;
        numberV = new int[temp];
        for (int i = 0; i < temp; i++) {
            number = Random.Range(1, 3);
            numberV[i] = i;
            plataforms[i].numbPlataform = i;
            plataforms[i].numbRandom = number;

        }

        nurseSlider.value = 0;
        playerSlider.value = 0;
    }
    public override void UpdateMe() {

        /*if (plataformControlerUser != null && plataformControlerUser.m_slide && !hasSlideButtom) {
            CrounchEnd();
        } else if (plataformControlerUser != null && hasSlideButtom && !plataformControlerUser.m_slide && plataformController2d.m_Grounded) {
            CrouchBT();
        }*/

        if (plataformController2d != null) {
            plataformController2d.ThisUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Return) && Time.timeScale == 0 && tutorVacine.activeInHierarchy == true && textointemCheck == true) {

            textointemCheck = false;
            BtEnter.onClick.Invoke();
        }



        if (plataformControlerUser != null) {
#if UNITY_STANDALONE || UNITY_EDITOR

            if (jumpButton.interactable && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && !batendoCaixa && !deslizandoLeite) {
                JumpBT();
            }

            if (isFlying && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && !batendoCaixa && !deslizandoLeite) {
                JumpBTPress();
            }



            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && plataformControlerUser.m_slide && !batendoCaixa && !deslizandoLeite) {
                CrounchEnd();
                JumpBT();
            }

            if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space)) && !batendoCaixa && !deslizandoLeite) {
                JumpBTRelease();
            }
            if ((!Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.Space)) && plataformControlerUser.m_Jump) {
                JumpBTRelease();
            }

            if (slideButton.interactable && Input.GetKeyDown(KeyCode.DownArrow) && !batendoCaixa && !deslizandoLeite) {
                CrouchBT();
            }

            if (Input.GetKey(KeyCode.DownArrow) && !plataformControlerUser.m_slide && slideButton.interactable && !batendoCaixa && !deslizandoLeite) {
                CrouchBT();
            }

            if ((Input.GetKeyUp(KeyCode.DownArrow)) && plataformControlerUser.m_slide && !slideButton.interactable && !batendoCaixa && !deslizandoLeite) {
                CrounchEnd();
            }



#endif
            if (plataformControlerUser != null && plataformControlerUser.m_slide == true && hasSlideButtom == false) {
                plataformControlerUser.m_slide = false;
            }


        }

        
            CameraUpdateZoom();
            

    }

    private void CameraUpdateZoom() {
        Vector3 pos = new Vector3();
        // Vector3 posS;

        float interpolation = smoothTime * Time.deltaTime;
        //float interpolation2 = smoothTime2 * Time.deltaTime;

        pos.z = mainCameraTransform.position.z;
    //posS.z = seringaT.position.z;
   // if (!checkCamZomm) {
        if (playerTransform != null && playerTransform.transform.position.y >= 5f) {
            if (mainCameraTransform.position.y <= 5.62f) {
                pos.y = Mathf.Lerp(mainCameraTransform.position.y,playerTransform.transform.position.y,interpolation);
            } else {
                pos.y = Mathf.Lerp(mainCameraTransform.position.y,5.62f,interpolation);                
            }
        } else {
            pos.y = Mathf.Lerp(mainCameraTransform.position.y,1f,interpolation);
        }
  //  }

        if (playerTransform != null) {
            // posS.y = Mathf.Lerp(seringaT.position.y, playerTransform.transform.position.y + 2.5f, interpolation2);
            //posS.y = playerTransform.transform.position.y + 2.5f;
            //posS.x = seringaT.position.x;
            pos.x = playerTransform.transform.position.x + offsetFollow.x;

            mainCameraTransform.position = pos;
            //seringaT.position = posS;
        }
    }

    public override void FixedUpdateMe() {
        OnPlayerWalk();
        OnNurseWalk();
    }

	#region methods

	public void startRunning(){
        checkstartRunning = true;
        CrounchEnd();
        CrossPlatformInputManager.SetAxisNegative("Horizontal");

        nurseManager.StartWalk();
		isGameRunning = true;
		//plataformController2d.currentSpeedChosen = 0;
		jumpButton.interactable = true;
		slideButton.interactable = true;
        Timing.RunCoroutine(CameraZoomOut());
        _log.StartTimerLudica(true);
       
	}

    public void VoltarCorrer() {
        CrounchEnd();
        if (checkstartRunning == true) {
          //  CrossPlatformInputManager.SetAxisNegative("Horizontal");
            jumpButton.interactable = true;
            slideButton.interactable = true;
            Invoke("EnableBt",0.6f);           
        }
       
    }

    public int ReturnHorizontalAxis() {
        return Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Horizontal"));
    }

    public void EnableBt() {
        jumpButton.interactable = true;
        slideButton.interactable = true;
        batendoCaixa = false;
        if (!nurseHit) {
            CrossPlatformInputManager.SetAxisNegative("Horizontal");
        }
    }

    public void EnableButtons() {
        jumpButton.interactable = true;
        slideButton.interactable = true;       
    }

    public void BackRunning() {
        CrounchEnd();
        if (!nurseHit) {            
            CrossPlatformInputManager.SetAxisNegative("Horizontal");
        }
       // ButtonsEnable(true);
    }

    public void StopRunning()
    {
        CrossPlatformInputManager.SetAxisZero("Horizontal");
        //OnNurseWalk();
    }

    public void PararCorrer() {
        CrossPlatformInputManager.SetAxisZero("Horizontal");
        ButtonsEnable(false);
        enfermeiraG.SetActive(false);
        if (SelPersons2.PersonSel == 0) {
            jucaZeca.SetActive(true);
            enfermeiraZeca.SetActive(true);
        } else {
            jucaAmigos.SetActive(true);
            enfermeiraAmigos.SetActive(true);
        }
        //Debug.Log("pararCorrer");
    }

    public void OnFallOff(){
		Debug.Log("Fall Off");
	}

	public void CallEndOfGame(){
		jumpButton.interactable = false;
		slideButton.interactable = false;
	}

	public void OnPlayerWalk(){
        if (playerRigidbody.velocity.x >= 0 ) {
            //float value = playerRigidbody.velocity.x * offsetIncreaseOnWalk;      

            float value = (posPlayerX - (playerTransform.position.x));
            playerSlider.value = Mathf.Abs(value);
        }
	}

	public void OnNurseWalk(){
        if (nurseRigidbody.velocity.x >= 0) {
            // float value = nurseRigidbody.velocity.x * offsetIncreaseOnWalk;
            float value = (posNurseX - (nurseManager.thisTransform.position.x));
            nurseSlider.value = Mathf.Abs(value);
        }
	}

	public void JumpBT(){
		if (jumpCount < maxJump && !isFlying && !plataformControlerUser.m_slide && jumpButton.interactable) {
			jumpCount++;
			plataformControlerUser.m_Jump = true;
            slideButton.interactable = false;
		}
	}

	public void JumpBTPress(){
		if (isFlying && jumpButton.interactable) {
			CrossPlatformInputManager.SetAxisPositive("Vertical");
			plataformController2d.isFlying = true;
		}
	}

	public void JumpBTRelease(){       
        if (isFlying) {
            CrossPlatformInputManager.SetAxisZero("Vertical");
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            plataformController2d.isFlying = false;
        }
        slideButton.interactable = true;
    }

    public void StopFlyCollision() {
        CrossPlatformInputManager.SetAxisZero("Vertical");
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
        plataformController2d.isFlying = false;
    }
   

    public void CrouchBT(){
        hasSlideButtom = true;
      
        if (plataformController2d.m_Grounded && !plataformControlerUser.m_Jump && !isFlying && slideButton.interactable) {
            plataformControlerUser.m_slide = true;
            jumpButton.interactable = false;
         //   animPerson.SetBool("Crouch", plataformControlerUser.m_slide);
        }

        /*if (isFlying) {
			CrossPlatformInputManager.SetAxisPositive("Vertical");
			plataformController2d.isFlying = true;
		} else {
			plataformControlerUser.m_slide = true;
		}*/
    }

	public void CrounchEnd(){
        hasSlideButtom = false;          
        plataformControlerUser.m_slide = false;       
        jumpButton.interactable = true;
        
    }

	public void OnGround(){
		jumpCount = 0;
	}

	public void OnJump(){
		jumpCount++;
		plataformControlerUser.m_Jump = false;
	}

	IEnumerator<float> Timer(){
		animEnfermeira.SetInteger ("NumbEnfer", 1);
		yield return Timing.WaitForSeconds(3f);
		float timer = 0f;
		float timerToStart = 3f;
        textContador.gameObject.SetActive(true);
        nurseSlider.value = 0;
        playerSlider.value = 0;
        while (timer < timerToStart) {
			timerToStart -= 1f;
			float stringText =  timerToStart + 1f;
			textContador.text = stringText.ToString();
			numEnfer = numEnfer + 1;

			yield return Timing.WaitForSeconds(1f);
			if (numEnfer == 3) {
				animEnfermeira.SetInteger ("NumbEnfer", 2);
               // Debug.Log(numEnfer);
			}
		}
        nurseSlider.value = 0;
        playerSlider.value = 0;
        posPlayerX = playerTransform.position.x;
        posNurseX = nurseManager.thisTransform.position.x;
        textContador.text = "Começar";
       /// animEnfermeira.SetInteger("NumbEnfer", 2);
        yield return Timing.WaitForSeconds(1f);
        animEnfermeira.SetInteger("NumbEnfer", 3);
        textContador.gameObject.SetActive(false);
		startRunning();
       // animEnfermeira.SetInteger("NumbEnfer", 3);
    }

#endregion

#region methodsItems

	public void CoinCatch(int value){
        _log.AddPontosLudica(value);
        soundManager.startSoundFX(coinCatchSound);
        Timing.KillCoroutines("IncreaseScore");
		Timing.RunCoroutine(IncreasePoints(value), "IncreaseScore");
	}

    private CoroutineHandle speedItemCoroutine;


	public void SpeedItem(int speedTo, float delay){
        //SpeedPlayer
        soundManager.startSoundFX(speedEffectSound);
		Timing.KillCoroutines (speedItemCoroutine);		
		isSpeedIncreased = false;
		speedItemCoroutine = Timing.RunCoroutine(SpeedItemEffect(speedTo,delay), "SpeedItemEfffects");

        /*if (!isSpeedIncreased) {
			isSpeedIncreased = true;
            OnPe.Invoke();
            defaultVelocity = plataformController2d.m_MaxSpeed;
			plataformController2d.speedItemSpeed = -speedTo;
            plataformController2d.isSpeedItem = true;
            //plataformController2d.currentSpeedChosen = speedTo;
            Timing.KillCoroutines("DecreaseSpeed");
            Timing.RunCoroutine(DelayDecreaseSpeed(delay), "DecreaseSpeed");
		}*/
	}

    IEnumerator<float> SpeedItemEffect(int speedTo, float _delay) {
        isSpeedIncreased = true;
        OnPe.Invoke();
        defaultVelocity = plataformController2d.m_MaxSpeed;
        plataformController2d.speedItemSpeed = -speedTo;
        plataformController2d.isSpeedItem = true;
		Debug.Log ("Speed Item Activated");
        yield return Timing.WaitForSeconds(_delay);
        DecreaseSpeed();
    }

    IEnumerator<float> DelayDecreaseSpeed(float _delay) {
        yield return Timing.WaitForSeconds(_delay);
        DecreaseSpeed();
    }

    public void HitSoundEffect() {
        soundManager.startSoundFX(hitSoundFX);
    }    

	public void DecreaseSpeed(){
		plataformController2d.m_MaxSpeed = defaultVelocity;
        //plataformController2d.currentSpeedChosen = 0;
        isSpeedIncreased = false;
        plataformController2d.isSpeedItem = false;
        OffPe.Invoke();
		Debug.Log ("Speed Item Deativate");
    }

	private CoroutineHandle flyItemEffect;

	public void StartFly(float delay){
        soundManager.startSoundFX(itemEffectSound);
		if (!isFlying) {
			isFlying = true;
			hasFlyEffect = true;
			defaultGravity = playerRigidbody.gravityScale;
			playerRigidbody.gravityScale = defaultGravity * gravityScaleFly;
			Timing.KillCoroutines(flyItemEffect);
			flyItemEffect = Timing.RunCoroutine(DelayStopFly(delay));        
            //plataformController2d.isFlying = true;
            jumpCount = maxJump;
			plataformController2d.hasFly = true;
            int _tempCount = capaVacine.Length;
            for (int i = 0; i < _tempCount; i++)
            {
                if (capaVacine[i] != null) {
                    capaVacine[i].SetActive(true);
                }
            }

        }
	}

    IEnumerator<float> DelayStopFly(float _delay){
        yield return Timing.WaitForSeconds(_delay);
        StopFly();
    }

    public void StopFly(){
		playerRigidbody.gravityScale = defaultGravity;
		CrossPlatformInputManager.SetAxisZero("Vertical");
		playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x ,0f);
		isFlying = false;
		hasFlyEffect = false;
		plataformController2d.isFlying = false;
		plataformController2d.hasFly = false;
        //  plataformController2d.hasFly = true;
        int _tempCount = capaVacine.Length;
        for (int i = 0; i < _tempCount; i++) {
            if (capaVacine[i] != null)
            {
                capaVacine[i].SetActive(false);
            }
        }
    }


	public void ReceiveSlowItem(){
		plataformController2d.currentSpeed = 0f;
	}

#endregion

    public void HitNurseSound() {
        soundManager.startSoundFX(nurseSoundHit);
    }

	IEnumerator<float> IncreasePoints(int value){

		float times = 0.0f;
		int startPoints = ScoreAmount;
		int scoreT = ScoreAmount + value;
		ScoreAmount += value;
		while (times < increaseScoreDuration)
		{
			times += Time.deltaTime;
			float s = times / increaseScoreDuration;
			int scory = (int)Mathf.Lerp (startPoints, scoreT, increaseScoreCurve.Evaluate (s));
			textScoreComp.text = scory.ToString ();
			yield return Timing.WaitForOneFrame;
		}

	}

    public void ButtonsEnable(bool enable) {
        jumpButton.interactable = enable;
        slideButton.interactable = enable;
        //batendoCaixa = false;
        deslizandoLeite = false;
    }

    IEnumerator<float> CameraZoomOut() {
        if (!checkCamZomm) {
            Vector3 startOffset = offsetFollow;
            Vector3 endOffset = defaultOffset;

            float times = 0.0f;
            while (times < transitionDuration) {
                times += Time.deltaTime;
                float s = times / transitionDuration;
                offsetFollow = Vector3.Lerp(startOffset,endOffset,transitionCurve.Evaluate(s));
                yield return Timing.WaitForOneFrame;
            }

            yield return Timing.WaitForSeconds(3f);
            playerisOnDefault = true;
        }
    }

    public void GoToDidatica() {
        _log.faseLudica = contadorLevel;
        managerNext.RunDidatica();
    }

    public void OnBeforeStartChangeDidatica() {
        faderTotal.gameObject.SetActive(true);
        faderTotal.raycastTarget = true;
    }

    public void TransitionDidaticaByLife() {
        _log.faseLudica = contadorLevel;
        OnBeforeStartChangeDidatica();

        int tempCount = SortingGroups.Length;
        for (int i = 0; i < tempCount; i++) {
            if(SortingGroups[i] != null) {
                SortingGroups[i].sortingLayerName = "Personagem Efeitos";
            }
        }

        Sequence DidaticaTransition = DOTween.Sequence();
        DidaticaTransition.Append(DOTween.To(() => circleFader.maskValue, x => circleFader.maskValue = x, 0.35f, speedCircleFader));        
        DidaticaTransition.Append(faderTotal.DOFade(1f, speedToFadeTotal));
        DidaticaTransition.AppendInterval(timeToWaitOnFader);
        DidaticaTransition.AppendCallback(() => managerNext.RunDidatica());
        DidaticaTransition.AppendCallback(() => circleFader.disable());
        DidaticaTransition.Play();
    }
    public void KillCoroutines() {
        CrossPlatformInputManager.SetAxisZero("Horizontal");
        Timing.KillCoroutines();
        StopAllCoroutines();
    }

    public void UpdateLifeText() {
        lifeTextAmount.text = playerLifes.ToString();
    }

    public void PauseGame() {
        if (this.enabled) {
            nurseManager.hspeed = 0f;
            nurseHit = true;
            nurseManager.UpdateSpeed();
            CrossPlatformInputManager.SetAxisZero("Horizontal");
        }
    }

    public void UnpauseGame() {
        if (this.enabled) {
            Timing.RunCoroutine(UnpauseTimer());
        }
    }

    IEnumerator<float> UnpauseTimer() {        
        float timer = 0f;
        float timerToStart = 3f;
        textContador.gameObject.SetActive(true);
        while (timer < timerToStart) {
            timerToStart -= 1f;
            float stringText = timerToStart + 1f;
            //textContador.text = stringText.ToString();
            TextCounterChanger(stringText.ToString());
            yield return Timing.WaitForSeconds(1f);
        }
        //textContador.text = "Começar";
        TextCounterChanger("Começar");
        textContador.gameObject.SetActive(false);
        VoltarCorrer();
        nurseManager.hspeed = -1f;
        nurseHit = false;
    }

    public void TextCounterChanger(string textTo) {
        DOTween.Kill(3, false);
        Sequence ChangeTextCounters = DOTween.Sequence();
        ChangeTextCounters.Append(textContador.DOFade(0f, 0.2f).From());
        ChangeTextCounters.AppendCallback(() => TextChangerCounter(textTo));
        ChangeTextCounters.Append(textContador.DOFade(1f, 0.2f));
        ChangeTextCounters.SetId(3);
        DOTween.Play(ChangeTextCounters);
    }

    public void TextChangerCounter(string textTo) {
        textContador.text = textTo;
    }



    public void PauseOff()
    {
        Time.timeScale = 1;
        if (tutorVacineGame) {
            tutorVacine.SetActive(false);
        }
    }

    public void LostSoundError() {
        soundManager.startSoundFX(somErro);
    }

    public ParticleSystem GetParticulaFromPool(Transform parentTransform, Vector3 localscale,Vector3 position) {
        if(ParticulaMoedaPool.Count <= 0) {
            ParticulaMoedaPool.Enqueue(ReturnToQueue());
        }
        ParticleSystem particle = ParticulaMoedaPool.Dequeue();
        Transform particleTransform = particle.transform;
        particleTransform.SetParent(parentTransform);
        particleTransform.localScale = localscale;
        particleTransform.position = parentTransform.position;
        ParticulaMoedaPoolOnScene.Enqueue(particle);
        return particle;
    }

    public ParticleSystem ReturnToQueue() {
        return ParticulaMoedaPoolOnScene.Dequeue();
    }

    public void LoadQueueParticle() {
        for (int i = 0; i < 10; i++) {
            GameObject instance = Instantiate(partivulaMoedaPrefab, this.transform.position, Quaternion.identity) as GameObject;
            
            ParticleSystem ps = instance.GetComponent<ParticleSystem>();
            ParticulaMoedaPool.Enqueue(ps);
        }
    }

    public void UpdateCurrentSpeeds() {
        nurseManager.VelocityMiddle = nurseManager.VelocityByLevel[contadorLevel];
        plataformController2d.currentSpeedChosen = contadorLevel;
        nurseManager.UpdateSpeed();
    }
}
