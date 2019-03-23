using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using MEC;

public class Manager1_4A : OverridableMonoBehaviour {

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Referencias")]
#endif
    public List<GridLayoutGroup> Grids = new List<GridLayoutGroup>();
    public Transform dragParent;
    public Transform towerParent;
    public Manager1_4B nextManager;
    public changeLevel _highlight;
    public GameObject highlightOB;
    public Transform towerGrid;
    public Button pauseButton;
    //[Header("Prefabs")]
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Prefabs")]
#endif
    public GameObject circlesOnGrid;
    public GameObject imagesOfItem;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Variáveis")]
#endif
    public bool isPlaying = false;
    public bool isDragging = false;
    [Range(0f, 100f)]
    public float chancesOfBadItem = 5f;
    public bool isGameEnded = false;
    public bool hasEndedByTime = false;
    public Color gridColor;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Dificuldade Atual")]
#endif
    public int currentDificult = 0;
    public int startDificult = 0;
    public Dificult1_4A dificult;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração Transparencia")]
#endif
    [Range(0f, 1f)]
    public List<float> transparentLevels = new List<float>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Listas de Dados")]
#endif
    public List<Item1_4A> itemsOfTower = new List<Item1_4A>();
    public List<Dificult1_4A> dificults = new List<Dificult1_4A>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Instancias")]
#endif
    public List<GameObject> circlesOnScene = new List<GameObject>();
    public List<GameObject> imagesLayerOnScene = new List<GameObject>();
    private List<ItemHandler1_4A> itemHandlers = new List<ItemHandler1_4A>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração de Animações")]
#endif
    public float itemAlphaChangeDuration = 1.0f;
    public AnimationCurve itemAlphaChangeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [Range(1f, 300f)]
    public float towerFallOffset = 1f;
    public float towerFallDuration = 1.0f;
    public AnimationCurve towerFallCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração do Raycast")]
#endif
    public Vector2 directionOfRayRight;
    public Color debugDrawRayColorRight;
    public Vector2 directionOfRayLeft;
    public Color debugDrawRayColorLeft;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração de Pontuação")]
#endif
    public int scoreAmount = 0;
    public int startScoreAmount = 0;
    public int amountScorePerCorrect = 0;
    public Text scoreTextComp;
    public float scoreIncreaseDuration = 1.0f;
    public AnimationCurve ScoreIncreaseCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public float scoreDecreaseDuration = 1.0f;
    public AnimationCurve ScoreDecreaseCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração de Estrelas")]
#endif
    public int starAmount = 0;
    public Sprite spriteFullStar;
    public Sprite spriteEmptyStar;
    public Transform starsParent;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração de Timer")]
#endif
    public float timerStartValue = 60f;
    public Slider timerSlider;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configuração de Combo")]
#endif
    public int combo = 0;
    public Text comboTextComp;
    public float showComboTextDuration = 1.0f;
    public AnimationCurve showComboTextCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    [HideInInspector]
    public bool isRemoving = false;
    private List<ItemHandler1_4A> ItemHandlerList = new List<ItemHandler1_4A>();
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Item Bonus!")]
#endif
    public int amountBonusItem = 0;
    public bool hasItemBonus = false;
    public float breakBetweenBonus;
    private ItemGroup1_4A itemWithBonusItem;
    [RangeAttribute(0f, 100f)]
    public float chancesOfBonus;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Configurações dos Baús!")]
#endif
    public float timeToRandomizeChest;
    [Range(0f, 100f)]
    public float chanceToRandomChest;
    public List<ItemType1_4A> itemtypes = new List<ItemType1_4A>();
    public List<ChestHandler1_4A> chests = new List<ChestHandler1_4A>();
    private List<ChestHandler1_4A> chests2 = new List<ChestHandler1_4A>();
    public Sprite[] towerFloors = new Sprite[5];
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Baú Bonus!")]
#endif
    public bool hasChestBonus = false;
    public int bonusChestScore;
    public bool chooseChestBonus;
    [RangeAttribute(0f, 100f)]
    public float chancesOfChestBonus;
    public float timeToChestBonus;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Baú Fechado")]
#endif

    [RangeAttribute(0f, 100f)]
    public float chancesOfChestClose;
    public float timeToChestClose;
    public bool hasChestClose = false;
    public bool isClosingChest = false;
    public bool isOpeningChest = false;
    private ChestHandler1_4A _closedChest;
    public Vector2 rangeTimeToOpen;
    public List<GameObject> chestsParent = new List<GameObject>();
    public List<GameObject> ChestOnRight = new List<GameObject>();

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Informações Iniciais[Historico]")]
#endif
    public Vector3 startTowerPos;

    public int valorTest = 10;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Eventos")]
#endif
    public UnityEvent OnChestsChange = new UnityEvent();
    public UnityEvent OnBonusItemShow = new UnityEvent();
    public UnityEvent OnChestBlock = new UnityEvent();
    public UnityEvent OnChestOpens = new UnityEvent();

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Tremedeira da Torre")]
#endif
    public bool shakeOn = false;
    public float shakePower = 0;
    // sprite original position
    public Vector3 originPosition;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Animação do Item Errado")]
#endif
    public Vector3 posOffsetThrow;
    public float itemThrowAwayDuration = 1.0f;
    public AnimationCurve itemThrowAwayScaleCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public AnimationCurve itemThrowAwayPositionCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public GameObject[] partBauTransp;
    public GameObject[] bauGO;
    //public ParticleSystem[]  partBauTransp2;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Animação do FadeInFadeOut")]
#endif
    public float fadeOutChestChangeDuration = 1.0f;
    public AnimationCurve fadeOutChestChangeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public float fadeInChestChangeDuration = 1.0f;
    public AnimationCurve fadeInChestChangeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public bool checkTorreDestroy;

    public GameObject jucaBaufechar;
    int _chestIndex;
    public bool checkJucaBau = false;
    public bool checkZecaBau = false;

    public bool checkBauOpen = false;

    public IEnumerator randomchest;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("Animação do FadeInFadeOut")]
#endif
    public AudioClip[] audios;
    public SoundManager soundManager;
    bool checkBauIni;
    int tutorTroca;
    int tutorObjLock;
    int TutorBauBonus;
    public GameObject tutorial;
    public GameObject montanha;
    public GameObject didatica;

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
    [SeparatorAttribute("LOG")]
#endif
    public LogSystem log;
    public ChestHandler1_4A chestBonus;

    private StringFast stringFast = new StringFast();

    public LayerMask layerItemHandler = new LayerMask();
    public TutorMontanha TutorMontanha2;

    public GameConfig config;
    public Minigames minigame;

    private CoroutineHandle chestBonusEnd;

    public GameObject painelMaeZeza;

    bool checkMaeZeca;

    [TextArea()]
    public string[] textos;
    // Use this for initialization
    void Start() {
        minigame = config.allMinigames[4];

        StartCoroutine(beginGame());
        startTowerPos = towerParent.position;
        nextManager.StartCoroutine(nextManager.startIt());
        _chestIndex = Random.Range(0, chests.Count - 1);
        log.StartTimerLudica(true);
#pragma warning disable CS1717 // Assignment made to same variable
        TutorMontanha2 = TutorMontanha2;
#pragma warning restore CS1717 // Assignment made to same variable
        Input.multiTouchEnabled = false;
    }


    // Update is called once per frame
    public override void UpdateMe() {

        if (isPlaying && timerSlider.value >= timerStartValue && hasEndedByTime == false) {
            timerSlider.value = 0;
            ////Debug.Log ("DONE");
            CancelInvoke();
            hasEndedByTime = true;
            int ItemHandlerListCount = ItemHandlerList.Count;
            for (int i = 0; i < ItemHandlerListCount; i++) {
                ItemHandlerList[i].hasEnded = true;
            }
        }
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.J)) {
            timerSlider.value = 0;
            ////Debug.Log ("DONE");
            CancelInvoke();
            hasEndedByTime = true;

            int ItemHandlerListCount = ItemHandlerList.Count;
            for (int i = 0; i < ItemHandlerListCount; i++) {
                ItemHandlerList[i].hasEnded = true;
            }
        }
#endif

        if (Input.GetKeyDown(KeyCode.L)) {
            ShakeCameraOn(shakePower);
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            ShakeCameraOff();
        }

        if (shakeOn) {

            // reset original position
            towerParent.transform.localPosition = originPosition;

            // generate random position in a 1 unit circle and add power
            Vector2 ShakePos = Random.insideUnitCircle * shakePower;

            // transform to new position adding the new coordinates
            towerParent.transform.localPosition = new Vector3(towerParent.transform.localPosition.x + ShakePos.x, towerParent.transform.localPosition.y + ShakePos.y, towerParent.transform.localPosition.z);

        }
    }

    void updateStarAmount(int _amount) {
        starAmount = _amount;
        if (_amount < 3) {
            _amount++;

            for (int i = 0; i < 3; i++) {
                starsParent.GetChild(i).GetComponent<Image>().sprite = spriteEmptyStar;
            }

            for (int i = 0; i < _amount; i++) {
                starsParent.GetChild(i).GetComponent<Image>().sprite = spriteFullStar;
            }
        }

    }
    /*void StartbeginGame(){
		StartCoroutine (beginGame ());
	}*/
    public IEnumerator beginGame() {
        isPlaying = false;
        hasEndedByTime = false;
        timerSlider.value = 0f;
        ////Debug.Log("Game Started");


        dificult = dificults[currentDificult];
        timerStartValue = dificult.timeToComplete;
        timerSlider.maxValue = timerStartValue;
        startScoreAmount = scoreAmount;
        startDificult = currentDificult;
        chancesOfBadItem = dificult.chancesOfBadItems;
        combo = 0;
        int tempSizeOfTower = dificult.sizeOfTower;
        Grids.ReturnReverseList();
        for (int i = 0; i < dificult.sizeOfTower; i++) {

            List<ItemHandler1_4A> handlersOfThis = new List<ItemHandler1_4A>();
            //Image img = Grids[i].GetComponent<Image>();
            Grids[i].GetComponent<Floor1_4A>().floorItems.Clear();
            for (int j = tempSizeOfTower - 1; j >= 0; j--) {
                GameObject circleOfItem = Instantiate(circlesOnGrid, Grids[i].gameObject.transform) as GameObject;
                ItemHandler1_4A itemHandler = circleOfItem.GetComponent<ItemHandler1_4A>();
                circleOfItem.transform.localScale = new Vector3(1f, 1f, 1f);
                itemHandler.gameManager = this;
                circlesOnScene.Add(circleOfItem);
                itemHandler.floorParent = Grids[i].GetComponent<Floor1_4A>();
                Grids[i].GetComponent<Floor1_4A>().floorItems.Add(itemHandler);
                handlersOfThis.Add(itemHandler);
                itemHandler.sortingOrder = tempSizeOfTower;
            }
            setHandlerOnHandler(handlersOfThis);
            tempSizeOfTower--;
        }

        Grids.ReturnReverseList();
        int circlesOnSceneCount = circlesOnScene.Count;
        for (int i = 0; i < circlesOnSceneCount; i++) {
            int tempIndex = dificult.sizeOfLayers;
            tempIndex = tempIndex - 1;
            ItemHandler1_4A itemHandler = circlesOnScene[i].GetComponent<ItemHandler1_4A>();
            ItemHandlerList.Add(itemHandler);
            for (int j = 0; j < dificult.sizeOfLayers; j++) {
                GameObject imageItem = Instantiate(imagesOfItem, circlesOnScene[i].transform) as GameObject;
                itemsOfTower.Suffle();
                imagesLayerOnScene.Add(imageItem);
                imageItem.transform.localScale = new Vector3(1f, 1f, 1f);
                itemHandler.itemsOnSlot.Add(imageItem);
                itemHandler.UpdateItemInfo(j, itemsOfTower[j], transparentLevels[tempIndex]);
                tempIndex--;
                imageItem.GetComponent<Canvas>().sortingOrder = itemHandler.sortingOrder;
            }
            itemHandler.UpdateItemHandlerOnChilds();
            itemHandler.UpdateDragItem();
            itemHandler.ActiveCollider2D();
            itemHandler.UpdateStartList();
            //yield return new WaitForSeconds (.2f);
            itemHandler.UpdateBadItemsLimit();
            itemHandler.slotCount = itemHandler.itemsOnSlot.Count;
            itemHandler.UpdateItemHandlersHistory();
            itemHandler.StartMaxBadItemsUpdate();
        }

        if (checkBauIni == false) {
            int chestsCount = chests.Count;
            checkBauIni = true;
            for (int i = 0; i < chestsCount; i++) {
                chests[i].isChestBonus = false;
                chests[i].isChestClose = false;
                chests[i].ToggleRaycastTarget(true);
                chests[i].chestGroup = itemtypes[i];
                chests[i].UpdateText(ChestNameByType(itemtypes[i]));
            }
        }
        else {
            int chestsCount = chests.Count;
            for (int i = 0; i < chestsCount; i++) {
                chests[i].isChestBonus = false;
                //chests [i].isChestClose = false;
                chests[i].chestGroup = itemtypes[i];
                chests[i].UpdateText(ChestNameByType(itemtypes[i]));

            }
            //if (chests [_chestIndex].ToggleRaycastTarget () == false) {
            //chests [_chestIndex].ToggleRaycastTarget (true);
            //chests[_chestIndex].zecaFecharBau.SetBool("AbrirBau",true);
            StartCoroutine(DesativBauAnim());
            //}

        }

        hasChestBonus = false;
        hasChestClose = false;


        updateItemHandlerList();
        StartCoroutine(endGame());
        InvokeRepeating("DecreaseTimeOverSeconds", 1f, 1f);
        InvokeRepeating("ItemBonus", breakBetweenBonus, breakBetweenBonus);
        Invoke("startRandomChest", timeToRandomizeChest);
        InvokeRepeating("ChooseChest", timeToChestBonus, timeToChestBonus);
        Invoke("randomChestToCLose", timeToChestClose);
        //StartCoroutine(RandomChestsTime());
        //yield return new WaitForSeconds (1f);
        yield return Yielders.Get(1f);
        isPlaying = true;
    }
    IEnumerator DesativBauAnim() {
        yield return Yielders.Get(2f);
        chests[_chestIndex].zecaFecharBau.SetBool("AbrirBau", true);
        int chestsCount = chests.Count;
        for (int i = 0; i < chestsCount; i++) {
            chests[i].GetComponent<Animator>().enabled = false;
        }
    }



    public void setHandlerOnHandler(List<ItemHandler1_4A> _handlerList) {
        for (int i = 0; i < _handlerList.Count; i++) {
            for (int j = 0; j < _handlerList.Count; j++) {
                if (_handlerList[i] != _handlerList[j]) {
                    _handlerList[i].AddHandler(_handlerList[j]);
                }
            }
        }
    }

    /// <summary>
    /// Item group is correct.
    /// </summary>
    /// <param name="itemGroup">Item group.</param>
    public void ItemGroupIsCorrect(ItemHandler1_4A itemGroup, bool _isChestBonus) {
        ItemGroup1_4A item = itemGroup.itemToDrag.GetComponent<ItemGroup1_4A>();
        if (item.hasObjectOnLeft || item.hasObjectOnRight) {
            ItemGroupIsWrong2(itemGroup);
        } else {
            soundManager.startSoundFX(audios[0]);
            isRemoving = true;
            item.DisableBackgroundImage();
            bool IsBonusItem = item.isBonusItem;
            int increasePoints = scoreToIncrease(IsBonusItem);
            item.DisableBackgroundImage();


            if (_isChestBonus && !IsBonusItem) {
                comboTextComp.text = "Baú bônus";
                increasePoints *= bonusChestScore;
                Timing.RunCoroutine(ShowComboText(), "ComboEffector");
            } else if (!_isChestBonus && IsBonusItem) {
                comboTextComp.text = "Item bônus";
                Timing.RunCoroutine(ShowComboText(), "ComboEffector");
            } else if (_isChestBonus && IsBonusItem) {
                comboTextComp.text = "Item bônus + Baú Bônus";
                Timing.RunCoroutine(ShowComboText(), "ComboEffector");
            }

            StartCoroutine(scoreIncrease(increasePoints));
            //StartCoroutine(ShowComboText());
            if (item.hasObjectOnLeft || item.hasObjectOnRight) {
                ResetToyTower();
            } else {
                StartCoroutine(UpdateLayerOfItemsIE(itemGroup, true));
            }
            item.DisableBackgroundImage();
        }
    }

    /// <summary>
    /// Item group is wrong.
    /// </summary>
    /// <param name="itemGroup">Item group.</param>
    public void ItemGroupIsWrong(ItemHandler1_4A itemGroup) {
        soundManager.startSoundFX(audios[1]);
        isRemoving = true;
        ItemGroup1_4A item = itemGroup.itemToDrag.GetComponent<ItemGroup1_4A>();
        item.DisableBackgroundImage();
        if (item.hasObjectOnLeft || item.hasObjectOnRight) {
            ResetToyTower();
        } else {
            item.DisableBackgroundImage();
            StartCoroutine(UpdateLayerOfItemsIE(itemGroup, false));
        }
        item.DisableBackgroundImage();
    }

    public void ItemGroupIsWrong2(ItemHandler1_4A itemGroup) {
        soundManager.startSoundFX(audios[1]);
        isRemoving = true;
        ItemGroup1_4A item = itemGroup.itemToDrag.GetComponent<ItemGroup1_4A>();
        item.DisableBackgroundImage();
        if (item.hasObjectOnLeft || item.hasObjectOnRight) {
            ResetToyTower();
        } else {
            item.DisableBackgroundImage();
            //StartCoroutine (UpdateLayerOfItemsIE (itemGroup, false));
        }
        item.DisableBackgroundImage();
    }

    public void ResetToyTower() {
        ////Debug.Log ("Torre será montada novamente!");
        isPlaying = false;
        Timing.RunCoroutine(fallOfTower());
    }

    /// <summary>
    /// Falls of The tower.
    /// </summary>
    /// <returns>Animation and Effect of Tower Falling</returns>
    IEnumerator<float> fallOfTower() {

        if (PlayerPrefs.HasKey("TutorM_Queda") == false) {
            montanha.GetComponent<GraphicRaycaster>().enabled = false;
            didatica.GetComponent<GraphicRaycaster>().enabled = false;
            PlayerPrefs.SetInt("TutorM_Queda", 0);
            yield return Timing.WaitForSeconds(.3f);
            this.tutorial.GetComponent<TutorMontanha>().animTutor.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().tutorialMontanha.GetComponent<Animator>().enabled = true;
            TutorMontanha2.numtext = 6;
            this.tutorial.GetComponent<TutorMontanha>().animTutor.SetInteger("numbTutor", 6);
            this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor[6];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[6]);
            TutorMontanha2.profBalao.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 4;
            foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
                item.SetActive(true);
            }
            this.tutorial.GetComponent<TutorMontanha>().gTutor[6].SetActive(false);
            this.tutorial.GetComponent<GraphicRaycaster>().enabled = true;
            this.tutorial.SetActive(true);
            Time.timeScale = 0f;
        } else {
            PlayerPrefs.SetInt("TutorM_Queda", 1);
            //	this.tutorObjLock = PlayerPrefs.GetInt ("TutorM_Queda", 1);
        }
        checkTorreDestroy = true;
        StartCoroutine(scoreReset());
        //yield return new WaitForSeconds (0.1f);
        yield return Timing.WaitForSeconds(0.1f);

        Vector3 startPos = towerGrid.transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - towerFallOffset, startPos.z);

        ShakeCameraOn(shakePower);
        float timer = 0.0f;
        while (timer < towerFallDuration)
        {
            timer += Time.deltaTime;
            float s = timer / towerFallDuration;

            //float scale = Mathf.Lerp (0f, 1f, towerFallCurve.Evaluate (s));
            towerGrid.transform.position = Vector3.Lerp(startPos, endPos, towerFallCurve.Evaluate(s));

            yield return Timing.WaitForOneFrame;
        }
        ShakeCameraOff();
        checkTorreDestroy = false;
        //yield return new WaitForSeconds (1f);
        yield return Timing.WaitForSeconds(1f);
        Timing.RunCoroutine(rebuildingTower());

    }
    IEnumerator TutorQuedaMont() {

        yield return Yielders.Get(0.5f);
        if (PlayerPrefs.HasKey("TutorM_Queda") == false) {
            montanha.GetComponent<GraphicRaycaster>().enabled = false;
            didatica.GetComponent<GraphicRaycaster>().enabled = false;
            PlayerPrefs.SetInt("TutorM_Queda", 0);
            yield return Yielders.Get(.3f);
            this.tutorial.GetComponent<TutorMontanha>().animTutor.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().tutorialMontanha.GetComponent<Animator>().enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().animTutor.SetInteger("numbTutor", 6);
            this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor[6];
            TutorMontanha2.profBalao.enabled = true;
            TutorMontanha2.numtext = 6;
            soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[6]);
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 4;
            foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
                item.SetActive(true);
            }
            this.tutorial.GetComponent<TutorMontanha>().gTutor[6].SetActive(false);
            this.tutorial.GetComponent<GraphicRaycaster>().enabled = true;
            this.tutorial.SetActive(true);
            Time.timeScale = 0f;
        } else {
            PlayerPrefs.SetInt("TutorM_Queda", 1);
            this.tutorObjLock = PlayerPrefs.GetInt("TutorM_Queda", 1);
        }
        // TutorMontanha2.profBalao.enabled = false;

    }

    public int scoreToIncrease(bool _hasBonusItem) {
        if (_hasBonusItem) {
            int score = amountBonusItem;
            return score;
        } else {
            int score = amountScorePerCorrect;
            return score;
        }
    }

    /// <summary>
    /// Rebuilding the tower.
    /// </summary>
    /// <returns>The tower.</returns>
    IEnumerator<float> rebuildingTower() {
        int ItemHandlerListCount = ItemHandlerList.Count;
        for (int i = 0; i < ItemHandlerListCount; i++) {
            ItemHandler1_4A itemHandler = ItemHandlerList[i];
            itemHandler.ResetItemsPos();
            UpdateLayerOfItemsIEWhitoutRemove(itemHandler);
            itemHandler.itemToDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
        towerGrid.position = startTowerPos;
        towerGrid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        towerGrid.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        //yield return new WaitForSeconds (1f);
        yield return Timing.WaitForSeconds(1f);
        isPlaying = true;
    }


    IEnumerator UpdateLayerOfItemsIE(ItemHandler1_4A _itemHandler, bool _isRight) {

        if (_itemHandler != null)
            _itemHandler.removeItemInFront(_isRight);

        //bool isBadItem = false;
        yield return new WaitForEndOfFrame();
        if (_itemHandler.slotCount > 0) {
            if (_itemHandler.amountBadItems < _itemHandler.maxBadItems && _itemHandler.slotCount >= 1) {
                float temp = Random.Range(0f, 100f);
                if (temp < chancesOfBadItem) {
                    // ("Bad Item");
                    _itemHandler.amountBadItems++;
                    _itemHandler.updateOtherHandlers();
                    _itemHandler.RemoveThisHandlerFromAll(_itemHandler);
                    _itemHandler.isRed = true;
                    _itemHandler.redItem();
                    //isBadItem = true;
                } else {
                    //Debug.Log ("Normal Item Inside");
                    //isBadItem = false;
                }
            } else {
                //Debug.Log ("normal Item");
                //isBadItem = false;
            }
            float times = 0.0f;
            float duration = itemAlphaChangeDuration;
            AnimationCurve curve = itemAlphaChangeCurve;

            List<Color> startColors = new List<Color>();
            List<Color> endColors = new List<Color>();

            List<float> transparency = new List<float>();
            for (int i = 0; i < _itemHandler.slotCount; i++) {
                transparency.Add(transparentLevels[i]);
            }

            for (int i = 0; i < _itemHandler.slotCount; i++) {
                startColors.Add(_itemHandler.itemsOnSlot[i].GetComponent<ItemGroup1_4A>().GetColor());
                endColors.Add(new Vector4(1f, 1f, 1f, transparency[i]));
            }

            _itemHandler.itensGroup.Clear();

            List<GameObject> _list = _itemHandler.itemsOnSlot.ReturnReverseList();

            for (int i = 0; i < _itemHandler.slotCount; i++) {
                _itemHandler.itensGroup.Add(_list[i].GetComponent<ItemGroup1_4A>());
            }

            while (times < duration) {
                times += Time.deltaTime;
                float s = times / duration;

                for (int i = 0; i < _itemHandler.slotCount; i++) {
                    //float alphaLerp = Mathf.Lerp (startColors [i].a, endColors [i].a, curve.Evaluate (s));
                    //_itemHandler.itemsOnSlot [i].GetComponent<ItemGroup1_4A> ().UpdateImage (alphaLerp);

                    if (i == 0 && _itemHandler.isRed) {
                        Color _color = Color.Lerp(startColors[i], Color.gray, curve.Evaluate(s));
                        _itemHandler.itemsOnSlot[i].GetComponent<ItemGroup1_4A>().UpdateImage(_color);
                        _itemHandler.itemsOnSlot[i].GetComponent<ItemGroup1_4A>().toggleLock(true);
                        if (PlayerPrefs.HasKey("TutorM_Lock") == false) {
                            montanha.GetComponent<GraphicRaycaster>().enabled = false;
                            didatica.GetComponent<GraphicRaycaster>().enabled = false;
                            PlayerPrefs.SetInt("TutorM_Lock", 0);
                            yield return Yielders.Get(.3f);
                            this.tutorial.GetComponent<TutorMontanha>().animTutor.enabled = true;
                            this.tutorial.GetComponent<TutorMontanha>().tutorialMontanha.GetComponent<Animator>().enabled = true;
                            this.tutorial.GetComponent<TutorMontanha>().animTutor.SetInteger("numbTutor", 3);
                            TutorMontanha2.numtext = 4;
                            this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor[4];
                            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[4]);
                            TutorMontanha2.profBalao.enabled = true;
                            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
                            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 4;
                            foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
                                item.SetActive(true);
                            }
                            this.tutorial.GetComponent<TutorMontanha>().gTutor[6].SetActive(false);
                            this.tutorial.GetComponent<GraphicRaycaster>().enabled = true;
                            this.tutorial.SetActive(true);
                            Time.timeScale = 0f;
                        } else {
                            PlayerPrefs.SetInt("TutorM_Lock", 1);
                            this.tutorObjLock = PlayerPrefs.GetInt("TutorM_Lock", 1);
                        }
                        //_itemHandler.itemsOnSlot [i].GetComponent<ItemGroup1_4A> ().
                    } else {
                        Color _color = Color.Lerp(startColors[i], endColors[i], curve.Evaluate(s));
                        _itemHandler.itemsOnSlot[i].GetComponent<ItemGroup1_4A>().UpdateImage(_color);
                    }

                }

                //WrongCards[i].position = Vector3.Lerp(posFrom[i],posTo[i], repositionCurve.Evaluate (s));

                yield return Yielders.EndOfFrame;
            }

            /*if(isBadItem){
				
			}*/

            //List<GameObject> _list2 = _itemHandler.itemsOnSlot.ReturnReverseList();
            /*if(_itemHandler.handlersOfTheFloorHistory.Count - _itemHandler.handlersOfTheFloor.Count >= _itemHandler.handlersOfTheFloorHistory.Count ){
				_itemHandler.GetComponentInParent<Image>().color = Color.clear;
			}*/
            _itemHandler.UpdateDragItem();
            _itemHandler.blockDrag = false;

            _itemHandler.ActiveCollider2D();

            _itemHandler.floorParent.CheckFloorDone();
            isRemoving = false;
        } else {
            _itemHandler.hasEnded = true;
            _itemHandler.maxBadItems--;
            _itemHandler.updateOtherHandlers();
            _itemHandler.UpdateDragItem();
            //_itemHandler.blockDrag = false;
            _itemHandler.ActiveCollider2D();
            _itemHandler.floorParent.CheckFloorDone();
            isRemoving = false;
            //Debug.Log ("Sem item");
        }
    }


    public void UpdateLayerOfItemsIEWhitoutRemove(ItemHandler1_4A itemGroup) {

        int tempCount = itemGroup.slotCount;

        List<Color> endColors = new List<Color>();
        Color[] endColorArray = new Color[tempCount];

        List<float> transparency = new List<float>();
        float[] transparencyArray = new float[tempCount];

        for (int i = 0; i < tempCount; i++) {
            //transparency.Add (transparentLevels [i]);
            //endColors.Add(new Vector4(1f, 1f, 1f, transparency[i]));
            transparencyArray[i] = transparentLevels[i];
            if (endColorArray.Length > i && transparency.Count > i) {
                endColorArray[i] = new Vector4(1f, 1f, 1f, transparency[i]);
            }
        }

        itemGroup.itensGroup.Clear();

        List<GameObject> _list = itemGroup.itemsOnSlot.ReturnReverseList();
        //GameObject[] _listArray = itemGroup.itemsOnSlot.ReturnReverseList().ToArray();

        tempCount = itemGroup.slotCount;

        for (int i = 0; i < tempCount; i++) {
            itemGroup.itensGroup.Add(_list[i].GetComponent<ItemGroup1_4A>());
            //itemGroup.itensGroup.Add(_listArray[i].GetComponent<ItemGroup1_4A>());
            if (itemGroup.itemsOnSlot.Count > i && endColors.Count > i) {
                itemGroup.itemsOnSlot[i].GetComponent<ItemGroup1_4A>().UpdateImage(endColors[i].a);
            }
        }

        //List<GameObject> _list2 = itemGroup.itemsOnSlot.ReturnReverseList();

        itemGroup.UpdateDragItem();
        //itemGroup.blockDrag = false;
        itemGroup.ActiveCollider2D();
        itemGroup.floorParent.CheckFloorDone();
        isRemoving = false;
    }

    IEnumerator endGame() {
        yield return new WaitUntil(() => !itemHandlers.Any(x => x.hasEnded == false));
        pauseButton.interactable = false;
        //	//Debug.Log ("Game Finisishsisisdjsaidjasoidjokjfaodkjfaçlksdjflaskdjfçlaksjflçkjdflça");
        CancelInvoke();
        isPlaying = false;
        currentDificult++;
        int circlesOnSceneCount = circlesOnScene.Count;
        for (int i = 0; i < circlesOnSceneCount; i++) {
            Destroy(circlesOnScene[i]);
        }

        //Debug.Log ("DONE DESTROY");

        circlesOnScene.Clear();
        imagesLayerOnScene.Clear();
        itemHandlers.Clear();
        ItemHandlerList.Clear();

        if (starAmount >= 3) {
            //Debug.Log ("END GAME - CHAMAR DIDATICA!");
        }

        //yield return new WaitForSeconds (1f);
        yield return Yielders.Get(1f);

        if (hasEndedByTime || (currentDificult > dificults.Count - 1)) {

            log.StartTimerLudica(true);
            log.pontosLudica = scoreAmount;
            if (hasEndedByTime) {
                log.faseLudica = currentDificult;
            } else {
                log.faseLudica = 4;
            }

            /*textos alternativos de finalização.
            if(hasEndedByTime && currentDificult <= 1){
                //texto 1
            } else if(hasEndedByTime == false && currentDificult == 4){
                //texto 2
            }
            */
            if (!checkMaeZeca) {
                ChamarMaeZeca();
            }

            // isGameEnded = true;

            StopAllCoroutines();
            //Debug.Log ("EndByTime");
        } else {
            _highlight.startChangeLevelAnimation(currentDificult + 1);
            //yield return new WaitForSeconds(3f);
            yield return Yielders.Get(3f);
            StartCoroutine(beginGame());
            //updateStarAmount ();
        }
        pauseButton.interactable = true;

    }
    public void ChamarMaeZeca() {
        painelMaeZeza.SetActive(true);
        checkMaeZeca = true;

        if (hasEndedByTime && currentDificult <= 1)
        {
            Debug.Log("texto1");
        }
        else if (hasEndedByTime == false && currentDificult == 4)
        {
            Debug.Log("texto2");

        }

    }

    public void chamarDidadica(){
        isGameEnded = true;
    }

    

	void updateItemHandlerList(){
		int circlesOnSceneCount = circlesOnScene.Count;
		for (int i = 0; i < circlesOnSceneCount; i++) {
			itemHandlers.Add (circlesOnScene [i].GetComponent<ItemHandler1_4A> ());
		}	
	}

	public IEnumerator scoreIncrease(int increase){

		//Debug.Log ("Start Increase Points");
		float times = 0.0f;
		int startPoints = scoreAmount;
		int scoreT = scoreAmount + increase;
		scoreAmount += increase;
		while (times < scoreIncreaseDuration)
		{
			times += Time.deltaTime;
			float s = times / scoreIncreaseDuration;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, ScoreIncreaseCurve.Evaluate (s));
			scoreTextComp.text = scory.ToString ();
			yield return Yielders.EndOfFrame;
		}

        UpdateStars(scoreT);


    }

	public IEnumerator scoreReset(){
		//Debug.Log ("Start Increase Points");
		float times = 0.0f;
		int startPoints = scoreAmount;
		int scoreT = startScoreAmount;
		scoreAmount = startScoreAmount;
		while (times < scoreDecreaseDuration)
		{
			times += Time.deltaTime;
			float s = times / scoreDecreaseDuration;

			int scory = (int)Mathf.Lerp (startPoints, scoreT, ScoreDecreaseCurve.Evaluate (s));
			scoreTextComp.text = scory.ToString ();
			yield return Yielders.EndOfFrame;
		}

        UpdateStars(scoreT);

    }

	void DecreaseTimeOverSeconds(){
		if(isPlaying){
			timerSlider.value += 1;
			////Debug.Log("Time reduced");
		}
	}


	string ComboText(){
        stringFast.Clear();
        stringFast.Append("Combo ").Append(combo).Append("X");
        string newText = stringFast.ToString();
		return newText;
	}

	public IEnumerator<float> ShowComboText(){
			float times = 0.0f;
			while (times < showComboTextDuration) {
				times += Time.deltaTime;
				float s = times / showComboTextDuration;

				Vector3 newScale = Vector3.Lerp (Vector3.zero, Vector3.one, showComboTextCurve.Evaluate (s));
				comboTextComp.transform.localScale = newScale;

				yield return Timing.WaitForOneFrame;
			}
	}

	void ItemBonus(){
		StartCoroutine(CreateItemBonus());
	}

	IEnumerator CreateItemBonus(){
		if(!hasItemBonus && isPlaying){
			int ItemHandlerListCount = ItemHandlerList.Count;
			yield return new WaitUntil(() => isDragging == false);
			float _randomChances = Random.Range(0f,100f);
			if(_randomChances <= chancesOfBonus){
				int indexItem = Random.Range(0,ItemHandlerListCount);
				if (ItemHandlerList.Count > 0 && ItemHandlerList [indexItem] != null) {
					while (ItemHandlerList [indexItem].hasEnded == true && ItemHandlerList.Count > 0) {
						indexItem = Random.Range (0, ItemHandlerListCount - 1);
					}
				}
				if (ItemHandlerList.Count > 0 && ItemHandlerList [indexItem] != null) {
					itemWithBonusItem = ItemHandlerList [indexItem].ItemBonusHere ();
                    itemWithBonusItem.toggleBonusItem(true);
                    hasItemBonus = true;
					////Debug.Log("Item Bonus created!");
					OnBonusItemShow.Invoke ();
				}
			}
		}		

		Invoke("endItemBonus", 8f);
	}

	void endItemBonus(){
		if (itemWithBonusItem != null) {
			if (hasItemBonus && itemWithBonusItem.isActiveAndEnabled) {
				if (itemWithBonusItem != null) {
					itemWithBonusItem.isBonusItem = false;
                    itemWithBonusItem.toggleBonusItem(false);
                }
				//itemWithBonusItem.UpdateColor(Color.white);
				hasItemBonus = false;
			}
		}
	}

	void startRandomChest(){

		float randomTemp = Random.Range(0f, 100f);
		if (randomTemp <= chanceToRandomChest) {
			if(randomchest !=null){
				StopCoroutine(randomchest);
			}
			randomchest = RandomChests();
			StartCoroutine(randomchest);
		}

	}

	IEnumerator RandomChests(){
      
        //chests[_chestIndex].GetComponent<Animator> ().enabled = true;

        yield return new WaitUntil(() => isClosingChest == false);
		yield return new WaitUntil(() => isDragging == false);
		yield return new WaitUntil(() => checkBauOpen == false);
		yield return new WaitUntil(() => checkJucaBau == false);
		yield return new WaitUntil(() => checkZecaBau == false);
		yield return new WaitUntil(() => isOpeningChest == false);
		
		yield return new WaitUntil(() => chooseChestBonus == false);

		foreach (var item in partBauTransp)
		{
		item.SetActive(true);
		item.GetComponent<Animator>().SetBool("TransporTrue",true);
		//yield return new WaitForSeconds(.2f);
	
		}	
	//	yield return new WaitForSeconds(.3f);

		

		//yield return new WaitForSeconds(.8f);

		yield return Yielders.Get(0.8f);
		//chests[_chestIndex].GetComponent<Animator> ().enabled = true;
	

		Color WhiteWithoutAlpha = new Vector4(1f,1f,1f,0f);

		float times = 0.0f;
		while (times < fadeInChestChangeDuration) {
			times += Time.deltaTime;
			float s = times / fadeInChestChangeDuration;
			int chestsCount = chests.Count;
			for (int i = 0; i < chestsCount; i++){
				chests[i].imageTamp.color = Color.Lerp(Color.white,WhiteWithoutAlpha, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].imageComp.color = Color.Lerp(Color.white,WhiteWithoutAlpha, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].textComp.color = Color.Lerp(Color.white,WhiteWithoutAlpha, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].imageBau.color = Color.Lerp(Color.white,WhiteWithoutAlpha, fadeOutChestChangeCurve.Evaluate(s));
			}
		
			foreach (var item in partBauTransp)
		{
	//	item.SetActive(true);
		item.GetComponent<Animator>().SetBool("TransporTrue",false);
		}	

			yield return Yielders.EndOfFrame;
			//chests[_chestIndex].GetComponent<Animator> ().enabled = true;

	
		}
		
	//	yield return new WaitForSeconds(1f);
		if(isPlaying){
			chestsParent.Suffle();
			Vector3 _anchoredPosition = chests[0].GetComponent<RectTransform>().anchoredPosition;
			Vector2 _sizeDelta = chests[0].GetComponent<RectTransform>().sizeDelta;
			int chestsCount = chests.Count;
			for (int i = 0; i < chestsCount; i++){
				chests[i].transform.SetParent(chestsParent[i].transform);
				chests[i].rectComp.anchoredPosition = _anchoredPosition;
				chests[i].rectComp.sizeDelta = _sizeDelta;
				/*Vector3 scaleText = Vector3.one;
				if(ChestOnRight.Contains(chestsParent[i])){					
					scaleText.x = -1f;
				}	*/			
				chests[i].transform.localScale = Vector3.one;
				chests[i].textComp.gameObject.transform.localScale = chestsParent[i].transform.localScale;
				chests[i].GetComponent<ChestHandler1_4A>().bauSort = chestsParent[i].GetComponent<BauNumber>().numbBau;
			}
			OnChestsChange.Invoke();
			////Debug.Log("Baus Randomizados");
		}
			

		times = 0.0f;
		while (times < fadeInChestChangeDuration) {
			times += Time.deltaTime;
			float s = times / fadeInChestChangeDuration;
			int chestsCount = chests.Count;
			for (int i = 0; i < chestsCount; i++){
				chests[i].imageTamp.color = Color.Lerp(WhiteWithoutAlpha,Color.white, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].imageComp.color = Color.Lerp(WhiteWithoutAlpha,Color.white, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].textComp.color = Color.Lerp(WhiteWithoutAlpha,Color.white, fadeOutChestChangeCurve.Evaluate(s));
				chests[i].imageBau.color = Color.Lerp(WhiteWithoutAlpha,Color.white, fadeOutChestChangeCurve.Evaluate(s));
				
			}

			yield return Yielders.EndOfFrame;
			//chests[_chestIndex].GetComponent<Animator> ().enabled = false;
		}

		//yield return Yielders.Get(1f);


		//yield return new WaitForSeconds(0.5f);
	
			
		Invoke("startRandomChest", timeToRandomizeChest);

		if (PlayerPrefs.HasKey ("TutorM_Troca") == false) {
			montanha.GetComponent<GraphicRaycaster> ().enabled = false;
			didatica.GetComponent<GraphicRaycaster> ().enabled = false;
			PlayerPrefs.SetInt ("TutorM_Troca", 0);
			yield return Yielders.Get(.75f);
            TutorMontanha2.numtext = 1;
            this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor [1];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[1]);
            TutorMontanha2.profBalao.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
			this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 1;
			foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
				item.SetActive (true);
			}	
			this.tutorial.GetComponent<TutorMontanha> ().gTutor [6].SetActive (false);
			this.tutorial.GetComponent<GraphicRaycaster> ().enabled = true;
			this.tutorial.SetActive (true);
			Time.timeScale = 0f;
			yield return Yielders.Get(.75f);
		} else {
			PlayerPrefs.SetInt ("TutorM_Troca", 1);
			this.tutorTroca = PlayerPrefs.GetInt ("TutorM_Troca", 1);
			yield return Yielders.Get(1.5f);
		}
		foreach (var item in partBauTransp)
		{
			//	item.GetComponent<Animator>().SetBool("TransporTrue",false);
//			item.GetComponent<ParticleSystem.EmitParams>().startLifetime = 0;
			item.SetActive(false);
			//yield return new WaitForSeconds(.2f);

		}	
	}

	void randomChestToCLose(){

		
		isClosingChest = true;
		if (!hasChestClose && isPlaying){
			float randomTemp = Random.Range(0f,100f);
            if (chestBonus != null) {
                chestBonus.isChestBonus = false;
                chestBonus.ToggleBonusParticle(false);
            }
            if (randomTemp <= chancesOfChestClose)
			{	
				_chestIndex = Random.Range(0,chests.Count-1);		
				while (chests[_chestIndex].isChestClose == true || chests[_chestIndex].isChestBonus == true){
					_chestIndex = Random.Range(0,chests.Count-1);
					//chests[_chestIndex].jucaFecharBau.enabled=true;
				}
			chests[_chestIndex].jucaFecharBau.enabled=true;
			//chests[_chestIndex].zecaFecharBau.enabled=true;
		//	chests[_chestIndex].GetComponent<Animator> ().enabled = true;
			chests[_chestIndex].jucaFecharBau.SetBool("fecharBau",true);
			chests[_chestIndex].jucaFecharBau.SetInteger("NumbBauSort",chests[_chestIndex].GetComponent<ChestHandler1_4A>().bauSort);
				StartCoroutine (TimeEnableBau());
			} 
			
			else {				
				Invoke("randomChestToCLose", timeToChestClose);
				isClosingChest = false;
			}

            for (int i = 0; i < chests.Count; i++) {
                if (chests[i].isChestBonus) {
                    chestBonus = chests[i];
                }
            }


		}
	}
	IEnumerator TimeEnableBau(){

		//yield return new WaitForSeconds (1f);
		yield return Yielders.Get(10f);
		//chests[_chestIndex].zecaFecharBau.SetBool("AbrirBau",false);
		chests[_chestIndex].jucaFecharBau.SetBool("fecharBau",false);
		//chests[_chestIndex].jucaFecharBau.enabled=false;
		//chests[_chestIndex].GetComponent<Animator> ().enabled = hasChestClose;



	}


	//Chamar no final da animação de fechar o bau.
	public void closeChest(){		
		chests[_chestIndex].isChestClose = true;
		chests[_chestIndex].ToggleRaycastTarget(false);
		_closedChest = chests[_chestIndex];
		hasChestClose = true;
		isClosingChest = false;
		float temp = Random.Range(rangeTimeToOpen.x,rangeTimeToOpen.y);
		Invoke("OpenChestAnimation", temp);
	}

	
	void OpenChestAnimation(){
		isOpeningChest = true;
		////Debug.Log("Abrindo Bau: "+_chestIndex);
		//chests[_chestIndex].GetComponent<Animator> ().enabled = true;
		//chests[_chestIndex].zecaFecharBau.enabled=true;
		chests[_chestIndex].zecaFecharBau.SetBool("AbrirBau",true);
		StartCoroutine (TimeEnableBau());
	}


	//Chamar no final da animação de abertura do báu.
	public void OpenChest(){
		if(hasChestClose && isPlaying){		
		 	checkZecaBau=true;
			_closedChest.isChestClose = false;
			chests[_chestIndex].zecaFecharBau.enabled=true;
			_closedChest.ToggleRaycastTarget(true);
			hasChestClose = false;		
		}
		isOpeningChest = false;
		////Debug.Log(isOpeningChest);
		Invoke("randomChestToCLose", timeToChestClose);
	}


	void ChooseChest(){
        Debug.Log("Bau Bonus Starting");
       /* if (hasChestBonus){
            chestBonus.ToggleBonusParticle(false);
            chestBonus.isChestBonus = false;
            hasChestBonus = false;
			OnChestOpens.Invoke();
            Debug.Log("Still Bonus so End it");
        }*/

		if(!hasChestBonus && isPlaying){
			float randomTemp = Random.Range(0f,100f);
			if(randomTemp <= chancesOfChestBonus){

                int temp = chests.Count;
                for (int i = 0; i < temp; i++) {
                    chests[i].isChestBonus = false;
                    chests[i].ToggleBonusParticle(false);
                }

                chests2 = chests.ToList();
				chests2.Suffle();               

                if (!chests2[0].isChestClose) {
                    chestBonus = chests2[0];
                    //chestBonus.isChestBonus = true;
                    //chestBonus.ToggleBonusParticle(true);
                    //Debug.Log("Bau Bonus");
                    StartCoroutine(TimeTutorBauBonus());
                } else if (!chests2[1].isChestClose) {
                    chestBonus = chests2[1];
                    //chestBonus.isChestBonus = true;
                    //chestBonus.ToggleBonusParticle(true);
                    //Debug.Log("Bau Bonus");
                    StartCoroutine(TimeTutorBauBonus());
                } else if(!chests2[2].isChestClose) {
                    chestBonus = chests2[2];
                    //chestBonus.isChestBonus = true;
                    //chestBonus.ToggleBonusParticle(true);
                    //Debug.Log("Bau Bonus");
                    StartCoroutine(TimeTutorBauBonus());
                }
                if (chestBonus == null) {
                    Debug.Log("Bau Bonus Nulled");
                }
                chestBonus.isChestBonus = true;
                chestBonus.particleBonusChest.SetActive(true);

                
                //OnChestBlock.Invoke();
                hasChestBonus = true;
				Debug.Log("Bau Bonus");
                Timing.KillCoroutines(chestBonusEnd);
                chestBonusEnd = Timing.RunCoroutine(endChestBonus(), "ChestBonusEnd");
                //Invoke("endChestBonus", 8f);
            }			
		}		

		
	}
	IEnumerator TimeTutorBauBonus() {
      
        yield return Yielders.Get (0.5f);

		if(PlayerPrefs.HasKey("TutorBauBonus_0")==false){
			montanha.GetComponent<GraphicRaycaster> ().enabled = false;
			didatica.GetComponent<GraphicRaycaster> ().enabled = false;
			this.tutorial.GetComponent<TutorMontanha> ().animTutor.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().tutorialMontanha.GetComponent<Animator>().enabled = true;
            this.tutorial.GetComponent<TutorMontanha> ().animTutor.SetInteger ("numbTutor",2);
			PlayerPrefs.SetInt("TutorBauBonus_0",1);
            TutorMontanha2.numtext = 3;
            this.tutorial.GetComponent<TutorMontanha>().btPulartext.text = "Continuar";
			this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 3;
			this.tutorial.GetComponent<TutorMontanha>().profBalao.text = this.tutorial.GetComponent<TutorMontanha>().TextTutor [3];
            TutorMontanha2.soundManager.startVoiceFXReturn(TutorMontanha2.audiosTutorial[3]);

               TutorMontanha2.profBalao.enabled = true;
            this.tutorial.GetComponent<TutorMontanha>().tutorNumber = 1;
			foreach (var item in tutorial.GetComponent<TutorMontanha>().gTutor) {
				item.SetActive (true);
			}	
			this.tutorial.GetComponent<TutorMontanha> ().gTutor [6].SetActive (false);
			this.tutorial.GetComponent<GraphicRaycaster> ().enabled = true;
			this.tutorial.SetActive (true);
			Time.timeScale = 0f;
		}
		else{
			//PlayerPrefs.SetInt("TutorJuca_0",1);
			this.TutorBauBonus = PlayerPrefs.GetInt("TutorBauBonus_0",1);

		}

	}

	IEnumerator<float> endChestBonus(){
        yield return Timing.WaitForOneFrame;
        hasChestBonus = true;
        chestBonus.isChestBonus = true;
        chestBonus.particleBonusChest.SetActive(true);

        yield return Timing.WaitForSeconds(8f);

        if (hasChestBonus && isPlaying){
			chestBonus.isChestBonus = false;
            chestBonus.ToggleBonusParticle(false);
			hasChestBonus = false;
			OnChestOpens.Invoke();
            yield return Timing.WaitForOneFrame;
		}
	}

	string ChestNameByType(ItemType1_4A _type){
		switch (_type){
			case ItemType1_4A.books:
				return "Livros";

			case ItemType1_4A.cars:
				return "Carros";
			case ItemType1_4A.dolls:
				return "Bonecos";
			case ItemType1_4A.sports:
				return "Esportes";
			default:
				return "None";
		}
	}

	public void ShakeCameraOn(float sPower){

		//save position before start shake, 
		//this it's really important otherwise 
		//the sprite can goes away and will not return 
		//in native position
		originPosition = towerParent.transform.localPosition;

		//enable shaking and setting power
		shakeOn = true;
		shakePower = sPower;
	}

	// shake off
	public void ShakeCameraOff(){

		// shake off
		shakeOn = false;

		// set original position after 
		towerParent.transform.localPosition = originPosition;
	}

	public void ThrowItemAway(Transform _transformItem){
		StartCoroutine(ThrowItemAwayIE(_transformItem));
	}

	IEnumerator ThrowItemAwayIE(Transform _transformItem){

		Vector3 startPos = _transformItem.position;
		Vector3 endPos = _transformItem.position + posOffsetThrow;
        ItemGroup1_4A item = _transformItem.GetComponent<ItemGroup1_4A>();

        item.DisableBackgroundImage();
		float timer = 0.0f;
		while (timer < itemThrowAwayDuration)
		{
			timer += Time.deltaTime;
			float s = timer / itemThrowAwayDuration;

			if(_transformItem != null){
            item.DisableBackgroundImage();
			_transformItem.position = Vector3.Lerp(startPos,endPos,itemThrowAwayPositionCurve.Evaluate(s));
			_transformItem.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 5f, itemThrowAwayScaleCurve.Evaluate(s));
			} else {
				break;
			}

			yield return Yielders.EndOfFrame;
		}
		if(_transformItem != null){
			_transformItem.localScale = Vector3.one;
			_transformItem.gameObject.SetActive(false);
		}
		////Debug.Log("Item Errado Jogado Fora!");

	}

	public void checkItensPos(){
		int itemHandlersCount = itemHandlers.Count;
		for (int i = 0; i < itemHandlersCount; i++) {
			itemHandlers[i].checkDragItem();
		}
	}

    public void UpdateStars(int _score) {

        if (_score >= minigame.limit.x && _score < minigame.limit.y) {
            updateStarAmount(1);
        } else if (_score >= minigame.limit.y && _score < minigame.limit.z) {
            updateStarAmount(2);
        } else if (_score >= minigame.limit.z) {
            updateStarAmount(3);
        } else {
            updateStarAmount(0);
        }
    }

}
