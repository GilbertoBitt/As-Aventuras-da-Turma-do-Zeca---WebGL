using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using DG.Tweening;

public class Manager1_3B : OverridableMonoBehaviour {

	#region Vars
	private Camera mainCamera;
	
    public LogSystem _log;
	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Controles")]
	#endif
	public bool isPlaying;
	public float rotationLimit;
	public int foodQuestionTodo;
	public int foodQuestionMade;
	public List<QuestionFoodGroup1_3B> questionFood = new List<QuestionFoodGroup1_3B> ();
	public int rightIndexFood;
	public FoodItem1_3B rightFoodItem;
	public BubbleFood1_3B rightBubbleFood;
	public int scoreAmountPerRight;
    public GameObject interagindoPanel;
    public PanelDesafioControl interagindoManager;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Pontuação")]
	#endif
	public int scoreAmount;
	public Text scoreText;
	public float scoreIncreaseDuration;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Grupos de Alimentos")]
	#endif
	public FoodGroup currentFoodGroup = FoodGroup.none;
	public FoodItem1_3B[] frutasList = new FoodItem1_3B[0];
	public int frutasTimes = 1;
	public FoodItem1_3B[] verdurasList = new FoodItem1_3B[0];
	public int verdurasTimes = 1;
	public FoodItem1_3B[] pratosList = new FoodItem1_3B[0];
	public int pratosTimes = 1;
	public FoodItem1_3B[] bebidasList = new FoodItem1_3B[0];
	public int bebidasTimes = 1;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Canon")]
	#endif

	public Transform canonTransform;
	public Transform canonShootPosition;
	public Vector3 startPosCannon;
	private float oldCanonXPos;
	public bool canShoot=false;
	public float delayReturnCannon;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Bullets")]
#endif
    [HideInInspector]
    public Queue<BulletManager1_3B> poolOfBullet = new Queue<BulletManager1_3B> ();
	public int poolBulletSize;
	public GameObject bulletPrefab;
	public float bulletSpeed;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("Bubbles")]
	#endif
	public BubbleFood1_3B[] bubbleSprites;
	public float delayFadeIn = 3f;
	public float gotoCenterDelay = 1f;
	public Transform positionCenter;

	#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
	[SeparatorAttribute("UI")]
	#endif
	public Text textFoodName;
	public float textFoodFadeInDelay;
	public float textFoodFadeOuDelay;
    public Image fadeInOutImg;
    public float fadeInDuration;
    public AnimationCurve FadeInImgCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public GameObject[] desactiveObjects;
    public GameObject[] activeGameObjects;
    public Manager1_3A oldManager;
    public Vector3 defaulCameraPos;
    public GameObject animLevel;
    Animator anim_animLevel;
    public changeLevel changeLevel2;
    public PauseManager pausemanager;
    public Button buttonPause;
    public RectTransform topLevel;
    public GameObject luz;
    public Vector2 topLevelG;
    public GameObject tutorG;
    public TutorVacine tutorVacine2;
    bool checkDitadita;
    bool checkDitadita2;
    public GameObject painelTeclado2;
    public ControlTecladoTutor ControlTecladoTutor2;
    public bool checkTutorteclado;
    public BoxCollider2D[] bolhasDid;
    bool checkTutorteclado1;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Curves")]
	#endif
	public AnimationCurve fadeInCurveText = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Sounds")]
#endif
    public AudioClip[] soundFXs = new AudioClip[0];
    public SoundManager soundM;

    public Cannon1_3B cannonManager;
    public ControlSomTutor ControlSomTutor2;

    public GameObject canudo;

    public bool didadMovel;

    #endregion

    // Use this for initialization
     void Start() {
        canudo = cannonManager.gameObject;
        ControlSomTutor2 = GetComponent<ControlSomTutor>();
    }
    void StartGame () {
        // topLevelG = topLevel.offsetMax;
        // tutorVacine2 = tutorG.GetComponent<TutorVacine>();
        topLevel.offsetMax = new Vector2(topLevel.offsetMax.x, 1633.984f);
        luz.SetActive(false);
        mainCamera = Camera.main;
        anim_animLevel = animLevel.GetComponent<Animator>();
        changeLevel2 = animLevel.GetComponent<changeLevel>();
        StartFoodQuiz ();
        mainCamera.transform.position = defaulCameraPos;
        mainCamera.orthographicSize = 5f;
        this.enabled = true;

    }

	public void OnCannonPressed(){
		if (canShoot && !didadMovel) {
			Vector3 mousePos = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			Vector3 newPos = canonTransform.position;
			newPos.x = mousePos.x;
			canonTransform.position = newPos;
		}
	}



	public void OnCannonReleased(){
		if (canShoot && poolOfBullet.Count >= 1){
			BulletManager1_3B thisBullet = poolOfBullet.Dequeue();
			Vector3 dir = canonShootPosition.position - canonTransform.position;
			thisBullet.ShootBulletOnDirection(dir.normalized);
		}
	}

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_STANDALONE
    public override void UpdateMe() {
        //Debug.Log("Updating", this);
        if (canShoot && checkTutorteclado==false) {
            //Debug.Log("Controle Override.", this);
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
                //Debug.Log("Indo para a Direita.", this);
                Vector3 newPos = canonTransform.position;
                if (newPos.x >= -8f) {
                    newPos.x -= .2f;
                }
                canonTransform.position = newPos;                
            } else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
              //  Debug.Log("Indo para a Esquerda.", this);
                Vector3 newPos = canonTransform.position;
                if (newPos.x <= 8f) {
                    newPos.x += .2f;
                }
                canonTransform.position = newPos;
            } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) {
                cannonManager.DownKey();
            } else if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space)) {
                cannonManager.UpKey();
            }

        }       
        
    }
#endif

    public void StartFoodQuiz(){
		startPosCannon = canonTransform.position;
		LoadBulletPool ();
		questionFood.Clear ();
		questionFood = randomQuestionFoods ();
		foodQuestionTodo = questionFood.Count;
		foodQuestionMade = 0;

		UpdateFoods ();

		Timing.RunCoroutine (FadeInText ());
        if(!didadMovel)
		canShoot = true;

	}

	public void UpdateFoods(){
		rightIndexFood = Random.Range (0, 4);

		textFoodName.text = questionFood [foodQuestionMade].foods [rightIndexFood].foodName;
		rightFoodItem = questionFood [foodQuestionMade].foods [rightIndexFood];
		rightBubbleFood = bubbleSprites [rightIndexFood];

		for (int i = 0; i < 4; i++) {
			bubbleSprites [i].UpdateFood (questionFood [foodQuestionMade].foods [i]);
			bubbleSprites [i].StartFadeIn(delayFadeIn);
		}
	}

	public void NextFoodsQuestion(){
		foodQuestionMade++;
        if (foodQuestionMade >= foodQuestionTodo) {
            interagindoPanel.SetActive(true);
            interagindoManager.scoreAmount = scoreAmount;
            interagindoManager.scoreInteragindo = scoreAmount;
            int tempCount = activeGameObjects.Length;
            for (int i = 0; i < tempCount; i++) {
                activeGameObjects[i].SetActive(false);
            }
            this.enabled = false;
        } else {
            UpdateFoods();
            Timing.RunCoroutine(FadeInText());
            canShoot = true;
        }
	}

	public void LoadBulletPool() {
        poolOfBullet = new Queue<BulletManager1_3B>();
        for (int i = 0; i < poolBulletSize; i++) {
			GameObject bullet = Instantiate(bulletPrefab,canonShootPosition.position * 1000f,Quaternion.identity,this.transform) as GameObject;
			BulletManager1_3B temp = bullet.GetComponent<BulletManager1_3B> ();
			temp.thisManager = this;
            temp.isOnPool = true;
            poolOfBullet.Enqueue (temp);
		}
	}

	public List<QuestionFoodGroup1_3B> randomQuestionFoods(){

		List<QuestionFoodGroup1_3B> resultList = new List<QuestionFoodGroup1_3B> ();

		for (int i = 0; i < frutasTimes; i++) {			
			FoodItem1_3B[] food = randomFoodFromGroup (FoodGroup.Frutas);
			QuestionFoodGroup1_3B qFood = new QuestionFoodGroup1_3B ();
			qFood.foods = food;
			resultList.Add (qFood);
		}

		for (int i = 0; i < verdurasTimes; i++) {
			FoodItem1_3B[] food = randomFoodFromGroup (FoodGroup.VerdurasVegetais);
			QuestionFoodGroup1_3B qFood = new QuestionFoodGroup1_3B ();
			qFood.foods = food;
			resultList.Add (qFood);
		}

		for (int i = 0; i < pratosTimes; i++) {
			FoodItem1_3B[] food = randomFoodFromGroup (FoodGroup.Pratos);
			QuestionFoodGroup1_3B qFood = new QuestionFoodGroup1_3B ();
			qFood.foods = food;
			resultList.Add (qFood);
		}

		for (int i = 0; i < bebidasTimes; i++) {
			FoodItem1_3B[] food = randomFoodFromGroup (FoodGroup.Bebidas);
			QuestionFoodGroup1_3B qFood = new QuestionFoodGroup1_3B ();
			qFood.foods = food;
			resultList.Add (qFood);
		}

		resultList.Suffle ();
		return resultList;
	
	}

	public FoodItem1_3B[] randomFoodFromGroup(FoodGroup foodType){

		FoodItem1_3B[] result = new FoodItem1_3B[4];

		if (foodType == FoodGroup.Frutas) {
			result = randomFood (frutasList);
		} else if (foodType == FoodGroup.VerdurasVegetais) {
			result = randomFood (verdurasList);
		} else if (foodType == FoodGroup.Pratos) {
			result = randomFood (pratosList);
		} else if (foodType == FoodGroup.Bebidas) {
			result = randomFood (bebidasList);
		}
		result.Suffle ();
		return result;
	}

	public FoodItem1_3B[] randomFood(FoodItem1_3B[] list){
		
		FoodItem1_3B[] result = new FoodItem1_3B[4];

		//List<int> temp = UniqueRandom (0, list.Length).ToList ();
		for (int i = 0; i < 4; i++) {
			result [i] = list [i];
		}

		return result;
	}

	public enum FoodGroup {
		none = 0,
		Frutas,
		VerdurasVegetais,
		Pratos,
		Bebidas,
	}

	/// <summary>
	/// Returns all numbers, between min and max inclusive, once in a random sequence.
	/// </summary>
	IEnumerable<int> UniqueRandom(int minInclusive, int maxInclusive)
	{
		List<int> candidates = new List<int>();
		for (int i = minInclusive; i <= maxInclusive; i++)
		{
			candidates.Add(i);
		}
		System.Random rnd = new System.Random();
		while (candidates.Count > 0)
		{
			int index = rnd.Next(candidates.Count);
			yield return candidates[index];
			candidates.RemoveAt(index);
		}
	}

	public void OnBulletHit(BubbleFood1_3B bubble){
        soundM.startSoundFX(soundFXs[0]);
        CorrectionOfFood (bubble);
		canShoot = false;

	}

	public void StopLooping(){
		for (int i = 0; i < 4; i++) {
			bubbleSprites [i].isLooping = false;
		}
	}

	public void CorrectionOfFood(BubbleFood1_3B userChoiseFood){
		StopLooping ();
        animLevel.SetActive(true);
        anim_animLevel.enabled = true;
        //topLevel.offsetMax = topLevelG;
        //topLevel.offsetMax = new Vector2(topLevel.offsetMax.x, 1633.984f);
        // Debug.Log(topLevel.offsetMax.y);
        if (userChoiseFood.food == rightFoodItem) {
			//Debug.Log ("Correta");
            //int rand = Random.Range(0, 1);
            Invoke("CorrectionSucess", 0.6f);
            _log.SaveEstatistica(58, true);
            userChoiseFood.StartCorrectcenter (gotoCenterDelay);
			Timing.KillCoroutines ("IncreaseEffectScore");
			Timing.RunCoroutine (scoreIncrease (scoreAmountPerRight), "IncreaseEffectScore");
            _log.AddPontosPedagogica(scoreAmountPerRight);
           /// soundM.startSoundFX(soundFXs[1]);

        } else {
            //Debug.Log ("Errada");
            Invoke("CorrectionFail", 0.6f);
            _log.SaveEstatistica(58, false);
            //changeLevel2.startErrado("Você errou o certo era");
            rightBubbleFood.StartCorrectcenter (gotoCenterDelay);
			userChoiseFood.MakeItRed ();
            //soundM.startSoundFX(soundFXs[2]);
        }

		Timing.RunCoroutine (EndQuestion());
	}

    public void CorrectionSucess() {
        soundM.startSoundFX(soundFXs[1]);
        changeLevel2.startCerto("Parabéns! Você acertou!");
    }

    public void CorrectionFail() {
        soundM.startSoundFX(soundFXs[2]);
        changeLevel2.startErrado("Você errou! O certo era:");
    }

    IEnumerator<float> EndQuestion(){
		yield return Timing.WaitForSeconds (3f);
		//Timing.RunCoroutine (ReturnCannon ());
        canonTransform.DOMove(startPosCannon, delayReturnCannon, false);
        //Timing.RunCoroutine (FadeOuText ());
        textFoodName.DOFade(0f, textFoodFadeOuDelay);
        textFoodName.transform.DOScale(Vector3.zero, textFoodFadeOuDelay);
        FadeOutBubbles(delayFadeIn);
        yield return Timing.WaitForSeconds (delayFadeIn + 0.2f);
		NextFoodsQuestion ();
	}

    public void FadeOutBubbles(float _delay) {
        for (int i = 0; i < 4; i++) {
            bubbleSprites[i].StartFadeOut(_delay);
        }
    }

	IEnumerator<float> FadeInText(){
		float times = 0.0f;

		Vector3 scaleEnd = new Vector3 (1f, 1f, 1f);
		Vector3 scaleStart = textFoodName.transform.localScale;

		Color colorEnd = textFoodName.color;
		colorEnd.a = 1f;
		Color startColor = textFoodName.color;
		startColor.a = 0f;

		while (times < textFoodFadeInDelay)
		{
			times += Time.deltaTime;
			float s = times / textFoodFadeInDelay;

			textFoodName.transform.localScale = Vector3.Lerp (scaleStart, scaleEnd, fadeInCurveText.Evaluate (s));
			textFoodName.color = Color.Lerp (startColor, colorEnd, fadeInCurveText.Evaluate (s));

			yield return Timing.WaitForOneFrame;
        }
	}

	IEnumerator<float> FadeOuText(){
		float times = 0.0f;

		Vector3 scaleStart = new Vector3 (1f, 1f, 1f);
		Vector3 scaleEnd = textFoodName.transform.localScale;

		Color startColor = textFoodName.color;
		startColor.a = 1f;
		Color colorEnd = textFoodName.color;
		colorEnd.a = 0f;

		while (times < textFoodFadeOuDelay)
		{
			times += Time.deltaTime;
			float s = times / textFoodFadeOuDelay;

			textFoodName.transform.localScale = Vector3.Lerp (scaleStart, scaleEnd, fadeInCurveText.Evaluate (s));
			textFoodName.color = Color.Lerp (startColor, colorEnd, fadeInCurveText.Evaluate (s));

			yield return Timing.WaitForOneFrame;
        }
	}

	public IEnumerator<float> scoreIncrease(int increase){

		//Debug.Log ("Start Increase Points");
		float times = 0.0f;
		int startPoints = scoreAmount;
		int scoreT = scoreAmount + increase;
		scoreAmount += increase;
		while (times < scoreIncreaseDuration)
		{
			times += Time.deltaTime;
			float s = times / scoreIncreaseDuration;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, fadeInCurveText.Evaluate (s));
			scoreText.text = scory.ToString ();
			yield return Timing.WaitForOneFrame;
        }	

	}

    public void RunDidatica() {
      //  tutorVacine2.gameObject.SetActive(true);
         ControlSomTutor2.numTutor = 6;
       // ControlSomTutor2.numTutor = 1;
        checkDitadita2 = true;
     
        if (PlayerPrefs.HasKey("TutorV_IniD") == false || PlayerPrefs.HasKey("TutorV_IniD") == true) {
            /*
            if (Application.platform == RuntimePlatform.WindowsPlayer == true && (PlayerPrefs.HasKey("TutorV_IniD") == false)) {

                checkTutorteclado = true;
                PlayerPrefs.SetInt("TutorV_IniD", 1);
                ControlTecladoTutor2.didatica = true;
                for (int i = 0; i < bolhasDid.Length; i++) {
                    bolhasDid[i].enabled = false;
                }
               
            } 
            */
             if (Application.platform == RuntimePlatform.WindowsPlayer == false && (PlayerPrefs.HasKey("TutorV_IniD") == false)) {
                oldManager.panelTeclado.SetActive(true);
                // checkTutorteclado = true;
                didadMovel = true;
                PlayerPrefs.SetInt("TutorV_IniD", 1);

                for (int i = 0; i < ControlTecladoTutor2.tecla.Length; i++) {
                    ControlTecladoTutor2.tecla[i].gameObject.SetActive(false);
                   
                }

            }
            tutorVacine2.btTelao.SetActive(false);
            tutorVacine2.pularTex.SetActive(false);
            //PlayerPrefs.SetInt("TutorV_IniD", 1);
            tutorG.SetActive(true);
            tutorVacine2.textTutor.text = "Agora, veja o nome do alimento e atire com o canudo na imagem correta.";
            //SomTutor(1);
            tutorVacine2.Som1();
            tutorVacine2.Onprof();
            tutorVacine2.iniciarG1.SetActive(true);
            tutorVacine2.iniciarG2.SetActive(false);
            _log.StartTimerLudica(false);
            Timing.RunCoroutine(StartDidatica());
           SequenceToChangeDidatica(true);
            checkDitadita = true;

        } else {
           
            checkDitadita2 = false;
            _log.StartTimerLudica(false);
            SequenceToChangeDidatica(false);
            //Timing.RunCoroutine(StartDidatica());
           

        }

      
    }

    void SomTutor(int recNum) {
        ControlSomTutor2.numTutor = recNum;
        ControlSomTutor2.SomTutor();
        //y//ield return Timing.WaitForSeconds(0.2f);


    }

    public void teclaDidadica () {
        if (ControlTecladoTutor2.didatica == true) {
            painelTeclado2.SetActive(true);
        }
        if (didadMovel == true) {
            canudo.SetActive(true);
            ControlTecladoTutor2.textTutor.SetActive(true);
            canudo.GetComponent<Animator>().enabled = true;
            canudo.GetComponent<Animator>().SetBool("Tutor", true);
        }

    }

    public void iniDid() {
        checkDitadita2 = false;
        if (checkDitadita) {
            
            Timing.RunCoroutine(StartDidatica());

        }

    }

    public void ChangeToDidatica() {
        int tempCount = desactiveObjects.Length;

        for (int i = 0; i < tempCount; i++) {
            desactiveObjects[i].SetActive(false);
        }

        tempCount = activeGameObjects.Length;

        for (int i = 0; i < tempCount; i++) {
            activeGameObjects[i].SetActive(true);
        }

        oldManager.enabled = false;

        scoreAmount = oldManager.ScoreAmount;
        scoreText.text = scoreAmount.ToString();

        pausemanager.btPause = buttonPause.gameObject;
    }

    public void EndChangeDidatica() {
        //fadeInOutImg.gameObject.SetActive(false);
        //fadeInOutImg.raycastTarget = false;
        if (!checkDitadita2) {
            StartGame();
        }
        _log.StartTimerDidatica(true);
    }

    public void OnBeforeStartChangeDidatica() {
        fadeInOutImg.gameObject.SetActive(true);
        fadeInOutImg.raycastTarget = true;
    }

    public void SequenceToChangeDidatica(bool tutorial) {

        oldManager.circleFader.maskValue = 0f;
        Sequence startDidaticaS = DOTween.Sequence();
        /*startDidaticaS.AppendCallback(OnBeforeStartChangeDidatica);
        if (tutorial == false) {
            startDidaticaS.Append(fadeInOutImg.DOFade(1f, fadeInDuration));
        }*/
        startDidaticaS.AppendCallback(ChangeToDidatica);
        startDidaticaS.Append(fadeInOutImg.DOFade(0f, fadeInDuration));
        startDidaticaS.AppendCallback(EndChangeDidatica);
        DOTween.Play(startDidaticaS);
    }

   IEnumerator<float> StartDidatica() {
        int tempCount = desactiveObjects.Length;

        for (int i = 0; i < tempCount; i++) {
            desactiveObjects[i].SetActive(false);
            yield return Timing.WaitForOneFrame;
        }

        tempCount = activeGameObjects.Length;

        for (int i = 0; i < tempCount; i++) {
            activeGameObjects[i].SetActive(true);
            //yield return Timing.WaitForOneFrame;
        }

        oldManager.enabled = false;
        //

        scoreAmount = oldManager.ScoreAmount;
        scoreText.text = scoreAmount.ToString();
       // yield return Timing.WaitForOneFrame;
        pausemanager.btPause = buttonPause.gameObject;

        yield return Timing.WaitForOneFrame;
        
        if (!checkDitadita2) {
            StartGame();
        }
        _log.StartTimerDidatica(true);

    }

    IEnumerator<float> ReturnCannon(){
		float times = 0.0f;

		Vector3 posEnd = startPosCannon;
		Vector3 posStart = canonTransform.position;	

		while (times < delayReturnCannon)
		{
			times += Time.deltaTime;
			float s = times / delayReturnCannon;

			canonTransform.position = Vector3.Lerp (posStart, posEnd, fadeInCurveText.Evaluate (s));

			yield return Timing.WaitForOneFrame;
		}
	}

    public void KillCoroutines() {
        Timing.KillCoroutines();
        StopAllCoroutines();
    }
}

[System.Serializable]
public class FoodItem1_3B{
    public string foodName;
    public Sprite spriteItem;	
}

[System.Serializable]
public class QuestionFoodGroup1_3B{
	public FoodItem1_3B[] foods = new FoodItem1_3B[4];
	//FoodGroup
}


