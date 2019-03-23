using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MEC;
using DG.Tweening;
using TMPro;
using System.Linq;

public class FQManager : OverridableMonoBehaviour {

    [AssetsOnly]
    public GameObject[] fishPrefabs;
    public GameObject FishToSpawn {
        get {
            return fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        }
    }
    public Transform parentTransformPool;
    [HideInInspector]
    public Queue<FishContainer> fishPool = new Queue<FishContainer>();
    public int poolSize;
    //public Transform[] LeftSpawnSpots;
    //public Transform[] RightSpawnSpots;
    public FQLineFish[] fishLines;
    //public FQLineFish RandomFishLine => fishLines[Random.Range(0, fishLines.Length)];
    public bool keepSpawning;
    public bool isPlaying;
    [HorizontalGroup("MinMaxTimerSpawn",LabelWidth = 0, Title = "Max/Min Timer To Spawn")]
    public float maxTimeSpawn;
    [HorizontalGroup("MinMaxTimerSpawn", LabelWidth = 0, Title = "Max/Min Timer To Spawn")]
    public float minTimeSpawn;
    private Coroutine spawnCoroutine;
    public Transform[] mRodFishTransform;
    public Transform rodFishTransform;
    public Transform bucketRefPos;
    public Vector3 initPositionRF;
    public Vector3 mousePositionOnWorld;
    public Camera mainCamera;
    public GameObject enableConfirmBox;
    public FishContainer holdedFish;
    public bool holdingFish = false;
    public LineRenderer lineRender;
    public TextMeshProUGUI enunciadoText;
    public TextMeshProUGUI acertoText;
    public TextMeshProUGUI erroText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalMessage;
    public TextMeshProUGUI timerbannerAnswer;
    private StringFast stringOpt = new StringFast();
    public Transform bucketTransformComp;
    public float maxTimer = 60;
    public float currentTimer;
    public float timerMaxRod;
    public bool canFishBeTroll = false;
    public FQQuestion[] AllQuestionsDB;
    public FQQuestion[] questionsdb;
    
    public FQQuestion CurrentQuestion {
        get {
            return questionsdb[currentRound];
        }
    }

    public int currentRound;
    public int maxRounds;
    private int minutes;
    private int seconds;
    private bool startTimer;
    public int rightCount;
    public int wrongCount;
    public int scoreAmount;
    public TextMeshProUGUI scoreText;
    public List<FishContainer> fishInstaces = new List<FishContainer>();

    public Sprite[] bannerVariants;
    public Animator[] mPescaPer;
    public Animator pescaPer;
    public Transform[] mPosiniVara;
    public Transform posiniVara;
    public Vector3 posLInhaG;
    public Transform[] mPosLInha;
    public Transform posLInha;

    public int person;

    public Sprite RandomBanner {
        get {
            return bannerVariants[Random.Range(0, bannerVariants.Length)];
        }
    }

    public Transform[] mLineRenderPos;
    public Transform lineRenderPos;


    public SoundManager SoundMngr;
    public AudioClip[] AudioClips;

    public void ReturnQuestionListByAnoLetivo(int anoLetivo){
        questionsdb = AllQuestionsDB.Where(x => x.AnoLetivo == anoLetivo).ToArray();
    }
    

    public FishContainer Fcontainer() {
        if (fishPool.Count >= 1) {
            FishContainer fc = null;
            do {
                fc = fishPool.Dequeue();
            } while ( fishInstaces.Contains(fc) );
            fishInstaces.Add(fc);
            return fc;
        } else {
            return PoolInstance();
        }
    }

    public FQLineFish RandomFishLine (){
        FQLineFish fish = null;
        if (isPlaying) {
            do {
                fish = fishLines[Random.Range(0, fishLines.Length)];
            } while (fish.isLocked);
            fish.isLocked = true;
            Timing.RunCoroutine(UnlockFishLine(4f, fish));
        } else {
            fish = fishLines[Random.Range(0, fishLines.Length)];           
        }
        fish.UpdatePos();
        return fish;
    }

    IEnumerator<float> UnlockFishLine(float timer, FQLineFish _fishLine) {
        yield return Timing.WaitForSeconds(timer);
        _fishLine.isLocked = false;
    }
    void seletar()
    {
        person = PlayerPrefs.GetInt("characterSelected", 0);
        switch (person)
        {
            case 0:
                rodFishTransform = mRodFishTransform[0];
                posiniVara = mPosiniVara[0];
                posLInha = mPosLInha[0];
                lineRenderPos = mLineRenderPos[0];
                pescaPer = mPescaPer[0];
                break;
            case 1:
            case 5:
                //tatiBia
                rodFishTransform = mRodFishTransform[1];
                posiniVara = mPosiniVara[1];
                posLInha = mPosLInha[1];
                lineRenderPos = mLineRenderPos[1];
                pescaPer = mPescaPer[1];
                break;
            case 2:
                rodFishTransform = mRodFishTransform[2];
                posiniVara = mPosiniVara[2];
                posLInha = mPosLInha[2];
                lineRenderPos = mLineRenderPos[2];
                pescaPer = mPescaPer[1];
                //Paulo
                break;
            case 3:
            case 4:
                rodFishTransform = mRodFishTransform[3];
                posiniVara = mPosiniVara[3];
                posLInha = mPosLInha[3];
                lineRenderPos = mLineRenderPos[3];
                pescaPer = mPescaPer[1];
                //Joaomanu
                break;
        }
    }
    public void Start() {
        seletar();


         posLInhaG = new Vector3(posLInha.transform.localPosition.x, posLInha.transform.localPosition.y, posLInha.transform.localPosition.z);
        fishPool = new Queue<FishContainer>();
        SetupPool();
        keepSpawning = false;
        initPositionRF = rodFishTransform.position;
        lineRender.SetPosition(0, posiniVara.position);
        lineRender.numCornerVertices = 20;
        ReturnQuestionListByAnoLetivo(1);
        questionsdb.Suffle();
        stringOpt = new StringFast();
        scoreText.SetText("0");
        //lineRender.numCapVertices = 3;
    }

    [Button("StartGame")]
    public void StartGame() {
        keepSpawning = false;
        initPositionRF = rodFishTransform.position;
        lineRender.SetPosition(0, lineRenderPos.position);
        lineRender.numCornerVertices = 20;
        ReturnQuestionListByAnoLetivo(1);
        questionsdb.Suffle();
        enunciadoText.SetText(CurrentQuestion.quetionText);
        isPlaying = true;
        StartSpawning();
        currentTimer = maxTimer;
        minutes = Mathf.FloorToInt(currentTimer / 60f);
        seconds = Mathf.FloorToInt(currentTimer % 60f);
        UpdateTimerText();
        startTimer = true;
        canFishBeTroll = true;
        Timing.RunCoroutine(GameTimer());
    }

    public IEnumerator<float> EndTheGame() {
        SpeedUpfishes();
        yield return Timing.WaitForSeconds(1f);
        startTimer = false;
        keepSpawning = false;
        isPlaying = false;        
        finalMessage.SetText("");
        finalMessage.SetText("Você acertou: " + rightCount + " e errou " + wrongCount + "!");
        scoreAmount += rightCount * 10;
        scoreText.SetText(scoreAmount.ToString());
        finalMessage.DOFade(1f, .5f);
        yield return Timing.WaitForSeconds(2f);
        finalMessage.DOFade(0f, .5f);
        rightCount = 0;
        wrongCount = 0;
        currentRound++;
        if(currentRound >= maxRounds) {
            Debug.Log("fim de jogo");
        } else {
            yield return Timing.WaitForSeconds(1f);
            StartGame();
        }
    }

    public void SpeedUpfishes() {
        int tempcount = fishInstaces.Count;
        for (int i = 0; i < tempcount; i++) {
            fishInstaces[i].SpeedUP();
        }
    }


    public IEnumerator<float> GameTimer() {
        while (startTimer) {            
            yield return Timing.WaitForSeconds(1f);
            currentTimer -= 1f;
            minutes = Mathf.FloorToInt(currentTimer / 60f);
            seconds = Mathf.FloorToInt(currentTimer % 60f);
            UpdateTimerText();
            if (!(currentTimer <= 0f)) continue;
            startTimer = false;
            Timing.KillCoroutines("EndGameCoroutine");
            Timing.RunCoroutine(EndTheGame(), "EndGameCoroutine");
        }
    }

    public void UpdateTimerText() {
        if (currentTimer > 0f) {
            stringOpt.Clear();
            stringOpt.Append(minutes).Append(":").Append(seconds.ToString("00"));
            timerText.SetText(stringOpt.ToString());
        } else {
            timerText.SetText("0:00");
        }
    }

    public override void UpdateMe() {
        if (isPlaying && canFishBeTroll && Input.GetMouseButtonDown(0) && Time.timeScale >= 1 && !holdingFish) {
            pescaPer.SetInteger("Jogar",1);
            Vector3 mousePosToWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosToWorld.z = initPositionRF.z;
            mousePositionOnWorld = mousePosToWorld;
           

        }
        lineRender.SetPosition(0, posiniVara.position);
        lineRender.SetPosition(1, lineRenderPos.position);
    }

    public void JogarLinha( ) {
        Sequence moveTR = DOTween.Sequence().SetId(002);
        moveTR.AppendCallback(() => canFishBeTroll = false);
        moveTR.Append(rodFishTransform.DOMove(mousePositionOnWorld, GetTimeByDistance(mousePositionOnWorld, initPositionRF)));
        moveTR.Append(rodFishTransform.DOMove(initPositionRF, 1f));
        pescaPer.SetInteger("Jogar", 2);
        moveTR.AppendCallback(metodofinallinha);
        moveTR.AppendCallback(ValidateFishHolding);
    }
    void metodofinallinha() {
        posLInha.transform.localPosition = posLInhaG;
        //Debug.Log("00");
    }

    public void ValidateFishHolding() {
        if (holdingFish) {
            if(holdedFish == null) {
                holdingFish = false;
               
            } else {
                Sequence JumpToBucket = DOTween.Sequence();
                JumpToBucket.Append(holdedFish.transform.DOJump(bucketRefPos.position, 1f, 1, 1f, false));
                JumpToBucket.Join(holdedFish.transform.DOScale(0f, 1f));
                JumpToBucket.AppendCallback(CorrectFishing);
            }
            pescaPer.SetInteger("Jogar", 0);

        } else {
            pescaPer.SetInteger("Jogar", 0);

        }

        canFishBeTroll = true;
    }

    public void CorrectFishing() {
        if (holdedFish != null  && holdedFish.valueAnswer != null && CurrentQuestion.ContainsCorrect(holdedFish.valueAnswer)) {
            Sequence fadeText = DOTween.Sequence();
            fadeText.Append(acertoText.DOFade(1f, .5f));
            fadeText.AppendCallback(() => SoundMngr.startSoundFX(AudioClips[0]));
            fadeText.Append(acertoText.DOFade(0f, .5f));
            rightCount++;
        } else {
            Sequence fadeText = DOTween.Sequence();
            fadeText.Append(erroText.DOFade(1f, .5f));
            fadeText.AppendCallback(() => SoundMngr.startSoundFX(AudioClips[1]));
            fadeText.Append(erroText.DOFade(0f, .5f));
            wrongCount++;
        }
        ResetTheHoldedFish();
    }

    public void ResetTheHoldedFish() {
        if (holdedFish != null) {
            holdedFish.ResetFish();
        }
        holdingFish = false;
        holdedFish = null;
    }

    public void HoldingFish(FishContainer _fc) {
        //_fc.transform.SetParent(rodFishTransform);
        //_fc.transform.localPosition = Vector3.zero;
        holdedFish = _fc;
        holdingFish = true;
        DOTween.Kill(002, false);
        enableConfirmBox.SetActive(true);
    }

    public void CatchFish() {        
        holdedFish.transform.SetParent(rodFishTransform);
        holdedFish.transform.localPosition = Vector3.zero;
        Vector2 directionByFishToRod = initPositionRF - holdedFish.transform.position;
        float angle = Mathf.Atan2(directionByFishToRod.y, directionByFishToRod.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        if (holdedFish.transform.localScale.x < 0f) {
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else {
            rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        }
        holdedFish.transform.DOLocalRotateQuaternion(rotation, .3f);
        holdedFish.StopMoving();
        enableConfirmBox.SetActive(false);
        Time.timeScale = 1;
        Sequence backInit = DOTween.Sequence();
        backInit.Append(rodFishTransform.DOMove(initPositionRF, 1f));
        backInit.AppendCallback(() => canFishBeTroll = true);
        backInit.AppendCallback(metodofinallinha);
        backInit.AppendCallback(() => ValidateFishHolding());
        backInit.Play();
    }

    public void ReleaseFish() {
        holdedFish.textComponent.alpha = 1f;
        timerbannerAnswer.SetText("");
        enableConfirmBox.SetActive(false);
        holdedFish.bannerFish.enabled = true;
        holdedFish = null;
        Time.timeScale = 1;
        Sequence backInit = DOTween.Sequence();
        backInit.Append(rodFishTransform.DOMove(initPositionRF, 1f));
        backInit.AppendCallback(() => canFishBeTroll = true);
        backInit.AppendCallback(metodofinallinha);
        backInit.AppendCallback(() => ValidateFishHolding());
        backInit.Play();

    }


    [ButtonGroup("Spawn")]
    [Button("SetupPool")]
    public void SetupPool() {
        for (int i = 0; i < poolSize; i++) {
            fishPool.Enqueue(PoolInstance());
        }
    }

    public float GetTimeByDistance(Vector3 fpos, Vector3 spos) {
        float distanceF = Vector3.Distance(fpos, spos);
        float timerByDistance = (distanceF * timerMaxRod) / 10f;
        //Debug.Log(timerByDistance);
        return timerByDistance;
    }

    public FishContainer PoolInstance() {
        GameObject fishInstance = Instantiate(FishToSpawn, GetSpawnPosition(), Quaternion.identity, parentTransformPool);
        FishContainer container = fishInstance.GetComponent<FishContainer>();
        container.manager = this;
        container.name = "FishingInstance";
        return container;
    }

    [ButtonGroup("Spawn")]
    [Button("StartSpawner")]
    public void StartSpawning() {
        // spawnCoroutine = SpawnManager(1f);
        keepSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnManager(1f));
    }

    [ButtonGroup("Spawn")]
    [Button("Stop Spawner")]
    public void StopSpawner() {
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnManager(float timerToStart) {
        yield return new WaitForSeconds(timerToStart);
        while (keepSpawning) {
            yield return new WaitUntil(() => fishLines.Any(x => x.isLocked == false));
            SpawnFish();
            yield return new WaitForSeconds(Random.Range(minTimeSpawn,maxTimeSpawn));
        }
    }

    public void SpawnFish() {
        FishContainer fc = Fcontainer();
        bool isRight = Random.Range(0, 2) == 1 ? true : false;
        fc.transform.position = GetSpawnPosition(isRight);
        fc.StartMoving(isRight);
    }
	
    public Vector3 GetSpawnPosition() {
        bool isRight = Random.Range(0,2) == 1 ? true : false;
        if (isRight) {
            return RandomFishLine().rightWorldPos;
        } else {
            return RandomFishLine().leftWorldPos;
        }
    }

    public Vector3 GetSpawnPosition(bool isRight) {
        //bool isRight = Random.Range(0, 1) == 1 ? true : false;
        if (isRight) {
            return RandomFishLine().rightWorldPos;
        } else {
            return RandomFishLine().leftWorldPos;
        }
    }

    public void KillRoutine() {
        StopAllCoroutines();
        Timing.KillCoroutines();
    }

}
